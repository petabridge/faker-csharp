using System.Diagnostics.Contracts;

namespace Faker.Selectors
{
    /// <summary>
    ///     <see cref="ITypeSelector" /> that uses an underlying <see cref="Fake{T}" />
    ///     to generate instances of type <see cref="T" />. Designed to allow composition
    ///     of fakes for complex classes.
    /// </summary>
    /// <typeparam name="T">the type of data structure we want to generate.</typeparam>
    public sealed class FakeSelector<T> : TypeSelectorBase<T>
    {
        private readonly Fake<T> _faker;

        public FakeSelector(Fake<T> faker)
        {
            Contract.Requires(faker != null);
            _faker = faker;
        }

        public override T Generate()
        {
            return _faker.Generate();
        }
    }
}