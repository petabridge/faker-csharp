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
    /// Type selector for generating standard strings
    /// </summary>
    public sealed class StringSelector : TypeSelectorBase<string>
    {
        /// <summary>
        /// By default, we constrain the length of the string to be between 10 and 40 characters
        /// </summary>
        public StringSelector()
        {
            MaxSize = 40;
            MinSize = 10;
        }

        #region Overrides of TypeSelectorBase<string>

        public override void Generate(object targetObject, PropertyInfo property)
        {
            var max = (int) MaxSize;
            var min = (int) MinSize;


            property.SetValue(targetObject, Strings.GenerateString(max), null);
        }

        #endregion
    }

    /// <summary>
    /// Type selector for first names...
    /// </summary>
    public sealed class LastNameSelector : TypeSelectorBase<string>
    {
        private static readonly Regex _regex = new Regex(SpecialFieldsRegex.LastNameRegex, RegexOptions.IgnoreCase);

        #region Overrides of TypeSelectorBase<string>

        public override bool CanBind(PropertyInfo field)
        {
            return _regex.IsMatch(field.Name);
        }

        public override void Generate(object targetObject, PropertyInfo property)
        {
            property.SetValue(targetObject, Names.Last(), null);
        }

        #endregion
    }

    /// <summary>
    /// Type selector for first names...
    /// </summary>
    public sealed class FirstNameSelector : TypeSelectorBase<string>
    {
        private static readonly Regex _regex = new Regex(SpecialFieldsRegex.FirstNameRegex, RegexOptions.IgnoreCase);

        #region Overrides of TypeSelectorBase<string>

        public override bool CanBind(PropertyInfo field)
        {
            return _regex.IsMatch(field.Name);
        }

        public override void Generate(object targetObject, PropertyInfo property)
        {
            property.SetValue(targetObject, Names.First(), null);
        }

        #endregion
    }

    /// <summary>
    /// Selector used for populating full names for fields that accept full names
    /// </summary>
    public sealed class FullNameSelector : TypeSelectorBase<string>
    {
        private static readonly Regex _regex = new Regex(SpecialFieldsRegex.FullNameRegex, RegexOptions.IgnoreCase);

        public override bool CanBind(PropertyInfo field)
        {
            return _regex.IsMatch(field.Name);
        }

        public override void Generate(object targetObject, PropertyInfo property)
        {
            property.SetValue(targetObject, Names.FullName(), null);
        }
    }

    public sealed class EmailSelector : TypeSelectorBase<string>
    {
        private static readonly Regex _regex = new Regex(SpecialFieldsRegex.EmailRegex, RegexOptions.IgnoreCase);

        #region Overrides of TypeSelectorBase<string>

        public override bool CanBind(PropertyInfo field)
        {
            return _regex.IsMatch(field.Name);
        }

        public override void Generate(object targetObject, PropertyInfo property)
        {
            property.SetValue(targetObject, EmailAddresses.Human(), null);
        }

        #endregion
    }
}
