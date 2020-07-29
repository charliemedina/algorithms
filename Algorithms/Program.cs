using Algorithms.Divide_and_Conquer;
using NUnit.Framework;
using System;

namespace Algorithms
{
    class Program
    {
        static void Main(string[] args)
        {
            dynamic x, y, result, expected, array, sortedArray;

            #region Divide and Conquer

            Console.WriteLine("\n--------------------------------------------- Divide and Conquer ---------------------------------------------");
            Console.WriteLine("\n- Karatsuba Multiplication\n");

            x = 1234;
            y = 5678;

            result = Part1.Karatsuba(x, y);
            expected = x * y;
            Assert.AreEqual(result, expected); 

            Console.WriteLine($"\tKaratsuba({x}, {y}) is: {result}\n");

            x = "1234";
            y = "5678";

            result = Part1.Karatsuba(x, y);
            expected = int.Parse(x) * int.Parse(y);
            Assert.AreEqual(result, expected);

            Console.WriteLine($"\tKaratsuba(\"{x}\", \"{y}\") is: {result}\n");

            Console.WriteLine("\n- Merge Sort\n");

            array = new[] { 2, 4, 5, 7, 1, 2, 3, 6 };

            Console.WriteLine($"\tOriginal Array: ");
            Helpers.PrintArray(array);

            sortedArray = new[] { 1, 2, 2, 3, 4, 5, 6, 7 };

            Part1.MergeSort(array, 0, array.Length - 1);

            CollectionAssert.AreEqual(array, sortedArray);

            Console.WriteLine($"\tSorted Array: ");
            Helpers.PrintArray(array);

            Console.WriteLine("\n- Counting Inversions\n");

            array = new[] { 4, 1, 3, 2, 9, 1 };
            sortedArray = new[] { 1, 1, 2, 3, 4, 9 };

            Console.WriteLine($"\tOriginal Array: ");
            Helpers.PrintArray(array);

            var count_inversions = Part1.Sort_and_Count(array);
            expected = 8;
          
            CollectionAssert.AreEqual(array, sortedArray);
            Assert.AreEqual(count_inversions, expected);

            Console.WriteLine($"\tSorted Array: ");
            Helpers.PrintArray(array);

            Console.Write($"\tNumber of inversions are: {count_inversions}\n");

            Console.WriteLine("\n- Find Max Crossing SubArray\n");

            var array1 = new[] { 13, -3, -25, 20, -3, -16, -23, 18, 20, -7, 12, -5, -22, 15, -4, 7 };

            Console.WriteLine($"\tArray: ");
            Helpers.PrintArray(array1);

            var (low, high, sum) = Part1.FindMaximunSubArray(array1, 0, array1.Length - 1);

            Assert.AreEqual(low, 7);
            Assert.AreEqual(high, 10);
            Assert.AreEqual(sum, 43);

            Console.Write($"\tThe maximum subarray is: from {low} (value {array1[low]}) to {high} (value {array1[high]}) and the sum is {sum}");

            #endregion

            Console.WriteLine();
        }

        static class Helpers
        {
            public static void PrintArray<T>(T[] array)
            {
                Console.Write("\t");
                for (int i = 0; i < array.Length; i++)
                {
                    Console.Write(array[i] + "   ");
                }
                Console.WriteLine("\n");
            }
            public static void Print2DArray<T>(T[,] matrix)
            {
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    Console.Write("\t");
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        Console.Write(matrix[i, j] + "\t");
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}
