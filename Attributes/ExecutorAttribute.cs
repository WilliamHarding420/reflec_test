using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionExample.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    internal class ExecutorAttribute : Attribute
    {
    }
}
