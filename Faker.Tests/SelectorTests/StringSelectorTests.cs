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
            public string name { get; set; }
            public string Name { get; set; }
            public string NAME { get; set; }
        }

        private class FirstNameTestClass
        {
            public string firstname { get; set; }
            public string firstName { get; set; }
            public string Firstname { get; set; }
            public string FirstName { get; set; }
            public string First_name { get; set; }
            public string First_Name { get; set; }
            public string first_name { get; set; }
            public string first_Name { get; set; }
        }

        private class LastNameTestClass
        {
            public string lastname { get; set; }
            public string lastName { get; set; }
            public string Lastname { get; set; }
            public string LastName { get; set; }
            public string Last_name { get; set; }
            public string Last_Name { get; set; }
            public string last_name { get; set; }
            public string last_Name { get; set; }
        }

        private class EmailTestClass
        {
            public string emailaddress { get; set; }
            public string email_address { get; set; }
            public string Email_address { get; set; }
            public string email_Address { get; set; }
            public string emailAddress { get; set; }
            public string EmailAddress { get; set; }
            public string Emailaddress { get; set; }
            public string email { get; set; }
            public string Email { get; set; }
            public string EMAIL { get; set; }
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

        [Test(Description = "Tests to see if our regex can match all of the variations of the FullName field")]
        public void First_Name_Variations_All_Match()
        {
            var nameSelector = new FirstNameSelector();
            var firstNameTestClass = new FirstNameTestClass();

            //Iterate over all of the properties in the fullNameClass object...
            foreach (var property in firstNameTestClass.GetType().GetProperties())
            {
                var nameSelectorResult = nameSelector.CanBind(property);
                Assert.IsTrue(nameSelectorResult, string.Format("{0} should have been a valid match", property.Name));
            }
        }

        [Test(Description = "Tests to see if all of our field values are properly injected...")]
        public void First_Name_Variations_All_Injected()
        {
            var nameSelector = new FirstNameSelector();
            var firstNameTestClass = new FirstNameTestClass();

            //Iterate over all of the properties in the fullNameClass object...
            foreach (var property in firstNameTestClass.GetType().GetProperties())
            {
                //Inject the value into the property
                nameSelector.Generate(firstNameTestClass, property);
            }

            //Iterate over all of the properties again
            foreach (var property in firstNameTestClass.GetType().GetProperties())
            {
                var fieldValue = property.GetValue(firstNameTestClass, null) as string;

                Assert.IsNotNullOrEmpty(fieldValue);
                Assert.IsAssignableFrom<string>(fieldValue, "Should be type of string...");
                Assert.That(fieldValue.Length > 0);
            }
        }

        [Test(Description = "Tests to see if our regex can match all of the variations of the FullName field")]
        public void Last_Name_Variations_All_Match()
        {
            var nameSelector = new LastNameSelector();
            var lastNameTestClass = new LastNameTestClass();

            //Iterate over all of the properties in the fullNameClass object...
            foreach (var property in lastNameTestClass.GetType().GetProperties())
            {
                var nameSelectorResult = nameSelector.CanBind(property);
                Assert.IsTrue(nameSelectorResult, string.Format("{0} should have been a valid match", property.Name));
            }
        }

        [Test(Description = "Tests to see if all of our field values are properly injected...")]
        public void Last_Name_Variations_All_Injected()
        {
            var nameSelector = new LastNameSelector();
            var lastNameTestClass = new LastNameTestClass();

            //Iterate over all of the properties in the fullNameClass object...
            foreach (var property in lastNameTestClass.GetType().GetProperties())
            {
                //Inject the value into the property
                nameSelector.Generate(lastNameTestClass, property);
            }

            //Iterate over all of the properties again
            foreach (var property in lastNameTestClass.GetType().GetProperties())
            {
                var fieldValue = property.GetValue(lastNameTestClass, null) as string;

                Assert.IsNotNullOrEmpty(fieldValue);
                Assert.IsAssignableFrom<string>(fieldValue, "Should be type of string...");
                Assert.That(fieldValue.Length > 0);
            }
        }

        [Test(Description = "Tests to see if our regex can match all of the variations of the EmailAddress field")]
        public void Email_Address_Variations_All_Match()
        {
            var emailSelector = new EmailSelector();
            var emailTestClass = new EmailTestClass();

            //Iterate over all of the properties in the EmailTestClass object...
            foreach (var property in emailTestClass.GetType().GetProperties())
            {
                var emailSelectorResult = emailSelector.CanBind(property);
                Assert.IsTrue(emailSelectorResult, string.Format("{0} should have been a valid match", property.Name));
            }
        }

        [Test(Description = "Tests to see if all of our field values are properly injected...")]
        public void Email_Address_Variations_All_Injected()
        {
            var emailSelector = new EmailSelector();
            var emailTestClass = new EmailTestClass();

            //Iterate over all of the properties in the fullNameClass object...
            foreach (var property in emailTestClass.GetType().GetProperties())
            {
                //Inject the value into the property
                emailSelector.Generate(emailTestClass, property);
            }

            //Iterate over all of the properties again
            foreach (var property in emailTestClass.GetType().GetProperties())
            {
                var fieldValue = property.GetValue(emailTestClass, null) as string;

                Assert.IsNotNullOrEmpty(fieldValue);
                Assert.IsAssignableFrom<string>(fieldValue, "Should be type of string...");
                Assert.That(fieldValue.Length > 0);
            }
        }
    }
}
