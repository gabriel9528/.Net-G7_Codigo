namespace AnaPrevention.GeneralMasterData.Api.Emails.EmailUsers.Application.Static
{
    public static class EmailUserStatic
    {
        public const string EmailMsgErrorMaxLength = "Email debe ser igual o menor de {0} caracteres";
        public const string PasswordMsgErrorMaxLength = "Password debe ser igual o menor de {0} caracteres";
        public const string NameMsgErrorMaxLength = "Nombre debe ser igual o menor de {0} caracteres";

        public const string PortMsgErrorMaxLength = "Nombre debe ser igual o menor de {0} caracteres";
        public const string HostMsgErrorMaxLength = "Nombre debe ser igual o menor de {0} caracteres";

        public const string EmailMsgErrorRequiered = "Email es obligatorio";
        public const string PasswordMsgErrorRequiered = "Passowrd es obligatorio";
        public const string NameMsgErrorRequiered = "Nombres es obligatorio";
        public const string PortMsgErrorRequiered = "Puerto es obligatorio";
        public const string HostMsgErrorRequiered = "Host es obligatorio";


        public const string PortMsgErrorFormat = "Puerto no cumple con el formato";
        public const string HostMsgErrorFormat = "Host no cumple con el formato";

        public const string EmailMsgErrorDuplicate = "Email ya existe";
        public const string NameMsgErrorDuplicate = "Nombre ya existe";
    }
}
