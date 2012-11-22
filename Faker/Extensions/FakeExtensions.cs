using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Faker.Selectors;

namespace Faker.Extensions
{
    /// <summary>
    /// Static class used to assist developers in defining their own custom Type selectors via LINQ expressions
    /// </summary>
    public static class FakeExtensions
    {
        public static TypeSelectorBase<TProperty> SetupSelector<T, TProperty>(this Fake<T> fake, Expression<Func<T, TProperty>> expression, Expression<Func<TProperty>> setter) where T : new() where TProperty:new()
        {
            ExpressionValidator.IsNotNull(() => setter, setter);

            var prop = expression.ToPropertyInfo();
            ThrowIfCantWrite(prop);

            var matchingSelector = fake.GetSelector(prop);
            if (matchingSelector is MissingSelector || !(matchingSelector is TypeSelectorBase<TProperty>))
            {
                return new CustomPropertySelector<TProperty>(prop, setter.Compile());
            }

            var baseSelector = matchingSelector as TypeSelectorBase<TProperty>;

            return new CustomDerivedPropertySelector<TProperty>(baseSelector, prop);
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
