using CSharpFunctionalExtensions;
using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Services;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.Doctors.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.Doctors.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Doctors.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Doctors.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.MedicalAreas.Application.Dtos;

using System.Text.Json;
using System.Transactions;


namespace AnaPrevention.GeneralMasterData.Api.Doctors.Application.Services
{
    public class DoctorApplicationService
    {
        private readonly AnaPreventionContext _context;
        private readonly RegisterDoctorValidator _registerDoctorsValidator;
        private readonly EditDoctorValidator _editDoctorsValidator;
        private readonly DoctorRepository _doctorsRepository;
        private readonly DoctorSpecialtyRepository _doctorSpecialtyRepository;
        private readonly DoctorMedicalAreaRepository _doctorMedicalAreaRepository;
        

        private readonly RegisterListMedicalAreaIdsValidator _registerListMedicalAreaIdsValidator;
       

        public DoctorApplicationService(
           AnaPreventionContext context,
           RegisterDoctorValidator registerDoctorsValidator,
           EditDoctorValidator editDoctorsValidator,
           DoctorRepository doctorsRepository,
           DoctorSpecialtyRepository doctorSpecialtyRepository,
           
           DoctorMedicalAreaRepository doctorMedicalAreaRepository,
           RegisterListMedicalAreaIdsValidator registerListMedicalAreaIdsValidator
           
           )
        {
            _context = context;
            _registerDoctorsValidator = registerDoctorsValidator;
            _editDoctorsValidator = editDoctorsValidator;
            _doctorsRepository = doctorsRepository;
            _doctorSpecialtyRepository = doctorSpecialtyRepository;
            
            _doctorMedicalAreaRepository = doctorMedicalAreaRepository;
            _registerListMedicalAreaIdsValidator = registerListMedicalAreaIdsValidator;
            
        }

        public async Task<Result<RegisterDoctorResponse, Notification>> RegisterDoctors(RegisterDoctorRequest request, Guid userId)
        {
            Notification notification = _registerDoctorsValidator.Validate(request);

            if (notification.HasErrors())
                return notification;

            string signs = request.Signs.Trim();
            string Photo = request.Photo.Trim();
            string code = request.Code ?? "";
            string certifications = JsonSerializer.Serialize(request.Certifications);
            Guid personId = request.PersonId;
            List<RegisterDoctorSpecialtyRequest>? listSpeciality = request.ListSpeciality;
            var _fileApplicationsService = new FileApplicationsService();

            string signsUrl = "";
            if (!string.IsNullOrWhiteSpace(signs))
            {
                Result<FileDto, Notification> resultSigns = _fileApplicationsService.ConverterBase64InBytes(signs, "");
                if (resultSigns.IsFailure)
                    return resultSigns.Error;

                using var stream = new MemoryStream(resultSigns.Value.Bytes);

                //var fileS3 = new S3FileDto
                //{
                //    FileName = "doctors/huella/" + request.PersonId + "." + resultSigns.Value.FileExtension,
                //    stream = stream,
                //};

                //S3ResponseDto signUpload = await _s3bucketService.UploadFileAsync(fileS3, CommonStatic.BucketNameFiles);

                //if (signUpload.StatusCode != 200)
                //{
                //    notification.AddError(signUpload.Message);
                //    return notification;
                //}

                //_fileApplicationsService.UploadFileInCloudAsync(resultSigns.Value);

                if (resultSigns.Value != null)
                    signsUrl = "doctors/huella/" + request.PersonId + "." + resultSigns.Value.FileExtension;
            }

            string? PhotoUrl = "";
            if (!string.IsNullOrWhiteSpace(signs))
            {
                Result<FileDto, Notification> resultPhoto = _fileApplicationsService.ConverterBase64InBytes(Photo, "");

                if (resultPhoto.IsFailure)
                    return resultPhoto.Error;

                using var stream = new MemoryStream(resultPhoto.Value.Bytes);

                //var fileS3 = new S3FileDto
                //{
                //    FileName = "doctors/photo/" + request.PersonId + "." + resultPhoto.Value.FileExtension,
                //    stream = stream,
                //};

                //var photoUpload = await _s3bucketService.UploadFileAsync(fileS3, CommonStatic.BucketNameFiles);

                //if (photoUpload.StatusCode != 200)
                //{
                //    notification.AddError(photoUpload.Message);
                //    return notification;
                //}

                if (resultPhoto.Value != null)
                    PhotoUrl = "doctors/photo/" + request.PersonId + "." + resultPhoto.Value.FileExtension;
            }

            Doctor doctor = new(code, certifications, PhotoUrl, signsUrl, personId, Guid.NewGuid());

            List<Guid>? ListMedicalAreaIds = request.ListMedicalAreaIds;

            using (var scope = new TransactionScope())
            {
                _doctorsRepository.Save(doctor);

                if (listSpeciality != null)
                {
                    foreach (var speciality in listSpeciality)
                    {
                        _doctorSpecialtyRepository.Save(new DoctorSpecialty(speciality.Code, doctor.Id, speciality.SpecialtyId, Guid.NewGuid()));
                    }
                }

                if (ListMedicalAreaIds != null)
                {
                    foreach (var medicalAreaId in ListMedicalAreaIds)
                    {
                        _doctorMedicalAreaRepository.Save(new DoctorMedicalArea(medicalAreaId, doctor.Id, Guid.NewGuid()));
                    }
                }

                _context.SaveChanges(userId);
                scope.Complete();
            }
            var response = new RegisterDoctorResponse
            {
                Id = doctor.Id,
                Signs = doctor.Signs,
                Photo = doctor.Photo,
                Code = doctor.Code,
                Status = doctor.Status,
                PersonId = doctor.PersonId
            };

            return response;
        }

