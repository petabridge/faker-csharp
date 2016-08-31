using System;
using System.Reflection;
using System.Text.RegularExpressions;
using Faker.Generators;

namespace Faker.Selectors
{
    /// <summary>
    ///     Injects DateTime values between the specified To and From ranges...
    /// </summary>
    public class DateTimeSelector : PrimitiveSelectorBase<DateTime>, IRangeSelector<DateTime>
    {
        public DateTimeSelector()
        {
            From = DateTime.UtcNow.AddDays(-5);
            To = DateTime.UtcNow.AddDays(5);
            Min = () => From;
            Max = () => To;
        }

        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public Func<DateTime> Max { get; set; }
        public Func<DateTime> Min { get; set; }

        #region Overrides of TypeSelectorBase<DateTime>

        public override DateTime Generate()
        {
            return DateTimes.GetDateTime(Min(), Max());
        }

        #endregion
    }

    /// <summary>
    ///     Injects DateTimeOffset values between the specified To and From ranges...
    /// </summary>
    public class DateTimeOffsetSelector : PrimitiveSelectorBase<DateTimeOffset>, IRangeSelector<DateTimeOffset>
    {
        public DateTimeOffsetSelector()
        {
            From = DateTime.UtcNow.AddDays(-5);
            To = DateTime.UtcNow.AddDays(5);
            Min = () => From;
            Max = () => To;
        }

        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public Func<DateTimeOffset> Max { get; set; }
        public Func<DateTimeOffset> Min { get; set; }

        #region Overrides of TypeSelectorBase<DateTimeOffset>

        public override DateTimeOffset Generate()
        {
            return DateTimes.GetDateTimeOffset(Min(), Max());
        }

        #endregion
    }

    /// <summary>
    ///     Injects a timestamp value as a long integer between the specified To and From ranges (expressed as DateTimes)...
    /// </summary>
    public class TimeStampSelector : TypeSelectorBase<long>, IRangeSelector<DateTime>
    {
        private static readonly Regex _regex = new Regex(SpecialFieldsRegex.TimeStampRegex, RegexOptions.IgnoreCase);

        public TimeStampSelector()
        {
            From = DateTime.UtcNow.AddDays(-5);
            To = DateTime.UtcNow.AddDays(5);
            Min = () => From;
            Max = () => To;
        }

        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public Func<DateTime> Max { get; set; }
        public Func<DateTime> Min { get; set; }

        #region Overrides of TypeSelectorBase<long>

        public override bool CanBind(PropertyInfo field)
        {
            return _regex.IsMatch(field.Name);
        }

        public override long Generate()
        {
            return DateTimes.GetTimeStamp(Min(), Max());
        }

        #endregion
    }
}