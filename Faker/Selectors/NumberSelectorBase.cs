using System;

namespace Faker.Selectors
{
    /// <summary>
    /// All numeric selectors should have the ability to support a range
    /// </summary>
    /// <typeparam name="T">a numeric type</typeparam>
    public abstract class NumberSelectorBaseBase<T> : PrimitiveSelectorBase<T>, IRangeSelector<T>
    {
        protected NumberSelectorBaseBase()
        {
            Max = () => MaxSize;
            Min = () => MinSize;
        }

        public T MinSize { get; set; }
        public T MaxSize { get; set; }
        public Func<T> Max { get; set; }
        public Func<T> Min { get; set; }
    }
}