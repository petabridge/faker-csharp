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
                var shuffle = original.Shuffle().ToArray();
                return (!original.SequenceEqual(shuffle))
                .When(original.Length > 1 && original.Distinct().Count() > 1)
                .Label($"Expected shuffle({string.Join(",", shuffle)}) to be " +
                       $"different than original({string.Join(",", original)})")
                
                .And(original.All(x => shuffle.Contains(x))
                .Label($"Expected shuffle({string.Join(",", shuffle)}) to contain" +
                       $" same items as original({string.Join(",", original)})"));
            }).QuickCheckThrowOnFailure();
        }
    }
}
