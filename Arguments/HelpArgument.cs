using ReflectionExample.Interfaces;

namespace ReflectionExample.Arguments
{
    internal class HelpArgument : IArgumentHandler
    {
        public string Argument => "help";

        public string Usage => "reflectionexample help <argument>";

        public string Description => "Returns the information regarding the given argument.";

        public bool HandleArgument(string[] args)
        {

            if (args.Length < 1) 
            {
                Console.WriteLine($"No argument supplied.. Usage: {Usage}");
                return false; 
            }

            ArgumentHandler handler = ArgumentHandler.Instance;

            bool checkArgumentExists = handler.CheckArgumentExist(args[0]);

            if (!checkArgumentExists)
            {
                Console.WriteLine($"Cannot get argument details.. argument {args[0]} cannot be found.");
                return false; 
            }

            IArgumentHandler argument = handler.GetArgumentHandler(args[0]);

            Console.WriteLine($"Argument: {argument.Argument}\n\tDescription: {argument.Description}\n\tUsage: {argument.Usage}");

            return true;

        }
    }
}
