using CSharpFunctionalExtensions;
using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.ServiceTypes.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.Subsidiaries.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.Subsidiaries.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Subsidiaries.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Subsidiaries.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Subsidiaries.Domain.ValueObjects;
using AnaPrevention.GeneralMasterData.Api.Subsidiaries.Infrastructure.Repositories;
using System.Transactions;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Services;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Attachments.Application.Dtos.S3;
using AnaPrevention.GeneralMasterData.Api.Attachments.Application.Services;

namespace AnaPrevention.GeneralMasterData.Api.Subsidiaries.Application.Services
{
    public class SubsidiaryApplicationService(
   AnaPreventionContext context,
   RegisterSubsidiaryValidator registerSubsidiaryValidator,
   EditSubsidiaryValidator editSubsidiaryValidator,
   SubsidiaryRepository subsidiaryRepository,
   SubsidiaryServiceTypeRepository subsidiaryServiceTypeRepository,
   S3AmazonService s3AmazonService)
    {
        private readonly AnaPreventionContext _context = context;
        private readonly RegisterSubsidiaryValidator _registerSubsidiaryValidator = registerSubsidiaryValidator;
        private readonly EditSubsidiaryValidator _editSubsidiaryValidator = editSubsidiaryValidator;
        private readonly SubsidiaryRepository _subsidiaryRepository = subsidiaryRepository;
        private readonly SubsidiaryServiceTypeRepository _subsidiaryServiceTypeRepository = subsidiaryServiceTypeRepository;
        private readonly S3AmazonService _s3AmazonService = s3AmazonService;

        public async Task<Result<RegisterSubsidiaryResponse, Notification>> RegisterSubsidiary(RegisterSubsidiaryRequest request, Guid companyId, Guid userId)
        {
            Notification notification = _registerSubsidiaryValidator.Validate(request, companyId);

            if (notification.HasErrors())
                return notification;


            string description = request.Description.Trim();
            string code = GenerateCode(companyId);
            string districtId = request.DistrictId.Trim();
            string ledgerAccount = request.LedgerAccount.Trim();
            string address = request.Address.Trim();
            string? phoneNumber = request.PhoneNumber;
            Guid subsidiaryTypeId = request.SubsidiaryTypeId;
            string geoLocation = request.GeoLocation.Trim();
            int capacity = request.Capacity;
            Guid doctorId = request.DoctorId;
            Guid? camoDoctorId = request.CamoDoctorId;
            string EmailForAppointment = request.EmailForAppointment;
            List<Dictionary<string, string>>? officeHours = [];

            if (request.OfficeHours != null)
            {
                foreach (RegisterOfficeHourRequest OfficeHourRequest in request.OfficeHours)
                {
                    officeHours.Add(new OfficeHour(
                                                    OfficeHourRequest.StartDay,
                                                    OfficeHourRequest.FinishDay,
                                                    OfficeHourRequest.HourStart,
                                                    OfficeHourRequest.MinuteStart,
                                                    OfficeHourRequest.HourFinish,
                                                    OfficeHourRequest.MinuteFinish
                                                    ).convertToDictionary());
                }
            }
            Guid id = Guid.NewGuid();
            string url = SubsidiaryStatic.DirectoryLogo + id;

            var _fileApplicationsService = new FileApplicationsService();
            if (!string.IsNullOrEmpty(request.LogoBase64))
            {
                Result<FileDto, Notification> logoResult = _fileApplicationsService.ConverterBase64InBytes(request.LogoBase64);
                using var streamRight = new MemoryStream(logoResult.Value.Bytes);

                var fileS3Right = new S3FileDto
                {
                    FileName = url,
                    stream = streamRight,
                };
                S3ResponseDto graphicRighttUpload = await _s3AmazonService.UploadFileAsync(fileS3Right, CommonStatic.BucketNameFiles);
            }


            Subsidiary subsidiary = new(description, code, districtId, ledgerAccount, address, subsidiaryTypeId, geoLocation, capacity, doctorId, System.Text.Json.JsonSerializer.Serialize(officeHours), companyId, id, phoneNumber, EmailForAppointment, camoDoctorId, url);

            List<Guid>? ListServiceTypeId = request.ListServiceTypeId;

            using (var scope = new TransactionScope())
            {
                _subsidiaryRepository.Save(subsidiary);

                _context.SaveChangesNoScope(userId);
                if (ListServiceTypeId != null)
                {
                    foreach (var ServiceTypeId in ListServiceTypeId)
                    {
                        _subsidiaryServiceTypeRepository.Save(new SubsidiaryServiceType(ServiceTypeId, subsidiary.Id));
                    }
                }
                _context.SaveChangesNoScope(userId);
                scope.Complete();
            }

            var response = new RegisterSubsidiaryResponse
            {
                Id = subsidiary.Id,
                Description = subsidiary.Description,
                Code = subsidiary.Code,
                DistrictId = subsidiary.DistrictId,
                OfficeHours = subsidiary.OfficeHours,
                LedgerAccount = subsidiary.LedgerAccount,
                Address = subsidiary.Address,
                ServiceTypeId = request.ListServiceTypeId,
                SubsidiaryTypeId = subsidiary.SubsidiaryTypeId,
                GeoLocation = subsidiary.GeoLocation,
                Capacity = subsidiary.Capacity,
                DoctorId = subsidiary.DoctorId,
                CompanyId = companyId,
                Status = subsidiary.Status,
            };
            return response;
        }
        public string GenerateCode(Guid companyId)
        {
            return _subsidiaryRepository.GenerateCode(companyId);
        }

        public Result<DistributionListEmailDto, Notification> RegisterDistributionListEmail(RegisterDistributionListEmailRequest request, Subsidiary subsidiary, Guid userId)
        {
            Notification notification = _editSubsidiaryValidator.ValidateEmail(request.DistributionList, SubsidiaryStatic.DistributionList);
            if (notification.HasErrors())
                return notification;

            notification = _editSubsidiaryValidator.ValidateEmail(request.DistributionListLaboratory, SubsidiaryStatic.DistributionListLaboratory);
            if (notification.HasErrors())
                return notification;


            subsidiary.DistributionListEmail = request.DistributionList;
            subsidiary.DistributionListEmaillaboratory = request.DistributionListLaboratory;

            _context.SaveChanges(userId);

            return new DistributionListEmailDto()
            {
                DistributionList = subsidiary.DistributionListEmail ?? "",
                DistributionListLaboratory = subsidiary.DistributionListEmaillaboratory ?? "",
            };

        }
        public DistributionListEmailDto GetDistributionListEmail(Subsidiary subsidiary)
        {
            return new()
            {
                DistributionList = subsidiary.DistributionListEmail ?? "",
                DistributionListLaboratory = subsidiary.DistributionListEmaillaboratory ?? "",
            };
        }

        public  async Task<EditSubsidiaryResponse> EditSubsidiary(EditSubsidiaryRequest request, Subsidiary subsidiary, Guid userId, Guid companyId)
        {
            subsidiary.Description = request.Description.Trim();
            subsidiary.Code = request.Code.Trim();
            subsidiary.DistrictId = request.DistrictId.Trim();
            subsidiary.GeoLocation = request.GeoLocation;
            subsidiary.Capacity = request.Capacity;
            subsidiary.DoctorId = request.DoctorId;
            subsidiary.Address = request.Address;
            subsidiary.SubsidiaryTypeId = request.SubsidiaryTypeId;
            subsidiary.LedgerAccount = request.LedgerAccount;
            subsidiary.PhoneNumber = request.PhoneNumber;
            subsidiary.EmailForAppointment = request.EmailForAppointment;
            subsidiary.CamoDoctorId = request.CamoDoctorId;

            if (request.IsDeleteLogo == false)
            {
                if (!string.IsNullOrEmpty(request.LogoBase64))
                {
                    var _fileApplicationsService = new FileApplicationsService();
                    string url = SubsidiaryStatic.DirectoryLogo + subsidiary.Id;
                    Result<FileDto, Notification> logoResult = _fileApplicationsService.ConverterBase64InBytes(request.LogoBase64);
                    using var streamRight = new MemoryStream(logoResult.Value.Bytes);

                    var fileS3Right = new S3FileDto
                    {
                        FileName = url,
                        stream = streamRight,
                    };
                    S3ResponseDto graphicRighttUpload = await _s3AmazonService.UploadFileAsync(fileS3Right, CommonStatic.BucketNameFiles);
                }
            }
            else
            {
                subsidiary.LogoUrl = null;
            }


            List<Dictionary<string, string>>? officeHours = new();
            if (request.OfficeHours != null)
            {
                foreach (RegisterOfficeHourRequest OfficeHourRequest in request.OfficeHours)
                {
                    officeHours.Add(new OfficeHour(
                                                    OfficeHourRequest.StartDay,
                                                    OfficeHourRequest.FinishDay,
                                                    OfficeHourRequest.HourStart,
                                                    OfficeHourRequest.MinuteStart,
                                                    OfficeHourRequest.HourFinish,
                                                    OfficeHourRequest.MinuteFinish
                                                    ).convertToDictionary());
                }

                subsidiary.OfficeHours = System.Text.Json.JsonSerializer.Serialize(officeHours);
            }
            else
                subsidiary.OfficeHours = "";


            List<Guid>? ListServiceTypeId = request.ListServiceTypeId;

            if (ListServiceTypeId != null)
            {
                List<SubsidiaryServiceType>? ListSubsidiaries = _subsidiaryServiceTypeRepository.GetServiceTypesBySubsidiary(subsidiary.Id);

                if (ListSubsidiaries?.Count > 0)
                    _subsidiaryServiceTypeRepository.RemoveSubsidiaryServiceTypeRange(ListSubsidiaries, userId);

                List<ServiceTypeDto>? ListServiceType = _subsidiaryServiceTypeRepository.GetDtoBySubsidiary(subsidiary.Id);

                foreach (var ServiceTypeId in ListServiceTypeId)
                {
                    if (ListServiceType != null)
                    {
                        _subsidiaryServiceTypeRepository.Save(new SubsidiaryServiceType(ServiceTypeId, subsidiary.Id));
                    }
                }
            }

            _context.SaveChanges(userId);

            var response = new EditSubsidiaryResponse
            {
                Id = subsidiary.Id,
                Description = subsidiary.Description,
                Code = subsidiary.Code,
                OfficeHours = subsidiary.OfficeHours,
                DistrictId = subsidiary.DistrictId,
                LedgerAccount = subsidiary.LedgerAccount,
                Address = subsidiary.Address,
                ServiceTypeId = request.ListServiceTypeId,
                SubsidiaryTypeId = subsidiary.SubsidiaryTypeId,
                GeoLocation = subsidiary.GeoLocation,
                Capacity = subsidiary.Capacity,
                DoctorId = subsidiary.DoctorId,
                CompanyId = companyId,
                Status = subsidiary.Status,
            };

            return response;
        }

        public EditSubsidiaryResponse ActiveSubsidiary(Subsidiary subsidiary, Guid userId)
        {
            subsidiary.Status = true;

            _context.SaveChanges(userId);

            var response = new EditSubsidiaryResponse
            {
                Id = subsidiary.Id,
                Description = subsidiary.Description,
                Code = subsidiary.Code,
                Status = subsidiary.Status
            };

            return response;
        }
        public Notification ValidateEditSubsidiaryRequest(EditSubsidiaryRequest request, Guid companyId)
        {
            return _editSubsidiaryValidator.Validate(request, companyId);
        }

        public EditSubsidiaryResponse RemoveSubsidiary(Subsidiary subsidiary, Guid userId)
        {
            subsidiary.Status = false;
            _context.SaveChanges(userId);

            var response = new EditSubsidiaryResponse
            {
                Id = subsidiary.Id,
                Description = subsidiary.Description,
                Code = subsidiary.Code,
                Status = subsidiary.Status
            };

            return response;
        }

        public Subsidiary? GetById(Guid id)
        {
            return _subsidiaryRepository.GetById(id);
        }

        public SubsidiaryMinDto? GetMinDtoById(Guid id, Guid companyId)
        {
            return _subsidiaryRepository.GetMinDtoById(id, companyId);
        }
        public SubsidiaryDto? GetDtoById(Guid id, Guid companyId)
        {
            return _subsidiaryRepository.GetDtoById(id, companyId);
        }


        public List<SubsidiaryDto> GetListAll(Guid companyId)
        {
            return _subsidiaryRepository.GetListAll(companyId);
        }
        public Tuple<IEnumerable<SubsidiaryDto>, PaginationMetadata> GetList(
            int pageNumber, int pageSize, Guid companyId, bool status, string descriptionSearch = "", string codeSearch = "", string serviceTypeSearch = "", string subsidiaryTypeSearch = "")
        {
            var (ListSubsidiary, paginationMetadata) = _subsidiaryRepository.GetList(pageNumber, pageSize, companyId, status, descriptionSearch, codeSearch, serviceTypeSearch, subsidiaryTypeSearch);

            return new Tuple<IEnumerable<SubsidiaryDto>, PaginationMetadata>
                (ListSubsidiary, paginationMetadata);
        }
        /***Falta implementar con apis**/
        //public List<SubsidiaryMinDto>? GetMinDtoByQuota(Guid businessId, DateTime date)
        //{
        //    List<QuotaBusinessDto>? quotas = _quotaBusinessRepository.GetCurrentByBussinesId(businessId, date);

        //    if (quotas != null)
        //    {
        //        var subsidiaryIds = quotas
        //       .SelectMany(q => q.Detail ?? Enumerable.Empty<DetailQuotaBusiness>())
        //       .Where(detail => detail.Quotas?.FirstOrDefault(t1 => t1.Date.Date == date.Date)?.Times?.Any(time => time.FreePlaces > 0) ?? false)
        //       .Select(detail => detail.SubsidiaryId)
        //       .Distinct()
        //       .ToList();

        //        return _subsidiaryRepository.GetMinDtoByIds(subsidiaryIds);
        //    }


        //    return null;
        //}
    }
}
