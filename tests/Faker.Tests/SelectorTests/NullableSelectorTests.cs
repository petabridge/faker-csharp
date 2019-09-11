using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Faker.Selectors;
using NUnit.Framework;

namespace Faker.Tests.SelectorTests
{
    
    public class NullableSelectorTests
    {
        #region Test classes

        public class MyNullablePrimitiveTypeClass
        {
            public int? TestOne { get; set; }
        }

        #endregion

        [Fact]
        public void NullableTypeSelector_supports_all_nullable_primivites()
        {
            var intSelector = new NullableTypeSelector<int?>(new IntSelector());
            var c = new MyNullablePrimitiveTypeClass();

            var ints = new List<int?>();

            // The distribution of nulls over a large period of time should roughly
            // be 1 in 10 by default, but the RNG may not distribute them evenly.
            foreach (var property in c.GetType().GetProperties())
            {
                for (var i = 0; i < 100; i++)
                {
                    intSelector.Generate(c, property);
                    ints.Add(c.TestOne);
                }
            }
            
            Assert.True(ints.Any(x => x == null));
            Assert.True(ints.Any(x => x != null));
        }

        [Fact]
        public void NullableTypeSelector_can_still_be_nullable()
        {
            var c = new MyNullablePrimitiveTypeClass();
            var original = new NullableTypeSelector<int?>(new IntSelector());
            var final = original.Nullable().Nullable(0.3d);

            Assert.IsInstanceOf<NullableTypeSelector<int?>>(original);
            Assert.IsInstanceOf<NullableTypeSelector<int?>>(final);
            Assert.AreNotSame(original, final);

            var ints = new List<int?>();

            // The distribution of nulls over a large period of time should roughly
            // be 1 in 10 by default, but the RNG may not distribute them evenly.
            foreach (var property in c.GetType().GetProperties())
            {
                for (var i = 0; i < 100; i++)
                {
                    final.Generate(c, property);
                    ints.Add(c.TestOne);
                }
            }

            Assert.True(ints.Any(x => x == null));
            Assert.True(ints.Any(x => x != null));
        }
    }
}
