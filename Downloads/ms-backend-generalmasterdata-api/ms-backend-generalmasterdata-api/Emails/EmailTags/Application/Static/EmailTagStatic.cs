using AnaPrevention.GeneralMasterData.Api.Emails.EmailTags.Domain.Enum;

namespace AnaPrevention.GeneralMasterData.Api.Emails.EmailTags.Application.Static
{
    public static class EmailTagStatic
    {
        public const string DescriptionMsgErrorMaxLength = "Descripcion debe ser igual o menor de {0} caracteres";
        public const string TagMsgErrorMaxLength = "Tag debe ser igual o menor de {0} caracteres";
        public const string NameMsgErrorMaxLength = "Nombre debe ser igual o menor de {0} caracteres";

        public const string DescriptionMsgErrorRequiered = "Descripcion es obligatorio";
        public const string TagMsgErrorRequiered = "Tag es obligatorio";
        public const string NameMsgErrorRequiered = "Nombre de campo es obligatorio";

        public const string DescriptionMsgErrorDuplicate = "Ya existe una TAG con esta descripcion";
        public const string TagMsgErrorDuplicate = "Tag ya existe";

        private static readonly Dictionary<EmailTagTemplateType, string> EmailTagTemplateTypeNames = new()
        {
            { EmailTagTemplateType.OCCUPATIONAL_APPOINTMENT, "Citas - Ocupacional" },
            { EmailTagTemplateType.OCCUPATIONAL_ORDER, "Ordenes de Atencion - Ocupacional" },
            { EmailTagTemplateType.CREDENTIALS, "Credenciales" },
        };

        public static Dictionary<EmailTagTemplateType, string> GetEmailTagTemplateType()
        {
            return EmailTagTemplateTypeNames;
        }
        public static string GetEmailTagTemplateTypeName(EmailTagTemplateType? result)
        {
            if (result == null)
            {
                return string.Empty;
            };

            if (EmailTagTemplateTypeNames.TryGetValue((EmailTagTemplateType)result, out string? name))
            {
                return name ?? string.Empty;
            }
            else
            {
                return "No definido";
            }
        }
    }
}
