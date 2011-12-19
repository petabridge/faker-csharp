using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Faker.Selectors;
using NUnit.Framework;

namespace Faker.Tests.SelectorTests
{
    [TestFixture(Description = "Ensures that our string selectors inject valid results")]
    public class StringSelectorTests
    {
        #region Test classes for use against our string selectors...
        private class FullNameTestClass
        {
            public string fullname { get; set; }
            public string fullName { get; set; }
            public string Fullname { get; set; }
            public string FullName { get; set; }
            public string Full_name { get; set; }
            public string Full_Name { get; set; }
            public string full_name { get; set; }
            public string full_Name { get; set; }
        }
        #endregion

        [Test(Description = "Tests to see if our regex can match all of the variations of the FullName field")]
        public void Full_Name_Variations_All_Match()
        {
            var nameSelector = new FullNameSelector();
            var fullNameClass = new FullNameTestClass();

            //Iterate over all of the properties in the fullNameClass object...
            foreach(var property in fullNameClass.GetType().GetProperties())
            {
                var nameSelectorResult = nameSelector.CanBind(property);
                Assert.IsTrue(nameSelectorResult, string.Format("{0} should have been a valid match", property.Name));
            }
        }

        [Test(Description = "Tests to see if all of our field values are properly injected...")]
        public void Full_Name_Variations_All_Injected()
        {
            var nameSelector = new FullNameSelector();
            var fullNameClass = new FullNameTestClass();

            //Iterate over all of the properties in the fullNameClass object...
            foreach (var property in fullNameClass.GetType().GetProperties())
            {
                //Inject the value into the property
                nameSelector.Generate(fullNameClass, property);
            }

            //Iterate over all of the properties again
            foreach(var property in fullNameClass.GetType().GetProperties())
            {
                var fieldValue = property.GetValue(fullNameClass, null) as string;

                Assert.IsNotNullOrEmpty(fieldValue);
                Assert.IsAssignableFrom<string>(fieldValue, "Should be type of string...");
                Assert.That(fieldValue.Length > 0);
            }
        }
    }
}
