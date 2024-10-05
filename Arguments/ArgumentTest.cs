using ReflectionExample.Attributes;
using ReflectionExample.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionExample.Arguments
{
    internal class ArgumentTest : IArgumentHandler
    {
        public string Argument => "test1";

        public string Usage => "test";

        public string Description => "fdhsjak";

        [Executor]
        public bool Execute(int test1, string[] testArray)
        {

            Console.WriteLine($"{test1}\t");

            foreach (string s in testArray) Console.WriteLine(s);

            return true;

        }
    }
}
