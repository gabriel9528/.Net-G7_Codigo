using AnaPrevention.GeneralMasterData.Api.CommercialDocumentTypes.Application.Services;
using AnaPrevention.GeneralMasterData.Api.CommercialDocumentTypes.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.CommercialDocumentTypes.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using Microsoft.OpenApi.Models;
using System.Reflection;
using AnaPrevention.GeneralMasterData.Api.EconomicActivities.Application.Services;
using AnaPrevention.GeneralMasterData.Api.EconomicActivities.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.EconomicActivities.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.CreditTimes.Application.Services;
using AnaPrevention.GeneralMasterData.Api.CreditTimes.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.CreditTimes.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.MeasurementUnits.Application.Services;
using AnaPrevention.GeneralMasterData.Api.MeasurementUnits.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.MeasurementUnits.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.SubsidiaryTypes.Application.Services;
using AnaPrevention.GeneralMasterData.Api.SubsidiaryTypes.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.SubsidiaryTypes.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.Specialties.Application.Services;
using AnaPrevention.GeneralMasterData.Api.Specialties.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Specialties.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.WorkingConditions.Application.Services;
using AnaPrevention.GeneralMasterData.Api.WorkingConditions.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.WorkingConditions.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.Taxes.Application.Services;
using AnaPrevention.GeneralMasterData.Api.Taxes.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Taxes.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.ExistenceTypes.Application.Services;
using AnaPrevention.GeneralMasterData.Api.ExistenceTypes.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.ExistenceTypes.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.ServiceTypes.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.ServiceTypes.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.ServiceTypes.Services;
using AnaPrevention.GeneralMasterData.Api.ItemTypes.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.ItemTypes.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.ItemTypes.Application.Services;
using AnaPrevention.GeneralMasterData.Api.IdentityDocumentTypes.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.IdentityDocumentTypes.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.IdentityDocumentTypes.Application.Services;
using AnaPrevention.GeneralMasterData.Api.GeographicLocations.Application.Services;
using AnaPrevention.GeneralMasterData.Api.GeographicLocations.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.Companies.Application.Services;
using AnaPrevention.GeneralMasterData.Api.Companies.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Companies.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.MedicalAreas.Application.Services;
using AnaPrevention.GeneralMasterData.Api.MedicalAreas.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.MedicalAreas.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Application.Services;
using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.BusinessAreas.Application.Services;
using AnaPrevention.GeneralMasterData.Api.BusinessAreas.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.BusinessAreas.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.BusinessCampaigns.Application.Services;
using AnaPrevention.GeneralMasterData.Api.BusinessCampaigns.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.BusinessCampaigns.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.BusinessContacts.Application.Services;
using AnaPrevention.GeneralMasterData.Api.BusinessContacts.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.BusinessContacts.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.BusinessCostCenters.Application.Services;
using AnaPrevention.GeneralMasterData.Api.BusinessCostCenters.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.BusinessCostCenters.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.BusinessPositions.Application.Services;
using AnaPrevention.GeneralMasterData.Api.BusinessPositions.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.BusinessPositions.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.BusinessProfiles.Application.Services;
using AnaPrevention.GeneralMasterData.Api.BusinessProfiles.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.BusinessProfiles.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.BusinessProjects.Application.Services;
using AnaPrevention.GeneralMasterData.Api.BusinessProjects.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.BusinessProjects.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.Businesses.Application.Services;
using AnaPrevention.GeneralMasterData.Api.Businesses.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Businesses.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.SubFamilies.Application.Services;
using AnaPrevention.GeneralMasterData.Api.SubFamilies.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.SubFamilies.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.Families.Application.Services;
using AnaPrevention.GeneralMasterData.Api.Families.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Families.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.Lines.Application.Services;
using AnaPrevention.GeneralMasterData.Api.Lines.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Lines.Infrastructure.Repositories;
using Asp.Versioning;
using AnaPrevention.GeneralMasterData.Api.Doctors.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.Subsidiaries.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.Persons.Application.Services;
using AnaPrevention.GeneralMasterData.Api.Persons.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Services;
using AnaPrevention.GeneralMasterData.Api.Fields.Application.Services;
using AnaPrevention.GeneralMasterData.Api.Fields.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Fields.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.Doctors.Application.Services;
using AnaPrevention.GeneralMasterData.Api.Doctors.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Subsidiaries.Application.Services;
using AnaPrevention.GeneralMasterData.Api.Subsidiaries.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.CostCenters.Application.Services;
using AnaPrevention.GeneralMasterData.Api.CostCenters.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Dimensions.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Dimensions.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.Dimensions.Application.Services;
using AnaPrevention.GeneralMasterData.Api.Persons.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Attachments.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.Attachments.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Attachments.Application.Services;
using AnaPrevention.GeneralMasterData.Api.Diagnostics.Domain.Services;
using AnaPrevention.GeneralMasterData.Api.Diagnostics.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.Equipments.Application.Services;
using AnaPrevention.GeneralMasterData.Api.Equipments.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.Equipments.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Diagnostics.Domain.Validators;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailTags.Application.Services;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailTags.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailTags.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailUsers.Application.Services;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailUsers.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailUsers.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailTemplates.Application.Services;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailTemplates.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailTemplates.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailContents.Applications.Services;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailContents.Applications.Validators;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailContents.Infraestructure.Repository;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Settings.Mail.Interfaces;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Settings.Mail;
using AnaPrevention.GeneralMasterData.Api.Common.Helper.Mail.Interfaces;
using AnaPrevention.GeneralMasterData.Api.Common.Helper.Mail;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setupAction =>
{
    var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);



    setupAction.AddSecurityDefinition("CleanArchitectureInfoApiBearerAuth", new OpenApiSecurityScheme()
    {
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        Description = "Input a valid token to access this API"
    });

    setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = "CleanArchitectureInfoApiBearerAuth" }
            }, new List<string>() }
    });
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped(_ => new AnaPreventionContext(builder.Configuration["ConnectionString"] ?? "", bool.Parse(builder.Configuration["useConsoleLogger"] ?? ""), bool.Parse(builder.Configuration["InitialHasData"] ?? ""), builder.Configuration["BucketName"] ?? ""));

