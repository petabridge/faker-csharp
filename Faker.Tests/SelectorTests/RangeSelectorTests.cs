using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Faker.Extensions;
using Faker.Selectors;
using NUnit.Framework;

namespace Faker.Tests.SelectorTests
{
    [TestFixture(Description = "Used to determine if Faker's range extension methods and custom setters work as expected")]
    public class RangeSelectorTests
    {
        #region Custom Test Classes

        public class IntegerTestClass
        {
            public int Int1 { get; set; }
            public int Int2 { get; set; }
        }

        public class DateTimeTestClass
        {
            public DateTime Date1 { get; set; }
            public DateTime Date2 { get; set; }
            public DateTime Date3 { get; set; }
        }

        public class SimpleStringTestClass
        {
            public string String1 { get; set; }
            public string String2 { get; set; }
        }

        #endregion

        #region Setup / Teardown
        #endregion

        #region Tests


        [Test(Description = "Tests to see if our functions for IRangeSelectors work for simple numerical types as expected")]
        public void Should_Use_Custom_Boundaries_for_Integer_Value_Injection()
        {
            //Create an instance of our test class
            var testInstance = new IntegerTestClass();

            var intMax = 10;
            var intMin = 1;

            //Create an instance of our IntSelector and set some custom bounds
            var selector =
                new IntSelector().SetMax(() => Faker.Generators.Numbers.Int(intMax, intMax))
                                 .SetMin(() => Faker.Generators.Numbers.Int(intMin, intMin));

            //Iterate over the test object's properties
            foreach (var property in testInstance.GetType().GetProperties())
            {
                Assert.IsTrue(selector.CanBind(property),
                              string.Format("should have been able to bind to property {0}", property.Name));

                //Inject the value into this property on our test instance class
                selector.Generate(testInstance, property);

                //Get the value out of the property
                var fieldValue = (int)property.GetValue(testInstance, null);
                Assert.IsNotNull(fieldValue);
                Assert.AreNotEqual(fieldValue, default(int));
                Assert.IsTrue(fieldValue <= intMax && fieldValue >= intMin, "Custom range should have worked");
            }
        }

        [Test(Description = "Tests to see if our functions for IRangeSelectors work for DateTime types as expected")]
        public void Should_Use_Custom_Boundaries_for_DateTime_Value_Injection()
        {
            //Create an instance of our test class
            var testInstance = new DateTimeTestClass();

            var dateTimeMax = DateTime.UtcNow.AddYears(1);
            var dateTimeMin = DateTime.UtcNow.AddYears(-1);

            //Create an instance of our IntSelector and set some custom bounds
            var selector =
                new DateTimeSelector().SetMax(() => Faker.Generators.DateTimes.GetDateTime(dateTimeMax, dateTimeMax))
                                 .SetMin(() => Faker.Generators.DateTimes.GetDateTime(dateTimeMin, dateTimeMin));

            //Iterate over the test object's properties
            foreach (var property in testInstance.GetType().GetProperties())
            {
                Assert.IsTrue(selector.CanBind(property),
                              string.Format("should have been able to bind to property {0}", property.Name));

                //Inject the value into this property on our test instance class
                selector.Generate(testInstance, property);

                //Get the value out of the property
                var fieldValue = (DateTime)property.GetValue(testInstance, null);
                Assert.IsNotNull(fieldValue);
                Assert.AreNotEqual(fieldValue, default(DateTime));
                Assert.IsTrue(fieldValue <= dateTimeMax && fieldValue >= dateTimeMin, "Custom range should have worked");
            }
        }

        [Test(Description = "Tests to see if our functions for IRangeSelectors work for the lengths of Strings as expected")]
        public void Should_Use_Custom_StringLength_Boundaries_for_String_Value_Injection()
        {
            //Create an instance of our test class
            var testInstance = new SimpleStringTestClass();

            var stringLengthMax = 30;
            var stringLengthMin = 10;

            //Create an instance of our IntSelector and set some custom bounds
            var selector =
                new StringSelector().SetMax(() => stringLengthMax)
                                 .SetMin(() => stringLengthMin);

            //Iterate over the test object's properties
            foreach (var property in testInstance.GetType().GetProperties())
            {
                Assert.IsTrue(selector.CanBind(property),
                              string.Format("should have been able to bind to property {0}", property.Name));

                //Inject the value into this property on our test instance class
                selector.Generate(testInstance, property);

                //Get the value out of the property
                var fieldValue = (string)property.GetValue(testInstance, null);
                Assert.IsNotNullOrEmpty(fieldValue);
                Assert.IsTrue(fieldValue.Length <= stringLengthMax && fieldValue.Length >= stringLengthMin, "Custom range should have worked");
            }
        }

        #endregion
    }
}
