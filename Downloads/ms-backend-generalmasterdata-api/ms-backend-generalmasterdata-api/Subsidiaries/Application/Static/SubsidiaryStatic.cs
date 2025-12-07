namespace AnaPrevention.GeneralMasterData.Api.Subsidiaries.Application.Static
{
    public static class SubsidiaryStatic
    {
        public const string DirectoryLogo = "logos/";
        public const int AddressMaxLength = 200;


        public const string AddressMsgErrorMaxLength = "Dirección debe ser igual o menor de {0} caracteres";

        public const string CompanyIdMsgErrorRequiered = "Compañia es obligatorio";
        public const string DistrictIdMsgErrorRequiered = "Distrito es obligatorio";
        public const string ServicesTypeIdMsgErrorRequiered = "Se necesita por lo menos un Tipo de servicio";
        public const string SubsidiaryTypeIdMsgErrorRequiered = "Tipo de sede es obligatoria";
        public const string AddressMsgErrorRequiered = "Dirección es obligatoria";
        public const string CapacityMsgErrorRequiered = "Aforo es obligatorio";
        public const string DoctorIdMsgErrorRequiered = "Doctor encargo es obligatorio";
        public const string SubsidiaryMsgErrorRequiered = "Sede es obligatoria";
        public const string OfficeHourMsgErrorRequiered = "Por lo menos se debe ingrear un horario de atencion";


        public const string EmailFormatMsgError = "uno o varios Correos ingresados en {0} no tiene el formato correcto";

        public const string DistributionList = "Lista de correos General";
        public const string DistributionListLaboratory = "Lista de correos de laboratorio";

        public const string DistrictMsgErrorNotFound = "Distrito no encontrado";
        public const string SubsidiaryMsgErrorNotFound = "Sede no existe";
        public const string ServiceTypeMsgErrorNotFound = "Tipo de servicio no encontrado";
        public const string SubsidiaryTypeMsgErrorNotFound = "Tipo de sede no existe";
        public const string DoctorMsgErrorNotFound = "Doctor no encontrado";


        public const string HourMsgErrorFormat = "Error en formato de hora del horario de atencion";
        public const string DaysMsgErrorFormat = "Error en formato de los dias del horario de atencion";
    }
}
