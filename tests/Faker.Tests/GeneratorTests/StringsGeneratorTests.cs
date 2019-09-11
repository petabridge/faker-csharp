using Faker.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Faker.Tests.GeneratorTests
{
    public class StringsGeneratorTests
    {
        [Fact(DisplayName = "Can we generate strings of fixed length reliably?")]
        public void Can_Generate_String_Of_Fixed_Length()
        {
            var sampleString = Strings.GenerateAlphaNumericString(20, 20);

            Assert.NotNull(sampleString);
            Assert.True(sampleString.Length == 20);
        }

        [Fact(DisplayName = "Can we generate a massive string in a short period of time (1 second)?", Timeout = 1000)]
        public void Can_Generate_Massive_String()
        {
            var sampleString = Strings.GenerateAlphaNumericString(40000, 40000); //Create a 40000 character string
            Assert.NotNull(sampleString);
            Assert.True(sampleString.Length == 40000);
        }
    }
}
