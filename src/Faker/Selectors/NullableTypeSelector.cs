using System;

namespace Faker.Selectors
{
    /// <summary>
    /// <see cref="ITypeSelector"/> implementation designed to support
    /// nullability - the ability for null types to be represented as null.
    /// </summary>
    public class NullableTypeSelector<T> : TypeSelectorBase<T> where T:new()
    {
        private readonly float _nullProbability;
        private readonly Func<T> _generatorFunc;

        public NullableTypeSelector(Func<T> generatorFunc, float nullProbability = SelectorConstants.DefaultNullProbability)
        {
            _nullProbability = nullProbability;
            _generatorFunc = generatorFunc;
        }

        public override T Generate()
        {
            return Generators.Numbers.Float(0.0f, _nullProbability) <= _nullProbability ? default(T) : _generatorFunc();
        }
    }
}
