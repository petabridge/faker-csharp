using System;

namespace Faker.Generators
{
    /// <summary>
    ///     Generator used for Boolean types
    /// </summary>
    public static class Booleans
    {
        private static readonly Random R = new Random();

        /// <summary>
        ///     Returns a random boolean value
        /// </summary>
        /// <returns>true or false</returns>
        public static bool Bool()
        {
            var num = (int) Math.Round(R.NextDouble());

            //in C any integer value other than 0 is true.
            return num != 0;
        }

        /// <summary>
        /// Randomly generates a <c>true</c> value within a certain probability.
        /// </summary>
        /// <param name="probability">A double between 0.0 and 1.0</param>
        /// <returns>true or false</returns>
        public static bool BoolWithProbability(double probability)
        {
            return R.NextDouble() <= probability;
        }
    }
}