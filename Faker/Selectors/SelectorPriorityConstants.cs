namespace Faker.Selectors
{
    /// <summary>
    /// Used to help determine how to rank competing selectors for the same type
    /// </summary>
    public static class SelectorPriorityConstants
    {
        public const int PrimitiveSelectorPriority = 1;
        public const int SpecialSelectorPriority = 10;
        public const int CustomSelectorPriorty = 100;
    }
}