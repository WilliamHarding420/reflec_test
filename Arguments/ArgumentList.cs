using ReflectionExample.Attributes;
using ReflectionExample.Interfaces;
using static ReflectionExample.ArgumentHandler;

namespace ReflectionExample.Arguments
{
    internal class ArgumentList : IArgumentHandler
    {
        public string Argument { get => "list"; }
        public string Usage { get => "reflectionexample list"; }
        public string Description { get => "Lists all of the existing arguments and their usage."; }

        [Executor]
        public bool HandleArgument()
        {
            
            ArgumentHandler handler = ArgumentHandler.Instance;

            HandlerInfo[] handlers = handler.GetHandlers();

            foreach (HandlerInfo argumentHandler in handlers)
            {

                Console.WriteLine($"Argument: {argumentHandler.Handler.Argument}");
                Console.WriteLine($"\tUsage: {argumentHandler.Handler.Usage}\n");

            }

            return true;

        }
    }
}