builder.Services.AddScoped<AttachmentRepository>();
builder.Services.AddScoped<EditAttachmentValidator>();
builder.Services.AddScoped<S3AmazonService>();
builder.Services.AddScoped<AttachmentApplicationService>();
builder.Services.AddScoped<S3AmazonRepository>();

builder.Services.AddScoped<IdentityDocumentTypeRepository>();
builder.Services.AddScoped<RegisterIdentityDocumentTypeValidator>();
builder.Services.AddScoped<EditIdentityDocumentTypeValidator>();
builder.Services.AddScoped<IdentityDocumentTypeApplicationService>();

builder.Services.AddScoped<PersonRepository>();
builder.Services.AddScoped<GenderRepository>();
builder.Services.AddScoped<DegreeInstructionRepository>();
builder.Services.AddScoped<DegreeInstructionApplicationService>();
builder.Services.AddScoped<MaritalStatusRepository>();
builder.Services.AddScoped<MaritalStatusApplicationService>();
builder.Services.AddScoped<RegisterPersonValidator>();
builder.Services.AddScoped<RegisterPersonFullValidator>();
builder.Services.AddScoped<EditPersonFullValidator>();
builder.Services.AddScoped<EditPersonValidator>();
builder.Services.AddScoped<PersonApplicationService>();
builder.Services.AddScoped<GenderApplicationService>();

builder.Services.AddScoped<SubsidiaryTypeRepository>();
builder.Services.AddScoped<RegisterSubsidiaryTypeValidator>();
builder.Services.AddScoped<EditSubsidiaryTypeValidator>();
builder.Services.AddScoped<SubsidiaryTypeApplicationService>();


builder.Services.AddScoped<CommercialDocumentTypeRepository>();
builder.Services.AddScoped<RegisterCommercialDocumentTypeValidator>();
builder.Services.AddScoped<EditCommercialDocumentTypeValidator>();
builder.Services.AddScoped<CommercialDocumentTypeApplicationService>();

