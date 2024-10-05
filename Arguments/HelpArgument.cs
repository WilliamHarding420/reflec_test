using ReflectionExample.Attributes;
using ReflectionExample.Interfaces;
using static ReflectionExample.ArgumentHandler;

namespace ReflectionExample.Arguments
{
    internal class HelpArgument : IArgumentHandler
    {
        public string Argument => "help";

        public string Usage => "reflectionexample help <argument>";

        public string Description => "Returns the information regarding the given argument.";

        [Executor]
        public bool HandleArgument(string argument)
        {

            ArgumentHandler handler = ArgumentHandler.Instance;

            bool checkArgumentExists = handler.CheckArgumentExist(argument);

            if (!checkArgumentExists)
            {
                Console.WriteLine($"Cannot get argument details.. argument {argument} cannot be found.");
                return false; 
            }

            HandlerInfo handlerInfo = handler.GetArgumentHandler(argument);
            IArgumentHandler argumentHandler = handlerInfo.Handler;

            Console.WriteLine($"Argument: {argumentHandler.Argument}\n\tDescription: {argumentHandler.Description}\n\tUsage: {argumentHandler.Usage}");

            return true;

        }
    }
}
