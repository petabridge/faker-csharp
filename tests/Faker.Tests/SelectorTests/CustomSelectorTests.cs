using Faker.Selectors;
using Xunit;

namespace Faker.Tests.SelectorTests
{
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

        #region Tests

        [Fact(DisplayName = "Should be able to match an entire class property using a custom selector")]
        public void Should_Match_Class_Property_with_CustomSelector()
        {
            //Create an instance of our test class
            var testInstance = new ContainerClass();

            var measureConst = 1;
            var nameConst = "AaronConst";

            
            var fake = new Fake<ContainerClass>();

            //Run some tests before we add the custom selector
            var standardFakeInstance = fake.Generate();

            Assert.NotEqual(measureConst, standardFakeInstance.Member.Measure);
            Assert.NotEqual(nameConst, standardFakeInstance.Member.Name);

            //Add the custom selector for the Member field
            var selector = fake.SetProperty(x => x.Member, () => new CustomMember() {Measure = measureConst, Name = nameConst});

            //Assert.True(selector.CanBind(typeof(CustomMember)));

            //Generate a new fake with the custom selector implemented
            var customFakeInstance = fake.Generate();

            Assert.Equal(measureConst, customFakeInstance.Member.Measure);
            Assert.Equal(nameConst, customFakeInstance.Member.Name);
        }

        [Fact(DisplayName = "Should be able to match a simple built-in property using a custom selector")]
        public void Should_Match_BuiltIn_Property_with_CustomSelector()
        {
            //Create an instance of our test class
            var testInstance = new ContainerClass();

            var nameConst = "AaronConst";

            var fake = new Fake<ContainerClass>();

            //Run some tests before we add the custom selector
            var standardFakeInstance = fake.Generate();

            Assert.NotEqual(nameConst, standardFakeInstance.Name);

            //Add the custom selector for the Member field
            var selector = fake.SetProperty(x => x.Name, () => nameConst);

            //Assert.True(selector.CanBind(typeof(string)));

            //Generate a new fake with the custom selector implemented
            var customFakeInstance = fake.Generate();

            Assert.Equal(nameConst, customFakeInstance.Name);
            Assert.NotEqual(nameConst, customFakeInstance.OtherName);
        }

        [Fact(DisplayName = "When we use the SetType method on a Fake, it should apply to all properties of that type")]
        public void Should_Match_All_Properties_of_Same_Type()
        {
            //Create an instance of our test class
            var testInstance = new ContainerClass();

            var nameConst = "AaronConst";

            var fake = new Fake<ContainerClass>();

            //Run some tests before we add the custom selector
            var standardFakeInstance = fake.Generate();

            Assert.NotEqual(nameConst, standardFakeInstance.Name);

            //Add the custom selector for the Member field
            var selector = fake.SetType(() => nameConst);

            //Assert.True(selector.CanBind(typeof(string)));

            //Generate a new fake with the custom selector implemented
            var customFakeInstance = fake.Generate();

            Assert.Equal(nameConst, customFakeInstance.Name);
            Assert.Equal(nameConst, customFakeInstance.OtherName);
            Assert.Equal(nameConst, customFakeInstance.Member.Name);
        }

        #endregion
    }
}
