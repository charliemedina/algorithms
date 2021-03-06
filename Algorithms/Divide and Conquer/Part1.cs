﻿using System;
using System.Drawing;
using System.Linq;

namespace Algorithms.Divide_and_Conquer
{
    public static class Part1
    {
        #region Karatsuba 

        // T(n) = 3T(n/2) + O(n)
        // T(n) = O(n ^ log2 3) 
        // T(n) = O(n ^ 1.59)

        public static decimal Karatsuba(long x, long y)
        {
            var @base = 10;
            if (x < @base || y < @base)
            {
                return x * y;
            }

            var n = Math.Max(x.ToString().Length, y.ToString().Length);
            var m = n / 2;

            var a = (long)Math.Floor(x / Math.Pow(10, m));
            var b = (long)(x % Math.Pow(10, m));
            var c = (long)Math.Floor(y / Math.Pow(10, m));
            var d = (long)(y % Math.Pow(10, m));

            var ac = Karatsuba(a, c);
            var bd = Karatsuba(b, d);
            var abcd = Karatsuba(a + b, c + d);
            var magic = abcd - ac - bd;

            return ac * (long)Math.Pow(10, (2 * m)) + magic * (long)Math.Pow(10, m) + bd;
        }

        public static decimal Karatsuba(string x, string y)
        {
            var xDecimal = Convert.ToDecimal(x);
            var yDecimal = Convert.ToDecimal(y);

            if (xDecimal < 10 && yDecimal < 10)
            {
                return xDecimal * yDecimal;
            }

            var xHalf = x.Length / 2;
            var yHalf = y.Length / 2;

            var a = x.Substring(0, xHalf);
            var b = x.Substring(xHalf);
            var c = y.Substring(0, yHalf);
            var d = y.Substring(yHalf);

            return Merge(a, b, c, d);
        }

        private static decimal Merge(string a, string b, string c, string d)
        {
            var n = a.Length + b.Length;
            var half = n / 2;

            var ac = Karatsuba(a, c);
            var bd = Karatsuba(b, d);
            var ad = Karatsuba(a, d);
            var bc = Karatsuba(b, c);

            return (long)Math.Pow(10, n) * ac + (long)Math.Pow(10, half) * (ad + bc) + bd;
        }

        #endregion

        #region Merge Sort 

        // T(n) = 2T(n/2) + O(n) 
        // T(n) = O(n log n)
        public static void MergeSort(int[] input, int left, int right)
        {
            if (left < right)
            {
                var middle = (left + right) / 2;
                MergeSort(input, left, middle); // O(n/2)
                MergeSort(input, middle + 1, right); // O(n/2)
                Merge(input, left, middle, right); // O(n)
            }
        }

        private static void Merge(int[] input, int left, int middle, int right)
        {
            var n1 = middle - left + 1;
            var n2 = right - middle;

            int[] leftArray = new int[n1];
            int[] rightArray = new int[n2];

            Array.Copy(input, left, leftArray, 0, n1);
            Array.Copy(input, middle + 1, rightArray, 0, n2);

            var i = 0;
            var j = 0;

            for (int k = left; k < right + 1; k++)
            {
                if (i == leftArray.Length)
                {
                    input[k] = rightArray[j];
                    j++;
                }
                else if (j == rightArray.Length)
                {
                    input[k] = leftArray[i];
                    i++;
                }
                else if (leftArray[i] <= rightArray[j])
                {
                    input[k] = leftArray[i];
                    i++;
                }
                else
                {
                    input[k] = rightArray[j];
                    j++;
                }
            }
        }

        #endregion

        #region Counting Inversions

        // T(n) = O(n log n)
        public static int Sort_and_Count(int[] input)
        {
            if (input.Length < 2)
                return 0;

            int middle = (input.Length + 1) / 2;
            int[] left = new int[middle];
            int[] right = new int[input.Length - middle];

            Array.Copy(input, 0, left, 0, middle);
            Array.Copy(input, middle, right, 0, input.Length - middle);

            return Sort_and_Count(left) + Sort_and_Count(right) + Merge_and_CountSplitInv(input, left, right);
        }

        private static int Merge_and_CountSplitInv(int[] input, int[] left, int[] right)
        {
            int i = 0, j = 0, count = 0;
            while (i < left.Length || j < right.Length)
            {
                if (i == left.Length)
                {
                    input[i + j] = right[j];
                    j++;
                }
                else if (j == right.Length)
                {
                    input[i + j] = left[i];
                    i++;
                }
                else if (left[i] <= right[j])
                {
                    input[i + j] = left[i];
                    i++;
                }
                else
                {
                    input[i + j] = right[j];
                    count += left.Length - i;
                    j++;
                }
            }
            return count;
        }

