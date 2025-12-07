namespace AnaPrevention.GeneralMasterData.Api.Businesses.Application.Static
{
    public static class BusinessStatic
    {
        
        public const int TradenameMaxLength = 200;
        public const int AddressMaxLength = 200;
        public const int DocumentNumberMaxLength = 30;

        public const string PermissionId = "4e1d5dd0-52f7-480c-9849-727f5ab7b09b";

        public const string TradenameMsgErrorMaxLength = "Nombre comercial debe ser igual o menor de {0} caracteres";
        public const string AddressMsgErrorMaxLength = "La dirección debe ser igual o menor de {0} caracteres";
        public const string DocumentNumberMsgErrorMaxLength = "La dirección debe ser igual o menor de {0} caracteres";
        
        
        public const string IdMsgErrorRequiered = "Id es obligatorio";
        public const string IdentityDocumentTypeIdMsgErrorRequiered = "Tipo de Documento de identidad es obligatorio";
        public const string DocumentNumberMsgErrorRequiered = "Numero de documento es obligatorio";
        public const string MedicalFormatIdMsgErrorRequiered = "Formato Medico es obligatorio";
        public const string CreditTimeIdMsgErrorRequiered = "Tiempo de Credito es obligatorio";
        public const string DistrictIdMsgErrorRequiered = "Distrito es obligatorio";
        public const string EconomicActivityIdMsgErrorRequiered = "Se debe ingresar un rubro por lo menos";
        public const string BusinessMsgErrorRequiered = "Empresa es obligatorio";


        public const string DocumentNumberMsgErrorDuplicate = "Numero de Documento ya existe";

        public const string IdentityDocumentTypeIdMsgErrorNoFound = "Tipo de Documento no existe";
        public const string MedicalFormatIdMsgErrorNoFound = "Formato Medico no existe";
        public const string CreditTimeIdMsgErrorNoFound = "Tiempo de Credito no existe";
        public const string DistrictIdMsgErrorNoFound = "Distrito no existe";
        public const string BusinessMsgErrorNotFound = "Empresa no existe";
        public const string BusinessSecondMsgErrorNotFound = "SubContrata no existe";
        public const string EconomicActivityIdMsgErrorNoFound = "Rubro no existe";

        public const string DateInscriptionIdMsgErrorFormat = "Formato de fecha errado";

        public const bool BusinessValueTrue = true;
        public const bool BusinessValueFalse = false;
        public const string BusinessUserExternalMedic = "MED";
        public const string BusinessUserExternalResourcesHuman = "RRHH";
    }
}