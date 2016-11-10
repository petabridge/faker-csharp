using System;
using System.Reflection;

namespace Faker.Selectors
{
    /// <summary>
    ///     Selector created for many user-defined queries
    /// </summary>
    /// <typeparam name="T">The Type for which we are selecting</typeparam>
    public class CustomPropertySelector<T> : TypeSelectorBase<T>
    {
        protected PropertyInfo CustomProperty;

        public CustomPropertySelector(PropertyInfo property, Func<T> setter)
        {
            CustomProperty = property;
            Setter = setter;
            Priority = SelectorConstants.CustomNamedPropertyPriorty;
        }

        public override bool CanBind(PropertyInfo field)
        {
            //Can only bind if the types are assignable and share the same member name
            return CanBind(field.PropertyType) && string.Equals(field.Name, CustomProperty.Name);
        }

        public override T Generate()
        {
            return Setter();
        }
    }
}