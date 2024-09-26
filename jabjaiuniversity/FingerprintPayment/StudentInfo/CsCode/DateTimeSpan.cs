using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FingerprintPayment.StudentInfo.CsCode
{
    public struct DateTimeSpan
    {
        public int Years { get; }
        public int Months { get; }
        public int Days { get; }
        public int Hours { get; }
        public int Minutes { get; }
        public int Seconds { get; }
        public int Milliseconds { get; }

        public DateTimeSpan(int years, int months, int days, int hours = 0, int minutes = 0, int seconds = 0, int milliseconds = 0)
        {
            Years = years;
            Months = months;
            Days = days;
            Hours = hours;
            Minutes = minutes;
            Seconds = seconds;
            Milliseconds = milliseconds;
        }

        enum Phase { Years, Months, Days, Done }

        public static DateTimeSpan CompareDates(DateTime date1, DateTime date2)
        {
            if (date2 < date1)
            {
                var sub = date1;
                date1 = date2;
                date2 = sub;
            }

            DateTime current = date1;
            int years = 0;
            int months = 0;
            int days = 0;

            Phase phase = Phase.Years;
            DateTimeSpan span = new DateTimeSpan();
            int officialDay = current.Day;

            while (phase != Phase.Done)
            {
                switch (phase)
                {
                    case Phase.Years:
                        if (current.AddYears(years + 1) > date2)
                        {
                            phase = Phase.Months;
                            current = current.AddYears(years);
                        }
                        else
                        {
                            years++;
                        }
                        break;
                    case Phase.Months:
                        if (current.AddMonths(months + 1) > date2)
                        {
                            phase = Phase.Days;
                            current = current.AddMonths(months);
                            if (current.Day < officialDay && officialDay <= DateTime.DaysInMonth(current.Year, current.Month))
                                current = current.AddDays(officialDay - current.Day);
                        }
                        else
                        {
                            months++;
                        }
                        break;
                    case Phase.Days:
                        if (current.AddDays(days + 1) > date2)
                        {
                            current = current.AddDays(days);
                            var timespan = date2 - current;
                            span = new DateTimeSpan(years, months, days, timespan.Hours, timespan.Minutes, timespan.Seconds, timespan.Milliseconds);
                            phase = Phase.Done;
                        }
                        else
                        {
                            days++;
                        }
                        break;
                }
            }

            return span;
        }

        public static DateTimeSpan CalculateAge(DateTime date1, DateTime date2)
        {
            // First Date //
            int birthDay = date1.Day;
            int birthMonth = date1.Month;
            int birthYear = date1.Year;

            // Second Date //
            int currentDay = date2.Day;
            int currentMonth = date2.Month;
            int currentYear = date2.Year;

            int[] daysOfMonth = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            if ((currentYear % 4 == 0) && (currentYear % 100 != 0))
            {
                daysOfMonth[1] = 29;
            }

            if (currentDay < birthDay)
            {
                if (currentMonth == 1)
                {
                    currentMonth += 12;
                    currentYear -= 1;
                }
                currentMonth -= 1;
                currentDay += daysOfMonth[currentMonth - 1];
            }

            if (currentMonth < birthMonth)
            {
                currentMonth += 12;
                currentYear -= 1;
            }

            int years = currentYear - birthYear;
            int months = currentMonth - birthMonth;
            int days = currentDay - birthDay;

            return new DateTimeSpan(years, months, days);
        }
    }
}