builder.Services.AddScoped<CompanyRepository>();
builder.Services.AddScoped<RegisterCompanyValidator>();
builder.Services.AddScoped<EditCompanyValidator>();
builder.Services.AddScoped<CompanyApplicationService>();


builder.Services.AddScoped<ServiceTypeRepository>();
builder.Services.AddScoped<RegisterServiceTypeValidator>();
builder.Services.AddScoped<EditServiceTypeValidator>();
builder.Services.AddScoped<ServiceTypeApplicationService>();



builder.Services.AddScoped<EconomicActivityRepository>();
builder.Services.AddScoped<RegisterEconomicActivityValidator>();
builder.Services.AddScoped<EditEconomicActivityValidator>();
builder.Services.AddScoped<EconomicActivityApplicationService>();


builder.Services.AddScoped<SpecialtyRepository>();
builder.Services.AddScoped<RegisterSpecialtyValidator>();
builder.Services.AddScoped<EditSpecialtyValidator>();
builder.Services.AddScoped<SpecialtyApplicationService>();


builder.Services.AddScoped<TaxRepository>();
builder.Services.AddScoped<RegisterTaxValidator>();
builder.Services.AddScoped<EditTaxValidator>();
builder.Services.AddScoped<TaxApplicationService>();

builder.Services.AddScoped<DimensionRepository>();
builder.Services.AddScoped<RegisterDimensionValidator>();
builder.Services.AddScoped<EditDimensionValidator>();
builder.Services.AddScoped<DimensionApplicationService>();

builder.Services.AddScoped<CostCenterRepository>();
builder.Services.AddScoped<RegisterCostCenterValidator>();
builder.Services.AddScoped<EditCostCenterValidator>();
builder.Services.AddScoped<CostCenterApplicationService>();



builder.Services.AddScoped<LineRepository>();
builder.Services.AddScoped<LineTypeRepository>();
builder.Services.AddScoped<RegisterLineValidator>();
builder.Services.AddScoped<EditLineValidator>();
builder.Services.AddScoped<LineApplicationService>();
builder.Services.AddScoped<LineTypeApplicationService>();

builder.Services.AddScoped<FamilyRepository>();
builder.Services.AddScoped<RegisterFamilyValidator>();
builder.Services.AddScoped<EditFamilyValidator>();
builder.Services.AddScoped<FamilyApplicationService>();


builder.Services.AddScoped<SubFamilyRepository>();
builder.Services.AddScoped<RegisterSubFamilyValidator>();
builder.Services.AddScoped<EditSubFamilyValidator>();
builder.Services.AddScoped<SubFamilyApplicationService>();

builder.Services.AddScoped<ExistenceTypeRepository>();
builder.Services.AddScoped<RegisterExistenceTypeValidator>();
builder.Services.AddScoped<EditExistenceTypeValidator>();
builder.Services.AddScoped<ExistenceTypeApplicationService>();

builder.Services.AddScoped<MedicalAreaRepository>();
builder.Services.AddScoped<RegisterMedicalAreaValidator>();
builder.Services.AddScoped<EditMedicalAreaValidator>();
builder.Services.AddScoped<MedicalAreaApplicationService>();


builder.Services.AddScoped<ItemTypeRepository>();
builder.Services.AddScoped<RegisterItemTypeValidator>();
builder.Services.AddScoped<EditItemTypeValidator>();
builder.Services.AddScoped<ItemTypeApplicationService>();

builder.Services.AddScoped<UomRepository>();
builder.Services.AddScoped<RegisterUomValidator>();
builder.Services.AddScoped<EditUomValidator>();
builder.Services.AddScoped<UomApplicationService>();

builder.Services.AddScoped<CountryRepository>();
builder.Services.AddScoped<CountryApplicationService>();

builder.Services.AddScoped<DepartmentRepository>();
builder.Services.AddScoped<DepartmentApplicationService>();

builder.Services.AddScoped<ProvinceRepository>();
builder.Services.AddScoped<ProvinceApplicationService>();

