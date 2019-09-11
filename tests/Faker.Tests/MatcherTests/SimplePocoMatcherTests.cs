using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Faker.Selectors;
using Xunit;

namespace Faker.Tests.MatcherTests
{
    public class SimplePocoMatcherTests
    {
        public const string ValidEmailRegex = @"(\b[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}\b)";

        public Regex _valid_email_regex = new Regex(ValidEmailRegex, RegexOptions.IgnoreCase);

        private Matcher _matcher = new Matcher();

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


        #region Tests

        [Fact(DisplayName = "Should bind the default (basetype) values to all of the properties on our DefaultValueTestClass")]
        public void Should_Bind_All_Properties_with_Default_Values()
        {
            //Create a new instance of our test class
            var testInstance = new DefaultValueTestClass();

            //Match all of the properties of the test instance...
            _matcher.Match(testInstance);

            //Test to see that proper values have been assigned to the DateTime properties
            Assert.NotEqual(testInstance.DateTime1, default(DateTime));
            Assert.NotEqual(testInstance.DateTime2, default(DateTime));

            //Test to see that the proper values have been assigned to the float properties
            Assert.NotEqual(testInstance.TestFloat, default(float));
            Assert.NotEqual(testInstance.TestFloat2, default(float));

            //Test to see that proper values have been assigned to the integer properties
            Assert.NotEqual(testInstance.TestInt, default(int));

            //Test to see that proper values have been assigned to the long properties
            Assert.NotEqual(testInstance.TestLong, default(long));

            //Test to see that proper values have been assigned to the Guid properties
            Assert.NotEqual(testInstance.TestGuid, default(Guid));

            //Test to see that proper values have been assigned ot the string properties
            Assert.NotNull(testInstance.RandomString);
        }

        [Fact(DisplayName = "For types without matching selectors, we should simply skip those.")]
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
            Assert.NotEqual(testInstance.DateTime1, default(DateTime));
            Assert.NotEqual(testInstance.DateTime2, default(DateTime));

            //Test to see that the proper values have been assigned to the float properties
            Assert.NotEqual(testInstance.TestFloat, default(float));
            Assert.NotEqual(testInstance.TestFloat, float.PositiveInfinity);
            Assert.NotEqual(testInstance.TestFloat, float.NegativeInfinity);
            Assert.NotEqual(testInstance.TestFloat2, default(float));
            Assert.NotEqual(testInstance.TestFloat2, float.PositiveInfinity);
            Assert.NotEqual(testInstance.TestFloat2, float.NegativeInfinity);

            /* ASSERT THAT THE PROPERTIES THAT DON'T HAVE ANY SELECTORS ARE NOT SET */

            Assert.Equal(testInstance.TestInt, default(int));
            Assert.Equal(testInstance.TestLong, default(long));
            Assert.Equal(testInstance.TestGuid, default(Guid));
            Assert.Null(testInstance.RandomString);
        }

        [Fact(DisplayName = "Should bind and populate special fields on our test class")]
        public void Should_Populate_Special_Fields()
        {
            //Create a new instance of our test class
            var testInstance = new SpecialFieldsTestClass();

            //Match all of the fields on our test instance
            _matcher.Match(testInstance);

            /* Assert to see that we have populated all of the fields on our test instance */
            Assert.NotEqual(testInstance.UserID, default(int));
            Assert.NotEqual(testInstance.Timestamp, default(long));
            Assert.NotEqual(testInstance.DateRegistered, default(DateTime));

            Assert.NotNull(testInstance.Name);
            Assert.NotNull(testInstance.Email);
            Assert.True(_valid_email_regex.IsMatch(testInstance.Email));
        }

        #endregion
    }
}
