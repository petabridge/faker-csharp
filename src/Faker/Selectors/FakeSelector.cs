using System;
using System.Diagnostics.Contracts;
using System.Reflection;

namespace Faker.Selectors
{
    /// <summary>
    ///     <see cref="ITypeSelector" /> that uses an underlying <see cref="Fake{T}" />
    ///     to generate instances of type <see cref="T" />. Designed to allow composition
    ///     of fakes for complex classes.
    /// </summary>
    public sealed class FakeSelector<T> : ITypeSelector<T>
    {
        private readonly IFake<T> _internalFake;

        public FakeSelector(IFake<T> internalFake)
        {
            Contract.Requires(internalFake != null);
            _internalFake = internalFake;
        }

        /// <summary>
        /// Always <see cref="SelectorConstants.CustomNamedPropertyPriorty"/> by default.
        /// </summary>
        public int Priority { get; set; }

        public Type TargetType => _internalFake.SupportedType;

        public ITypeSelector Nullable(double nullProbability = SelectorConstants.DefaultNullProbability)
        {
            return NullableSelectorHelper.CreateNullableTypeSelector(nullProbability, TargetType, this);
        }

        public bool CanBind(PropertyInfo field)
        {
            return CanBind(field.PropertyType);
        }

        public bool CanBind(Type type)
        {
            return type.IsAssignableFrom(TargetType);
        }

        public void Generate(object targetObject, PropertyInfo property)
        {
            property.SetValue(targetObject, _internalFake.Generate(), null);
        }

        public object Generate(ref object targetObject)
        {
            targetObject = _internalFake.Generate();
            return targetObject;
        }

        public object GenerateInstance()
        {
            return _internalFake.Generate();
        }

        private bool Equals(FakeSelector<T> other)
        {
            return Priority == other.Priority && TargetType == other.TargetType;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is FakeSelector<T> && Equals((FakeSelector<T>) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (TargetType.GetHashCode()*397) ^ Priority;
            }
        }

        public T Generate()
        {
            return _internalFake.Generate();
        }
    }
}