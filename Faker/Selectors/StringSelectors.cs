using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Faker.Generators;

namespace Faker.Selectors
{
    /// <summary>
    /// Selector used for populating full names for fields that accept full names
    /// </summary>
    public sealed class FullNameSelector : TypeSelectorBase<string>
    {
        private static Regex _regex = new Regex(SpecialFieldsRegex.FullNameRegex);

        public override bool CanBind(PropertyInfo field)
        {
            return _regex.IsMatch(field.Name);
        }

        public override void Generate(object targetObject, PropertyInfo property)
        {
            property.SetValue(targetObject, Names.FullName(), null);
        }
    }
}