        public async Task<Result<EditDoctorResponse, Notification>> EditDoctors(EditDoctorRequest request, Doctor doctor, Guid userId)
        {
            var _fileApplicationsService = new FileApplicationsService();
            Notification notification = new();

            if (!string.IsNullOrWhiteSpace(request.Signs))
            {
                Result<FileDto, Notification> resultSign = _fileApplicationsService.ConverterBase64InBytes(request.Signs);

                if (resultSign.IsFailure)
                    return resultSign.Error;

                using var stream = new MemoryStream(resultSign.Value.Bytes);

                //var fileS3 = new S3FileDto
                //{
                //    FileName = "doctors/huella/" + doctor.PersonId + "." + resultSign.Value.FileExtension,
                //    stream = stream,
                //};

                //var photoUpload = await _s3bucketService.UploadFileAsync(fileS3, CommonStatic.BucketNameFiles);

                //if (photoUpload.StatusCode != 200)
                //{
                //    notification.AddError(photoUpload.Message);
                //    return notification;
                //}


                if (resultSign.Value != null)
                    doctor.Signs = "doctors/huella/" + doctor.PersonId + "." + resultSign.Value.FileExtension;
            }

            if (!string.IsNullOrWhiteSpace(request.Photo))
            {
                Result<FileDto, Notification> resultPhoto = _fileApplicationsService.ConverterBase64InBytes(request.Photo);

                if (resultPhoto.IsFailure)
                    return resultPhoto.Error;
                using var stream = new MemoryStream(resultPhoto.Value.Bytes);

                //var fileS3 = new S3FileDto
                //{
                //    FileName = "doctors/photo/" + doctor.PersonId + "." + resultPhoto.Value.FileExtension,
                //    stream = stream,
                //};

                //var photoUpload = await _s3bucketService.UploadFileAsync(fileS3, CommonStatic.BucketNameFiles);

                //if (photoUpload.StatusCode != 200)
                //{
                //    notification.AddError(photoUpload.Message);
                //    return notification;
                //}

                if (resultPhoto.Value != null)
                    doctor.Photo = "doctors/photo/" + doctor.PersonId + "." + resultPhoto.Value.FileExtension;
            }

            doctor.Code = request.Code ?? "";
            doctor.Certifications = JsonSerializer.Serialize(request.Certifications);

            List<EditDoctorSpecialtyRequest>? listSpecialtyRequest = request.ListSpecialty;

            if (listSpecialtyRequest != null)
            {
                List<DoctorSpecialtyDto>? ListSpeciality = _doctorSpecialtyRepository.GetSpecialyDtoByDoctoId(doctor.Id);
                if (ListSpeciality != null)
                {
                    foreach (DoctorSpecialtyDto SpecialityDto in ListSpeciality)
                    {
                        EditDoctorSpecialtyRequest? specialty = listSpecialtyRequest.Find(x => x.SpecialtyId == SpecialityDto.SpecialtyId);

                        if (specialty == null)
                        {
                            var businessEconomicActivity = _doctorSpecialtyRepository.GetbyDoctorIdSpecialtyId(doctor.Id, SpecialityDto.SpecialtyId);
                            if (businessEconomicActivity != null)
                                _doctorSpecialtyRepository.Remove(businessEconomicActivity);
                        }
                    }
                    foreach (var SpecialtyRequest in listSpecialtyRequest)
                    {
                        DoctorSpecialtyDto? specialtyDto = ListSpeciality.Find(x => x.SpecialtyId == SpecialtyRequest.SpecialtyId);

                        if (specialtyDto == null)
                            _doctorSpecialtyRepository.Save(new DoctorSpecialty(SpecialtyRequest.Code, doctor.Id, SpecialtyRequest.SpecialtyId, Guid.NewGuid()));
                        else
                        {
                            DoctorSpecialty? doctorSpecialty = _doctorSpecialtyRepository.GetbyDoctorIdSpecialtyId(doctor.Id, specialtyDto.SpecialtyId);
                            if (doctorSpecialty != null)
                            {
                                doctorSpecialty.Code = SpecialtyRequest.Code;
                            }

                        }
                    }
                }
            }

            List<Guid>? ListMedicalAreaIds = request.ListMedicalAreaIds;

            RegisterDoctorMedicalArea(doctor, ListMedicalAreaIds, userId);
            _context.SaveChanges(userId);

            var response = new EditDoctorResponse
            {
                Id = doctor.Id,
                Signs = doctor.Signs,
                Code = doctor.Code,
                Status = doctor.Status
            };

            return response;
        }
        public Result<EditDoctorResponse, Notification> RegisterDoctorMedicalArea(Doctor doctor, List<Guid>? ListMedicalAreaIds, Guid userId)
        {
            Notification notification = _registerListMedicalAreaIdsValidator.Validate(ListMedicalAreaIds);

            if (notification.HasErrors())
                return notification;

            if (ListMedicalAreaIds != null)
            {
                List<MedicalAreaDto>? ListMedicalAreaDto = _doctorMedicalAreaRepository.GetDtoByDoctorId(doctor.Id);
                if (ListMedicalAreaDto != null)
                {
                    foreach (MedicalAreaDto medicalAreaDto in ListMedicalAreaDto)
                    {
                        Guid? medicalAreaId = ListMedicalAreaIds.Find(x => x == medicalAreaDto.Id);

                        if (medicalAreaId == null)
                        {
                            var subsidiaryServiceType = _doctorMedicalAreaRepository.GetbyDoctorIdMedicalAreaId(doctor.Id, medicalAreaDto.Id);
                            if (subsidiaryServiceType != null)
                                _doctorMedicalAreaRepository.Remove(subsidiaryServiceType);
                        }

                    }
                }
                foreach (var medicalAreaId in ListMedicalAreaIds)
                {
                    if (ListMedicalAreaDto != null)
                    {
                        MedicalAreaDto? medicalAreaDto = ListMedicalAreaDto.Find(x => x.Id == medicalAreaId);

                        if (medicalAreaDto == null)
                            _doctorMedicalAreaRepository.Save(new DoctorMedicalArea(medicalAreaId, doctor.Id, Guid.NewGuid()));
                    }

                }
            }
            _context.SaveChanges(userId);

            var response = new EditDoctorResponse
            {
                Id = doctor.Id,
                Signs = doctor.Signs,
                Code = doctor.Code,
                Status = doctor.Status
            };

            return response;
        }