builder.Services.AddScoped<DistrictRepository>();
builder.Services.AddScoped<DistrictApplicationService>();

builder.Services.AddScoped<SubsidiaryRepository>();
builder.Services.AddScoped<RegisterSubsidiaryValidator>();
builder.Services.AddScoped<EditSubsidiaryValidator>();
builder.Services.AddScoped<SubsidiaryApplicationService>();

builder.Services.AddScoped<DoctorRepository>();
builder.Services.AddScoped<DoctorMedicalAreaRepository>();
builder.Services.AddScoped<RegisterDoctorValidator>();
builder.Services.AddScoped<RegisterListMedicalAreaIdsValidator>();
builder.Services.AddScoped<EditDoctorValidator>();
builder.Services.AddScoped<DoctorApplicationService>();

builder.Services.AddScoped<ServiceCatalogRepository>();
builder.Services.AddScoped<ServiceCatalogServiceTypeRepository>();
builder.Services.AddScoped<ServiceCatalogFieldRepository>();
builder.Services.AddScoped<FieldMedicalFormatRepository>();
builder.Services.AddScoped<RegisterServiceCatalogValidator>();
builder.Services.AddScoped<EditServiceCatalogValidator>();
builder.Services.AddScoped<ServiceCatalogApplicationService>();

builder.Services.AddScoped<CreditTimeRepository>();
builder.Services.AddScoped<RegisterCreditTimeValidator>();
builder.Services.AddScoped<EditCreditTimeValidator>();
builder.Services.AddScoped<CreditTimeApplicationService>();

builder.Services.AddScoped<MedicalFormatRepository>();
builder.Services.AddScoped<RegisterMedicalFormatValidator>();
builder.Services.AddScoped<EditMedicalFormatValidator>();
builder.Services.AddScoped<MedicalFormatApplicationService>();

builder.Services.AddScoped<BusinessRepository>();
builder.Services.AddScoped<RegisterBusinessValidator>();
builder.Services.AddScoped<EditBusinessValidator>();
builder.Services.AddScoped<BusinessApplicationService>();



builder.Services.AddScoped<BusinessProjectRepository>();
builder.Services.AddScoped<RegisterBusinessProjectValidator>();
builder.Services.AddScoped<RegisterListBusinessProjectValidator>();
builder.Services.AddScoped<EditBusinessProjectValidator>();
builder.Services.AddScoped<BusinessProjectApplicationService>();

builder.Services.AddScoped<BusinessCostCenterRepository>();
builder.Services.AddScoped<RegisterBusinessCostCenterValidator>();
builder.Services.AddScoped<RegisterListBusinessCostCenterValidator>();
builder.Services.AddScoped<EditBusinessCostCenterValidator>();
builder.Services.AddScoped<BusinessCostCenterApplicationService>();

builder.Services.AddScoped<BusinessCampaignRepository>();
builder.Services.AddScoped<RegisterBusinessCampaignValidator>();
builder.Services.AddScoped<EditBusinessCampaignValidator>();
builder.Services.AddScoped<BusinessCampaignApplicationService>();

builder.Services.AddScoped<BusinessAreaRepository>();
builder.Services.AddScoped<RegisterBusinessAreaValidator>();
builder.Services.AddScoped<RegisterListBusinessAreaValidator>();
builder.Services.AddScoped<EditBusinessAreaValidator>();
builder.Services.AddScoped<BusinessAreaApplicationService>();

builder.Services.AddScoped<BusinessProfileRepository>();
builder.Services.AddScoped<RegisterBusinessProfileValidator>();
builder.Services.AddScoped<RegisterListBusinessProfileValidator>();
builder.Services.AddScoped<EditBusinessProfileValidator>();
builder.Services.AddScoped<BusinessProfileApplicationService>();

builder.Services.AddScoped<BusinessPositionRepository>();
builder.Services.AddScoped<RegisterBusinessPositionValidator>();
builder.Services.AddScoped<RegisterListBusinessPositionValidator>();
builder.Services.AddScoped<EditBusinessPositionValidator>();
builder.Services.AddScoped<BusinessPositionApplicationService>();

