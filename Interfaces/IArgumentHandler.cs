using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionExample.Interfaces
{
    internal interface IArgumentHandler
    {

        string Argument { get; }

        string Usage { get; }

        string Description { get; }

        bool HandleArgument(string[] args);

    }
}
