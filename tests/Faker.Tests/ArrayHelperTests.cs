﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faker.Helpers;
using Xunit;

namespace Faker.Tests
{
    
    public class ArrayHelperTests
    {
        [Fact]
        public void Should_shuffle_array()
        {
            var randomIntegers = Fake.Create<int>().Generate(100);
            var shuffled = randomIntegers.Shuffle().ToList();
            Assert.Equal(randomIntegers.Count, shuffled.Count);
            Assert.False(randomIntegers.SequenceEqual(shuffled));

            var hashset1 = new HashSet<int>(randomIntegers);
            var hashset2 = new HashSet<int>(shuffled);
            Assert.True(hashset1.SetEquals(hashset2));
        }
    }
}
