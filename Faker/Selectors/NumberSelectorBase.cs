namespace Faker.Selectors
{
    /// <summary>
    /// All numeric selectors should have the ability to support a range
    /// </summary>
    /// <typeparam name="T">a numeric type</typeparam>
    public abstract class NumberSelectorBaseBase<T> : PrimitiveSelectorBase<T>
    {
        public T MinSize { get; set; }
        public T MaxSize { get; set; }
    }
}