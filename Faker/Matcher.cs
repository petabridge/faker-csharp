using System;
using System.Collections.Generic;
using System.Linq;
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
        public void Match<T>(T targetObject) where T : new()
        {
            //Get all of the properties of the class
            var properties = typeof (T).GetProperties();

            //Iterate over the properties
            foreach(var property in properties)
            {
                //Get the type of each property
                var propertyType = property.PropertyType;

                //var selectorCount = TypeMap.CountSelectors<property.PropertyType>();

                //If the type is primitive
                if(propertyType.IsPrimitive)
                {
                    
                }
            }
        }
    }
}
