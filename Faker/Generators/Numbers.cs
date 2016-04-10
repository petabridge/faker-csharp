using System;
using Faker.Helpers;

namespace Faker.Generators
{
    /// <summary>
    ///     Generator used for numeric types
    /// </summary>
    public static class Numbers
    {
        private static readonly Random R = new Random();

        /// <summary>
        ///     Generates a random integer within the specified range
        /// </summary>
        /// <param name="min">The minimum number in the range - default is 0 (you can still use negative numbers)</param>
        /// <param name="max">The maximum number in the range - default is Int32.MaxValue</param>
        /// <returns>An integer</returns>
        public static int Int(int min = 0, int max = int.MaxValue)
        {
            return R.Next(min, max);
        }

        /// <summary>
        ///     Generates a random long within the specified range
        /// </summary>
        /// <param name="min">The minimum number in the range - default is 0 (you can still use negative numbers)</param>
        /// <param name="max">The maximum number in the range - default is Int64.MaxValue</param>
        /// <returns>A long</returns>
        public static long Long(long min = 0, long max = long.MaxValue)
        {
            var nextVal = R.NextDouble();

            //Shave off the decimal since we're trying to generate a long...
            nextVal = nextVal - Math.Truncate(nextVal);

            //Return a number within range
            return (long) (nextVal*(max - min) + min);
        }

        /// <summary>
        ///     Generates a random float within the specified range
        /// </summary>
        /// <param name="min">The minimum number in the range - default is 0.0f (you can still use negative numbers)</param>
        /// <param name="max">The maximum number in the range - default is Single.MaxValue</param>
        /// <returns>A float</returns>
        public static float Float(float min = 0.0f, float max = float.MaxValue)
        {
            var nextVal = R.NextDouble();
            var range = max - (double) min;

            return (float) (nextVal*range + min);
        }

        /// <summary>
        ///     Generates a random double within the specified range
        /// </summary>
        /// <param name="min">The minimum number in the range - default is 0.0d (you can still use negative numbers)</param>
        /// <param name="max">The maximum number in the range - default is Double.MaxValue</param>
        /// <returns>A double</returns>
        public static double Double(double min = 0.0d, double max = double.MaxValue)
        {
            var nextVal = R.NextDouble();

            return nextVal*(max - min) + min;
        }

        /// <summary>
        ///     Generates a random short integer within the specified range
        /// </summary>
        /// <param name="min">The minimum number in the range - default is 0 (you can still use negative numbers)</param>
        /// <param name="max">The maximum number in the range - default is Int16.MaxValue</param>
        /// <returns>A short integer</returns>
        public static short Short(short min = 0, short max = short.MaxValue)
        {
            var nextVal = R.NextDouble();

            //Shave off the decimal point
            nextVal = nextVal - Math.Truncate(nextVal);

            return (short) (nextVal*(max - min) + min);
        }

        /// <summary>
        ///     Generates a random decimal within the specified range
        /// </summary>
        /// <param name="min">The minimum number in the range - default is 0.0d (you can still use negative numbers)</param>
        /// <param name="max">The maximum number in the range - default is Decimal.MaxValue</param>
        /// <returns>A decimal</returns>
        public static decimal Decimal(decimal min = 0.0m, decimal max = decimal.MaxValue)
        {
            return R.NextDecimal(min, max);
        }

        /// <summary>
        ///     Generates a random unsigned long within the specified range
        /// </summary>
        /// <param name="min">The minimum number in the range - default is 0 (you can still use negative numbers)</param>
        /// <param name="max">The maximum number in the range - default is UInt64.MaxValue</param>
        /// <returns>An unsigned long</returns>
        public static ulong ULong(ulong min = 0, ulong max = ulong.MaxValue)
        {
            var nextVal = R.NextDouble();

            //Shave off the decimal since we're trying to generate a long...
            nextVal = nextVal - Math.Truncate(nextVal);

            //Return a number within range
            return (ulong) (nextVal*(max - min) + min);
        }

        /// <summary>
        ///     Generates a random unsigned integer within the specified range
        /// </summary>
        /// <param name="min">The minimum number in the range - default is 0 (you can still use negative numbers)</param>
        /// <param name="max">The maximum number in the range - default is Int32.MaxValue</param>
        /// <returns>An unsigned integer</returns>
        public static uint UInt(uint min = 0, uint max = uint.MaxValue)
        {
            var nextVal = R.NextDouble();

            //Shave off the decimal since we're trying to generate a long...
            nextVal = nextVal - Math.Truncate(nextVal);

            //Return a number within range
            return (uint) (nextVal*(max - min) + min);
        }

        /// <summary>
        ///     Generates a random unsigned short integer within the specified range
        /// </summary>
        /// <param name="min">The minimum number in the range - default is 0 (you can still use negative numbers)</param>
        /// <param name="max">The maximum number in the range - default is Int16.MaxValue</param>
        /// <returns>An unsigned short integer</returns>
        public static ushort Short(ushort min = 0, ushort max = ushort.MaxValue)
        {
            var nextVal = R.NextDouble();

            //Shave off the decimal point
            nextVal = nextVal - Math.Truncate(nextVal);

            return (ushort) (nextVal*(max - min) + min);
        }

        /// <summary>
        ///     Generates a random integer byte (SByte) within the specified range
        /// </summary>
        /// <param name="min">The minimum number in the range - default is SByte.MinValue</param>
        /// <param name="max">The maximum number in the range - default is SByte.MaxValue</param>
        /// <returns>An integer byte (SByte)</returns>
        public static sbyte SByte(sbyte min = sbyte.MinValue, sbyte max = sbyte.MaxValue)
        {
            var nextVal = R.NextDouble();

            //Shave off the decimal point
            nextVal = nextVal - Math.Truncate(nextVal);

            return (sbyte) (nextVal*(max - min) + min);
        }
    }
}