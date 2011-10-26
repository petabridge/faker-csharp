using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Faker.Helpers
{
    /// <summary>
    /// Extension methods for working with arrays
    /// </summary>
    public static class ArrayHelpers
    {
        private static Random r = new Random();

        public static IEnumerable<T> GetRandomSelection<T>(this IEnumerable<T> array, int count = 1)
        {
            //Determine the max length of our incoming array
            var maxLength = array.Count() - 1;

            //Create a new list to contain our selection...
            var randomSelection = new List<T>();

            for(var i = 0; i < count; i++)
            {
                var randomInterval = r.Next(0, maxLength);
                randomSelection.Add(array.ElementAt(randomInterval));
            }

            return randomSelection;
        }

        public static T GetRandom<T>(this IEnumerable<T> array)
        {
            //Determine the max length of our incoming array
            var maxLength = array.Count() - 1;

            return array.ElementAt(r.Next(0, maxLength));
        }
    }
}
