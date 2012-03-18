using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace Faker.Tests.MatcherTests
{
    [TestFixture(Description = "Tests for validating that our matcher is able to ")]
    public class ListMatcherTests
    {
        public const string ValidEmailRegex = @"(\b[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}\b)";

        public Regex _valid_email_regex = new Regex(ValidEmailRegex, RegexOptions.IgnoreCase);

        private Matcher _matcher;

        #region List test classes

        public class SpecialFieldsTestClass
        {
            public int UserID { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public long Timestamp { get; set; }
            public DateTime DateRegistered { get; set; }
        }

        public class SimpleListClass
        {
            public List<string> strings { get; set; }
            public List<DateTime> dates { get; set; }
        }

        public class PocoListsClass
        {
            public List<SpecialFieldsTestClass> Users { get; set; }
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

        [Test(Description = "Should be able to inject and map values for lists that contain other POCO objects")]
        public void Should_Bind_Lists_With_Poco_Objects()
        {
            var richListClass = new PocoListsClass();

            _matcher.Match(richListClass);

            Assert.IsNotNull(richListClass.Users);
            Assert.IsTrue(richListClass.Users.Count > 0);

            foreach(var user in richListClass.Users)
            {
                /* Assert to see that we have populated all of the fields on our test instance */
                Assert.AreNotEqual(user.UserID, default(int));
                Assert.AreNotEqual(user.Timestamp, default(long));
                Assert.AreNotEqual(user.DateRegistered, default(DateTime));

                Assert.IsNotNullOrEmpty(user.Name);
                Assert.IsNotNullOrEmpty(user.Email);
                Assert.IsTrue(_valid_email_regex.IsMatch(user.Email));
            }
        }

        #endregion
    }
}
