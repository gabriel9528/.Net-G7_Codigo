using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;

using System.Net.Mail;

namespace AnaPrevention.GeneralMasterData.Api.Common.Application.Validators
{
    public class Validator
    {

        protected void ValidatorString(
            Notification notification, string? value, int maxLength, string maxLengthMsg, string requieredMsg = "", bool isRequiered = false)
        {
            value = string.IsNullOrWhiteSpace(value) ? "" : value.Trim();

            if (isRequiered && string.IsNullOrWhiteSpace(value))
                notification.AddError(requieredMsg);

            if (value.Length > maxLength)
                notification.AddError(String.Format(maxLengthMsg, maxLength.ToString()));
        }
        protected void ValidatorImage(Notification notification,string image,string description= "")
        {
            image = string.IsNullOrWhiteSpace(image) ? "" : image.Trim();

            if (!string.IsNullOrWhiteSpace(image))
            {
                if (image.Contains("data:image"))
                {
                    int index = image.IndexOf('/') + 1;
                    string fileExtension = image[index..image.LastIndexOf(';')];
                    image = image[(image.LastIndexOf(',') + 1)..];
                    if (!CommonStatic.ImageFormartAccepted.Contains(fileExtension.ToUpper()))
                        notification.AddError(string.Format(CommonStatic.ImageMsgErrorExtension,description));

                    if (string.IsNullOrEmpty(image))
                    {
                        notification.AddError(string.Format(CommonStatic.ImageMsgErrorErrorBase64, description));
                    }

                    if (!Convert.TryFromBase64String(image, new(new byte[image.Length]), out _))
                        notification.AddError(string.Format(CommonStatic.ImageMsgErrorErrorBase64, description));
                }
                else
                    notification.AddError(string.Format(CommonStatic.ImageMsgErrorErrorFormart, description));

                
            }
        }

        protected void ValidatorFile(Notification notification, string base64File, string description = "")
        {
            base64File = string.IsNullOrWhiteSpace(base64File) ? "" : base64File.Trim();

            if (!string.IsNullOrWhiteSpace(base64File))
            {
                if (base64File.Contains("data:"))
                {
                    int index = base64File.IndexOf('/') + 1;
                    string fileExtension = base64File[index..base64File.LastIndexOf(';')];
                    base64File = base64File[(base64File.LastIndexOf(',') + 1)..];

                    string extension = fileExtension.ToUpper();

                    if (!CommonStatic.ImageFormartAccepted.Contains(extension))
                        notification.AddError(string.Format(CommonStatic.FileMsgErrorExtension, description));

                    if (!Convert.TryFromBase64String(base64File, new(new byte[base64File.Length]), out _))
                        notification.AddError(string.Format(CommonStatic.FileMsgErrorErrorBase64, description));

                }
                else
                    notification.AddError(string.Format(CommonStatic.FileMsgErrorErrorFormart, description));
            }
        }
          public static bool IsValidEmail(string email)
        {
            try
            {
                var mailAddress = new MailAddress(email);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
