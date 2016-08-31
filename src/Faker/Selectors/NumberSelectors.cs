using Faker.Generators;

namespace Faker.Selectors
{
    /// <summary>
    ///     Injects doubles into double fields within a range
    /// </summary>
    public sealed class DoubleSelector : NumberSelectorBaseBase<double>
    {
        public DoubleSelector()
        {
            MinSize = double.MinValue;
            MaxSize = double.MaxValue;
        }

        #region Overrides of TypeSelectorBase<double>

        public override double Generate()
        {
            return Numbers.Double(Min(), Max());
        }

        #endregion
    }

    public sealed class IntSelector : NumberSelectorBaseBase<int>
    {
        public IntSelector()
        {
            MinSize = int.MinValue;
            MaxSize = int.MaxValue;
        }

        #region Overrides of TypeSelectorBase<int>

        public override int Generate()
        {
            return Numbers.Int(Min(), Max());
        }

        #endregion
    }

    public sealed class LongSelector : NumberSelectorBaseBase<long>
    {
        public LongSelector()
        {
            MinSize = long.MinValue;
            MaxSize = long.MaxValue;
        }

        #region Overrides of TypeSelectorBase<long>

        public override long Generate()
        {
            return Numbers.Long(Min(), Max());
        }

        #endregion
    }

    public sealed class DecimalSelector : NumberSelectorBaseBase<decimal>
    {
        public DecimalSelector()
        {
            MinSize = new decimal(int.MinValue);
            MaxSize = new decimal(int.MaxValue);
        }

        #region Overrides of TypeSelectorBase<decimal>

        public override decimal Generate()
        {
            return Numbers.Decimal(Min(), Max());
        }

        #endregion
    }

    public sealed class FloatSelector : NumberSelectorBaseBase<float>
    {
        public FloatSelector()
        {
            MinSize = float.MinValue;
            MaxSize = float.MaxValue;
        }

        #region Overrides of TypeSelectorBase<float>

        public override float Generate()
        {
            return Numbers.Float(Min(), Max());
        }

        #endregion
    }
}