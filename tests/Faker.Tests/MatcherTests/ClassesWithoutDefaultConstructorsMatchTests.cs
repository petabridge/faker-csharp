using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Faker.Tests.MatcherTests
{
    [TestFixture(Description = "Verify that we can construct instances of objects that do not have default constructors")]
    public class ClassesWithoutDefaultConstructorsMatchTests
    {
        private Matcher _matcher;

        #region Setup / Teardown

        [SetUp]
        public void SetUp()
        {
            _matcher = new Matcher();
        }

        #endregion

        public class MyClassWithCtorArgs
        {
            public MyClassWithCtorArgs(string foo, int bar)
            {
                Foo = foo;
                Bar = bar;
            }
            public string Foo { get; private set; }

            public int Bar { get; private set; }
        }

        [Test]
        public void Should_create_object_without_default_constructor()
        {
            var fake = Fake.Create<MyClassWithCtorArgs>();
            var instance = fake.Generate();
            Assert.AreNotEqual(string.Empty, instance.Foo);
            Assert.IsNotNull(instance.Foo);
        }
    }
}
