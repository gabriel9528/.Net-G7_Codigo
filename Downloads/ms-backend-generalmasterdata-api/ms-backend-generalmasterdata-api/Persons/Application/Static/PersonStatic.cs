namespace AnaPrevention.GeneralMasterData.Api.Persons.Application.Static
{
    public static class PersonStatic
    {
        public const int NamesMaxLength = 200;
        public const int DocumentNumberMaxLength = 50;
        public const int LastNameMaxLength = 200;
        public const int SecondLastNameMaxLength = 200;
        public const int PhoneNumberMaxLength = 30;
        public const int EmailMaxLength = 200;
        public const int PersonalPhoneNumberMaxLength = 30;
        public const int PersonalEmailMaxLength = 200;
        public const int SecondDocumentNumberMaxLength = 50;
        public const int EmergencyContactNameMaxLength = 200;
        public const int EmergencyContactNumberPhoneMaxLength = 200;
        public const int EmergencyContactRelationshipMaxLength = 200;
        public const int PersonalAddressMaxLength = 200;
        public const int CountryResidenceMaxLength = 10;
        public const int CountryBirthMaxLength = 10;
        public const int DistrictResidenceMaxLength = 10;
        public const int DistrictBirthMaxLength = 10;

        public const string NamesMsgErrorMaxLength = "Nombres debe ser igual o menor de {0} caracteres";
        public const string EmergencyContactNameMsgErrorMaxLength = "Nombres de Persona de contacto en caso de emergencia debe ser igual o menor de {0} caracteres";
        public const string EmergencyContactNumberPhoneMsgErrorMaxLength = "Telefono de Persona de contacto en caso de emergencia debe ser igual o menor de {0} caracteres";
        public const string EmergencyContactRelationshipMsgErrorMaxLength = "Relacion de Persona de contacto en caso de emergencia debe ser igual o menor de {0} caracteres";
        public const string LastNameMsgErrorMaxLength = "Primer apellido debe ser igual o menor de {0} caracteres";
        public const string SecondLastNameMsgErrorMaxLength = "Segundo Apellido debe ser igual o menor de {0} caracteres";
        public const string DocumentNumberMsgErrorMaxLength = "Numero de documento debe ser igual o menor de {0} caracteres";
        public const string PhoneNumberMsgErrorMaxLength = "Telefono debe ser igual o menor de {0} caracteres";
        public const string PersonalPhoneNumberMsgErrorMaxLength = "Telefono personal debe ser igual o menor de {0} caracteres";
        public const string PersonalEmailMsgErrorMaxLength = "Email personal debe ser igual o menor de {0} caracteres";
        public const string EmailMsgErrorMaxLength = "Email  debe ser igual o menor de {0} caracteres";
        public const string SecondDocumentNumberMsgErrorMaxLength = "Segundo tipo de documento debe ser igual o menor de {0} caracteres";
        public const string PersonalAddressMsgErrorMaxLength = "Direccion debe ser igual o menor de {0} caracteres";

        public const string CountryResidenceMsgErrorMaxLength = "Pais de residencia debe ser igual o menor de {0} caracteres";
        public const string CountryBirthMsgErrorMaxLength = "Pais de Nacimiento debe ser igual o menor de {0} caracteres";
        public const string DistrictResidenceMsgErrorMaxLength = "Ubigeo de residencia debe ser igual o menor de {0} caracteres";
        public const string DistrictBirthMsgErrorMaxLength = "Ubigeo de Nacimiento debe ser igual o menor de {0} caracteres";

        public const string NamesMsgErrorRequiered = "Nombres son obligatoria";
        public const string LastNameMsgErrorRequiered = "Primer apellido es obligatorio";
        public const string DocumentNumberMsgErrorRequiered = "Documento es obligatorio";
        public const string IdMsgErrorRequiered = "Id es obligatorio";
        public const string IdentityDocumentTypeMsgErrorRequiered = "Tipo de documento es obligatorio";
        public const string GenderIdMsgErrorRequiered = "Género es obligatorio";
        public const string DegreeInstructionIdMsgErrorRequiered = "Grado de instrucción es obligatorio";
        public const string MaritalStatusIdMsgErrorRequiered = "Estado Civil es obligatorio";
        public const string PersonalAddressMsgErrorRequiered = "Dirección es obligatoria";
        public const string EmergencyContactNameMsgErrorRequiered = "Nombres de Persona de contacto en caso de emergencia es obligatorios";
        public const string EmergencyContactNumberPhoneMsgErrorRequiered = "Telefono de Persona de contacto en caso de emergencia es obligatorio";
        public const string EmergencyContactRelationshipMsgErrorRequiered = "Relacion de Persona de contacto en caso de emergencia es obligatoria";
        public const string CountryResidenceMsgErrorRequiered = "Pais de residencia es obligatorio";
        public const string CountryBirthMsgErrorRequiered = "Pais de Nacimiento es obligatorio";
        public const string DistrictResidenceMsgErrorRequiered = "Ubigeo de residencia es obligatorio";
        public const string DistrictBirthMsgErrorRequiered = "Ubigeo de Nacimiento es obligatorio";

        public const string DocumentNumberMsgErrorDuplicate = "Ya existe una persona con ese documento";
        public const string DocumentNumberMsgErrorDuplicateAndStatusFalse = "La Persona se Encuentra Inactiva, favor de Comunicarse con el Administrador para Activarla";

        public const string IdentityDocumentTypeMsgErrorNotFound = "Tipo de documento no existe";
        public const string MaritalStatusMsgErrorNotFound = "Estado civil no existe";
        public const string GenderMsgErrorNotFound = "Género no existe";
        public const string DegreeInstructionMsgErrorNotFound = "Grado de instrucción no existe";
        public const string SecondIdentityDocumentTypeMsgErrorNotFound = "Segundo Tipo de documento no existe";
        public const string CountryResidenceMsgErrorNotFound = "Pais de residencia no existe";
        public const string CountryBirthMsgErrorNotFound = "Pais de Nacimiento no existe";
        public const string DistrictResidenceMsgErrorNotFound = "Ubigeo de residencia no existe";
        public const string DistrictBirthMsgErrorNotFound = "Ubigeo de Nacimiento no existe";
        public const string PersonMsgErrorNotFound = "Persona no encontrada";

        public const string PhotoMsgErrorErrorFormart = "Imagen con errores [Foto]";
        public const string PhotoMsgErrorErrorBase64 = "Imagen con errores [Foto]";
        public const string PhotoMsgErrorExtension = "Extension de Archivo invalido [Foto]";

        public const string DateBirth = "Fecha de Nacimiento";


    }
}
