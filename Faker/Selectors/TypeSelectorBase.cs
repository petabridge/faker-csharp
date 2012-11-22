using System;
using System.Reflection;

namespace Faker.Selectors
{
    //Base class used to signify to the TypeTable that this is the option of last resort for any given type
    public abstract class PrimitiveSelectorBase<T> : TypeSelectorBase<T>{}

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
            return CanBind(field.PropertyType);
        }

        public virtual bool CanBind(Type type)
        {
            return type.IsAssignableFrom(TargetType);
        }

        public void Generate(object targetObject, PropertyInfo property)
        {
            property.SetValue(targetObject, Generate(), null);
        }

        public void Generate(object targetObject)
        {
            throw new NotImplementedException();
        }

        public void Generate(ref object propertyObject)
        {
            propertyObject = Generate();
        }

        public abstract T Generate();

        public Type TargetType { get; private set; }
    }
}