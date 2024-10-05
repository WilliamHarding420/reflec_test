using ReflectionExample.Attributes;
using ReflectionExample.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionExample
{
    internal class ArgumentHandler
    {

        public struct HandlerInfo
        {
            public IArgumentHandler Handler { get; set; }

            public MethodInfo ExecuteMethod { get; set; }
        }

        // Dictionary of arguments and their corresponding handler
        private Dictionary<string, HandlerInfo> ArgumentHandlers;

        // Singleton implementation, this ensures that only one instance of this class can exist at any given time
        // this is accessed the same you would a regular variable, ie. ArgumentHandler.Instance
        // if no instance has been created when this is accessed, a new one will be created, and if an instance exists, it is returned
        public static ArgumentHandler Instance 
        { 
            get
            {

                if (_Instance == null)
                    _Instance = new ArgumentHandler();

                return _Instance;
            } 
            private set
            {
                _Instance = value;
            } 
        }

        // Singleton instance
        private static ArgumentHandler? _Instance;

        // Making the constructor private (stops any more instances being created outside this class)
        private ArgumentHandler() 
        { 
            RegisterHandlers();
        }

        private void RegisterHandlers()
        {

            ArgumentHandlers = new Dictionary<string, HandlerInfo>();

            // Retrieves all the types in the program and filters to only the types where it implements the IArgumentHandler interface
            foreach (Type type in Assembly.GetExecutingAssembly().GetTypes().Where(type => type.GetInterfaces().Contains(typeof(IArgumentHandler)))) 
            {

                // Creating handler variable
                IArgumentHandler handler;
                MethodInfo executorInfo;

                // attempting to create an instance of the current handler
                // skipping this handler if creating the instance failed
                try
                {
                    handler = (IArgumentHandler)Activator.CreateInstance(type);

                    executorInfo = type.GetMethods().Where(
                        method => method.GetCustomAttribute<ExecutorAttribute>() != null
                        && method.ReturnType == typeof(bool)).ToArray()[0];
                } catch (Exception ex)
                {
                    Console.WriteLine($"Could not register handler. {ex}");
                    continue;
                }

                // Adding the handler to the handlers dictionary if an instance was successfully created
                ArgumentHandlers.Add(handler.Argument, new HandlerInfo
                {
                    Handler = handler,
                    ExecuteMethod = executorInfo
                });
            
            }

        }

        public bool CheckArgumentExist(string argument)
        {

            if (!ArgumentHandlers.ContainsKey(argument))
            {
                Console.WriteLine("Argument not found");
                return false;
            }

            return true;

        }

        public object[]? ConverArgsToParameterArray(string argument, string[] args)
        {

            HandlerInfo handlerInfo = ArgumentHandlers[argument];
            MethodInfo executor = handlerInfo.ExecuteMethod;

            ParameterInfo[] parameters = executor.GetParameters();

            if (parameters.Length > args.Length)
            {
                return null;
            }

            object[]? argsArray = new object[parameters.Length];
            for (int param = 0; param < parameters.Length; param++)
            {
                try
                {

                    if (parameters[param].ParameterType == typeof(string[])) 
                    { 
                        if (param == parameters.Length - 1)
                        {
                            argsArray[param] = Convert.ChangeType(args[param..^0], typeof(string[]));
                        } else
                        {
                            Console.WriteLine("string[] parameter must be last executor parameter.");
                            
                        }
                        continue;
                    }

                    argsArray[param] = Convert.ChangeType(args[param], parameters[param].ParameterType);
                }catch (Exception ex)
                {
                    Console.WriteLine("Could not convert argument to given type.");
                    return null;
                }
            }

            return argsArray;

        }

        public void HandleArgument(string argument, string[] args)
        {

            if (!CheckArgumentExist(argument))
            {
                Console.WriteLine("Could not execute argument.");
                return;
            }

            HandlerInfo handlerInfo = ArgumentHandlers[argument];

            IArgumentHandler handler = handlerInfo.Handler;
            MethodInfo executorInfo = handlerInfo.ExecuteMethod;
            ParameterInfo[] parameters = executorInfo.GetParameters();

            bool success;
            if (parameters.Length < 1)
            {
                success = (bool) executorInfo.Invoke(handler, null);
            } else
            {

                object[]? argsArray = ConverArgsToParameterArray(argument, args);

                if (argsArray == null)
                {
                    Console.WriteLine("1Could not execute argument.");
                    return;
                }

                success = (bool) executorInfo.Invoke(handler, argsArray);
            }

            if (success)
                Console.WriteLine("Argument executed successfully.");
            else
                Console.WriteLine("Argument failed to successfully execute.");

        }

        public HandlerInfo[] GetHandlers()
        {
            return ArgumentHandlers.Values.ToArray();
        }

        public HandlerInfo GetArgumentHandler(string argument)
        {
            return ArgumentHandlers[argument];
        }

    }
}
