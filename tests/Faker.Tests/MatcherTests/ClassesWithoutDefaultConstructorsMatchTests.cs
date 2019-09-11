using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Faker.Tests.MatcherTests
{
    public class ClassesWithoutDefaultConstructorsMatchTests
    {
        private Matcher _matcher = new Matcher();

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

        [Fact]
        public void Should_create_object_without_default_constructor()
        {
            var fake = Fake.Create<MyClassWithCtorArgs>();
            var instance = fake.Generate();
            Assert.NotEqual(string.Empty, instance.Foo);
            Assert.NotNull(instance.Foo);
        }
    }
}
