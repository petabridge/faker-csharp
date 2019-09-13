using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Faker.Generators;
using Xunit;

namespace Faker.Tests.GeneratorTests
{
    public class DateTimeGeneratorTests
    {
        [Fact(DisplayName = "Can we generate random datetimes?")]
        public void Can_Create_DateTimes_Within_Range()
        {
            var dateTimes = new List<DateTime>();
            for (var i = 0; i < 1000; i++)
            {
                dateTimes.Add(DateTimes.GetDateTime(DateTime.Now, DateTime.Now.AddYears(1)));
            }

            //Should not have any dates below the current date
            Assert.DoesNotContain(dateTimes, x => x < DateTime.Now);

            //All dates should be greater than the current date
            Assert.True(dateTimes.All(x => x >= DateTime.Now));

            //All dates should be less than today's date one year from now
            Assert.True(dateTimes.All(x => x <= DateTime.Now.AddYears(1)));

            //All dates should not be the same
            Assert.False(dateTimes.All(x => x == dateTimes[0]));
        }
        
        [Fact(DisplayName = "Can we generate datetimes within a couple-hour range?")]
        public void Can_Spawn_DateTimes_Within_Short_Range()
        {
            var dateTimes = new List<DateTime>();
            for (var i = 0; i < 1000; i++)
            {
                dateTimes.Add(DateTimes.GetDateTime(DateTime.Now, DateTime.Now.AddHours(2)));
            }

            //Should not have any dates below the current date
            Assert.DoesNotContain(dateTimes, x => x < DateTime.Now);

            //All dates should be greater than the current date
            Assert.True(dateTimes.All(x => x >= DateTime.Now));

            //All dates should be less than today's date one year from now
            Assert.True(dateTimes.All(x => x <= DateTime.Now.AddYears(1)));

            //All dates should not be the same
            Assert.False(dateTimes.All(x => x == dateTimes[0]));
        }

        [Fact(DisplayName = "Can we generate random DateTimeOffsets?")]
        public void Can_Create_DateTimeOffsets_Within_Range()
        {
            var dateTimes = new List<DateTimeOffset>();
            for (var i = 0; i < 1000; i++)
            {
                dateTimes.Add(DateTimes.GetDateTimeOffset(DateTimeOffset.Now, DateTimeOffset.Now.AddYears(1)));
            }

            //Should not have any dates below the current date
            Assert.DoesNotContain(dateTimes, x => x < DateTimeOffset.Now);

            //All dates should be greater than the current date
            Assert.True(dateTimes.All(x => x >= DateTimeOffset.Now));

            //All dates should be less than today's date one year from now
            Assert.True(dateTimes.All(x => x <= DateTimeOffset.Now.AddYears(1)));

            //All dates should not be the same
            Assert.False(dateTimes.All(x => x == dateTimes[0]));
        }

        [Fact(DisplayName = "Can we generate DateTimeOffsets within a couple-hour range?")]
        public void Can_Spawn_DateTimeOffsets_Within_Short_Range()
        {
            var dateTimes = new List<DateTimeOffset>();
            for (var i = 0; i < 1000; i++)
            {
                dateTimes.Add(DateTimes.GetDateTimeOffset(DateTimeOffset.Now, DateTimeOffset.Now.AddHours(2)));
            }

            //Should not have any dates below the current date
            Assert.DoesNotContain(dateTimes, x => x.Ticks < DateTimeOffset.Now.Ticks);

            //All dates should be greater than the current date
            Assert.True(dateTimes.All(x => x.Ticks >= DateTimeOffset.Now.Ticks));

            //All dates should be less than today's date one year from now
            Assert.True(dateTimes.All(x => x.Ticks <= DateTimeOffset.Now.AddYears(1).Ticks));

            //All dates should not be the same
            Assert.False(dateTimes.All(x => x == dateTimes[0]));
        }

        [Fact(DisplayName = "Can we generate random timestamps?")]
        public void Can_Create_TimeStamps_Within_Range()
        {
            var timestamps = new List<long>();
            for (var i = 0; i < 1000; i++)
            {
                timestamps.Add(DateTimes.GetTimeStamp(DateTime.Now, DateTime.Now.AddYears(1)));
            }

            //Should not have any dates below the current date
            Assert.DoesNotContain(timestamps, x => x < DateTimes.GetTimeStamp(DateTime.Now));

            //All dates should be greater than the current date
            Assert.True(timestamps.All(x => x >= DateTimes.GetTimeStamp(DateTime.Now)));

            //All dates should be less than today's date one year from now
            Assert.True(timestamps.All(x => x <= DateTimes.GetTimeStamp(DateTime.Now.AddYears(1))));

            //All dates should not be the same
            Assert.False(timestamps.All(x => x == timestamps[0]));
        }

        [Fact(DisplayName = "Can we generate random timestamps?")]
        public void Can_Create_TimeStamps_Within_Short_Range()
        {
            var timestamps = new List<long>();
            for (var i = 0; i < 1000; i++)
            {
                timestamps.Add(DateTimes.GetTimeStamp(DateTime.Now, DateTime.Now.AddHours(2)));
            }

            //Should not have any dates below the current date
            Assert.DoesNotContain(timestamps, x => x < DateTimes.GetTimeStamp(DateTime.Now));

            //All dates should be greater than the current date
            Assert.True(timestamps.All(x => x >= DateTimes.GetTimeStamp(DateTime.Now)));

            //All dates should be less than today's date one year from now
            Assert.True(timestamps.All(x => x <= DateTimes.GetTimeStamp(DateTime.Now.AddHours(2))));

            //All dates should not be the same
            Assert.False(timestamps.All(x => x == timestamps[0]));
        }
    }
}
