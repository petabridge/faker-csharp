using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Faker.Generators;
using NUnit.Framework;

namespace Faker.Tests.GeneratorTests
{
    [TestFixture(Description = "Test fixture for ensuring that our generated email addresses work as expected")]
    public class EmailGeneratorTests
    {
        public const string ValidEmailRegex = @"(\b[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}\b)|(\b[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}\b)";

        public Regex _r = new Regex(ValidEmailRegex);

        [Test(Description = "Can we generate valid emails using our fully-randomized method?")]
        public void Can_Generate_Valid_Random_Emails()
        {
            var email = EmailAddresses.Generate();

            Assert.IsTrue(_r.IsMatch(email), "Expected a valid email address");
        }

        [Test(Description = "Can we generate valid emails using our 'humanized' method?")]
        public void Can_Generate_Valid_Human_Emails()
        {
            var email = EmailAddresses.Human();

            Assert.IsTrue(_r.IsMatch(email), "Expected a valid email address");
        }
    }
}
