using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Faker.Generators
{
    /// <summary>
    /// Generator for spawning random DateTime objects
    /// </summary>
    public static class DateTimes
    {
        private static readonly Random R = new Random();

        /// <summary>
        /// Generates a random DateTime between the From and To times
        /// </summary>
        /// <param name="from">The beginning of the date range</param>
        /// <param name="to">The end of the date range</param>
        /// <returns>A DateTime between From and To</returns>
        public static DateTime GetDateTime(DateTime @from, DateTime to)
        {
            var span = new TimeSpan(to.Ticks - from.Ticks);
            return from + new TimeSpan((long)(span.Ticks * R.NextDouble()));
        }

        /// <summary>
        /// Generates a random DateTime sometime between now and +/- 70 years
        /// </summary>
        /// <returns>A valid DateTime</returns>
        public static DateTime GetDateTime()
        {
            return GetDateTime(DateTime.Now.AddYears(-70), DateTime.Now.AddYears(70));
        }
    }
}
