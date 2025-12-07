using Microsoft.EntityFrameworkCore;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using System.Text.Json;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Enums;
using AnaPrevention.GeneralMasterData.Api.Common.Helper.AuditTrail;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using System.Text.RegularExpressions;
using AnaPrevention.GeneralMasterData.Api.CommercialDocumentTypes.Configuration;
using AnaPrevention.GeneralMasterData.Api.Common.Tools.Extensions;
using AnaPrevention.GeneralMasterData.Api.EconomicActivities.Configuration;
using AnaPrevention.GeneralMasterData.Api.CreditTimes.Configuration;
using AnaPrevention.GeneralMasterData.Api.MeasurementUnits.Configuration;
using AnaPrevention.GeneralMasterData.Api.SubsidiaryTypes.Configuration;
using AnaPrevention.GeneralMasterData.Api.Specialties.Configuration;
using AnaPrevention.GeneralMasterData.Api.WorkingConditions.Configuration;
using AnaPrevention.GeneralMasterData.Api.Taxes.Configuration;
using AnaPrevention.GeneralMasterData.Api.ExistenceTypes.Configuration;
using AnaPrevention.GeneralMasterData.Api.ServiceTypes.Configuration;
using AnaPrevention.GeneralMasterData.Api.ItemTypes.Configuration;
using AnaPrevention.GeneralMasterData.Api.IdentityDocumentTypes.Configuration;
using AnaPrevention.GeneralMasterData.Api.GeographicLocations.Configuration;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.MedicalAreas.Configuration;
using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Configuration;
using AnaPrevention.GeneralMasterData.Api.BusinessAreas.Configuration;
using AnaPrevention.GeneralMasterData.Api.BusinessCampaigns.Configuration;
using AnaPrevention.GeneralMasterData.Api.BusinessContacts.Configuration;
using AnaPrevention.GeneralMasterData.Api.BusinessCostCenters.Configuration;
using AnaPrevention.GeneralMasterData.Api.Businesses.Configuration;
using AnaPrevention.GeneralMasterData.Api.BusinessPositions.Configuration;
using AnaPrevention.GeneralMasterData.Api.BusinessProfiles.Configuration;
using AnaPrevention.GeneralMasterData.Api.BusinessProjects.Configuration;
using AnaPrevention.GeneralMasterData.Api.Families.Configuration;
using AnaPrevention.GeneralMasterData.Api.SubFamilies.Configuration;
using AnaPrevention.GeneralMasterData.Api.Lines.Configuration;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Dtos.Security.Views;
using AnaPrevention.GeneralMasterData.Api.Dimensions.Configuration;
using AnaPrevention.GeneralMasterData.Api.Companies.Configuration;
using AnaPrevention.GeneralMasterData.Api.Doctors.Configuration;
using AnaPrevention.GeneralMasterData.Api.Fields.Configuration;
using AnaPrevention.GeneralMasterData.Api.Persons.Configuration;
using AnaPrevention.GeneralMasterData.Api.Subsidiaries.Configuration;
using AnaPrevention.GeneralMasterData.Api.Attachments.Configuration;
using AnaPrevention.GeneralMasterData.Api.Diagnostics.Configuration;
using AnaPrevention.GeneralMasterData.Api.Equipments.Configuration;
using AnaPrevention.GeneralMasterData.Api.Common.Configuration;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailContents.Configuration;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailTags.Configuration;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailTemplates.Configuration;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailUsers.Configuration;

namespace AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF
{
    public class AnaPreventionContext : DbContext
    {
        private readonly string _connectionString = null!;
        private readonly string _bucketName = null!;
        private readonly bool _useConsoleLogger = false;
        private readonly bool _initialHasData = false;


        public AnaPreventionContext(string connectionString, bool useConsoleLogger, bool initialHasData, string bucketName = null)
        {
            _connectionString = connectionString;
            _useConsoleLogger = useConsoleLogger;
            _initialHasData = initialHasData;
            _bucketName = bucketName;

            string envioroment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "";
            if (string.IsNullOrWhiteSpace(envioroment))
            {
                try
                {
                    _bucketName = Environment.GetEnvironmentVariable("BUCKET_NAME")??"";
                    SecretAccessDB secret = JsonSerializer.Deserialize<SecretAccessDB>(Environment.GetEnvironmentVariable("SECRET_ACCESS") ?? "") ?? new();
                    _connectionString = $"server={secret?.host};port={secret?.port};user={secret?.username};password={secret?.password};database={secret?.database}";
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine("================================== ERROR: Conexion BD ======================");
                    Console.WriteLine(ex.ToString());
                }
            }
        }
        public string GetConnectionString()
        {
            return _connectionString;
        }

        public string GetBucketName()
        {
            return _bucketName;
        }

        public bool GetUseConsoleLogger()
        {
            return _useConsoleLogger;
        }

