using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Faker.Tests.FakeTests
{
    /// <summary>
    /// Testing harnesss for validating that Faker itself can get the job done
    /// </summary>
    public class FakeTests
    { 

        #region Test classes

        public class Project
        {
            public Guid ProjectId { get; set; }

            public Guid ApiKey { get; set; }

            public string ProjectSlug { get; set; }

            public string ProjectName { get; set; }

            public string ProjectDescription { get; set; }

            public DateTime DateCreated { get; set; }

            public DateTimeOffset LastActivity { get; set; }

            public ProjectType TargetOS { get; set; }

            public IList<AppVersion> Versions { get; set; }

            public Project()
            {
                Versions = new List<AppVersion>();
            }
        }

        public enum ProjectType
        {
            iOS,
            Android,
            WindowsPhone
        };

        public class AppVersion
        {
            public Guid VersionId { get; set; }

            public string VersionSlug { get; set; }

            public string VersionName { get; set; }
        }

        public struct TestStruct
        {
            public string Id { get; set; }
            public DateTime Date { get; set; }
            public int SomeNumber { get; set; }
        }

        public class ClassWithStruct
        {
            public TestStruct Struct { get; set; }
            public string Name { get; set; }
        }


        #endregion

        #region Setup / Teardown
        #endregion

        #region Tests

        [Fact(DisplayName = "Should be able to fake a single instance of a rich class")]
        public void Should_Fake_Single_Instance_Of_Rich_Class()
        {
            var fake = new Fake<Project>();

            var projectInstance = fake.Generate();

            Assert.NotNull(projectInstance);
            Assert.True(projectInstance.Versions.Count > 0);
        }

        [Fact(DisplayName = "Should be able to fake a single instance of DateTime, a built-in struct that has a full matching selector")]
        public void Should_Fake_Single_Instance_of_DateTime()
        {
            var fake = new Fake<DateTime>();

            var dateInstance = fake.Generate();

            Assert.Equal(DateTime.MinValue, dateInstance);
            Assert.True(dateInstance.Year > 1);
        }

        [Fact(DisplayName = "Should be able to fake a single instance of a custom struct that has to be constructed component-wise")]
        public void Should_Fake_Single_Instance_of_CustomStruct()
        {
            var fake = new Fake<TestStruct>();

            var customStructInstance = fake.Generate();

            Assert.NotEqual(DateTime.MinValue, customStructInstance.Date);
            Assert.True(default(int) != customStructInstance.SomeNumber);
            Assert.NotNull(customStructInstance.Id);
        }

        [Fact(DisplayName = "Should be able to fake a single instance of a class that constains another custom struct")]
        public void Should_Fake_Single_Instance_of_ClassThatContainsStruct()
        {
            var fake = new Fake<ClassWithStruct>();

            var classWithStructInstance = fake.Generate();

            Assert.NotEqual(DateTime.MinValue, classWithStructInstance.Struct.Date);
            Assert.True(default(int) != classWithStructInstance.Struct.SomeNumber);
            Assert.NotNull(classWithStructInstance.Struct.Id);
            Assert.NotNull(classWithStructInstance.Name);
        }

        [Fact(DisplayName = "Should be able to fake a single instance of string which has matching selectors.")]
        public void Should_Fake_Single_Instance_of_String()
        {
            var fake = new Fake<string>();

            var stringInstance = fake.Generate();

            Assert.NotNull(stringInstance);
        }

        #endregion
    }
}
