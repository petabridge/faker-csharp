using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Faker.Extensions
{
    /// <summary>
    /// Static class used to assist developers in defining their own custom Type selectors via LINQ expressions
    /// </summary>
    public static class FakeExtensions
    {
        public static Fake<T> SetupSelector<T, TProperty>(Fake<T> fake, Expression<Func<T, TProperty>> expression, Expression<Func<TProperty>> setter) where T : new()
        {
            var prop = expression.ToPropertyInfo();
            ThrowIfCantWrite(prop);

            return fake;
        }

        /// <summary>
        /// Throws an exception if a developer tries to create a TypeSelector for a property to which we can't write
        /// </summary>
        /// <param name="prop">The Property we're testing</param>
        private static void ThrowIfCantWrite(PropertyInfo prop)
        {
            if (!prop.CanWrite)
            {
                throw new ArgumentException(string.Format(CultureInfo.InvariantCulture,
                                                          "Cannot create TypeSelector for Property with no accessible Set method {0}",
                                                          prop.Name));
            }
        }
    }
}