        public bool GetInitialHasData()
        {
            return _initialHasData;
        }
        public virtual int SaveChangesNoScope(Guid userId)
        {
            ConvertToUpperDataEntry();

            var addedEntries = ChangeTracker.Entries().Where(e => e.State == EntityState.Added).ToList();
            var modifiedEntries = ChangeTracker.Entries().Where(e => e.State == EntityState.Deleted || e.State == EntityState.Modified).ToList();

            foreach (var entry in modifiedEntries)
            {
                new AuditHelper(this).AddAuditLogs(userId, entry);
            }
            var result = base.SaveChanges();
            foreach (var entry in addedEntries)
            {
                new AuditHelper(this).AddAuditLogs(userId, entry, AuditType.Create);
            }
            base.SaveChanges();
            return result;
        }
        public virtual int SaveChanges(Guid userId)
        {

            var addedEntries = ChangeTracker.Entries().Where(e => e.State == EntityState.Added).ToList();
            var modifiedEntries = ChangeTracker.Entries().Where(e => e.State == EntityState.Deleted || e.State == EntityState.Modified).ToList();

            foreach (var entry in modifiedEntries)
            {
                new AuditHelper(this).AddAuditLogs(userId, entry);
            }
            foreach (var entry in addedEntries)
            {
                new AuditHelper(this).AddAuditLogs(userId, entry, AuditType.Create);
            }

            ConvertToUpperDataEntry();

            var result = base.SaveChanges();
            //scope.Complete();
            return result;
        }

        public override int SaveChanges()
        {
            ConvertToUpperDataEntry();
            return base.SaveChanges();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 17));
            optionsBuilder.UseMySql(_connectionString, serverVersion);

