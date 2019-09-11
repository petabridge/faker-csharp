using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Faker.Generators;
using Xunit;

namespace Faker.Tests.GeneratorTests
{
    public class NumberGeneratorTests
    {
        [Fact(DisplayName = "Does our integer generator behave as expected?")]
        public void Can_Generate_Ints_Within_Range()
        {
            var ints = new List<int>();
            for(var i = 0; i < 1000; i++)
            {
                ints.Add(Numbers.Int());
            }

            //Should not have any integers below zero
            Assert.DoesNotContain(ints, x => x < 0);

            //All integers should be greater than or equal to zero
            Assert.True(ints.All(x => x >= 0));

            //All integers should not be the same
            Assert.False(ints.All(x => x == ints[0]));
        }

        [Fact(DisplayName = "Does our double generator behave as expected?")]
        public void Can_Generate_Doubles_Within_Range()
        {
            var doubles = new List<double>();
            for (var i = 0; i < 1000; i++)
            {
                doubles.Add(Numbers.Double());
            }

            //Should not have any integers below zero
            Assert.DoesNotContain(doubles, x => x < 0);

            //All integers should be greater than or equal to zero
            Assert.True(doubles.All(x => x >= 0));

            //All integers should not be the same
            Assert.False(doubles.All(x => Math.Abs(x - doubles[0]) < 0.0d));
        }
        
        [Fact(DisplayName = "Can we generate doubles in a negative range?")]
        public void Can_Generate_Negative_Doubles()
        {
            var coordinates = new List<double>();
            for(var i = 0; i < 10000; i++)
            {
                coordinates.Add(Numbers.Double(-180.0d, 180.0d));
            }

            //Verify that we have some negative coordinates
            Assert.Contains(coordinates, x => x < 0.0d);

            //Verify that we have some positive coordinates
            Assert.Contains(coordinates, x => x > 0.0d);
        }

        [Fact(DisplayName = "Does our long generator behave as expected?")]
        public void Can_Generate_Longs_Within_Range()
        {
            var longs = new List<long>();
            for (var i = 0; i < 1000; i++)
            {
                longs.Add(Numbers.Long());
            }

            //Should not have any integers below zero
            Assert.DoesNotContain(longs, x => x < 0);

            //All integers should be greater than or equal to zero
            Assert.True(longs.All(x => x >= 0));

            //All integers should not be the same
            Assert.False(longs.All(x => x == longs[0]));
        }

        [Fact(DisplayName = "Does our float generator create floats within a valid range?")]
        public void Should_Generate_Valid_Floats_Within_Range()
        {
            var floats = new List<float>();
            for (var i = 0; i < 1000; i++)
            {
                floats.Add(Numbers.Float(Single.MinValue));
            }

            //All floats should be greater than or equal to zero
            Assert.True(floats.All(x => !float.IsNegativeInfinity(x)));
            Assert.True(floats.All(x => !float.IsPositiveInfinity(x)));
        }
    }
}
