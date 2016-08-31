using System;
using Faker.Generators;

namespace Faker.Selectors
{
    /// <summary>
    ///     Type selector used for generating Guid values
    /// </summary>
    public sealed class GuidSelector : PrimitiveSelectorBase<Guid>
    {
        #region Overrides of TypeSelectorBase<Guid>

        public override Guid Generate()
        {
            return Guids.GetGuid();
        }

        #endregion
    }
}