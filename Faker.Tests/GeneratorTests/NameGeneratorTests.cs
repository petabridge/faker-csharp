using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Faker.Generators;
using NUnit.Framework;

namespace Faker.Tests.GeneratorTests
{
    [TestFixture(Description = "Used to ensure that our name generation methods work properly")]
    public class NameGeneratorTests
    {
        [Test(Description = "Simple test to verify that we can extract a first name without error")]
        public void Can_Get_First_Name()
        {
            var firstName = Name.First();
            Assert.IsNotNullOrEmpty(firstName);
        }

        [Test(Description = "Simple test to verify that we can extract a last name without error")]
        public void Can_Get_Last_Name()
        {
            var lastName = Name.Last();
            Assert.IsNotNullOrEmpty(lastName);
        }
        
        [Test(Description = "Test to verify that we can get a lexically correct full name")]
        public void Can_Get_Full_Name()
        {
            var fullName = Name.FullName();
            Assert.IsNotNullOrEmpty(fullName);

            //Should be able to break a full name apart into two components separated by a space
            Assert.IsTrue(fullName.Split(' ').Count() == 2);
        }

        [Test(Description = "Ensures that we have some variety in our naming conventions")]
        public void Names_Have_Variety()
        {
            var names = new List<string>();

            for(var i = 0;i < 100; i++)
            {
                names.Add(Name.FullName());
            }

            //Make sure not all of the names are equal (there is SOME variety)
            Assert.IsFalse(names.All(x => x.Equals(names[0])));
        }
    }
}
