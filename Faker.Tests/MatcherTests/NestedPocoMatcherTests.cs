using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Faker.Tests.MatcherTests
{
    [TestFixture(Description = "Tests to ensure that Matcher can work with nested POCO classes")]
    public class NestedPocoMatcherTests
    {
        private Matcher _matcher;

        #region Nested POCO Test Classes...

        public class SpecialFieldsTestClass
        {
            public int UserID { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public long Timestamp { get; set; }
            public DateTime DateRegistered { get; set; }
        }

        public class ComplexPocoTestClass
        {
            public float Float1 { get; set; }
            public float Float2 { get; set; }
            public long Long1 { get; set; }
            public double Double1 { get; set; }
            public Guid Guid1 { get; set; }
            public SpecialFieldsTestClass SpecialClass { get; set; }
        }

        public class NestedPocoTestClass
        {
            
        }

        #endregion

        #region Setup / Teardown

        [SetUp]
        public void SetUp()
        {
            _matcher = new Matcher();
        }
        #endregion

        #region Tests

        [Test(Description = "Matcher should inject the values of a subclass in addition to those of the parent class")]
        public void Should_Populate_Fields_Of_SubClass()
        {
            //Create a new instance of our test class
            var testInstance = new ComplexPocoTestClass();

            //Match the fields...
            _matcher.Match(testInstance);

            /* ASSERTIONS */

            //Assert that the subclass has been instantiated at least
            Assert.IsNotNull(testInstance.SpecialClass);
        }

        #endregion
    }
}
