using CSharpFunctionalExtensions;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Common.Tools.Class;
using System.Globalization;

namespace AnaPrevention.GeneralMasterData.Api.Common.Domain.ValueObjects
{
    public class Date : ValueObject
    {
        public string StringValue { get; }
        public DateTime DateTimeValue { get; }

        private Date(string value)
        {
            StringValue = value;
            DateTimeValue = CreateDateTime(value);
        }
        public static DateTime CreateDateTime(string date = "")
        {
            var culture = CultureInfo.CreateSpecificCulture("es-US");
            var styles = DateTimeStyles.None;
            if (DateTime.TryParse(date, culture, styles, out DateTime dateResult))
            {
                return dateResult;
            }
            return DateTimePersonalized.NowPeru;
            
        }

        public static Date Create()
        {
            return new Date(DateTimePersonalized.NowPeru.ToString("yyyy-MM-dd"));
        }
        public static Result<Date, Notification> Create(DateTime input)
        {
            return new Date(input.ToString("yyyy-MM-dd"));
        }
        public static Result<Date, Notification> Create(string input = "", string description = "")
        {
            Notification notification = new();

            if (string.IsNullOrWhiteSpace(description))
                description = CommonStatic.DateDescriptionDefault;

            if (string.IsNullOrWhiteSpace(input))
                notification.AddError(string.Format(CommonStatic.DateMsgErrorRequiered, description));

            string date = input.Trim();

            var culture = CultureInfo.CreateSpecificCulture("es-US");
            var styles = DateTimeStyles.None;
            if (!DateTime.TryParse(date, culture, styles, out DateTime dateResult))
            {
                notification.AddError(string.Format(CommonStatic.DateMsgErrorInvalidFormat, description));
                return notification;
            }

            DateTime dateMin = new(CommonStatic.YearMin, CommonStatic.MonthMin, CommonStatic.DayMin);

            if(dateMin > dateResult)
            {
                notification.AddError(string.Format(CommonStatic.DateMsgErrorMinError, description, dateMin.ToString("yyyy-MM-dd")));
                return notification;
            }

            return new Date(dateResult.ToString("yyyy-MM-dd"));
        }

        public static Result<Date, Notification> CreateWhithTime(string input = "", string description = "")
        {
            Notification notification = new();

            if (string.IsNullOrWhiteSpace(description))
                description = CommonStatic.DateDescriptionDefault;

            if (string.IsNullOrWhiteSpace(input))
                notification.AddError(string.Format(CommonStatic.DateMsgErrorRequiered, description));

            string date = input.Trim();

            var culture = CultureInfo.CreateSpecificCulture("es-US");
            var styles = DateTimeStyles.None;
            if (!DateTime.TryParse(date, culture, styles, out DateTime dateResult))
            {
                notification.AddError(string.Format(CommonStatic.DateMsgErrorInvalidFormat, description));
                return notification;
            }

            DateTime dateMin = new(CommonStatic.YearMin, CommonStatic.MonthMin, CommonStatic.DayMin);

            if (dateMin > dateResult)
            {
                notification.AddError(string.Format(CommonStatic.DateMsgErrorMinError, description, dateMin.ToString("yyyy-MM-dd HH:mm:ss")));
                return notification;
            }

            return new Date(dateResult.ToString("yyyy-MM-dd  HH:mm:ss"));
        }

        public static Result<DateTime, Notification> CreateLocalDateTime(string input = "")
        {
            Notification notification = new();                
            string date = input.Trim();

            var culture = CultureInfo.CreateSpecificCulture("es-PE");
            var timeZone = TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time");
            var styles = DateTimeStyles.None;

            if (DateTime.TryParse(date, culture, styles, out DateTime dateResult))
            {
                return TimeZoneInfo.ConvertTime(dateResult, timeZone);
            }

            return TimeZoneInfo.ConvertTime(DateTimePersonalized.NowPeru, timeZone);
        }

        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return StringValue;
            yield return DateTimeValue;
        }
    }
}
