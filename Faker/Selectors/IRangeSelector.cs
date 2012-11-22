using System;

namespace Faker.Selectors
{
    /// <summary>
    /// Interface used for TypeSelectors that need to use ranges
    /// </summary>
    /// <typeparam name="T">The type we use to derive our maxs / mins</typeparam>
    public interface IRangeSelector<T>
    {
        Func<T> Max { get; set; }
        Func<T> Min { get; set; }
    }
}