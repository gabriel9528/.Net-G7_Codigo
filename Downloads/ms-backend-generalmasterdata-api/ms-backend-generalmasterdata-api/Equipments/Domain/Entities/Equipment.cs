using AnaPrevention.GeneralMasterData.Api.Companies.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.MedicalAreas.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Persons.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Subsidiaries.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.Equipments.Domain.Entities
{
    public class Equipment
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Supplier { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string SerialNumber { get; set; }

        public MedicalArea MedicalArea { get; set; }
        public Guid MedicalAreaId { get; set; }
        public Subsidiary Subsidiary { get; set; }
        public Guid SubsidiaryId { get; set; }

        public Person Person { get; set; }
        public Guid PersonDeviceManagerId { get; set; }

        public Company Company { get; set; }
        public Guid CompanyId { get; set; }
        public bool Status { get; set; }

        public Equipment() { }

        public Equipment(string description, string brand, string model, string serialNumber, Guid medicalAreaId, Guid subsidiaryId, string code, Guid companyId, string supplier, Guid personDeviceManagerId, Guid id)
        {
            Description = description;
            Status = true;
            Brand = brand;
            Model = model;
            SerialNumber = serialNumber;
            MedicalAreaId = medicalAreaId;
            SubsidiaryId = subsidiaryId;
            Code = code;
            CompanyId = companyId;
            Supplier = supplier;
            PersonDeviceManagerId = personDeviceManagerId;
            Id = id;
        }
    }
}
