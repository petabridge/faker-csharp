using System;

namespace Faker.Selectors
{
    /// <summary>
    /// <see cref="ITypeSelector"/> implementation designed to support
    /// nullability - the ability for null types to be represented as null.
    /// </summary>
    public class NullableTypeSelector<T> : TypeSelectorBase<T>
    {
        private readonly double _nullProbability;
        private readonly Func<object> _generatorFunc;

        /// <summary>
        /// Default constructor for nullable types.
        /// </summary>
        /// <param name="generatorFunc">A delegate capable of producing a type.</param>
        /// <param name="nullProbability">The likelihood of generating a null versus a concrete value.</param>
        public NullableTypeSelector(Func<object> generatorFunc, double nullProbability = SelectorConstants.DefaultNullProbability)
        {
            _nullProbability = nullProbability;
            _generatorFunc = generatorFunc;
        }

        /// <summary>
        /// Default constructor for nullable types.
        /// </summary>
        /// <param name="selector">A concrete type selector for the supported type.</param>
        /// <param name="nullProbability">The likelihood of generating a null versus a concrete value.</param>
        public NullableTypeSelector(ITypeSelector selector, double nullProbability = SelectorConstants.DefaultNullProbability)
            : this(selector.GenerateInstance, nullProbability) { }

        public override T Generate()
        {
            var output = _generatorFunc();
            if (Generators.Booleans.BoolWithProbability(_nullProbability))
                return default(T);
            return (T)output;
        }
    }
}
