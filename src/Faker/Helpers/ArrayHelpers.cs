using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Faker.Helpers
{
    /// <summary>
    ///     Extension methods for working with arrays
    /// </summary>
    public static class ArrayHelpers
    {
        private static readonly Random R = new Random();

        public static T GetRandomSubSelection<T>(this IEnumerable<T> array, int start, int count)
        {
            //Get a sub-selection of the current array if the parameters are valid...
            var subArray = array.ToList().GetRange(start, count);

            //Determine the max length of our incoming array
            var maxLength = subArray.Count;

            return subArray.ElementAt(R.Next(0, maxLength));
        }

        public static IEnumerable<T> GetRandomSelection<T>(this IEnumerable<T> array, int count = 1)
        {
            //Determine the max length of our incoming array
            var maxLength = array.Count();

            //Create a new list to contain our selection...
            var randomSelection = new List<T>();

            for (var i = 0; i < count; i++)
            {
                var randomInterval = R.Next(0, maxLength);
                randomSelection.Add(array.ElementAt(randomInterval));
            }

            return randomSelection;
        }

        public static T GetRandom<T>(this IEnumerable<T> array)
        {
            //Determine the max length of our incoming array
            var maxLength = array.Count();

            return array.ElementAt(R.Next(0, maxLength));
        }

        /// <summary>
        /// Produces the exact same content as the input array, but in different orders.
        /// 
        /// IMMUTABLE.
        /// </summary>
        /// <typeparam name="T">The type of entity in the array</typeparam>
        /// <param name="array">The target input</param>
        /// <returns>A randomized, shuffled copy of the original array</returns>
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> array)
        {
            var original = array.ToList();
            if (original.Count <= 1) // can't shuffle an array with 1 or fewer elements
                return original;

            /*
             * https://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle
             */
            var newList = new T[original.Count];

            for (var i = 0; i < original.Count;i++)
            {
                var j = Faker.Generators.Numbers.Int(0, i);
                if (j != i)
                    newList[i] = newList[j];
                newList[j] = original[i];
            }
            return newList;
        }
    }
}