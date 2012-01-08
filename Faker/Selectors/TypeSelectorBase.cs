using System;
using System.Reflection;

namespace Faker.Selectors
{
    /// <summary>
    /// Abstract base class used to enforce some constraints on how we manage TypeSelectors
    /// </summary>
    /// <typeparam name="T">The type that this selector works for</typeparam>
    public abstract class TypeSelectorBase<T> : ITypeSelector
    {
        protected TypeSelectorBase()
        {
            //Set the targetType to the value of the type selector
            TargetType = typeof(T);
        }

        protected bool _can_be_null;

        public int Priority
        { get; set; }

        public void BeNull(bool canBeNull = false)
        {
            _can_be_null = canBeNull;
        }

        public virtual bool CanBind(PropertyInfo field)
        {
            return field.PropertyType == TargetType;
        }

        public void Generate(object targetObject, PropertyInfo property)
        {
            property.SetValue(targetObject, Generate(), null);
        }

        public abstract T Generate();

        public Type TargetType { get; private set; }
    }
}