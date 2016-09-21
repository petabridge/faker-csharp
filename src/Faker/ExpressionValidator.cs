using System;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;
using Faker.Selectors;

namespace Faker
{
    /// <summary>
    ///     Static class used to validate all Expressions used in Faker extension methods
    /// </summary>
    public static class ExpressionValidator
    {
        /// <summary>
        ///     Validates that an incoming Lamba expression isn't null
        /// </summary>
        /// <typeparam name="T">The type returned by the expression</typeparam>
        /// <param name="expression">An invocation of the expression</param>
        /// <param name="rawExp">The expression object itself</param>
        public static void IsNotNull<T>(Expression<Func<T>> expression, T rawExp)
        {
            if (rawExp == null)
            {
                throw new ArgumentNullException(((MemberExpression) expression.Body).Member.Name);
            }
        }

        /// <summary>
        ///     Validate that our Type selector is not null
        /// </summary>
        /// <param name="selector">The TypeSelector instance</param>
        public static void IsNotNull(ITypeSelector selector)
        {
            if (selector == null)
            {
                throw new ArgumentNullException("selector");
            }
        }

        /// <summary>
        ///     Used to extract property information from an Expression. Necessary for generating user-defined TypeSelectors.
        /// </summary>
        /// <param name="expression">The expression used to access a property of a class</param>
        /// <returns>A PropertyInfo object</returns>
        public static PropertyInfo ToPropertyInfo(this LambdaExpression expression)
        {
            var memberExpression = expression.Body as MemberExpression;

            //If this is a member expression
            if (memberExpression != null)
            {
                var propertyInfo = memberExpression.Member as PropertyInfo;
                if (propertyInfo != null)
                {
                    return propertyInfo;
                }
            }

            throw new ArgumentException(string.Format(CultureInfo.InvariantCulture,
                string.Format("{0} is not a property", expression)));
        }
    }
}