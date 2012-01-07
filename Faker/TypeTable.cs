using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Faker.Selectors;

namespace Faker
{
    public enum SelectorPosition
    {
        First,
        Last
    }

    /// <summary>
    /// The repository which determines the order in which selectors are picked for any given type
    /// </summary>
    sealed class TypeTable
    {
        //Our internal map for all types and their selectors used within Faker
        private Dictionary<Type, LinkedList<ITypeSelector>> _typeMap;

        /// <summary>
        /// Default constructor
        /// </summary>
        public TypeTable():this(new Dictionary<Type, LinkedList<ITypeSelector>>()){}

        /// <summary>
        /// Private constructor which accepts an inbound typemap as an argument
        /// </summary>
        /// <param name="typeMap">A typemap instance</param>
        private TypeTable(Dictionary<Type, LinkedList<ITypeSelector>> typeMap)
        {
            _typeMap = typeMap;
        }

        /// <summary>
        /// Internal method for securely creating type rows where needed
        /// </summary>
        /// <param name="incomingType">The type to which we're mapping this object</param>
        private void CreateTypeIfNotExists(Type incomingType)
        {
            //If we already have a collection in our dictionary that corresponds to this type, bail out
            if (_typeMap.ContainsKey(incomingType))
                return;

            //Otherwise, create the row in our dictionary
            _typeMap.Add(incomingType, new LinkedList<ITypeSelector>());
        }

        /// <summary>
        /// Add a strongly typed selector to the 
        /// </summary>
        /// <typeparam name="T">The type for which this selector is used</typeparam>
        /// <param name="selector">A TypeSelectorBase implementation for Type T</param>
        /// <param name="position">Optional parameter - indicates whether you want this type selector to be used first or last in the sequence of available selectors for this type</param>
        public void AddSelector<T>(TypeSelectorBase<T> selector, SelectorPosition position = SelectorPosition.First)
        {
            var activeType = typeof (T);
            CreateTypeIfNotExists(activeType);

            if (position == SelectorPosition.First) //If the user wants to add this selector to the front of the list (default), do that
                _typeMap[activeType].AddFirst(selector);
            else //Otherwise, add this type to the back of the list
                _typeMap[activeType].AddLast(selector);
        }

        /// <summary>
        /// Get all of the selectors for a given type
        /// </summary>
        /// <typeparam name="T">The type for which we need value selectors</typeparam>
        /// <returns>An enumerable list of selectors</returns>
        public IEnumerable<TypeSelectorBase<T>> GetSelectors<T>()
        {
            var activeType = typeof(T);
            CreateTypeIfNotExists(activeType);
            return _typeMap[activeType].Cast<TypeSelectorBase<T>>();
        }
    }
}
