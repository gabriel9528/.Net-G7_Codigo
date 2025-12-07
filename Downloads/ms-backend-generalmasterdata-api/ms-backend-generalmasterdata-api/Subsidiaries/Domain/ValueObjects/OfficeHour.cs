using CSharpFunctionalExtensions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AnaPrevention.GeneralMasterData.Api.Subsidiaries.Domain.ValueObjects
{
    public class OfficeHour : ValueObject
    {
        public DayOfWeek StartDay { get; }
        public DayOfWeek FinishDay { get; }
        public TimeOnly HourStart { get; }
        public TimeOnly HourFinish { get; }

        public OfficeHour(
            DayOfWeek startDay,
            DayOfWeek finishDay,
            int hourStart,
            int minuteStart,
            int hourFinish,
            int minuteFinish
            )
        {
            StartDay = startDay;
            FinishDay = finishDay;
            HourStart = new TimeOnly(hourStart, minuteStart);
            HourFinish = new TimeOnly(hourFinish, minuteFinish);

        }
        public OfficeHour(
            int startDay,
            int finishDay,
            int hourStart,
            int minuteStart,
            int hourFinish,
            int minuteFinish
            )
        {
            StartDay = (DayOfWeek)startDay;
            FinishDay = (DayOfWeek)finishDay;
            HourStart = new TimeOnly(hourStart, minuteStart);
            HourFinish = new TimeOnly(hourFinish, minuteFinish);
        }

        public Dictionary<string, string> convertToDictionary()
        {
            Dictionary<string, string> list = new Dictionary<string, string>()
            {
                { "StartDay",((int)StartDay).ToString() },
                { "FinishDay",((int)FinishDay).ToString() },
                { "HourStart",HourStart.ToShortTimeString() },
                { "HourFinish",HourFinish.ToShortTimeString() }

            };
            return list;
        }

        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return StartDay;
            yield return FinishDay;
            yield return HourStart;
            yield return HourFinish;
        }
    }
}
