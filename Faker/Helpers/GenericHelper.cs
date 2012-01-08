using System;

namespace Faker.Helpers
{
    public static class GenericHelper
    {
        /// <summary>
        /// Internal method for creating instances of generic objects
        /// </summary>
        /// <param name="generic">the Generic type</param>
        /// <param name="innerType">the inner type</param>
        /// <param name="args">Any additional arguments that need to be passed to the constructor</param>
        /// <returns>An instantiated generic instance of generic{innerType}()</returns>
        public static object CreateGeneric(Type generic, Type innerType, params object[] args)
        {
            var specificType = generic.MakeGenericType(new[] { innerType });
            return Activator.CreateInstance(specificType, args);
        }
    }
}