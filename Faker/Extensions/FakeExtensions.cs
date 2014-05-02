using System;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;
using Faker.Extensions;
using Faker.Selectors;

namespace Faker
{
    /// <summary>
    /// Static class used to assist developers in defining their own custom Type selectors via LINQ expressions
    /// </summary>
    public static class FakeExtensions
    {
        /// <summary>
        /// Creates a custom Faker rule for a specific property on an object
        /// </summary>
        /// <typeparam name="T">The type of the parent object</typeparam>
        /// <typeparam name="TProperty">The type of the property</typeparam>
        /// <param name="fake">The Fake object for which we're creating this rule</param>
        /// <param name="expression">The expression for retreiving the property</param>
        /// <param name="setter">The expression for setting the value of the property</param>
        /// <returns>An updated Fake</returns>
        public static Fake<T> SetProperty<T, TProperty>(this Fake<T> fake, Expression<Func<T, TProperty>> expression, Expression<Func<TProperty>> setter)
        {
            ExpressionValidator.IsNotNull(() => setter, setter);

            var prop = expression.ToPropertyInfo();
            ThrowIfCantWrite(prop);

            var matchingSelector = fake.GetBaseSelector(prop.PropertyType);
            if (matchingSelector is MissingSelector || !(matchingSelector is TypeSelectorBase<TProperty>))
            {
                var customSelector = new CustomPropertySelector<TProperty>(prop, setter.Compile());
                fake.AddSelector(customSelector);
                return fake;
            }

            var baseSelector = Activator.CreateInstance(matchingSelector.GetType()) as TypeSelectorBase<TProperty>;
            var customDerivedSelector = new CustomDerivedPropertySelector<TProperty>(baseSelector.Set(setter.Compile()), prop);
            fake.AddSelector(customDerivedSelector);

            return fake;
        }

        /// <summary>
        /// Creates a custom Faker rule for all properties of a single type on an object
        /// </summary>
        /// <typeparam name="T">The type of the parent object</typeparam>
        /// <typeparam name="TS">The type for which this rule will be applied</typeparam>
        /// <param name="fake">The Faker instance to be modified</param>
        /// <param name="setter">The expression for which we should be setting a value to</param>
        /// <returns>An updated Fake</returns>
        public static Fake<T> SetType<T, TS>(this Fake<T> fake, Expression<Func<TS>> setter)
        {
            ExpressionValidator.IsNotNull(() => setter, setter);
            var targetType = typeof(TS);

            var selector = new CustomTypeSelector<TS>(setter.Compile());
            fake.AddSelector(selector);
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
