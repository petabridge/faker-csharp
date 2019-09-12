using System;
using System.Collections.Generic;
using Faker.Selectors;

namespace Faker
{
    /// <summary>
    /// Generic interface for Fake types
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IFake<T> : IFake
    {
        /// <summary>
        ///     Generates a single fake value for a given type
        /// </summary>
        /// <returns>A populated instance of a given class</returns>
        new T Generate();

        /// <summary>
        ///     Generates a list of fake values for a given type
        /// </summary>
        /// <returns>A list of populated instances with length [count] of a given class</returns>
        new IList<T> Generate(int count);

        /// <summary>
        ///     Adds a selector to the TypeTable; User-defined selectors always take precedence over the built-in ones.
        /// </summary>
        /// <typeparam name="TS">The type that matches the selector</typeparam>
        /// <param name="selector">A TypeSelectorBase instance for all instances of a TS type</param>
        void AddSelector<TS>(ITypeSelector<TS> selector);
    }

    /// <summary>
    /// Non-generic version of <see cref="IFake"/> interface.
    /// </summary>
    public interface IFake
    {
        /// <summary>
        /// The concrete type yielded by this <see cref="IFake"/> instance.
        /// </summary>
        Type SupportedType { get; }

        /// <summary>
        ///     Generates a single fake value for <see cref="SupportedType"/>
        /// </summary>
        /// <returns>A populated instance of a given class</returns>
        object Generate();

        /// <summary>
        ///     Generates a list of fake values for <see cref="SupportedType"/>
        /// </summary>
        /// <returns>A list of populated instances with length [count] of a given class</returns>
        IList<object> Generate(int count);
    }
}