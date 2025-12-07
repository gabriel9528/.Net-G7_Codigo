namespace AnaPrevention.GeneralMasterData.Api.Doctors.Application.Static
{
    public static class DoctorStatic
    {

        public const int CodeMaxLength = 10;
        public const int DescriptionCertificationsMaxLength = 200;

        public const string CodeMsgErrorMaxLength = "CPM debe ser igual o menor de {0} caracteres";
        public const string DescriptionCertificationsMsgErrorMaxLength = "Descripcion del certificado debe ser igual o menor de {0} caracteres";


        public const string CodeMsgErrorRequiered = "CPM es obligatorio";
        public const string IdMsgErrorRequiered = "Id de Doctor es obligatorio";
        public const string PersonIdMsgErrorRequiered = "Persona es obligatoria";
        public const string DescriptionCertificationsMsgErrorRequiered = "Descripcion Certificado es obligatoria";
        public const string DateCertificationsMsgErrorRequiered = "Fecha del certificado es obligatoria";

        public const string DateCertificationsMsgErrorFormat = "Formato de la Fecha tiene errores";

        public const string CodeMsgErrorDuplicate = "CMP ya existe";
        public const string CodeSpecialtyMsgErrorDuplicate = "RNE de especialidad ya existe";

        public const string SignsMsgErrorErrorFormart = "Imagen con errores [Firma]";
        public const string SignsMsgErrorErrorBase64 = "Imagen con errores [Firma]";
        public const string SignsMsgErrorExtension = "Extension de Archivo invalido [Firma]";

        public const string PhotoMsgErrorErrorFormart = "Imagen con errores [Foto]";
        public const string PhotoMsgErrorErrorBase64 = "Imagen con errores [Foto]";
        public const string PhotoMsgErrorExtension = "Extension de Archivo invalido [Foto]";

        public const string PersonMsgErrorNotFound = "Persona no existe";
        public const string DoctorMsgErrorNotFound = "Doctor no existe";
        public const string DoctorUserMsgErrorNotFound = "Usuario logeado no esta asoacido como Doctor";
        public const string DoctorSpecialtyMsgErrorNotFound = "Especialidad no existe";
        public const string MedicalAreaMsgErrorNotFound = "Area Medica no existe";

        public const string PersonTypeMsgErrorAssigned = "La persona ya esta asignada a un Medico";


    }
}
