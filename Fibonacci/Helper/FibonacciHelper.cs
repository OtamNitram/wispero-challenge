using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fibonacci
{
    public static class FibonacciHelper
    {
        /// <summary>
        /// This method will return the fibonacci number for an integer.
        /// </summary>
        /// <param name="n">Input number</param>
        /// <returns>Fibonacci number based on 'n'.</returns>
        public static int GetFibonacci(int n)
        {
            if (n == 0)
            {
                return 0;
            }

            if (n == 1)
            {
                return 1;
            }

            return GetFibonacci(n - 1) + GetFibonacci(n - 2);
        }
    }
}