            if (_useConsoleLogger)
            {
                optionsBuilder
                    .UseLoggerFactory(CreateLoggerFactory())
                    .EnableSensitiveDataLogging();
            }
            else
            {
                optionsBuilder
                    .UseLoggerFactory(CreateEmptyLoggerFactory());
            }
        }

        private static ILoggerFactory CreateLoggerFactory()
        {
            return LoggerFactory.Create(builder => builder
                .AddFilter((category, level) => category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information)
                .AddConsole());
        }

        private static ILoggerFactory CreateEmptyLoggerFactory()
        {
            return LoggerFactory.Create(builder => builder
                .AddFilter((_, _) => false));
        }




        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ConvertToUpperDataEntry();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void ConvertToUpperDataEntry()
        {
            foreach (var entry in ChangeTracker.Entries())
            {

                if (entry.Entity.ToString() != "AnaPrevention.GeneralMasterData.Api.Occupationals.Medicines.Domain.Entities.CemMedicalQuestionnaire" && entry.Entity.ToString() != "AnaPrevention.GeneralMasterData.Api.ReferenceRanges.Domain.Entities.ReferenceRangeMedicalForm" && entry.Entity.ToString() != "AnaPrevention.GeneralMasterData.Api.DefaultValues.Domain.Entities.DefaultValue" && entry.Entity.ToString() != "AnaPrevention.GeneralMasterData.Api.Occupationals.Reports.Domain.Entities.ReportPivot")
                {
                    if ((entry.State == EntityState.Added || entry.State == EntityState.Modified))
                    {
                        foreach (var property in entry.Properties)
                        {
                            if (property.Metadata.Name != "Password" && property.Metadata.Name != "NavigatorJson" && property.CurrentValue is string value)
                            {
                                var valueUpper = value.ToUpper();
                                property.CurrentValue = value;

                                if (CommonStatic.IsValidJson(value))
                                {
                                    string pattern = @"\\U([0-9a-fA-F]{4})";
                                    string output = Regex.Replace(valueUpper, pattern, match => "\\u" + match.Groups[1].Value.ToLower());

                                    output = (output).Replace(":TRUE", ":true");
                                    output = (output).Replace(":FALSE", ":false");
                                    output = (output).Replace("\\N", "\\n");
                                    output = (output).Replace("\\T", "\\t");
                                    output = (output).Replace(":NULL", ":null");
                                    property.CurrentValue = (output).Replace("[NULL]", "[null]");
                                }
                            }
                        }
                    }
                }
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AttachmentConfig());
            modelBuilder.ApplyConfiguration(new EconomicActivityConfig());

            modelBuilder.ApplyConfiguration(new LineConfig());
            modelBuilder.ApplyConfiguration(new LineTypeConfig());

            modelBuilder.ApplyConfiguration(new CompanyConfig());
            modelBuilder.ApplyConfiguration(new IdentityDocumentTypeConfig());
            modelBuilder.ApplyConfiguration(new MedicalFormatConfig());
            modelBuilder.ApplyConfiguration(new CreditTimeConfig());

            modelBuilder.ApplyConfiguration(new CountryConfig());
            modelBuilder.ApplyConfiguration(new DepartmentConfig());
            modelBuilder.ApplyConfiguration(new ProvinceConfig());
            modelBuilder.ApplyConfiguration(new DistrictConfig());

            modelBuilder.ApplyConfiguration(new BusinessConfig());
            modelBuilder.ApplyConfiguration(new BusinessEconomicActivityConfig());
            modelBuilder.ApplyConfiguration(new BusinessCampaignConfig());
            modelBuilder.ApplyConfiguration(new BusinessAreaConfig());
            modelBuilder.ApplyConfiguration(new BusinessContactConfig());
            modelBuilder.ApplyConfiguration(new BusinessPositionConfig());
            modelBuilder.ApplyConfiguration(new BusinessProfileConfig());
            modelBuilder.ApplyConfiguration(new BusinessProjectConfig());
            modelBuilder.ApplyConfiguration(new BusinessCostCenterConfig());

            modelBuilder.ApplyConfiguration(new PersonConfig());
            modelBuilder.ApplyConfiguration(new GenderConfig());
            modelBuilder.ApplyConfiguration(new DegreeInstructionConfig());
            modelBuilder.ApplyConfiguration(new MaritalStatusConfig());

            modelBuilder.ApplyConfiguration(new CostCenterConfig());
            modelBuilder.ApplyConfiguration(new DimensionConfig());
            modelBuilder.ApplyConfiguration(new SubsidiaryTypeConfig());
            modelBuilder.ApplyConfiguration(new SubsidiaryConfig());
            modelBuilder.ApplyConfiguration(new SubsidiaryServiceTypeConfig());

            modelBuilder.ApplyConfiguration(new ServiceTypeConfig());
            modelBuilder.ApplyConfiguration(new SpecialtyConfig());
            modelBuilder.ApplyConfiguration(new TaxConfig());
            modelBuilder.ApplyConfiguration(new ExistenceTypeConfig());
            modelBuilder.ApplyConfiguration(new MedicalAreaConfig());
            modelBuilder.ApplyConfiguration(new ItemTypeConfig());
            modelBuilder.ApplyConfiguration(new UomConfig());
            modelBuilder.ApplyConfiguration(new WorkingConditionConfig());
            modelBuilder.ApplyConfiguration(new DoctorConfig());
            modelBuilder.ApplyConfiguration(new DoctorSpecialtyConfig());
            modelBuilder.ApplyConfiguration(new DoctorMedicalAreaConfig());
            modelBuilder.ApplyConfiguration(new AuditPersonConfig());
            modelBuilder.ApplyConfiguration(new CommercialDocumentTypeConfig());
            modelBuilder.ApplyConfiguration(new TaxConfig());
            modelBuilder.ApplyConfiguration(new FamilyConfig());
            modelBuilder.ApplyConfiguration(new SubFamilyConfig());
            modelBuilder.ApplyConfiguration(new EquipmentConfig());
            modelBuilder.ApplyConfiguration(new EquipmentCalibrationConfig());
            modelBuilder.ApplyConfiguration(new AuditDoctorConfig());
            modelBuilder.ApplyConfiguration(new ServiceCatalogConfig());
            modelBuilder.ApplyConfiguration(new ServiceCatalogServiceTypeConfig());
            modelBuilder.ApplyConfiguration(new ServiceCatalogFieldConfig());
            modelBuilder.ApplyConfiguration(new AuditServiceCatalogConfig());
            modelBuilder.ApplyConfiguration(new AuditBusinessConfig());
            modelBuilder.ApplyConfiguration(new MedicalFormConfig());
            modelBuilder.ApplyConfiguration(new ServiceCatalogMedicalFormConfig());
            modelBuilder.ApplyConfiguration(new FieldParameterConfig());
            modelBuilder.ApplyConfiguration(new FieldConfig());
            modelBuilder.ApplyConfiguration(new FieldConfig());
            modelBuilder.ApplyConfiguration(new AuditFieldConfig());
            modelBuilder.ApplyConfiguration(new DiagnosticConfig()); ;
            modelBuilder.ApplyConfiguration(new FieldMedicalFormatConfig()); 
            modelBuilder.ApplyConfiguration(new EmailUserConfig());
            modelBuilder.ApplyConfiguration(new EmailTagConfig());
            modelBuilder.ApplyConfiguration(new EmailContentConfig());
            modelBuilder.ApplyConfiguration(new EmailTemplateConfig());
            modelBuilder.ApplyConfiguration(new AuditEmailTemplateConfig());

            modelBuilder.ApplyConfiguration(new AuditConfig());


            modelBuilder.Entity<UserByCompany>().ToView("user_by_companies_view");
            modelBuilder.Entity<UserMin>().ToView("user_min_view");


            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.SetTableName(entity.GetTableName()?.ToSnakeCase());

                foreach (var property in entity.GetProperties())
                {
                    property.SetColumnName(property.GetColumnName()?.ToSnakeCase());
                }
            }
        }
    }
}
