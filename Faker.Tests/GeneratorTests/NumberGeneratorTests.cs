using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Faker.Generators;
using NUnit.Framework;

namespace Faker.Tests.GeneratorTests
{
    [TestFixture(Description = "Used to ensure that our number generators behave as expected")]
    public class NumberGeneratorTests
    {
        [Test(Description = "Does our integer generator behave as expected?")]
        public void Can_Generate_Ints_Within_Range()
        {
            var ints = new List<int>();
            for(var i = 0; i < 1000; i++)
            {
                ints.Add(Numbers.Int());
            }

            //Should not have any integers below zero
            Assert.IsFalse(ints.Any(x => x < 0));

            //All integers should be greater than or equal to zero
            Assert.IsTrue(ints.All(x => x >= 0));

            //All integers should not be the same
            Assert.IsFalse(ints.All(x => x == ints[0]));
        }

        [Test(Description = "Does our double generator behave as expected?")]
        public void Can_Generate_Doubles_Within_Range()
        {
            var doubles = new List<double>();
            for (var i = 0; i < 1000; i++)
            {
                doubles.Add(Numbers.Double());
            }

            //Should not have any integers below zero
            Assert.IsFalse(doubles.Any(x => x < 0));

            //All integers should be greater than or equal to zero
            Assert.IsTrue(doubles.All(x => x >= 0));

            //All integers should not be the same
            Assert.IsFalse(doubles.All(x => Math.Abs(x - doubles[0]) < 0.0d));
        }
        
        [Test(Description = "Can we generate doubles in a negative range?")]
        public void Can_Generate_Negative_Doubles()
        {
            var coordinates = new List<double>();
            for(var i = 0; i < 10000; i++)
            {
                coordinates.Add(Numbers.Double(-180.0d, 180.0d));
            }

            //Verify that we have some negative coordinates
            Assert.IsTrue(coordinates.Any(x => x < 0.0d));

            //Verify that we have some positive coordinates
            Assert.IsTrue(coordinates.Any(x => x > 0.0d));
        }

        [Test(Description = "Does our long generator behave as expected?")]
        public void Can_Generate_Longs_Within_Range()
        {
            var longs = new List<long>();
            for (var i = 0; i < 1000; i++)
            {
                longs.Add(Numbers.Long());
            }

            //Should not have any integers below zero
            Assert.IsFalse(longs.Any(x => x < 0));

            //All integers should be greater than or equal to zero
            Assert.IsTrue(longs.All(x => x >= 0));

            //All integers should not be the same
            Assert.IsFalse(longs.All(x => x == longs[0]));
        }

        [Test(Description = "Does our float generator create floats within a valid range?")]
        public void Should_Generate_Valid_Floats_Within_Range()
        {
            var float1 = Numbers.Float(Single.MinValue);
            
            Assert.AreNotEqual(float1, default(float));
            Assert.AreNotEqual(float1, float.NegativeInfinity);
            Assert.AreNotEqual(float1, float.PositiveInfinity);
        }
    }
}