        #endregion

        #region Find Max Crossing SubArray

        // T(n) = O(1) + 2T(n/2) + O(n) + O(1)
        // T(n) = 2T(n/2) + O(n) 
        // T(n) = O(n log n)
        public static (int low, int high, int sum) FindMaximunSubArray(int[] input, int low, int high)
        {
            if (low == high) // base case: only one element
            {
                return (low, high, input[low]); // O(1)
            }
            else
            {
                int mid = (low + high) / 2;
                var (left_low, left_high, left_sum) = FindMaximunSubArray(input, low, mid); // O(n/2)
                var (right_low, right_high, right_sum) = FindMaximunSubArray(input, mid + 1, high);  // O(n/2)
                var (cross_low, cross_high, cross_sum) = FindMaxCrossingSubArray(input, low, mid, high);  // O(n)

                // O(1)
                if (left_sum >= right_sum && left_sum >= cross_sum)
                {
                    return (left_low, left_high, left_sum);
                }
                else if (right_sum >= left_sum && right_sum >= cross_sum)
                {
                    return (right_low, right_high, right_sum);
                }
                else
                {
                    return (cross_low, cross_high, cross_sum);
                }
            }
        }

        private static (int low, int high, int sum) FindMaxCrossingSubArray(int[] input, int low, int mid, int high)
        {
            var left_sum = int.MinValue;
            var sum1 = 0;
            var max_left = 0;

            for (int i = mid; i > low; i--)
            {
                sum1 += input[i];
                if (sum1 > left_sum)
                {
                    left_sum = sum1;
                    max_left = i;
                }
            }

            var right_sum = int.MinValue;
            var sum2 = 0;
            var max_right = 0;

            for (int i = mid + 1; i < high; i++)
            {
                sum2 += input[i];
                if (sum2 > right_sum)
                {
                    right_sum = sum2;
                    max_right = i;
                }
            }
            return (max_left, max_right, left_sum + right_sum);
        }

        #endregion

        #region Closet Pair 

        // T(n) = 2T(n/2) + O(n) + O(n logn) + O(n)
        // T(n) = 2T(n/2) + O(n log n)
        // T(n) = T(n x log n x log n)

        public static double ClosetPair(Point[] points, int n)
        {
            points = points.OrderBy(p => p.X).ToArray();

            if (n <= 3) // If there are 2 or 3 points, then use brute force  
            {
                return BruteForce(points, n);
            }

            int mid = n / 2;
            Point midPoint = points[mid];

            var lp = new Point[mid];
            var rp = new Point[points.Length - mid];

            Array.Copy(points, 0, lp, 0, mid);
            Array.Copy(points, mid, rp, 0, n - mid);
                
            var left_dist = ClosetPair(rp, mid);
            var right_dist = ClosetPair(lp, n - mid);

            var dist = Math.Min(left_dist, right_dist);

            var strip = new Point[n];
            int j = 0;

            for (int i = 0; i < n; i++)
            {
                if (Math.Abs(points[i].X - midPoint.X) < dist)
                {
                    strip[j] = points[i];
                    j++;
                }
            }
            return Math.Min(dist, ClosetSplitPair(strip, j, dist));
        }

        private static double ClosetSplitPair(Point[] strip, int size, double dist)
        {
            var min = dist;

            strip = strip.OrderBy(p => p.Y).ToArray();

            for (int i = 0; i < size; ++i)
            {
                for (int j = i + 1; j < size && (strip[j].Y - strip[i].Y) < min; ++j)
                {
                    var d = EuclideanDistance(strip[i], strip[j]);
                    if (d < min)
                    {
                        min = d;
                    }
                }
            }
            return min;
        }

        private static double BruteForce(Point[] points, int n)
        {
            var min = (double)int.MaxValue;

            for (int i = 0; i < n; ++i)
            {
                for (int j = i + 1; j < n; ++j)
                {
                    if (EuclideanDistance(points[i], points[j]) < min)
                    {
                        min = EuclideanDistance(points[i], points[j]);
                    }
                }
            }
            return min;
        }

        private static double EuclideanDistance(Point p1, Point p2) => 
            Math.Sqrt((p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y));

        #endregion
    }
}
