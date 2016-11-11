using System;
using System.Reflection;

namespace Faker.Selectors
{
    public interface ITypeSelector
    {
        /// <summary>
        ///     Settable priority which indicates the priority of this TypeSelector relative to the
        ///     other selectors for this type. A fake can execute multiple strategies for selecting a value of a
        ///     given type
        /// </summary>
        int Priority { get; set; }

        /// <summary>
        ///     The underlying Type used for this mapping
        /// </summary>
        Type TargetType { get; }

        /// <summary>
        ///     Determines if we can allow nulls for a given type
        /// </summary>
        /// <param name="nullProbability">The probability that this value will be null.</param>
        ITypeSelector Nullable(double nullProbability = SelectorConstants.DefaultNullProbability);

        /// <summary>
        ///     Determines if this strategy can be successfully executed for this field.
        /// </summary>
        /// <param name="field">This method determines whether or not we will attempt to execute this strategy for a given type.</param>
        /// <returns>true if we can go forward with this strategy, false otherwise.</returns>
        bool CanBind(PropertyInfo field);

        /// <summary>
        ///     Determines if this strategy can be successfully executed for this type.
        /// </summary>
        /// <param name="type">The type of object to which we are attemping to bind.</param>
        /// <returns>true if this selector can bind to the provided type. False other</returns>
        bool CanBind(Type type);

        /// <summary>
        ///     Injects the generator function into the property
        /// </summary>
        /// <param name="targetObject">The target object designed for property injection</param>
        /// <param name="property">The meta-data for the current property we're testing</param>
        void Generate(object targetObject, PropertyInfo property);

        /// <summary>
        ///     Directly assigns a generated value to the object itself, in the case of custom selectors
        /// </summary>
        /// <param name="targetObject">The object to be replaced with a generated value</param>
        object Generate(ref object targetObject);

        /// <summary>
        /// Generate an instance of the underlying selector.
        /// </summary>
        /// <returns>An object instance.</returns>
        object GenerateInstance();
    }
}