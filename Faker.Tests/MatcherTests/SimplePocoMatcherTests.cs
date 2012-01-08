using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Faker.Tests.MatcherTests
{
    [TestFixture(Description = "The matcher should be able to process simple POCO classes that don't have any arrays, IEnumerables, or or nested classes")]
    public class SimplePocoMatcherTests
    {
        private Matcher matcher;

        #region Simple POCO test classes...

        public class DefaultValueTestClass
        {
            public int TestInt { get; set; }
            public decimal TestDecimal { get; set; }
            public Guid TestGuid { get; set; }
            public DateTime DateTime1 { get; set; }
            public DateTime DateTime2 { get; set; }
            public string RandomString { get; set; }
            public float TestFloat { get; set; }
            public float TestFloat2 { get; set; }
            public long TestLong { get; set; }
        }

        #endregion

        #region Setup / Teardown

        [SetUp]
        public void SetUp()
        {
            matcher = new Matcher();
        }

        #endregion

        #region Tests

        [Test(Description = "Should bind the default (basetype) values to all of the properties on our DefaultValueTestClass")]
        public void Should_Bind_All_Properties_with_Default_Values()
        {
            //Create a new instance of our test class
            var testInstance = new DefaultValueTestClass();

            //Match all of the properties of the test instance...
            matcher.Match(testInstance);

            var properties = testInstance.GetType().GetProperties();

            //Iterate over the properties of the test class and ensure that each instance is populated
            foreach(var property in properties)
            {
                //Get the value of the property
                var fieldValue = property.GetValue(property, null);
                Assert.IsNotNull(fieldValue);
            }
        }

        #endregion
    }
}
