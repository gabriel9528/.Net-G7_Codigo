using AnaPrevention.GeneralMasterData.Api.Settings.Domain.Enums;

namespace AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Application.Static
{
    public class MedicalFormStatic
    {
        public const string ServiceTypeIdMsgErrorRequiered = "Tipo de servicio es obligatorio";
        public const string MedicalAreaIdMsgErrorRequiered = "Area Medica es obligatorio";
        public const string MedicalFormIdMsgErrorRequiered = "Formulario Medico es obligatorio";

        public const string ServiceTypeIdMsgErrorNotFound = "Tipo de servicio no existe";
        public const string MedicalAreaIdMsgErrorNotFound = "Area Medica no existe";
        public const string MedicalFormMsgErrorNotFound = "Formulario Medico no existe";

        private static readonly Dictionary<OrderFileType, string> OrderFileTypeNames = new()
        {
            { OrderFileType.FINAL_REPORT_FILE, "Informe Final" },
            { OrderFileType.SIMPLE_REPORT_FILE, "Informe Medico" },
            { OrderFileType.MEDICAL_CERTIFICATE_FILE, "Certificado" },
            { OrderFileType.CERTIFICATE_FILE, "Certificado RRHH" },
            { OrderFileType.MEDICAL_PASS, "Pase Medico" },
            { OrderFileType.CONSENT_DOCUMENT, "Consetimiento Informado" },
            { OrderFileType.SWORN_DECLARATION_PENDING_EXAM, "Declaracion Jurada – Examen Pendiente " },

            };


        public static Dictionary<OrderFileType, string> GetOrderFileTypeNames()
        {
            return OrderFileTypeNames;
        }


    }
}
