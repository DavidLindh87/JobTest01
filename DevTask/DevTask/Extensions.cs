﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DevTask
{
    public static class Extensions
    {

        public static IEnumerable<string> Split(this string str, int n)
        {
            if (String.IsNullOrEmpty(str) || n < 1)
            {
                throw new ArgumentException();
            }
            return Enumerable.Range(0, str.Length / n).Select(i => str.Substring(i * n, n));
        }

    }
}
