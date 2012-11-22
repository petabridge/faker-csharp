using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Faker.Tests.MatcherTests
{
    [TestFixture(Description = "Tests to ensure that Faker can create new Structs as the base type")]
    public class StructMatcherTests
    {
        private Matcher _matcher;

        #region Setup / Teardown

        [SetUp]
        public void SetUp()
        {
            _matcher = new Matcher();
        }

        #endregion

        #region Tests

        [Test(Description = "Matcher should be able to match simple DateTime structs if needed")]
        public void Should_Bind_DateTime()
        {
            var dateTimeTest = new DateTime();

            _matcher.Match(dateTimeTest);

            /* Assert that we populated all of the fields of the DateTime object */
            Assert.AreNotEqual(DateTime.MinValue, dateTimeTest);
            Assert.AreNotEqual(DateTime.MaxValue, dateTimeTest);
        }

        #endregion
    }
}
