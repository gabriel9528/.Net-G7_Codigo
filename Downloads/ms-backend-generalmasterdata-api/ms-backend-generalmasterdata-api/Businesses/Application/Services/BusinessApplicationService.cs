using CSharpFunctionalExtensions;
using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.Businesses.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.Businesses.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Businesses.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Businesses.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.EconomicActivities.Application.Dtos;
using System.Text.Json;

namespace AnaPrevention.GeneralMasterData.Api.Businesses.Application.Services
{
    public class BusinessApplicationService(AnaPreventionContext context,
                                            RegisterBusinessValidator registerBusinessValidator,
                                            EditBusinessValidator editBusinessValidator,
                                            BusinessRepository businessRepository,
                                            BusinessEconomicActivityRepository? businessEconomicActivityRepository)
    {
        private readonly AnaPreventionContext _context = context;
        private readonly RegisterBusinessValidator _registerBusinessValidator = registerBusinessValidator;
        private readonly EditBusinessValidator _editBusinessValidator = editBusinessValidator;
        private readonly BusinessRepository _businessRepository = businessRepository;
        private readonly BusinessEconomicActivityRepository? _businessEconomicActivityRepository = businessEconomicActivityRepository;

        public async Task<Result<RegisterBusinessResponse, Notification>> RegisterBusiness(RegisterBusinessRequest request, Guid userId)
        {
            Notification notification = _registerBusinessValidator.Validate(request);

            if (notification.HasErrors())
                return notification;
            
            string description = request.Description.Trim();
            string tradename = request.Tradename.Trim();
            string address = request.Address.Trim();
            string districtId = request.DistrictId.Trim();
            string documentNumber = request.DocumentNumber.Trim();
            string comment = request.Comment.Trim();
            Guid identityDocumentTypeId = request.IdentityDocumentTypeId;
            Guid medicalFormatId = request.MedicalFormatId;
            Guid creditTimeId = request.CreditTimeId;            
            bool isActive = request.IsActive;
            bool isGenerateUsers = request.IsGenerateUsers;
            bool isMedicalReportDisplay = request.IsMedicalReportDisplay;
            bool isPatientResults = request.IsPatientResults;
            bool isSendingResultsPatients = request.IsSendingResultsPatients;
            bool isWaybillShipping = request.IsWaybillShipping;
            string exceptionsByMainBusinessJson = JsonSerializer.Serialize(request.ExceptionsByMainBusiness);


            TimeZoneInfo zonaHorariaPeru = TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time");

            DateTime horaPeru = TimeZoneInfo.ConvertTimeFromUtc(request.DateInscription, zonaHorariaPeru);

            Business business = new(description, tradename, address, identityDocumentTypeId, medicalFormatId, creditTimeId, districtId, documentNumber, comment, horaPeru, isActive, Guid.NewGuid(), isWaybillShipping, isSendingResultsPatients, isPatientResults, isMedicalReportDisplay, isGenerateUsers, exceptionsByMainBusinessJson);


            List<Guid>? ListEconomicActivityId = request.ListEconomicActivityId;

            List<string>? fileUrl = [];

            _businessRepository.Save(business);

            //if (request.Attachments != null)
            //{
            //    foreach (var attachment in request.Attachments)
            //    {
            //        Result<string, Notification> result = await _attachmentApplicationService.RegisterAttachment(attachment, business.Id, EntityType.BUSINESS);
            //        if (result.IsFailure)
            //            return result.Error;

            //        fileUrl.Add(result.Value);
            //    }

            //}

            if (ListEconomicActivityId != null)
            {
                foreach (var economicActivityId in ListEconomicActivityId)
                {
                    _businessEconomicActivityRepository.Save(new BusinessEconomicActivity(economicActivityId, business.Id, Guid.NewGuid()));
                }
            }

           

            if (isGenerateUsers)
            {
                                                  

              //  GenerateUserExtalPortal(business, userId);

          
            }
            _context.SaveChanges(userId);

            var response = new RegisterBusinessResponse
            {
                Id = business.Id,
                Description = business.Description,
                Tradename = business.Tradename,
                Address = business.Address,
                DistrictId = business.DistrictId,
                DocumentNumber = business.DocumentNumber,
                Comment = business.Comment,
                IdentityDocumentTypeId = business.IdentityDocumentTypeId,
                MedicalFormatId = business.MedicalFormatId,
                CreditTimeId = business.CreditTimeId,
                DateInscription = business.DateInscription,
                IsActive = business.IsActive,
                Status = business.Status,
                FileUrl = fileUrl
            };

            return await Task.FromResult(response);
        }


        //public void GenerateUserExtalPortalFull(Guid userId)
        //{
        //    var businesses = _businessRepository.GetListAll();

        //    foreach (var business in businesses)
        //    {
        //        var userNameRrhh = BusinessStatic.BusinessUserExternalResourcesHuman + business.DocumentNumber;
        //        var userNameDoctor = BusinessStatic.BusinessUserExternalMedic + business.DocumentNumber;
        //        var usersExternal = _userExternalApplicationService.GetListByBusinessId(business.Id);

        //        var generateUserMedical = true;
        //        var generateUserRrhh = true;


        //        if (usersExternal != null)
        //        {

        //            foreach (var userExternal in usersExternal)
        //            {
        //                if(userExternal.UserName == userNameRrhh)
        //                {
        //                    generateUserRrhh = false;
        //                    if(userExternal.Status == false)
        //                    {
        //                        userExternal.Status = true;
        //                    }
        //                }
        //                if (userExternal.UserName == userNameDoctor)
        //                {
        //                    generateUserMedical = false;
        //                    if (userExternal.Status == false)
        //                    {
        //                        userExternal.Status = true;
        //                    }
        //                }
        //            }
        //        }

        //        if(generateUserMedical || generateUserRrhh)
        //        {
        //           // GenerateUserExtalPortal(business,userId,generateUserMedical,generateUserRrhh);
        //        }


        //    }
        //    _context.SaveChanges(userId);
        //}

        public async Task<Result<EditBusinessResponse, Notification>> EditBusiness(EditbusinessRequest request, Business business, Guid userId)
        {
            business.Description = request.Description.Trim();
            business.Tradename = request.Tradename.Trim();
            business.Address = request.Address.Trim();
            business.DistrictId = request.DistrictId.Trim();
            business.DocumentNumber = request.DocumentNumber.Trim();
            business.Comment = request.Comment.Trim();

            business.IdentityDocumentTypeId = request.IdentityDocumentTypeId;
            business.MedicalFormatId = request.MedicalFormatId;
            business.CreditTimeId = request.CreditTimeId;            
            business.IsActive = request.IsActive;
            business.ExceptionsByMainBusinessJson = JsonSerializer.Serialize(request.ExceptionsByMainBusiness);
            business.IsGenerateUsers = request.IsGenerateUsers;
            business.IsMedicalReportDisplay = request.IsMedicalReportDisplay;
            business.IsPatientResults = request.IsPatientResults;
            business.IsSendingResultsPatients = request.IsSendingResultsPatients;
            business.IsWaybillShipping = request.IsWaybillShipping;
            
            TimeZoneInfo zonaHorariaPeru = TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time");            
            DateTime horaPeru = TimeZoneInfo.ConvertTimeFromUtc(request.DateInscription, zonaHorariaPeru);        
            business.DateInscription = horaPeru;

            List<Guid>? ListEconomicActivityId = request.ListEconomicActivityId;

            List<string>? fileUrl = new();

            //if (request.Attachments != null && request.Attachments.Count > 0)
            //{

            //    foreach (var attachment in request.Attachments)
            //    {
            //        Result<string, Notification> result = await _attachmentApplicationService.RegisterAttachment(attachment, business.Id, EntityType.BUSINESS);

            //        if (result.IsFailure)
            //            return result.Error;

            //        fileUrl.Add(result.Value);
            //    }
            //}


            //if (request.AttachmentsEdit != null)
            //{
            //    foreach (var attachment in request.AttachmentsEdit)
            //    {
            //        _attachmentApplicationService.EditAttachment(attachment);
            //    }
            //}
            //if (request.AttachmentsDelete != null)
            //{
            //    foreach (var attachmentId in request.AttachmentsDelete)
            //    {
            //        _attachmentApplicationService.RemoveAttachment(attachmentId);
            //    }
            //}
            if (ListEconomicActivityId != null)
            {
                List<EconomicActivityDto>? ListEconomicActivity = _businessEconomicActivityRepository.GetListEconomicActivityByBusinessId(business.Id);
                if (ListEconomicActivity != null)
                {
                    foreach (EconomicActivityDto economicActivityDto in ListEconomicActivity)
                    {
                        Guid? economicActivityId = ListEconomicActivityId.Find(t1 => t1 == economicActivityDto.Id);

                        if (economicActivityId != null)
                        {
                            var businessEconomicActivity = _businessEconomicActivityRepository.GetbyEconomicActivityId(economicActivityDto.Id, business.Id);
                            if (businessEconomicActivity != null)
                                _businessEconomicActivityRepository.Remove(businessEconomicActivity);
                        }
                    }

                    foreach (var economicActivityId in ListEconomicActivityId)
                    {

                        _businessEconomicActivityRepository.Save(new BusinessEconomicActivity(economicActivityId, business.Id, Guid.NewGuid()));
                    }
                }
            }

            _context.SaveChanges(userId);

            var response = new EditBusinessResponse
            {
                Id = business.Id,
                Description = business.Description,
                Tradename = business.Tradename,
                Address = business.Address,
                DistrictId = business.DistrictId,
                DocumentNumber = business.DocumentNumber,
                Comment = business.Comment,
                IdentityDocumentTypeId = business.IdentityDocumentTypeId,
                MedicalFormatId = business.MedicalFormatId,
                CreditTimeId = business.CreditTimeId,
                DateInscription = business.DateInscription,
                IsActive = business.IsActive,
                Status = business.Status,
                FileUrl = fileUrl
            };

            return response;
        }

        public EditBusinessResponse ActiveBusiness(Business business, Guid userId)
        {
            business.Status = true;

            _context.SaveChanges(userId);

            var response = new EditBusinessResponse
            {
                Id = business.Id,
                Description = business.Description,
                Tradename = business.Tradename,
                Address = business.Address,
                DistrictId = business.DistrictId,
                DocumentNumber = business.DocumentNumber,
                Comment = business.Comment,
                IdentityDocumentTypeId = business.IdentityDocumentTypeId,
                MedicalFormatId = business.MedicalFormatId,
                CreditTimeId = business.CreditTimeId,
                DateInscription = business.DateInscription,
                IsActive = business.IsActive,
                Status = business.Status,
            };

            return response;
        }
        public Notification ValidateEditBusinessRequest(EditbusinessRequest request)
        {
            return _editBusinessValidator.Validate(request);
        }

        public EditBusinessResponse RemoveBusiness(Business business, Guid userId)
        {
            business.Status = false;
            _context.SaveChanges(userId);

            var response = new EditBusinessResponse
            {
                Id = business.Id,
                Description = business.Description,
                Tradename = business.Tradename,
                Address = business.Address,
                DistrictId = business.DistrictId,
                DocumentNumber = business.DocumentNumber,
                Comment = business.Comment,
                IdentityDocumentTypeId = business.IdentityDocumentTypeId,
                MedicalFormatId = business.MedicalFormatId,
                CreditTimeId = business.CreditTimeId,
                DateInscription = business.DateInscription,
                IsActive = business.IsActive,
                Status = business.Status,
            };

            return response;
        }

        public Business? GetById(Guid id)
        {
            return _businessRepository.GetById(id);
        }

        public BusinessDto? GetDtoById(Guid id)
        {
            BusinessDto? businessDto = _businessRepository.GetDtoById(id);
            return businessDto;
        }
        public List<BusinessDto> GetListAll(string? documentNumberAndDescription = "")
        {
            return _businessRepository.GetListAll(documentNumberAndDescription);
        }

        public Tuple<IEnumerable<BusinessDto>, PaginationMetadata> GetList(int pageNumber, int pageSize, bool status, string descriptionSearch = "", string documentNumberSearch = "")
        {
            return _businessRepository.GetList(pageNumber, pageSize, status, descriptionSearch, documentNumberSearch);
        }

        
    }
}
