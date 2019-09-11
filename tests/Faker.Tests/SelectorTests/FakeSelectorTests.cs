using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Faker.Tests.SelectorTests
{
    
    public class FakeSelectorTests
    {
        public struct MyFoo
        {
            public int NumberOfBeers { get; set; }
            public double Sqrt { get; set; }
        }

        public class MyBar
        {
            public MyFoo Foo1 { get; set;  }
            public MyFoo Foo2 { get; set; }
            public string SomeName { get; set; }
        }

        [Fact]
        public void Should_use_internal_Fake_for_property()
        {
            var fake1 = Fake.Create<MyFoo>().SetProperty(x => x.NumberOfBeers, () => 0).SetProperty(x => x.Sqrt, () => -1.0d);
            var fake2 = Fake.Create<MyBar>(fake1);
            Assert.True(fake2.Generate(1000).All(x => x.Foo1.NumberOfBeers == x.Foo2.NumberOfBeers && x.Foo1.NumberOfBeers == 0));
            Assert.True(fake2.Generate(1000).All(x => x.Foo1.Sqrt == x.Foo2.Sqrt && x.Foo1.Sqrt == -1.0d));
        }
    }
}
