using System;
using Faker.Selectors;

namespace Faker
{
    /// <summary>
    /// Fluent interface definition designed to make it easy to customize user-defined TypeSelectors
    /// </summary>
    public static class TypeSelectorExtensions
    {
        /// <summary>
        /// Extension method for setting maximum bounds on a setter if it supports range selection
        /// </summary>
        /// <typeparam name="T">The type used in the range</typeparam>
        /// <param name="selector">The TypeSelector we're modifying</param>
        /// <param name="setter">The express used to set </param>
        /// <returns></returns>
        public static IRangeSelector<T> SetMax<T>(this IRangeSelector<T> selector, Func<T> setter)
        {
            ValidateExpression(selector, setter);

            selector.Max = setter;
            return selector;
        }

        /// <summary>
        /// Extension method for setting minimum bounds on a setter if it supports range selection
        /// </summary>
        /// <typeparam name="T">The type used in the range</typeparam>
        /// <param name="selector">The TypeSelector we're modifying</param>
        /// <param name="setter">The express used to set </param>
        /// <returns></returns>
        public static IRangeSelector<T> SetMin<T>(this IRangeSelector<T> selector, Func<T> setter)
        {
            ValidateExpression(selector, setter);

            selector.Min = setter;
            return selector;
        }

        public static TypeSelectorBase<T> Set<T>(this TypeSelectorBase<T> selector, Func<T> setter)
        {
            ValidateExpression(selector, setter);

            selector.Setter = setter;
            return selector;
        }

        internal static void ValidateExpression<T>(ITypeSelector selector, Func<T> setter)
        {
            ExpressionValidator.IsNotNull(() => setter, setter);
            ExpressionValidator.IsNotNull(selector);
        }
    }
}
