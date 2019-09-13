using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Faker.End2End.Tests
{
    /// <summary>
    /// Test to see if Faker can correctly generate instances of a sealed class
    /// with no public setters.
    /// </summary>

    public class SealedClassesSpec
    {
        public sealed class MyClass
        {
            public MyClass(string myStr, bool myBool, double myDouble)
            {
                MyStr = myStr;
                MyBool = myBool;
                MyDouble = myDouble;
            }

            public string MyStr { get; private set; }
            public bool MyBool { get; private set; }
            public double MyDouble { get; private set; }
        }

        public struct MyStruct
        {
            public MyStruct(int foo, double bar)
            {
                Foo = foo;
                Bar = bar;
            }
            public int Foo { get; }

            public double Bar { get; }
        }

        public sealed class MyNestedClass
        {
            public MyNestedClass(MyClass c, MyStruct s)
            {
                Class = c;
                Struct = s;
            }

            public MyClass Class { get; }

            public MyStruct Struct { get; }
        }

        public sealed class MyPoorlyDesignedClass
        {
            public MyPoorlyDesignedClass(string myStr, bool myBool, double myDouble)
            {
                MyStr = myStr;
                MyBool = myBool;
                MyDouble = myDouble;
            }

            public string MyStr { get; private set; }
            public bool MyBool { get; private set; }
            public double MyDouble { get; private set; }
            public string MyOtherStr { get; set; }
            public double MyOtherDouble { get; set; }
        }

        [Fact]
        public void Faker_should_populate_class_via_constructor()
        {
            // DSL for properties won't do anything since the property isn't directly settable
            // but the DSL for types should work
            var fake = Fake.Create<MyClass>().SetProperty(x => x.MyBool, () => true).SetType(() => 1.0D).Generate();
            Assert.NotNull(fake);
            Assert.NotNull(fake.MyStr);
            Assert.Equal(1.0D, fake.MyDouble);
        }

        [Fact]
        public void Faker_should_populate_struct_via_constructor()
        {
            var fake = Fake.Create<MyStruct>().SetType(() => 1.0D).SetType(() => 1).Generate();
            fake.Bar.Should().Be(1.0d);
            fake.Foo.Should().Be(1);
        }

        [Fact]
        public void Faker_should_populate_class_via_constructor_and_properties()
        {
            var fake = Fake.Create<MyPoorlyDesignedClass>().SetType(() => 1.0D).SetProperty(x => x.MyOtherDouble, () => -0.1d).Generate();
            Assert.NotNull(fake);
            Assert.NotNull(fake.MyStr);
            Assert.NotNull(fake.MyOtherStr);
            Assert.Equal(1.0D, fake.MyDouble);
            Assert.Equal(-0.1d, fake.MyOtherDouble); // error - property setting is more specific than an individual type. That should override.
        }

        [Fact]
        public void Faker_should_populate_complex_types_via_constructor()
        {
            var fake = Fake.Create<MyNestedClass>().Generate();
            fake.Class.Should().NotBeNull();
        }
    }
}
