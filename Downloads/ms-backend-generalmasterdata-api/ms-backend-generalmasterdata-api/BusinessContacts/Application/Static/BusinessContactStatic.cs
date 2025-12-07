namespace AnaPrevention.GeneralMasterData.Api.BusinessContacts.Application.Static
{
    public static class BusinessContactStatic
    {
        public const int FirstNameMaxLength = 200;
        public const int LastNameMaxLength = 200;
        public const int PositionMaxLength = 200;
        public const int CellPhoneMaxLength = 200;
        public const int PhoneMaxLength = 200;
        public const int SecondPhoneMaxLength = 200;
        public const int SecondCellPhoneMaxLength = 200;
        public const int EmailMaxLength = 200;

        public const string FirstNameMsgErrorMaxLength = "Los nombres debe ser igual o menor de {0} caracteres";
        public const string LastNameMsgErrorMaxLength = "Los apellidos debe ser igual o menor de {0} caracteres";
        public const string PositionMsgErrorMaxLength = "El cargo debe ser igual o menor de {0} caracteres";
        public const string CellPhoneMsgErrorMaxLength = "Telefono Celular 1 debe ser igual o menor de {0} caracteres";
        public const string PhoneMsgErrorMaxLength = "Telefono  1 debe ser igual o menor de {0} caracteres";
        public const string SecondPhoneMsgErrorMaxLength = "Telefono 2 debe ser igual o menor de {0} caracteres";
        public const string SecondCellPhoneMsgErrorMaxLength = "Telefono Celular 2 debe ser igual o menor de {0} caracteres";
        public const string EmailMsgErrorMaxLength = "Email debe ser igual o menor de {0} caracteres";

        public const string FirstNameMsgErrorRequiered = "Los nombres son obligatorios";
        public const string lastNameMsgErrorRequiered = "Los apellidos son obligatorios";
        public const string IdMsgErrorRequiered = "Id es obligatorio";
        public const string BusinessIdMsgErrorRequiered = "Empresa es obligatoria";


        public const string BusinessIdMsgErrorNotFound = "Empresa no existe";
    }
}
