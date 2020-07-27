﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms.Divide_and_Conquer
{
    public static class Class1
    {
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

            if (xDecimal < 10 && yDecimal < 10) {
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
    }
}