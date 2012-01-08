using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Faker.Generators;

namespace Faker.Selectors
{
    /// <summary>
    /// Type selector used for generating Guid values
    /// </summary>
    public sealed class GuidSelector : TypeSelectorBase<Guid>
    {
        #region Overrides of TypeSelectorBase<Guid>

        public override void Generate(object targetObject, PropertyInfo property)
        {
            property.SetValue(targetObject, Guids.GetGuid(), null);
        }

        #endregion
    }
}
