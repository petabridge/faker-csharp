using System;
using System.Reflection;

namespace Faker.Selectors
{
    /// <summary>
    /// Selector created for many user-defined queries
    /// </summary>
    /// <typeparam name="T">The Type for which we are selecting</typeparam>
    public class CustomDerivedPropertySelector<T> : TypeSelectorBase<T>
    {
        protected TypeSelectorBase<T> InternalSelector;
        protected PropertyInfo CustomProperty; 

        public CustomDerivedPropertySelector(TypeSelectorBase<T> baseSelector, PropertyInfo property)
        {
            InternalSelector = baseSelector;
            CustomProperty = property;
            Priority = SelectorPriorityConstants.CustomSelectorPriorty;
        }

        public override Func<T> Setter
        {
            get
            {
                return InternalSelector.Setter;
            }
            set
            {
                InternalSelector.Setter = value;
            }
        }

        public override bool CanBind(PropertyInfo field)
        {
            //Can only bind if the types are assignable and share the same member name
            return CanBind(field.PropertyType) && string.Equals(field.Name, CustomProperty.Name);
        }

        public override T Generate()
        {
            return InternalSelector.Generate();
        }
    }
}
