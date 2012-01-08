using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Faker.Selectors;

namespace Faker
{
    /// <summary>
    /// Class used to match a type selector from the TypeTable with the properties of an object
    /// </summary>
    public class Matcher
    {
        public TypeTable TypeMap { get; protected set; }

        /// <summary>
        /// Default constructor - uses the default TypeTable
        /// </summary>
        public Matcher():this(new TypeTable()){}

        /// <summary>
        /// Constructor which accepts a TypeTable as an argument
        /// </summary>
        /// <param name="table">an instantiated type table that can be accessed via the TypeMap property later</param>
        public Matcher(TypeTable table)
        {
            TypeMap = table;
        }

        /// <summary>
        /// Method matches all properties on a given class with a 
        /// </summary>
        /// <typeparam name="T">a class with a parameterless constructor (POCO class)</typeparam>
        /// <param name="targetObject">an instance of the class</param>
        public virtual void Match<T>(T targetObject) where T : new()
        {
            //Get all of the properties of the class
            var properties = typeof (T).GetProperties();

            //Iterate over the properties
            foreach(var property in properties)
            {
                var selector = GetMatchingSelector(property);
            }
        }

        /// <summary>
        /// Protected method used to implement our selector-matching strategy. Uses a greedy approach.
        /// </summary>
        /// <param name="property">The meta-data about the property for which we will be finding a match</param>
        protected virtual ITypeSelector GetMatchingSelector(PropertyInfo property)
        {
            //Get the type of the property
            var propertyType = property.PropertyType;

            //Determine if we have a selector-on-hand for this data type
            var selectorCount = TypeMap.CountSelectors(propertyType);

            //We have some matching selectors, so we'll evaluate and return the best match
            if(selectorCount > 0)
            {
                return EvaluateSelectors(propertyType, TypeMap.GetSelectors<propertyType>());
            }

            //If the type is primitive
            if (propertyType.IsPrimitive)
            {
            }
        }
    }
}
