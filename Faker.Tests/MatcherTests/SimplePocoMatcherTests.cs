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

            //Test to see that proper values have been assigned to the DateTime properties
            Assert.AreNotEqual(testInstance.DateTime1, default(DateTime));
            Assert.AreNotEqual(testInstance.DateTime2, default(DateTime));

            //Test to see that the proper values have been assigned to the float properties
            Assert.AreNotEqual(testInstance.TestFloat, default(float));
            Assert.AreNotEqual(testInstance.TestFloat2, default(float));

            //Test to see that proper values have been assigned to the integer properties
            Assert.AreNotEqual(testInstance.TestInt, default(int));

            //Test to see that proper values have been assigned to the long properties
            Assert.AreNotEqual(testInstance.TestLong, default(long));

            //Test to see that proper values have been assigned to the Guid properties
            Assert.AreNotEqual(testInstance.TestGuid, default(Guid));

            //Test to see that proper values have been assigned ot the string properties
            Assert.IsNotNullOrEmpty(testInstance.RandomString);
        }

        #endregion
    }
}
