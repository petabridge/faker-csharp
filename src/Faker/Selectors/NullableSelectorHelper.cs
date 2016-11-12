using System;
using System.Diagnostics.Contracts;
using Faker.Helpers;

namespace Faker.Selectors
{
    public static class NullableSelectorHelper
    {
        public static ITypeSelector CreateNullableTypeSelector(double nullProbability, Type targetType, ITypeSelector baseSelector)
        {
            Contract.Requires(targetType != null);
            Contract.Requires(baseSelector != null);

            if (targetType.IsClass)
            {
                return 
                (ITypeSelector)
                GenericHelper.CreateGeneric(SelectorConstants.NullableTypeSelector, 
                    targetType, 
                    baseSelector, 
                    nullProbability);
            }

            // if we're here, then we're working with a value type
            // and will need to construct a Nullable<T> instance.
            var nullable = GenericHelper.GetGenericType(SelectorConstants.Nullable, targetType);
            var selector =
                (ITypeSelector)
                GenericHelper.CreateGeneric(SelectorConstants.NullableTypeSelector, 
                    nullable, 
                    baseSelector, 
                    nullProbability);
            return selector;
        }
    }
}