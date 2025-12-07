
using System.ComponentModel;

namespace AnaPrevention.GeneralMasterData.Api.Persons.Application.Dtos
{
    [Category("Paciente")]
    public class PersonDto
    {
        public Guid PersonId { get; set; }
        public Guid Id { get; set; }
        [Description("Numero de Documento")]
        public string DocumentNumber { get; set; } = string.Empty;
        public Guid? IdentityDocumentTypeId { get; set; }
        [Description("Tipo de Documento")]
        public string? IdentityDocumentTypeDescription { get; set; } = string.Empty;

        [Description("Tipo de Documento Abreviatura")] public string? IdentityDocumentTypeAbbreviation { get; set; } = string.Empty;
        [Description("Nombres")]
        public string? Names { get; set; } = string.Empty;
        [Description("Nombre")]
        public string? Name { get; set; } = string.Empty;
        [Description("Apellido Paterno")]
        public string? LastName { get; set; } = string.Empty;
        [Description("Apellido Materno")]
        public string? SecondLastName { get; set; } = string.Empty;
        [Description("Nro de Telefono")]
        public string? PhoneNumber { get; set; } = string.Empty;
        [Description("Email")]
        public string? Email { get; set; } = string.Empty;
        public Guid? GenderId { get; set; }
        [Description("Sexo")]
        public string? Gender { get; set; } = string.Empty;
        [Description("Fecha de naciemiento")]
        public string? DateBirth { get; set; } = string.Empty;
        [Description("Email Personal")]
        public string? PersonalEmail { get; set; } = string.Empty;
        [Description("Numero Personal")]
        public string? PersonalPhoneNumber { get; set; } = string.Empty;

        public string? SecondDocumentNumber { get; set; } = string.Empty;
        public Guid? SecondIdentityDocumentTypeId { get; set; }

        public string? SecondIdentityDocumentType { get; set; } = string.Empty;
        [Description("Direccion")]
        public string? PersonalAddress { get; set; } = string.Empty;
        [Description("Contacto de Emergencia - Nombre")]
        public string? EmergencyContactName { get; set; } = string.Empty;
        [Description("Contacto de Emergencia - Nro Telefono")]
        public string? EmergencyContactNumberPhone { get; set; } = string.Empty;
        [Description("Contacto de Emergencia - Relacion")]
        public string? EmergencyContactRelationship { get; set; } = string.Empty;
        public Guid? DegreeInstructionId { get; set; }
        [Description("Grado de instruccion")]
        public string? DegreeInstruction { get; set; } = string.Empty;
        public Guid? MaritalStatusId { get; set; }
        [Description("Estado Civil")]
        public string? MaritalStatus { get; set; } = string.Empty;

        public string? CountryResidenceId { get; set; } = string.Empty;
        [Description("Lugar de Residencia")]
        public string? CountryResidence { get; set; } = string.Empty;

        public string? CountryBirthId { get; set; } = string.Empty;
        [Description("Lugar de Nacimiento")]
        public string? CountryBirth { get; set; } = string.Empty;

        public string? DistrictResidenceId { get; set; } = string.Empty;
        [Description("Distrito De Residencia")]
        public string? DistrictResidence { get; set; } = string.Empty;

        public string? ProvinceResidenceId { get; set; } = string.Empty;
        [Description("Provincia de Residencia")]
        public string? ProvinceResidence { get; set; } = string.Empty;

        public string? DepartmentResidenceId { get; set; } = string.Empty;
        [Description("Departamento de Residencia")]
        public string? DepartmentResidence { get; set; } = string.Empty;

        public string? DistrictBirthId { get; set; } = string.Empty;
        [Description("Distrito de Nacimiento")]
        public string? DistrictBirth { get; set; } = string.Empty;

        public string? ProvinceBirthId { get; set; } = string.Empty;
        [Description("Provincia de Nacimiento")]
        public string? ProvinceBirth { get; set; } = string.Empty;

        public string? DepartmentBirthId { get; set; } = string.Empty;
        [Description("Departamento de Nacimiento")]
        public string? DepartmentBirth { get; set; } = string.Empty;

        public bool ExistDoctor { get; set; }
        public bool ExistUser { get; set; }
        public bool Status { get; set; }
        [Description("Autorizacion para Procesamiento de Datos")]
        public bool DataProcessingAuthorization { get; set; }
        [Description("Seguro de Salud")] public List<OptionYesOrNot>? HealthInsurance { get; set; }
    }
}
