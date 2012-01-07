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

        #endregion
    }
}
