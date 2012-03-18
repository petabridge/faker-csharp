using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Faker.Tests.MatcherTests
{
    [TestFixture(Description = "Tests for validating that our matcher is able to ")]
    public class ListMatcherTests
    {
        private Matcher _matcher;

        #region Primitive list test classes

        public class SimpleListClass
        {
            public List<string> strings { get; set; }
            public List<DateTime> dates { get; set; }
        }

        public class PocoListsClass
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

        [Test(Description = "Should be able to inject and inject values for lists of primitive elements, even with the lists currently set to null")]
        public void Should_Bind_Primitive_Lists_With_Uninstantiated_Target_Objects()
        {
            var simpleListClass = new SimpleListClass();

            _matcher.Match(simpleListClass);

            Assert.IsNotNull(simpleListClass.dates);
            Assert.IsNotNull(simpleListClass.strings);
            Assert.IsTrue(simpleListClass.dates.Count > 0);
            Assert.IsTrue(simpleListClass.strings.Count > 0);
            Assert.IsTrue(simpleListClass.dates.All(x => x != default(DateTime)));
            Assert.IsTrue(simpleListClass.strings.All(x => x != default(string)));
        }

        [Test(Description = "Should be able to inject and inject values for lists of primitive elements, even with the lists currently set to null")]
        public void Should_Bind_Primitive_Lists_With_Instantiated_Target_Objects()
        {
            var simpleListClass = new SimpleListClass {dates = new List<DateTime>(), strings = new List<string>()};

            _matcher.Match(simpleListClass);

            Assert.IsNotNull(simpleListClass.dates);
            Assert.IsNotNull(simpleListClass.strings);
            Assert.IsTrue(simpleListClass.dates.Count > 0);
            Assert.IsTrue(simpleListClass.strings.Count > 0);
            Assert.IsTrue(simpleListClass.dates.All(x => x != default(DateTime)));
            Assert.IsTrue(simpleListClass.strings.All(x => x != default(string)));
        }

        #endregion
    }
}
