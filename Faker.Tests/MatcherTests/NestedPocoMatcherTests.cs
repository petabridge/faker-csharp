using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace Faker.Tests.MatcherTests
{
    [TestFixture(Description = "Tests to ensure that Matcher can work with nested POCO classes")]
    public class NestedPocoMatcherTests
    {
        public const string ValidEmailRegex = @"(\b[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}\b)";

        public Regex _valid_email_regex = new Regex(ValidEmailRegex, RegexOptions.IgnoreCase);

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
            public string SampleString { get; set; }
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

            //Assert that all of the fields on the sub-class have been injected and instantiated
            Assert.IsNotNull(testInstance.SpecialClass);
            Assert.AreNotEqual(testInstance.SpecialClass.DateRegistered, default(DateTime));
            Assert.AreNotEqual(testInstance.SpecialClass.UserID, default(int));
            Assert.AreNotEqual(testInstance.SpecialClass.Timestamp, default(long));
            Assert.IsNotNullOrEmpty(testInstance.SpecialClass.Name);
            Assert.IsNotNullOrEmpty(testInstance.SpecialClass.Email);
            Assert.IsTrue(_valid_email_regex.IsMatch(testInstance.SpecialClass.Email));

            //Assert that all of the fields on the main class have been injected and instantiated
            Assert.AreNotEqual(testInstance.Double1, default(double));
            Assert.AreNotEqual(testInstance.Float1, default(float));
            Assert.AreNotEqual(testInstance.Float2, default(float));
            Assert.AreNotEqual(testInstance.Long1, default(long));
            Assert.AreNotEqual(testInstance.Guid1, default(Guid));
            Assert.IsNotNullOrEmpty(testInstance.SampleString);
        }

        #endregion
    }
}
