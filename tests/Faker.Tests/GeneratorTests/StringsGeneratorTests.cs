using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Faker.Generators;
using NUnit.Framework;

namespace Faker.Tests.GeneratorTests
{
    [TestFixture(Description = "Tests to see if our string generators behave as expected")]
    public class StringsGeneratorTests
    {
        [Test(Description = "Can we generate strings of fixed length reliably?")]
        public void Can_Generate_String_Of_Fixed_Length()
        {
            var sampleString = Strings.GenerateAlphaNumericString(20, 20);

            Assert.IsNotNullOrEmpty(sampleString);
            Assert.IsTrue(sampleString.Length == 20);
        }

        [Test(Description = "Can we generate a massive string in a short period of time (1 second)?"), Timeout(1000)]
        public void Can_Generate_Massive_String()
        {
            var sampleString = Strings.GenerateAlphaNumericString(40000, 40000); //Create a 40000 character string
            Assert.IsNotNullOrEmpty(sampleString);
            Assert.IsTrue(sampleString.Length == 40000);
        }
    }
}
