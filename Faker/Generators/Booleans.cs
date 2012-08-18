using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Faker.Generators
{
    /// <summary>
    /// Generator used for Boolean types
    /// </summary>
    public static class Booleans
    {
        private static readonly Random R = new Random();

        /// <summary>
        /// Returns a random boolean value
        /// </summary>
        /// <returns>true or false</returns>
        public static bool Bool()
        {
            var num = R.Next(0, 2);

            //in C any integer value other than 0 is true.
            return num != 0;
        }
    }
}
