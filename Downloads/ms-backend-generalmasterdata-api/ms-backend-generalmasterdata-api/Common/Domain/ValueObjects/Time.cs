using CSharpFunctionalExtensions;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using System.Globalization;

namespace AnaPrevention.GeneralMasterData.Api.Common.Domain.ValueObjects
{
    public class Time : ValueObject
    {
        public string StringValue { get; }
        public DateTime TimeValue { get; }

        private Time(string value)
        {
            StringValue = value;
            TimeValue = CreateTime(value);
        }
        public static DateTime CreateTime(string time = "")
        {
            var culture = CultureInfo.CreateSpecificCulture("es-US");
            var styles = DateTimeStyles.None;
            if (DateTime.TryParse(time, culture, styles, out DateTime timeResult))
            {
                return timeResult;
            }
            return DateTime.MinValue;

        }
        public static Result<Time, Notification> Create(DateTime input)
        {
            return new Time(input.ToShortTimeString());
        }
        public static Result<Time, Notification> Create(string input = "", string description = "")
        {
            Notification notification = new();

            if (string.IsNullOrWhiteSpace(description))
                description = CommonStatic.TimeDescriptionDefault;

            if (string.IsNullOrWhiteSpace(input))
                notification.AddError(string.Format(CommonStatic.TimeMsgErrorRequiered, description));

            string time = input.Trim();

            var culture = CultureInfo.CreateSpecificCulture("es-US");
            var styles = DateTimeStyles.None;
            if (!DateTime.TryParse(time, culture, styles, out DateTime timeResult))
            {
                notification.AddError(string.Format(CommonStatic.TimeMsgErrorInvalidFormat, description));
                return notification;
            }

            return new Time(timeResult.ToLongTimeString());
        }
        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return StringValue;
            yield return TimeValue;
        }
    }
}
