using System;

namespace Faker.Selectors
{
    /// <summary>
    /// Used to help determine how to rank competing selectors for the same type
    /// </summary>
    public static class SelectorConstants
    {
        public const int PrimitiveSelectorPriority = 1;
        public const int SpecialSelectorPriority = 10;
        public const int CustomTypePriority = 25;
        public const int CustomNamedPropertyPriorty = 100;

        /// <summary>
        /// Null 10% of the time by default
        /// </summary>
        public const double DefaultNullProbability = 0.1d;

        /// <summary>
        /// No possibility of null
        /// </summary>
        public const double NoNullProbability = -0.1d;
        
        /// <summary>
        /// Used for generating nullable definitions
        /// </summary>
        internal static readonly Type Nullable = typeof(Nullable<>);

        /// <summary>
        /// Used for generating a <see cref="NullableTypeSelector"/>, typically
        /// for value types.
        /// </summary>
        internal static readonly Type NullableTypeSelector = typeof(NullableTypeSelector<>);
    }
}