using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Faker.Selectors;
using NUnit.Framework;

namespace Faker.Tests.SelectorTests
{
    [TestFixture(Description = "Tests to ensure that our typeselectors for datetimes work as expected")]
    public class DateTimeSelectorTests
    {
        #region Test classes for use against our DateTime and timestamp selectors...

        private class DateTimeTestClass
        {
            public DateTime dateTime1 { get; set; }
            public DateTime dateTime2 { get; set; }
            public DateTime asdfsadfsadfsadfdsaf { get; set; }
        }

        private class DateTimeOffsetTestClass
        {
            public DateTimeOffset offSet1 { get; set; }
            public DateTimeOffset offSet2 { get; set; }
        }

        private class TimeStampTestClass
        {
            public long TimeStamp { get; set; }
            public long timeStamp { get; set; }
            public long Timestamp { get; set; }
            public long TIMESTAMP { get; set; }
            public long time_stamp { get; set; }
            public long Time_Stamp { get; set; }
            public long Time_stamp { get; set; }
            public long time_Stamp { get; set; }
        }

        #endregion

        #region Tests

        [Test(Description = "Tests to see if all of our field values are properly injected...")]
        public void DateTime_Selector_Injects_All_DateTime_Values()
        {
            var dateTimeSelector = new DateTimeSelector();
            var dateTimeTestClass = new DateTimeTestClass();

            //Iterate over all of the properties in the fullNameClass object...
            foreach (var property in dateTimeTestClass.GetType().GetProperties())
            {
                //Inject the value into the property
                dateTimeSelector.Generate(dateTimeTestClass, property);
            }

            //Iterate over all of the properties again
            foreach (var property in dateTimeTestClass.GetType().GetProperties())
            {
                var fieldValue = (DateTime)property.GetValue(dateTimeTestClass, null);

                Assert.IsAssignableFrom<DateTime>(fieldValue, "Should be type of DateTime...");
                Assert.AreNotEqual(fieldValue, default(DateTime));
            }
        }

        [Test(Description = "Tests to see if all of our field values are properly injected...")]
        public void DateTimeOffset_Selector_Injects_All_DateTime_Values()
        {
            var timeSelector = new DateTimeOffsetSelector();
            var dateTimeTestClass = new DateTimeOffsetTestClass();

            //Iterate over all of the properties in the fullNameClass object...
            foreach (var property in dateTimeTestClass.GetType().GetProperties())
            {
                //Inject the value into the property
                timeSelector.Generate(dateTimeTestClass, property);
            }

            //Iterate over all of the properties again
            foreach (var property in dateTimeTestClass.GetType().GetProperties())
            {
                var fieldValue = (DateTimeOffset)property.GetValue(dateTimeTestClass, null);

                Assert.IsAssignableFrom<DateTimeOffset>(fieldValue, "Should be type of DateTime...");
                Assert.AreNotEqual(fieldValue, default(DateTimeOffset));
            }
        }

        [Test(Description = "Does our timestamp selector match fields with valid names?")]
        public void TimeStamp_Selector_Matches_Valid_Names()
        {
            var timeStampSelector = new TimeStampSelector();
            var timeStampTestClass = new TimeStampTestClass();

            //Iterate over all of the properties in the EmailTestClass object...
            foreach (var property in timeStampTestClass.GetType().GetProperties())
            {
                var canBind = timeStampSelector.CanBind(property);
                Assert.IsTrue(canBind, string.Format("{0} should have been a valid match", property.Name));
            }
        }

        [Test(Description = "Tests to see if all of our field values are properly injected...")]
        public void TimeStamp_Selector_Injects_All_TimeStamp_Values()
        {
            var timeStampSelector = new TimeStampSelector();
            var timeStampTestClass = new TimeStampTestClass();

            //Iterate over all of the properties in the fullNameClass object...
            foreach (var property in timeStampTestClass.GetType().GetProperties())
            {
                //Inject the value into the property
                timeStampSelector.Generate(timeStampTestClass, property);
            }

            //Iterate over all of the properties again
            foreach (var property in timeStampTestClass.GetType().GetProperties())
            {
                var fieldValue = (long)property.GetValue(timeStampTestClass, null);

                Assert.IsAssignableFrom<long>(fieldValue, "Should be type of long...");
                Assert.AreNotEqual(fieldValue, default(long));
            }
        }

        [Test(Description = "Must be able to convert a TimeStampSelector into a nullable type")]
        public void TimeStamp_Select_Must_be_Nullable()
        {
            var timeStampSelector = new TimeStampSelector().Nullable(); // 10% certainty
            var instances = Enumerable.Range(0, 100).Select(x => (long?) timeStampSelector.GenerateInstance()).ToList();
            Assert.True(instances.Any(x => x == null));
            Assert.True(instances.Any(x => x != null));
        }

        #endregion
    }
}
