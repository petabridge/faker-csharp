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
        public const float DefaultNullProbability = 0.1f;
    }
}