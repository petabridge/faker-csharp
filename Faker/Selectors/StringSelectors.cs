using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Faker.Selectors
{
    /// <summary>
    /// Selector used for populating full names for fields that accept full names
    /// </summary>
    public sealed class FullNameSelector : TypeSelectorBase<string>
    {

        public override bool CanBind(PropertyInfo field)
        {
            throw new NotImplementedException();
        }

        public override void Generate(PropertyInfo property)
        {
            throw new NotImplementedException();
        }
    }
}
