using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using Faker.Selectors;

namespace Faker
{
    /// <summary>
    /// A convenience class for creating quick Fakes
    /// </summary>
    public static class Fake
    {
        public static Fake<T> Create<T>()
        {
            return new Fake<T>();
        }

        public static Fake<T> Create<T>(params IFake[] otherFakes)
        {
            return new Fake<T>(otherFakes);
        }
    }

    /// <summary>
    ///     Used for dynamically generating fakes of simple POCO objects
    /// </summary>
    public class Fake<T> : IFake<T>
    {
        /// <summary>
        ///     Engine used to power our fakes
        /// </summary>
        private readonly Matcher _matcher;

        public Fake()
        {
            _matcher = new Matcher();
        }

        /// <summary>
        /// Constructor that takes multiple internal fakes to use for internal properties.
        /// </summary>
        /// <param name="fakes">A list of one or more fakes to use</param>
        public Fake(params IFake[] fakes) : this()
        {
            Contract.Requires(fakes != null);
            foreach(var fake in fakes)
                AddSelector(fake);
        }

        /// <summary>
        ///     Generates a single fake value for a given type
        /// </summary>
        /// <returns>A populated instance of a given class</returns>
        public T Generate()
        {
            //create a new instance of the type we want to Fake
            var instance = (T) _matcher.SafeObjectCreate(typeof (T));

            if (typeof(T).IsValueType)
            {
                _matcher.MatchStruct(ref instance);
            }
            else
            {
                //Match all of the properties of the object and come up with the most reasonable guess we can as to the type of data needed
                instance = _matcher.Match(instance);
            }

            //Return the instance once matching is complete
            return instance;
        }

        IList<object> IFake.Generate(int count)
        {
            return Generate(count).Cast<object>().ToList();
        }

        object IFake.Generate()
        {
            return Generate();
        }

        /// <summary>
        ///     Generates a list of fake values for a given type
        /// </summary>
        /// <returns>A list of populated instances with length [count] of a given class</returns>
        public IList<T> Generate(int count)
        {
            //Create a list to hold all of the fakes we want to return back to the caller
            var items = new List<T>();

            //Build the list of objects
            for (var i = 0; i < count; i++)
            {
                items.Add(Generate());
            }

            //Return the list to the caller
            return items;
        }

        /// <summary>
        ///     Adds a selector to the TypeTable; User-defined selectors always take precedence over the built-in ones.
        /// </summary>
        /// <typeparam name="TS">The type that matches the selector</typeparam>
        /// <param name="selector">A TypeSelectorBase instance for all instances of a TS type</param>
        public void AddSelector<TS>(TypeSelectorBase<TS> selector)
        {
            AddSelector((ITypeSelector)selector);
        }

        /// <summary>
        ///     Adds a selector to the TypeTable; User-defined selectors always take precedence over the built-in ones.
        /// </summary>
        /// <param name="selector">A TypeSelectorBase instance for all instances of a TS type</param>
        public void AddSelector(ITypeSelector selector)
        {
            _matcher.TypeMap.AddSelector(selector, SelectorPosition.First);
        }

        /// <summary>
        /// Adds another <see cref="IFake"/> instance to use as an internal selector for a type.
        /// </summary>
        /// <param name="fake">Another fake instance</param>
        public void AddSelector(IFake fake)
        {
            Contract.Requires(fake != null);
            Contract.Assert(fake.SupportedType != this.SupportedType);
            AddSelector(new FakeSelector(fake));
        }

        /// <summary>
        ///     Returns the first matching selector for the appropriate type
        /// </summary>
        /// <typeparam name="TS">The Type that we're evaluating</typeparam>
        /// <returns>A selector for the appropriate matching type</returns>
        internal ITypeSelector GetSelector<TS>()
        {
            return GetSelector(typeof (TS));
        }

        /// <summary>
        ///     Returns the first matching selector for the appropriate type
        /// </summary>
        /// <returns>A selector for the appropriate matching type</returns>
        internal ITypeSelector GetSelector(Type ts)
        {
            return _matcher.EvaluateSelectors(ts, _matcher.TypeMap.GetSelectors(ts));
        }

        /// <summary>
        ///     Returns the first matching selector for the appropriate type for a given property
        /// </summary>
        /// <returns>A selector for the appropriate matching type</returns>
        internal ITypeSelector GetSelector(PropertyInfo propertyInfo)
        {
            return _matcher.EvaluateSelectors(propertyInfo, _matcher.TypeMap.GetSelectors(propertyInfo.PropertyType));
        }

        /// <summary>
        ///     Returns the base-level selector for the appropriate type for a given property
        /// </summary>
        /// <param name="ts">A selector appropriate for the matching type</param>
        /// <returns>The base-level selector that can bind to type TS</returns>
        internal ITypeSelector GetBaseSelector(Type ts)
        {
            return _matcher.TypeMap.GetBaseSelector(ts);
        }

        #region Static members

        /// <summary>
        ///     Static method for creating new Faker instances, designed to make it less arduous to work with the FluentInterface
        /// </summary>
        public static Fake<T> Create()
        {
            return new Fake<T>();
        }

        #endregion

        public Type SupportedType => typeof (T);
    }
}