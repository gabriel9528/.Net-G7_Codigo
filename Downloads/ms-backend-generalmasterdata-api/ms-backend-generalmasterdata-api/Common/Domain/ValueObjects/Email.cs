using CSharpFunctionalExtensions;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using System.Text.RegularExpressions;

namespace AnaPrevention.GeneralMasterData.Api.Common.Domain.ValueObjects
{
    public class Email : ValueObject
    {
        public string Value { get; }

        private Email(string value)
        {
            Value = value;
        }

        public static Result<Email, Notification> Create(string input)
        {
            Notification notification = new();

            if (string.IsNullOrWhiteSpace(input))
                notification.AddError(CommonStatic.EmailMsgErrorRequiered);

            string email = input.Trim();

            if (email.Length > CommonStatic.EmailMaxLength)
                notification.AddError(CommonStatic.EmailMsgErrorInvalidLength);

            if (Regex.IsMatch(email, @"^(.+)@(.+)$") == false)
                notification.AddError(CommonStatic.EmailMsgErrorInvalidFormat);

            if (notification.HasErrors())
                return notification;

            return new Email(email);
        }

        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return Value;
        }

        internal static object Create(object email)
        {
            throw new NotImplementedException();
        }
    }
}
