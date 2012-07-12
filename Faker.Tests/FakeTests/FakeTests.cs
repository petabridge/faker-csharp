using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Faker.Tests.FakeTests
{
    /// <summary>
    /// Testing harnesss for validating that Faker itself can get the job done
    /// </summary>
    [TestFixture(Description = "Tests that validate that Faker behaves as expected")]
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


        #endregion

        #region Setup / Teardown
        #endregion

        #region Tests

        [Test(Description = "Should be able to fake a single instance of a rich class")]
        public void Should_Fake_Single_Instance_Of_Rich_Class()
        {
            var fake = new Fake<Project>();

            var projectInstance = fake.Generate();

            Assert.IsNotNull(projectInstance);
            Assert.IsTrue(projectInstance.Versions.Count > 0);
        }

        #endregion
    }
}
