using System;
using System.Reflection;

namespace Faker
{
    /// <summary>
    /// Interface used to produce objects of a given type
    /// </summary>
    public interface ITypeSelector<T>
    {
        /// <summary>
        /// Sets the floor for the minimum range of a type (if applicable)
        /// </summary>
        /// <param name="min">The minimum value that can be generated</param>
        void MinSize(T min);

        /// <summary>
        /// Sets the roof for the maximum range of a type (if applicable)
        /// </summary>
        /// <param name="max">The maximum value that can be generated for this type</param>
        void MaxSize(T max);

        /// <summary>
        /// Determines if we can allow nulls for a given type
        /// </summary>
        /// <param name="canBeNull">If true, we can set nulls - false by default</param>
        void BeNull(bool canBeNull = false);

        /// <summary>
        /// Injects the generator function into the property
        /// </summary>
        /// <param name="property"></param>
        void Generate(PropertyInfo property);
    }

    public abstract class TypeSelectorBase<T> : ITypeSelector<T>
    {

        protected T _min_size;
        protected T _max_size;
        protected bool _can_be_null;

        public void MinSize(T min)
        {
            _min_size = min;
        }

        public void MaxSize(T max)
        {
            _max_size = max;
        }

        public void BeNull(bool canBeNull = false)
        {
            _can_be_null = canBeNull;
        }

        public abstract void Generate(PropertyInfo property);
    }
}
