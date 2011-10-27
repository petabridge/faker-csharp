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
        /// Generates a random string of the specified length
        /// </summary>
        /// <param name="length">The length of the random string</param>
        /// <returns>A string</returns>
        public static string GenerateString(int length = 20)
        {
            var sb = new StringBuilder();

            for(var i = 0; i < length; i++)
            {
                var next = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * R.NextDouble() + 65)));
                sb.Append(next);
            }

            return sb.ToString();
        }
    }
}
