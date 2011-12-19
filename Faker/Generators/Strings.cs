using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Faker.Generators
{
    /// <summary>
    /// Generator class used for creating random stings
    /// </summary>
    public static class Strings
    {
        private static readonly Random R = new Random();

        /// <summary>
        /// Generates a random string of the specified maxLength
        /// </summary>
        /// <param name="minLength">The minimum lenght of the random string</param>
        /// <param name="maxLength">The maxLength of the random string</param>
        /// <returns>A string</returns>
        public static string GenerateString(int minLength = 10, int maxLength = 40)
        {
            var sb = new StringBuilder();
            var stringLength = R.Next(minLength, maxLength);

            for(var i = 0; i < stringLength; i++)
            {
                var next = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * R.NextDouble() + 65)));
                sb.Append(next);
            }

            return sb.ToString();
        }
    }
}
