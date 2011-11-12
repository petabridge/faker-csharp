using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Faker.Selectors;

namespace Faker
{
    /// <summary>
    /// Used for dynamically generating fakes of simple POCO objects
    /// </summary>
    public class Fake<T> where T : new()
    {
        private IList<ITypeSelector> _selectors;

        public Fake()
        {
            _selectors = new List<ITypeSelector>();
        }

        /// <summary>
        /// Generates a single fake value for a given type
        /// </summary>
        /// <returns>A populated instance of a given class</returns>
        T Generate()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Generates a list of fake values for a given type
        /// </summary>
        /// <returns>A list of populated instances with length [count] of a given class</returns>
        IList<T> Generate(int count)
        {
            throw new NotImplementedException();
        }
    }
}