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
        /// Generates a random DateTimeOffset between the From and To times
        /// </summary>
        /// <param name="from">The beginning of the date range</param>
        /// <param name="to">The end of the date range</param>
        /// <returns>A DateTimeOffset between From and To</returns>
        public static DateTimeOffset GetDateTimeOffset(DateTimeOffset @from, DateTimeOffset to)
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

        /// <summary>
        /// Returns a timestamp for the specified DateTime
        /// </summary>
        /// <param name="when">The date to be converted to a timestamp</param>
        /// <returns>A timestamp in long format</returns>
        public static long GetTimeStamp(DateTime when)
        {
            //Calculate the TimeSpan since the UNIX epoch
            var span = (when - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToUniversalTime());

            //Return the timestamp as a long measured in total seconds
            return (long) span.TotalSeconds;
        }

        /// <summary>
        /// Returns a timestamp within the specified DateTime range
        /// </summary>
        /// <param name="from">The beginning of the date range</param>
        /// <param name="to">The end of the date range</param>
        /// <returns>A timestamp between From and To</returns>
        public static long GetTimeStamp(DateTime @from, DateTime to)
        {
            var dateTime = GetDateTime(from, to);
            return GetTimeStamp(dateTime);
        }

        /// <summary>
        /// Returns a random timestamp
        /// </summary>
        /// <returns>A timestamp in long format</returns>
        public static long GetTimeStamp()
        {
            var dateTime = GetDateTime();
            return GetTimeStamp(dateTime);
        }
    }
}
