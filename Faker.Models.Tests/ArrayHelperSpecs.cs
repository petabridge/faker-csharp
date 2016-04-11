using System.Linq;
using Faker.Helpers;
using FsCheck;
using NUnit.Framework;

namespace Faker.Models.Tests
{
    [TestFixture(Description = "Validates our extension methods for working with arrays")]
    public class ArrayHelperSpecs
    {
        [Test(Description = "Ensure that our shuffle function works over a range of intervals")]
        public void Shuffled_lists_should_never_match_original()
        {
            Prop.ForAll<int[]>(original =>
            {
                var shuffle = original.Shuffle();
                return (!original.SequenceEqual(shuffle)).When(original.Length > 1 && original.Distinct().Count() > 1);
            }).QuickCheckThrowOnFailure();
        }
    }
}
