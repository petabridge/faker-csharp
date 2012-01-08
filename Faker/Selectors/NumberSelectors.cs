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
    public sealed class DoubleSelector : NumberSelectorBase<double>
    {
        public DoubleSelector()
        {
            MinSize = Double.MinValue;
            MaxSize = Double.MaxValue;
        }

        #region Overrides of TypeSelectorBase<double>

        public override double Generate()
        {
            return Numbers.Double(MinSize, MaxSize);
        }

        #endregion
    }

    public sealed class IntSelector : NumberSelectorBase<int>
    {
        public IntSelector()
        {
            MinSize = Int32.MinValue;
            MaxSize = Int32.MaxValue;
        }

        #region Overrides of TypeSelectorBase<int>

        public override int Generate()
        {
            return Numbers.Int(MinSize, MaxSize);
        }

        #endregion
    }

    public sealed class LongSelector : NumberSelectorBase<long>
    {
        public LongSelector()
        {
            MinSize = Int64.MinValue;
            MaxSize = Int64.MaxValue;
        }

        #region Overrides of TypeSelectorBase<long>

        public override long Generate()
        {
            return Numbers.Long(MinSize, MaxSize);
        }

        #endregion
    }

    public sealed class DecimalSelector : NumberSelectorBase<decimal>
    {
        public DecimalSelector()
        {
            MinSize = new decimal(Int32.MinValue);
            MaxSize = new decimal(Int32.MaxValue);
        }

        #region Overrides of TypeSelectorBase<decimal>

        public override decimal Generate()
        {
            return Numbers.Decimal(MinSize, MaxSize);
        }

        #endregion
    }

    public sealed class FloatSelector : NumberSelectorBase<float>
    {
        public FloatSelector()
        {
            MinSize = float.MinValue;
            MaxSize = float.MaxValue;
        }

        #region Overrides of TypeSelectorBase<float>

        public override float Generate()
        {
            return Numbers.Float(MinSize, MaxSize);
        }

        #endregion
    }
}
