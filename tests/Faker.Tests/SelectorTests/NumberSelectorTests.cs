using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Faker.Selectors;
using NUnit.Framework;

namespace Faker.Tests.SelectorTests
{
    [TestFixture(Description = "Test fixture for validating that all of our type selectors for numbers behave as expected")]
    public class NumberSelectorTests
    {
        #region Test classes for our number selector tests

        public class DecimalTestClass
        {
            public decimal Decimal1 { get; set; }
            public decimal Decimal2 { get; set; }
        }

        #endregion

        #region Tests

        [Fact(DisplayName = "Should be able to bind and inject all decimals")]
        public void Should_Match_And_Inject_Demicals()
        {
            //Create an instance of our test class
            var testInstance = new DecimalTestClass();

            //Create an instance of our DecimalSelector
            var selector = new DecimalSelector(); 

            //Iterate over the test object's properties
            foreach(var property in testInstance.GetType().GetProperties())
            {
                Assert.True(selector.CanBind(property),
                              string.Format("should have been able to bind to property {0}", property.Name));

                //Inject the value into this property on our test instance class
                selector.Generate(testInstance, property);

                //Get the value out of the property
                var fieldValue = (decimal)property.GetValue(testInstance, null);
                Assert.NotNull(fieldValue);
                Assert.NotEqual(fieldValue, default(decimal));
            }
        }

        #endregion
    }
}