builder.Services.AddScoped<BusinessContactRepository>();
builder.Services.AddScoped<RegisterBusinessContactValidator>();
builder.Services.AddScoped<EditBusinessContactValidator>();
builder.Services.AddScoped<BusinessContactApplicationService>();


builder.Services.AddScoped<FieldParameterRepository>();
builder.Services.AddScoped<FieldRepository>();
builder.Services.AddScoped<RegisterFieldParameterValidator>();
builder.Services.AddScoped<EditFieldParameterValidator>();
builder.Services.AddScoped<FieldParameterApplicationService>();
builder.Services.AddScoped<FieldApplicationService>();

builder.Services.AddScoped<EditFieldValidator>();
builder.Services.AddScoped<RegisterFieldValidator>();
builder.Services.AddScoped<FieldApplicationService>();

builder.Services.AddScoped<WorkingConditionRepository>();
builder.Services.AddScoped<RegisterWorkingConditionValidator>();
builder.Services.AddScoped<EditWorkingConditionValidator>();
builder.Services.AddScoped<WorkingConditionApplicationService>();

builder.Services.AddScoped<MedicalFormRepository>();
builder.Services.AddScoped<ServiceCatalogMedicalFormRepository>();
builder.Services.AddScoped<RegisterMedicalFormValidator>();
builder.Services.AddScoped<EditMedicalFormValidator>();
builder.Services.AddScoped<MedicalFormApplicationService>();

builder.Services.AddScoped<BusinessEconomicActivityRepository>();
builder.Services.AddScoped<SubsidiaryServiceTypeRepository>();
builder.Services.AddScoped<DoctorSpecialtyRepository>();

builder.Services.AddScoped<DiagnosticApplicationService>();
builder.Services.AddScoped<DiagnosticRepository>();

builder.Services.AddScoped<FileApplicationsService>();

builder.Services.AddScoped<EquipmentApplicationService>();
builder.Services.AddScoped<EquipmentRepository>(); 
builder.Services.AddScoped<EditEquipmentValidator>();
builder.Services.AddScoped<EquipmentCalibrationRepository>();
builder.Services.AddScoped<EditDiagnosticValidator>();
builder.Services.AddScoped<RegisterDiagnosticValidator>();
builder.Services.AddScoped<RegisterEquipmentValidator>();


builder.Services.AddScoped<EmailUserApplicationService>();
builder.Services.AddScoped<EmailUserRepository>();
builder.Services.AddScoped<RegisterEmailUserValidator>();
builder.Services.AddScoped<EditEmailUserValidator>();

builder.Services.AddScoped<EmailTagApplicationService>();
builder.Services.AddScoped<EmailTagRepository>();
builder.Services.AddScoped<RegisterEmailTagValidator>();

builder.Services.AddScoped<EmailTemplateRepository>();
builder.Services.AddScoped<EmailTemplateValidator>();
builder.Services.AddScoped<EmailTemplateApplicationService>();

builder.Services.AddScoped<EmailContentApplicationServices>();
builder.Services.AddScoped<EmailContentRepository>();
builder.Services.AddScoped<RegisterEmailContentValidator>();
builder.Services.AddScoped<ApiApplicationService>();


builder.Services.AddSingleton<IEmailConfiguration>(builder.Configuration.GetSection("EmailConfiguration")?.Get<EmailConfiguration>() ?? new());

builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddScoped<EmailService>();

builder.Services.AddApiVersioning(setupAction =>
{
    setupAction.AssumeDefaultVersionWhenUnspecified = true;
    setupAction.ReportApiVersions = true;

    setupAction.DefaultApiVersion = new ApiVersion(1, 0);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseCors(options =>
{
    options.AllowAnyOrigin();
    options.WithOrigins("http://localhost:3000", "https://onehealthqa.pulsosalud.com", "https://anaprevention.com").AllowAnyMethod();
    options.AllowAnyMethod();
    options.AllowAnyHeader();
    options.SetIsOriginAllowed((host) => true);
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
