using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Faker.Generators;

namespace Faker.Selectors
{
    /// <summary>
    /// Injects doubles into double fields within a range
    /// </summary>
    public sealed class DoubleSelector : TypeSelectorBase<double>
    {
        public DoubleSelector()
        {
            MinSize = Double.MinValue;
            MaxSize = Double.MaxValue;
        }

        public double MaxSize { get; set; }
        public double MinSize { get; set; }

        #region Overrides of TypeSelectorBase<double>

        public override void Generate(object targetObject, PropertyInfo property)
        {
            property.SetValue(targetObject, Numbers.Double(MinSize, MaxSize), null);
        }

        #endregion
    }

    public sealed class IntSelector : TypeSelectorBase<int>
    {
        public IntSelector()
        {
            MinSize = Int32.MinValue;
            MaxSize = Int32.MaxValue;
        }

        public int MaxSize { get; set; }
        public int MinSize { get; set; }

        #region Overrides of TypeSelectorBase<int>

        public override void Generate(object targetObject, PropertyInfo property)
        {
            property.SetValue(targetObject, Numbers.Int(MinSize, MaxSize), null);
        }

        #endregion
    }
}
