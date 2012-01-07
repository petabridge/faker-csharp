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
        private static readonly Random R = new Random();

        public static T GetRandomSubSelection<T>(this IEnumerable<T> array, int start, int count)
        {
            //Get a sub-selection of the current array if the parameters are valid...
            var subArray = array.ToList().GetRange(start, count);

            //Determine the max length of our incoming array
            var maxLength = subArray.Count - 1;

            return subArray.ElementAt(R.Next(0, maxLength));
        }

        public static IEnumerable<T> GetRandomSelection<T>(this IEnumerable<T> array, int count = 1)
        {
            //Determine the max length of our incoming array
            var maxLength = array.Count() - 1;

            //Create a new list to contain our selection...
            var randomSelection = new List<T>();

            for(var i = 0; i < count; i++)
            {
                var randomInterval = R.Next(0, maxLength);
                randomSelection.Add(array.ElementAt(randomInterval));
            }

            return randomSelection;
        }

        public static T GetRandom<T>(this IEnumerable<T> array)
        {
            //Determine the max length of our incoming array
            var maxLength = array.Count() - 1;

            return array.ElementAt(R.Next(0, maxLength));
        }
    }
}
