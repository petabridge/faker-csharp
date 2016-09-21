using System;

namespace Faker.Helpers
{
    /*
        Implementation courtesy of Jon Skeet http://stackoverflow.com/questions/609501/generating-a-random-decimal-in-c-sharp
     */

    public static class RandomHelper
    {
        /// <summary>
        ///     Returns an Int32 with a random value across the entire range of
        ///     possible values.
        /// </summary>
        public static int NextInt32(this Random rng)
        {
            var firstBits = rng.Next(0, 1 << 4) << 28;
            var lastBits = rng.Next(0, 1 << 28);
            return firstBits | lastBits;
        }

        public static decimal NextDecimal(this Random rng)
        {
            var scale = (byte) rng.Next(29);
            var sign = rng.Next(2) == 1;
            return new decimal(rng.NextInt32(),
                rng.NextInt32(),
                rng.NextInt32(),
                sign,
                scale);
        }

        public static decimal NextDecimal(this Random rng, decimal min, decimal max)
        {
            var scale = (byte) rng.Next(29);
            var sign = rng.Next(2) == 1;
            return new decimal(Convert.ToInt32(min),
                rng.NextInt32(),
                Convert.ToInt32(max),
                sign,
                scale);
        }
    }
}