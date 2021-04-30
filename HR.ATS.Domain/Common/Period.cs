using System;
using System.Collections.Generic;

namespace HR.ATS.Domain.Common
{
    public abstract class PeriodBase : ValueObject
    {
        protected static void CheckPeriod(DateTime? startDate, DateTime? endDate)
        {
            if (startDate > endDate)
                throw new ArgumentException(nameof(startDate));
        }
    }

    public class OpenEndedPeriod : PeriodBase
    {
        public OpenEndedPeriod(DateTime startDate, DateTime? endDate)
        {
            CheckPeriod(startDate, endDate);
            StartDate = CheckStartDate(startDate);
            EndDate = CheckEndDate(endDate);
        }

        public DateTime StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }

        private static DateTime CheckStartDate(DateTime startDate)
        {
            if (startDate == default) throw new ArgumentNullException(nameof(startDate));

            return startDate;
        }

        private static DateTime? CheckEndDate(DateTime? endDate)
        {
            if (endDate is not null && endDate == default) throw new ArgumentNullException(nameof(endDate));

            return endDate;
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return StartDate;
            yield return EndDate;
        }

        public static implicit operator OpenEndedPeriod((DateTime startDate, DateTime? endDate) value)
        {
            return new(value.startDate, value.endDate);
        }
    }
}