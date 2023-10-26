﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace WPF.Extenstions
{
    internal static class EnumerablePickExtention
    {
        private static readonly Random rnd = new Random(Environment.TickCount);

        public static IEnumerable<T> PickRandom<T>(this IEnumerable<T> source, int count, Random random = null, Func<T, bool> predicate = default)
        {
            return source.Where(predicate).ShakeAll(random).Take(count);
        }

        public static T PickRandom<T>(this IEnumerable<T> source, Random random = null, Func<T, bool> predicate = default)
        {
            var size = source.Count();
            random ??= rnd;

            if (predicate == null)
            {
                return source.ElementAt(random.Next(size));
            }
            else
            {
                T output;
                do
                {
                    output = source.ElementAt(random.Next(size));
                } while (!predicate(output));
                return output;
            }
        }

        public static bool TryPickRandom<T>(this IEnumerable<T> source, out T result, Random random = null, Func<T, bool> predicate = default)
        {
            random ??= rnd;

            var selected = predicate is null ? source.ToArray() : source.Where(predicate).ToArray();
            var size = selected.Length;
            var tryResult = size > 0;
            result = selected.ElementAtOrDefault(random.Next(size));
            return tryResult;
        }

        public static IEnumerable<T> ShakeAll<T>(this IEnumerable<T> source, Random random = null)
        {
            return source.OrderBy(x => (random ?? rnd).Next());
        }
    }
}
