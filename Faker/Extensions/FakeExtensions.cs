using System;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;
using Faker.Selectors;

namespace Faker.Extensions
{
    /// <summary>
    /// Static class used to assist developers in defining their own custom Type selectors via LINQ expressions
    /// </summary>
    public static class FakeExtensions
    {
        public static TypeSelectorBase<TProperty> SetupSelector<T, TProperty>(this Fake<T> fake, Expression<Func<T, TProperty>> expression, Expression<Func<TProperty>> setter) 
        {
            ExpressionValidator.IsNotNull(() => setter, setter);

            var prop = expression.ToPropertyInfo();
            ThrowIfCantWrite(prop);

            var matchingSelector = fake.GetBaseSelector(prop.PropertyType);
            if (matchingSelector is MissingSelector || !(matchingSelector is TypeSelectorBase<TProperty>))
            {
                var customSelector =  new CustomPropertySelector<TProperty>(prop, setter.Compile());
                fake.AddSelector(customSelector);
                return customSelector;
            }

            var baseSelector = matchingSelector as TypeSelectorBase<TProperty>;
            var customDerivedSelector = new CustomDerivedPropertySelector<TProperty>(baseSelector.Set(setter.Compile()), prop);
            fake.AddSelector(customDerivedSelector);

            return customDerivedSelector;
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
