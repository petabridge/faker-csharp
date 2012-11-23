using Faker.Selectors;
using NUnit.Framework;
using Faker.Extensions;

namespace Faker.Tests.SelectorTests
{
    [TestFixture(Description = "Tests for enusring that all of the custom selectors we support work as expected")]
    public class CustomSelectorTests
    {
        #region Test Classes

        public class CustomMember
        {
            public string Name { get; set; }
            public int Measure { get; set; }
        }

        public class ContainerClass
        {
            public CustomMember Member { get; set; }
            public string Name { get; set; }
            public string OtherName { get; set; }
        }

        #endregion

        #region Setup / Teardown
        #endregion

        #region Tests

        [Test(Description = "Should be able to match an entire class property using a custom selector")]
        public void Should_Match_Class_Property_with_CustomSelector()
        {
            //Create an instance of our test class
            var testInstance = new ContainerClass();

            var measureConst = 1;
            var nameConst = "AaronConst";

            
            var fake = new Fake<ContainerClass>();

            //Run some tests before we add the custom selector
            var standardFakeInstance = fake.Generate();

            Assert.AreNotEqual(measureConst, standardFakeInstance.Member.Measure);
            Assert.AreNotEqual(nameConst, standardFakeInstance.Member.Name);

            //Add the custom selector for the Member field
            var selector = fake.SetupSelector(x => x.Member, () => new CustomMember() {Measure = measureConst, Name = nameConst});

            Assert.IsTrue(selector.CanBind(typeof(CustomMember)));

            //Generate a new fake with the custom selector implemented
            var customFakeInstance = fake.Generate();

            Assert.AreEqual(measureConst, customFakeInstance.Member.Measure);
            Assert.AreEqual(nameConst, customFakeInstance.Member.Name);
        }

        [Test(Description = "Should be able to match a simple built-in property using a custom selector")]
        public void Should_Match_BuiltIn_Property_with_CustomSelector()
        {
            //Create an instance of our test class
            var testInstance = new ContainerClass();

            var nameConst = "AaronConst";

            var fake = new Fake<ContainerClass>();

            //Run some tests before we add the custom selector
            var standardFakeInstance = fake.Generate();

            Assert.AreNotEqual(nameConst, standardFakeInstance.Name);

            //Add the custom selector for the Member field
            var selector = fake.SetupSelector(x => x.Name, () => nameConst);

            Assert.IsTrue(selector.CanBind(typeof(string)));

            //Generate a new fake with the custom selector implemented
            var customFakeInstance = fake.Generate();

            Assert.AreEqual(nameConst, customFakeInstance.Name);
            Assert.AreNotEqual(nameConst, customFakeInstance.OtherName);
        }

        #endregion
    }
}
