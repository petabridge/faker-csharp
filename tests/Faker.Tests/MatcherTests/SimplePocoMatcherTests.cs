using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Faker.Selectors;
using NUnit.Framework;

namespace Faker.Tests.MatcherTests
{
    [TestFixture(Description = "The matcher should be able to process simple POCO classes that don't have any arrays, IEnumerables, or or nested classes")]
    public class SimplePocoMatcherTests
    {
        public const string ValidEmailRegex = @"(\b[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}\b)";

        public Regex _valid_email_regex = new Regex(ValidEmailRegex, RegexOptions.IgnoreCase);

        private Matcher _matcher;

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

        public class SpecialFieldsTestClass
        {
            public int UserID { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public long Timestamp { get; set; }
            public DateTime DateRegistered { get; set; }
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

        [Test(Description = "Should bind the default (basetype) values to all of the properties on our DefaultValueTestClass")]
        public void Should_Bind_All_Properties_with_Default_Values()
        {
            //Create a new instance of our test class
            var testInstance = new DefaultValueTestClass();

            //Match all of the properties of the test instance...
            _matcher.Match(testInstance);

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

        [Test(Description = "For types without matching selectors, we should simply skip those.")]
        public void Should_Skip_Types_Without_Selectors()
        {
            //Create a new TypTable that doesn't use any of the default types
            var typeTable = new TypeTable(false);

            //Add just the Float and DateTime selectors
            typeTable.AddSelector(new FloatSelector());
            typeTable.AddSelector(new DateTimeSelector());

            //Create a new matcher that users only the type selectors we've specified
            var typlessMatcher = new Matcher(typeTable);

            //Create a new instance of our test class
            var testInstance = new DefaultValueTestClass();

            //Match the properties for which we have selectors
            typlessMatcher.Match(testInstance);

            /* ASSERT THAT THE PROPERTIES FOR WHICH WE HAVE INJECTORS ARE ALL SET */

            //Test to see that proper values have been assigned to the DateTime properties
            Assert.AreNotEqual(testInstance.DateTime1, default(DateTime));
            Assert.AreNotEqual(testInstance.DateTime2, default(DateTime));

            //Test to see that the proper values have been assigned to the float properties
            Assert.AreNotEqual(testInstance.TestFloat, default(float));
            Assert.AreNotEqual(testInstance.TestFloat, float.PositiveInfinity);
            Assert.AreNotEqual(testInstance.TestFloat, float.NegativeInfinity);
            Assert.AreNotEqual(testInstance.TestFloat2, default(float));
            Assert.AreNotEqual(testInstance.TestFloat2, float.PositiveInfinity);
            Assert.AreNotEqual(testInstance.TestFloat2, float.NegativeInfinity);

            /* ASSERT THAT THE PROPERTIES THAT DON'T HAVE ANY SELECTORS ARE NOT SET */

            Assert.AreEqual(testInstance.TestInt, default(int));
            Assert.AreEqual(testInstance.TestLong, default(long));
            Assert.AreEqual(testInstance.TestGuid, default(Guid));
            Assert.IsNullOrEmpty(testInstance.RandomString);
        }

        [Test(Description = "Should bind and populate special fields on our test class")]
        public void Should_Populate_Special_Fields()
        {
            //Create a new instance of our test class
            var testInstance = new SpecialFieldsTestClass();

            //Match all of the fields on our test instance
            _matcher.Match(testInstance);

            /* Assert to see that we have populated all of the fields on our test instance */
            Assert.AreNotEqual(testInstance.UserID, default(int));
            Assert.AreNotEqual(testInstance.Timestamp, default(long));
            Assert.AreNotEqual(testInstance.DateRegistered, default(DateTime));

            Assert.IsNotNullOrEmpty(testInstance.Name);
            Assert.IsNotNullOrEmpty(testInstance.Email);
            Assert.IsTrue(_valid_email_regex.IsMatch(testInstance.Email));
        }

        #endregion
    }
}
