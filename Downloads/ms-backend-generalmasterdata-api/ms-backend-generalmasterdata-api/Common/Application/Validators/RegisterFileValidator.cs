using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.Common.Application.Validators
{
    public class RegisterFileValidator: Validator
    {
        public Notification ValidateFile(string base64File, string description = "")
        {
            Notification notification = new();

            ValidatorFile(notification, base64File, description);

            return notification;
        }

        public Notification ValidateImage(string base64File, string description = "")
        {
            Notification notification = new();

            ValidatorImage(notification, base64File, description);

            return notification;
        }
    }
}
