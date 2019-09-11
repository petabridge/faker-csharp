using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Faker.Tests.MatcherTests
{
    public class StructMatcherTests
    {
        private Matcher _matcher = new Matcher();

        #region Custom structs

        public struct TestStruct
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public DateTime When { get; set; }
        }

        #endregion

        #region Tests

        [Fact(DisplayName = "Matcher should be able to match simple DateTime structs if needed")]
        public void Should_Bind_DateTime()
        {
            var dateTimeTest = new DateTime();

            _matcher.MatchStruct<DateTime>(ref dateTimeTest);

            /* Assert that we populated all of the fields of the DateTime object */
            Assert.NotEqual(DateTime.MinValue, dateTimeTest);
            Assert.NotEqual(DateTime.MaxValue, dateTimeTest);
        }

        [Fact(DisplayName = "Matcher should be able to match user-defined structs if needed")]
        public void Should_Bind_CustomStruct()
        {
            var dateTimeTest = new TestStruct();

            _matcher.MatchStruct<TestStruct>(ref dateTimeTest);

            /* Assert that we populated all of the fields of the DateTime object */
           Assert.NotNull(((TestStruct)dateTimeTest).Name);
        }

        #endregion
    }
}
