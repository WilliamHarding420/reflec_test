using ReflectionExample.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionExample.Arguments
{
    internal class ArgumentList : IArgumentHandler
    {
        public string Argument { get => "list"; }
        public string Usage { get => "reflectionexample list"; }
        public string Description { get => "Lists all of the existing arguments and their usage."; }

        public bool HandleArgument(string[] args)
        {
            
            ArgumentHandler handler = ArgumentHandler.Instance;

            IArgumentHandler[] handlers = handler.GetHandlers();

            foreach (IArgumentHandler argumentHandler in handlers)
            {

                Console.WriteLine($"Argument: {argumentHandler.Argument}");
                Console.WriteLine($"\tUsage: {argumentHandler.Usage}\n");

            }

            return true;

        }
    }
}
