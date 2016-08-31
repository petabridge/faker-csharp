using System;

namespace Faker.Generators
{
    /// <summary>
    ///     Generator for creating new Guids
    /// </summary>
    public static class Guids
    {
        /// <summary>
        ///     Returns a new Guid
        /// </summary>
        /// <returns>A Guid</returns>
        public static Guid GetGuid()
        {
            return Guid.NewGuid();
        }
    }
}