        public EditDoctorResponse ActiveDoctors(Doctor doctors, Guid userId)
        {
            doctors.Status = true;

            _context.SaveChanges(userId);

            var response = new EditDoctorResponse
            {
                Id = doctors.Id,
                Signs = doctors.Signs,
                Code = doctors.Code,
                Status = doctors.Status
            };

            return response;
        }
        public Notification ValidateEditDoctorsRequest(EditDoctorRequest request)
        {
            return _editDoctorsValidator.Validate(request);
        }

        public EditDoctorResponse RemoveDoctors(Doctor doctors, Guid userId)
        {
            doctors.Status = false;
            _context.SaveChanges(userId);

            var response = new EditDoctorResponse
            {
                Id = doctors.Id,
                Signs = doctors.Signs,
                Code = doctors.Code,
                Status = doctors.Status
            };

            return response;
        }

        public Doctor? GetById(Guid id)
        {
            return _doctorsRepository.GetById(id);
        }

        public DoctorDto? GetDtoById(Guid id)
        {
            return _doctorsRepository.GetDtoById(id);
        }
        public List<DoctorDto> GetListAll(string fullName = "")
        {
            return _doctorsRepository.GetListAll(fullName);
        }


      
        public Tuple<IEnumerable<DoctorDto>, PaginationMetadata> GetList(
            int pageNumber, int pageSize, bool status, string displayNameSearch = "", string codeSearch = "", string documentNumberSearch = "", string SpecialtySearch = "")
        {
            var (ListDoctorDto, paginationMetadata) = _doctorsRepository.GetList(pageNumber, pageSize, status, displayNameSearch, codeSearch, documentNumberSearch, SpecialtySearch);

            return new Tuple<IEnumerable<DoctorDto>, PaginationMetadata>
                (ListDoctorDto, paginationMetadata);
        }

      

    }
}
