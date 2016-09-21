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
    public class EmailAddressGeneratorTests
    {
        public const string ValidEmailRegex = @"(\b[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}\b)";
        public const string MajorDomainRegex = @"(.com)|(.net)|(.org)|(.edu)";

        public Regex _valid_email_regex = new Regex(ValidEmailRegex, RegexOptions.IgnoreCase);
        public Regex _major_domain_regex = new Regex(MajorDomainRegex);

        [Test(Description = "Can we generate valid emails using our fully-randomized method?")]
        public void Can_Generate_Valid_Random_Emails()
        {
            for(var i = 0; i < 200; i++)
            {
                var email = EmailAddresses.Generate();

                Assert.IsTrue(_valid_email_regex.IsMatch(email), "Expected a valid email address");
            }
            
        }

        [Test(Description = "Can we generate valid emails using our 'humanized' method?")]
        public void Can_Generate_Valid_Human_Emails()
        {
            for (var i = 0; i < 200; i++)
            {
                var email = EmailAddresses.Human();

                Assert.IsTrue(_valid_email_regex.IsMatch(email), "Expected a valid email address");
            }
        }

        [Test(Description = "We should only include major domain extensions (.com, .net, .org, and .edu) for randomly generated addresses when the flag is set correctly.")]
        public void Should_Include_Only_Major_Domain_Extensions_When_Flag_Is_Set_For_Random_Addresses()
        {
            var email = EmailAddresses.Generate(true);

            var extensionPos = email.LastIndexOf('.');
            var extension = email.Substring(extensionPos, email.Length - extensionPos);

            Assert.IsTrue(_major_domain_regex.IsMatch(extension));
        }

        [Test(Description = "We should only include major domain extensions (.com, .net, .org, and .edu) for 'humanized' addresses when the flag is set correctly.")]
        public void Should_Include_Only_Major_Domain_Extensions_When_Flag_Is_Set_For_Human_Addresses()
        {
            var email = EmailAddresses.Human(true);

            var extensionPos = email.LastIndexOf('.');
            var extension = email.Substring(extensionPos, email.Length - extensionPos);

            Assert.IsTrue(_major_domain_regex.IsMatch(extension));
        }

        [Test(Description = "We should only generate email addresses within the specified length")]
        public void Should_Generate_Emails_Within_Range()
        {
            for (var i = 0; i < 200; i++)
            {
                var email = EmailAddresses.Generate(maxLength:100, minLength:25);

                Assert.IsTrue(email.Length >= 25);
                Assert.IsTrue(email.Length <= 100);
            }
        }
    }
}
