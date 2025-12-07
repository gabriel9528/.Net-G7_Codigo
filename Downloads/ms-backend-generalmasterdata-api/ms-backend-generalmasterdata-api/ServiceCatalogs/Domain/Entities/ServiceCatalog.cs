using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.ExistenceTypes.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.MeasurementUnits.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.MedicalAreas.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.SubFamilies.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Taxes.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Domain.Entities
{
    public class ServiceCatalog
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public string CodeSecond { get; set; }
        public Guid SubFamilyId { get; set; }
        public SubFamily SubFamily { get; set; }
        public Guid UomId { get; set; }
        public Uom Uom { get; set; }
        public Guid UomSecondId { get; set; }
        public Uom UomSecond { get; set; }
        public Guid ExistenceTypeId { get; set; }
        public ExistenceType ExistenceType { get; set; }
        public Guid TaxId { get; set; }
        public Tax Tax { get; set; }
        public bool IsActive { get; set; }
        public bool IsSales { get; set; }
        public bool IsBuy { get; set; }
        public bool IsInventory { get; set; }
        public bool IsRetention { get; set; }
        public string Comment { get; set; }
        public bool Status { get; set; }
        public int OrderRow { get; set; }
        public int OrderRowTourSheet { get; set; }
        public Guid? MedicalAreaId { get; set; }
        public MedicalArea? MedicalArea { get; set; }
        public int OrderRowLaboratory { get; set; }

        public ServiceCatalog() { }

        public ServiceCatalog(
            string description,
            string code,
            string codeSecond,
            Guid subFamilyId,
            Guid uomId,
            Guid uomSecondId,
            Guid existenceTypeId,
            Guid taxId,
            bool isBuy,
            bool isSales,
            bool isActive,
            bool isInventory,
            bool isRetention,
            string comment,
            Guid id,
            int orderRow = CommonStatic.DefaultOrderRow,
            int orderRowTourSheet = CommonStatic.DefaultOrderRow,
            Guid? medicalAreaId = null,
            int orderRowLaboratory = 9999)
        {
            Description = description;
            Code = code;
            CodeSecond = codeSecond;
            SubFamilyId = subFamilyId;
            UomId = uomId;
            UomSecondId = uomSecondId;
            ExistenceTypeId = existenceTypeId;
            TaxId = taxId;
            IsBuy = isBuy;
            IsSales = isSales;
            IsActive = isActive;
            IsInventory = isInventory;
            Status = true;
            IsRetention = isRetention;
            Comment = comment;
            Id = id;
            OrderRow = orderRow;
            OrderRowTourSheet = orderRowTourSheet;
            MedicalAreaId = medicalAreaId;
            OrderRowLaboratory = orderRowLaboratory;
        }
    }
}
