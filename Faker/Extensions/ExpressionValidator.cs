using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Faker.Selectors;

namespace Faker.Extensions
{
    /// <summary>
    /// Static class used to validate all Expressions used in Faker extension methods
    /// </summary>
    public static class ExpressionValidator
    {
        /// <summary>
        /// Validates that an incoming Lamba expression isn't null
        /// </summary>
        /// <typeparam name="T">The type returned by the expression</typeparam>
        /// <param name="expression">An invocation of the expression</param>
        /// <param name="rawExp">The expression object itself</param>
        public static void IsNotNull<T>(Expression<Func<T>> expression, T rawExp)
        {
            if (rawExp == null)
            {
                throw new ArgumentNullException(((MemberExpression)expression.Body).Member.Name);
            }
        }

        /// <summary>
        /// Validate that our Type selector is not null
        /// </summary>
        /// <param name="selector">The TypeSelector instance</param>
        public static void IsNotNull(ITypeSelector selector)
        {
            if (selector == null)
            {
                throw new ArgumentNullException("selector");
            }
        }
    }
}
