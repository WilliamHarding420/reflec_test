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

        // Dictionary of arguments and their corresponding handler
        private Dictionary<string, IArgumentHandler> ArgumentHandlers;

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

            ArgumentHandlers = new Dictionary<string, IArgumentHandler>();

            // Retrieves all the types in the program and filters to only the types where it implements the IArgumentHandler interface
            foreach (Type type in Assembly.GetExecutingAssembly().GetTypes().Where(type => type.GetInterfaces().Contains(typeof(IArgumentHandler)))) 
            {

                // Creating handler variable
                IArgumentHandler handler;

                // attempting to create an instance of the current handler
                // skipping this handler if creating the instance failed
                try
                {
                    handler = (IArgumentHandler)Activator.CreateInstance(type);
                } catch (Exception ex)
                {
                    Console.WriteLine($"Could not register handler. {ex}");
                    continue;
                }

                // Adding the handler to the handlers dictionary if an instance was successfully created
                ArgumentHandlers.Add(handler.Argument, handler);
            
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

        public void HandleArgument(string argument, string[] args)
        {

            if (!CheckArgumentExist(argument))
            {
                Console.WriteLine("Could not execute argument.");
                return;
            }

            IArgumentHandler handler = ArgumentHandlers[argument];

            bool success = handler.HandleArgument(args);

            if (success)
                Console.WriteLine("Argument executed successfully.");
            else
                Console.WriteLine("Argument failed to successfully execute.");

        }

        public IArgumentHandler[] GetHandlers()
        {
            return ArgumentHandlers.Values.ToArray();
        }

        public IArgumentHandler GetArgumentHandler(string argument)
        {
            return ArgumentHandlers[argument];
        }

    }
}
