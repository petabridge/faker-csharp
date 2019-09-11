using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Faker.Generators;
using Xunit;

namespace Faker.Tests.GeneratorTests
{
    public class NameGeneratorTests
    {
        [Fact(DisplayName = "Simple test to verify that we can extract a first name without error")]
        public void Can_Get_First_Name()
        {
            var firstName = Names.First();
            Assert.NotNull(firstName);
        }

        [Fact(DisplayName = "Simple test to verify that we can extract a last name without error")]
        public void Can_Get_Last_Name()
        {
            var lastName = Names.Last();
            Assert.NotNull(lastName);
        }
        
        [Fact(DisplayName = "Test to verify that we can get a lexically correct full name")]
        public void Can_Get_Full_Name()
        {
            var fullName = Names.FullName();
            Assert.NotNull(fullName);

            //Should be able to break a full name apart into two components separated by a space
            Assert.True(fullName.Split(' ').Count() == 2);
        }

        [Fact(DisplayName = "Ensures that we have some variety in our naming conventions")]
        public void Names_Have_Variety()
        {
            var names = new List<string>();

            for(var i = 0;i < 100; i++)
            {
                names.Add(Names.FullName());
            }

            //Make sure not all of the names are equal (there is SOME variety)
            Assert.False(names.All(x => x.Equals(names[0])));
        }
    }
}
