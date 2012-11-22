using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Faker.Generators;
using Faker.Helpers;
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
        public Matcher() : this(new TypeTable()) { }

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
            var properties = typeof(T).GetProperties();

            ProcessProperties(properties, targetObject);
        }

        /// <summary>
        /// Method for iterating over all of the indivdual properties in for a given object
        /// </summary>
        /// <param name="properties">The set of properties available to an object instance</param>
        /// <param name="targetObject">The object against which type selectors will inject values</param>
        protected virtual void ProcessProperties(PropertyInfo[] properties, object targetObject)
        {
            //Iterate over the properties
            foreach (var property in properties)
            {
                if (!property.CanWrite) //Bail if we can't write to the property
                    continue;

                if(property.PropertyType == targetObject.GetType()) //If the property is a tree structure, bail (causes infinite recursion otherwise)
                    continue;

                ProcessProperty(property, targetObject);
            }
        }

        /// <summary>
        /// Protected method used to implement our selector-matching strategy. Uses a greedy approach.
        /// </summary>
        /// <param name="property">The meta-data about the property for which we will be finding a match</param>
        /// <param name="targetObject">The object which will receive the property injection</param>
        protected virtual void ProcessProperty(PropertyInfo property, object targetObject)
        {
            //Get the type of the property
            var propertyType = property.PropertyType;

            if (MapFromSelector(property, targetObject, propertyType)) return; //Exit

            //Check to see if the type is a class and has a default constructor
            if (propertyType.IsClass && propertyType.GetConstructor(Type.EmptyTypes) != null && !IsArray(propertyType))
            {
                var subProperties = propertyType.GetProperties();

                //Create an instance of the underlying subclass
                var subClassInstance = Activator.CreateInstance(propertyType);

                //Match all of the properties on the subclass 
                ProcessProperties(subProperties, subClassInstance);

                //Bind the sub-class back onto the original target object
                property.SetValue(targetObject, subClassInstance, null);

                return; //Exit
            }

            //Check to see if the type is an array or any other sort of collection
            if (IsArray(propertyType))
            {
                //Get the underlying type used int he array
                //var elementType = propertyType.GetElementType(); //Works only for arrays
                var elementType = propertyType.GetGenericArguments()[0]; //Works for IList<T> / IEnumerable<T>

                //Get a number of elements we want to create 
                //Note: (between 1 and 10 for now)
                var elementCount = Numbers.Int(1, 10);

                //Create an instance of our target array
                IList arrayInstance = null;

                //If we're working with a generic list or any other sort of collection
                if (propertyType.IsGenericTypeDefinition)
                {
                    arrayInstance = (IList)GenericHelper.CreateGeneric(propertyType, elementType);
                }
                else
                {
                    arrayInstance = (IList)GenericHelper.CreateGeneric(typeof(List<>), elementType);
                }

                //Determine if there's a selector available for this type
                var hasSelector = TypeMap.CountSelectors(elementType) > 0;
                ITypeSelector selector = null;

                //So we have a type available for this selector..
                if (hasSelector)
                {
                    selector = TypeMap.GetBaseSelector(elementType);
                }

                //If the element in the array isn't the same type as the parent object (recursive objects, like trees)
                if (elementType != targetObject.GetType())
                {
                    for (var i = 0; i < elementCount; i++)
                    {
                        //Create a new element instance
                        var element = SafeObjectCreate(elementType);

                        if (hasSelector)
                        {
                            selector.Generate(ref element);
                        }

                        //If the element type is a class populate it recursively
                        else if (elementType.IsClass)
                        {
                            var subProperties = elementType.GetProperties();

                            //Populate all of the properties on this object
                            ProcessProperties(subProperties, element);
                        }

                        arrayInstance.Add(element);
                    }
                }

                //Bind the sub-class back onto the original target object
                property.SetValue(targetObject, arrayInstance, null);
            }

        }

        private bool MapFromSelector(PropertyInfo property, object targetObject, Type propertyType)
        {
            //Determine if we have a selector-on-hand for this data type
            var selectorCount = TypeMap.CountSelectors(propertyType);

            //We have some matching selectors, so we'll evaluate and return the best match
            if (selectorCount > 0)
            {
                //Evaluate all of the possible selectors and find the first available match
                var selector = EvaluateSelectors(property, TypeMap.GetSelectors(propertyType));

                //We found a matching selector
                if (!(selector is MissingSelector))
                {
                    selector.Generate(targetObject, property); //Bind the property
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Returns true if the targeted type is an array of some sort
        /// </summary>
        /// <param name="targetType">the type we want to test</param>
        /// <returns>true if it's an array, false otherwise</returns>
        protected virtual bool IsArray(Type targetType)
        {
            if (!targetType.IsGenericType)
                return false;
            var genericArguments = targetType.GetGenericArguments();
            if (genericArguments.Length != 1)
                return false;

            var listType = typeof(IList<>).MakeGenericType(genericArguments);
            return listType.IsAssignableFrom(targetType);
        }

        /// <summary>
        /// Method used for safely creating new instances of type objects; handles a few special cases
        /// where activation has to be done carefully.
        /// </summary>
        /// <param name="t">The target type we want to instantiate</param>
        /// <returns>an instance of the specified type</returns>
        public static object SafeObjectCreate(Type t)
        {
            //If the object is a string (tricky)
            if (t == typeof(string))
            {
                return string.Empty;
            }

            return Activator.CreateInstance(t);
        }

        /// <summary>
        /// Evaluates a set of selectors and grabs the first available match
        /// </summary>
        /// <param name="propertyType">The type for which we're trying to find a match</param>
        /// <param name="selectors">A list of selectors from the TypeTable</param>
        /// <returns>the first matching ITypeSelector instance we could find</returns>
        protected virtual ITypeSelector EvaluateSelectors(PropertyInfo propertyType, IEnumerable<ITypeSelector> selectors)
        {
            foreach (var selector in selectors)
            {
                //If the selector can bind
                if (selector.CanBind(propertyType))
                {
                    //Return it
                    return selector;
                }
            }

            //Otherwise, return a MissingSelector and let them know that we can't do it.
            return new MissingSelector();
        }
    }
}
