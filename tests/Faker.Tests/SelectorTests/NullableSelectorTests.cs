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
    [TestFixture]
    public class NullableSelectorTests
    {
        #region Test classes

        public class MyNullablePrimitiveTypeClass
        {
            public int? TestOne { get; set; }
        }

        #endregion

        [Test]
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
                    if (i % 10 == 0)
                        Thread.Sleep(20);
                }
            }
            
            Assert.True(ints.Any(x => x == null));
            Assert.True(ints.Any(x => x != null));
        }
    }
}
