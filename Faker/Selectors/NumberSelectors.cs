using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Faker.Generators;

namespace Faker.Selectors
{
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
}
