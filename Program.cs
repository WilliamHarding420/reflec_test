namespace ReflectionExample
{
    internal class Program
    {
        static void Main(string[] args)
        {

            ArgumentHandler handler = ArgumentHandler.Instance;

            // If no argument is supplied, list all arguments.
            if (args.Length == 0)
            {
                handler.HandleArgument("list", new string[0]);
                return;
            }

            // Passes in the argument and the rest of the arguments array, cutting off the first element as it's the argument
            // [1..^0] passes in all but the first element of the array, it means to create a new array consisting of the element from the index 1 to the last one
            // ^0 means 0 elements from the end, so the last element, ^1 would cut off the last element of the array
            handler.HandleArgument(args[0], args[1..^0]);

        }
    }
}
