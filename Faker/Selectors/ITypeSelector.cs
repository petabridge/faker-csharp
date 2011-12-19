using System;
using System.Reflection;

namespace Faker.Selectors
{
    internal interface ITypeSelector
    {
        /// <summary>
        /// Settable priority which indicates the priority of this TypeSelector relative to the
        /// other selectors for this type. A fake can execute multiple strategies for selecting a value of a 
        /// given type
        /// </summary>
        int Priority { get; set; }

        /// <summary>
        /// Sets the floor for the minimum range of a type (if applicable)
        /// </summary>
        object MinSize { get; set; }

        /// <summary>
        /// Sets the roof for the maximum range of a type (if applicable)
        /// </summary>
       object MaxSize { get; set; }

        /// <summary>
        /// Determines if we can allow nulls for a given type
        /// </summary>
        /// <param name="canBeNull">If true, we can set nulls - false by default</param>
        void BeNull(bool canBeNull = false);

        /// <summary>
        /// Determines if this strategy can be successfully executed for this field.
        /// </summary>
        /// <param name="field">This method determines whether or not we will attempt to execute this strategy for a given type.</param>
        /// <returns>true if we can go forward with this strategy, false otherwise.</returns>
        bool CanBind(PropertyInfo field);

        /// <summary>
        /// Injects the generator function into the property
        /// </summary>
        /// <param name="targetObject">The target object designed for property injection</param>
        /// <param name="property">The meta-data for the current property we're testing</param>
        void Generate(object targetObject, PropertyInfo property);

        /// <summary>
        /// The underlying Type used for this mapping
        /// </summary>
        Type TargetType { get; }
    }


    /// <summary>
    /// Abstract base class used to enforce some constraints on how we manage TypeSelectors
    /// </summary>
    /// <typeparam name="T">The type that this selector works for</typeparam>
    public abstract class TypeSelectorBase<T> : ITypeSelector
    {
        protected TypeSelectorBase()
        {
            //Set the targetType to the value of the type selector
            TargetType = typeof (T);
        } 

        protected bool _can_be_null;

        public int Priority
        { get; set; }

        public object MinSize { get; set; }
        public object MaxSize { get; set; }

        public void BeNull(bool canBeNull = false)
        {
            _can_be_null = canBeNull;
        }

        public virtual bool CanBind(PropertyInfo field)
        {
            return field.PropertyType == TargetType;
        }

        public abstract void Generate(object targetObject, PropertyInfo property);

        public Type TargetType { get; private set; }
    }
}
