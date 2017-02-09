using Microsoft.Practices.Unity.InterceptionExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMHub.Aspects
{
    internal class LoggerCallHandler : ICallHandler
    {
        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            IMethodReturn result = getNext()(input, getNext);

            Console.WriteLine("Parameters:");
            foreach (var parameter in input.Arguments)
            {
                Console.WriteLine(parameter.ToString());
            }

            Console.WriteLine();

            return result;
        }

        public int Order { get; set; }
    }
}