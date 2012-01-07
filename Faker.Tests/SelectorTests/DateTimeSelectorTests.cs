using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        #endregion
    }
}
