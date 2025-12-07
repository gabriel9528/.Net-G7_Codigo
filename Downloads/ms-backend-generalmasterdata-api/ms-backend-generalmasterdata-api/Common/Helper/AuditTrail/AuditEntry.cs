using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Enums;
using AnaPrevention.GeneralMasterData.Api.Businesses.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Common.Tools.Class;
using AnaPrevention.GeneralMasterData.Api.Doctors.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Persons.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Domain.Entities;
using Newtonsoft.Json;
using AnaPrevention.GeneralMasterData.Api.Fields.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.Common.Helper.AuditTrail
{
    public class AuditEntry
    {
        public EntityEntry Entry { get; }
        public AuditType AuditType { get; set; }
        public Guid UserId { get; set; }
        public Guid ItemId { get; set; }
        public string TableName { get; set; }
        public Dictionary<string, object>
               KeyValues
        { get; } = [];
        public Dictionary<string, object>
               OldValues
        { get; } = [];
        public Dictionary<string, object>
               NewValues
        { get; } = [];
        public List<string> ChangedColumns { get; } = new List<string>();

        public AuditEntry(EntityEntry entry, Guid userId, AuditType auditType)
        {
            Entry = entry;
            this.UserId = userId;
            SetChanges(auditType);
        }

        public AuditEntry(EntityEntry entry, Guid userId)
        {
            Entry = entry;
            UserId = userId;
            SetChanges();
        }
        private void SetChanges(AuditType auditType)
        {

            TableName = Entry.Entity.GetType().Name;
            foreach (PropertyEntry property in Entry.Properties)
            {
                string propertyName = property.Metadata.Name;

                _ = Guid.TryParse(property.CurrentValue?.ToString(), out var itemId);

                if (property.Metadata.IsPrimaryKey())
                {
                    KeyValues[propertyName] = property.CurrentValue ?? "";
                    ItemId = itemId;
                    continue;
                }
                NewValues[propertyName] = property.CurrentValue ?? "";
                AuditType = auditType;
            }

        }
        private void SetChanges()
        {

            TableName = Entry.Entity.GetType().Name;
            foreach (PropertyEntry property in Entry.Properties)
            {
                string propertyName = property.Metadata.Name;
                string dbColumnName = property.EntityEntry.GetType().Name;

                if (property.Metadata.IsPrimaryKey())
                {

                    _ = Guid.TryParse(property.CurrentValue?.ToString(), out var itemId);

                    KeyValues[propertyName] = property.CurrentValue ?? "";
                    ItemId = itemId;
                    continue;
                }

                switch (Entry.State)
                {
                    case EntityState.Added:
                        NewValues[propertyName] = property.CurrentValue ?? "";
                        AuditType = AuditType.Create;
                        break;

                    case EntityState.Deleted:
                        OldValues[propertyName] = property.OriginalValue ?? "";
                        AuditType = AuditType.Delete;
                        break;

                    case EntityState.Modified:
                        if (property.IsModified)
                        {
                            ChangedColumns.Add(dbColumnName);

                            OldValues[propertyName] = property.OriginalValue ?? "";
                            NewValues[propertyName] = property.CurrentValue ?? "";
                            if (propertyName == "Status" && property.CurrentValue != null && (bool)property.CurrentValue == false)
                                AuditType = AuditType.Delete;
                            else if (propertyName == "Status" && property.CurrentValue != null && (bool)property.CurrentValue == true)
                                AuditType = AuditType.Active;
                            else
                                AuditType = AuditType.Update;
                        }
                        break;
                }
            }
        }

        public object ToAudit()
        {
            Audit? audit = TableName switch
            {
                "Person" => new AuditPerson(),
                "Doctor" or "DoctorSpecialty" or "DoctorMedicalArea" => new AuditDoctor(),
                "ServiceCatalog" or "ServiceCatalogServiceType" or "ServiceCatalogMedicalForm" => new AuditServiceCatalog(),
                "Field" or "FieldParameter" or "FieldParameterHeader" or "FieldParameterHeaderMedicalFormat" => new AuditField(),
                "Business" or "BusinessEconomicActivity"
                           or "BusinessProject" or "BusinessCostCenter"
                           or "BusinessCampaign" or "BusinessArea"
                           or "BusinessPosition" or "BusinessProfile" => new AuditBusiness(),
                _ => null,
            };


        audit ??= new Audit();

            audit.Datetime = DateTimePersonalized.NowPeru;
            audit.AuditType = AuditType.ToString();
            audit.UserId = UserId;
            audit.TableName = TableName;
            audit.KeyValues = JsonConvert.SerializeObject(KeyValues);
            audit.OldValues = OldValues?.Count == 0 ?
                              null : JsonConvert.SerializeObject(OldValues);
            audit.NewValues = NewValues.Count == 0 ?
                              null : JsonConvert.SerializeObject(NewValues);
            audit.ItemId = ItemId;

            return audit;
        }
    }
}
