using Algorithms.Divide_and_Conquer;
using NUnit.Framework;
using System;

namespace Algorithms
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Divide and Conquer

            Console.WriteLine("\n--------------------------------------------- Divide and Conquer ---------------------------------------------");
            Console.WriteLine("\n********* Karatsuba Multiplication **********\n");

            var x = 1234;
            var y = 5678;

            var result = Class1.Karatsuba(x, y);
            var expected = x * y;
            Assert.AreEqual(result, expected); 

            Console.WriteLine($"Karatsuba({x}, {y}) is: {result}\n");

            var x1 = "1234";
            var y1 = "5678";

            var result1 = Class1.Karatsuba(x1, y1);
            var expected1 = int.Parse(x1) * int.Parse(y1);
            Assert.AreEqual(result1, expected1);

            Console.WriteLine($"Karatsuba(\"{x1}\", \"{y1}\") is: {result1}\n");

            #endregion
        }
    }
}
