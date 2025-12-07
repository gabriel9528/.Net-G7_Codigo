using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Doctors.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.Doctors.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Doctors.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Doctors.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.MedicalAreas.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.MedicalAreas.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.Persons.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Persons.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.Specialties.Entities;
using AnaPrevention.GeneralMasterData.Api.Specialties.Infrastructure.Repositories;

namespace AnaPrevention.GeneralMasterData.Api.Doctors.Application.Validators
{
    public class EditDoctorValidator
    {
        private readonly DoctorRepository _doctorsRepository;
        private readonly SpecialtyRepository _specialtyRepository;
        private readonly DoctorSpecialtyRepository _doctorSpecialtyRepository;
        private readonly MedicalAreaRepository _medicalAreaRepository;

        public EditDoctorValidator(
            DoctorRepository doctorsRepository,
            SpecialtyRepository specialtyRepository,
            DoctorSpecialtyRepository doctorSpecialtyRepository,
            MedicalAreaRepository medicalAreaRepository)
        {
            _doctorsRepository = doctorsRepository;
            _doctorSpecialtyRepository = doctorSpecialtyRepository;
            _specialtyRepository = specialtyRepository;
            _medicalAreaRepository = medicalAreaRepository;
        }

        public Notification Validate(EditDoctorRequest request)
        {
            Notification notification = new();

            if (request.Id == Guid.Empty)
                notification.AddError(DoctorStatic.IdMsgErrorRequiered);


            if (request.ListSpecialty != null)
            {
                foreach (var speciality in request.ListSpecialty)
                {
                    bool CodeTakenForEditSpecialty = _doctorSpecialtyRepository.CodeTakenForEdit(request.Id, speciality.SpecialtyId, speciality.Code);
                    if (CodeTakenForEditSpecialty)
                        notification.AddError(DoctorStatic.CodeSpecialtyMsgErrorDuplicate);


                    Specialty? specialty = _specialtyRepository.GetById(speciality.SpecialtyId);
                    if (specialty == null)
                        notification.AddError(DoctorStatic.DoctorSpecialtyMsgErrorNotFound);
                }
            }

            if (request.Certifications != null)
            {
                foreach (RegisterCertificationsRequest? certificationRequest in request.Certifications)
                {
                    string description = string.IsNullOrWhiteSpace(certificationRequest.Description) ? "" : certificationRequest.Description.Trim();

                    if (string.IsNullOrWhiteSpace(description))
                        notification.AddError(DoctorStatic.DescriptionCertificationsMsgErrorRequiered);

                    if (description.Length > DoctorStatic.DescriptionCertificationsMaxLength)
                        notification.AddError(String.Format(DoctorStatic.DescriptionCertificationsMsgErrorMaxLength, DoctorStatic.DescriptionCertificationsMaxLength.ToString()));

                    string date = string.IsNullOrWhiteSpace(certificationRequest.Date) ? "" : certificationRequest.Date.Trim();


                    if (string.IsNullOrWhiteSpace(date))
                        notification.AddError(DoctorStatic.DescriptionCertificationsMsgErrorRequiered);

                    if (!DateTime.TryParse(date, out _))
                        notification.AddError(DoctorStatic.DateCertificationsMsgErrorFormat);
                }
            }

            if (request.ListMedicalAreaIds != null)
            {
                foreach (var medicalAreaId in request.ListMedicalAreaIds)
                {
                    MedicalArea? medicalArea = _medicalAreaRepository.GetById(medicalAreaId);
                    if (medicalArea == null)
                    {
                        notification.AddError(DoctorStatic.MedicalAreaMsgErrorNotFound);
                        return notification;
                    }
                }
            }

            if (notification.HasErrors())
                return notification;

            string signs = string.IsNullOrWhiteSpace(request.Signs) ? "" : request.Signs.Trim();

            if (!string.IsNullOrWhiteSpace(signs))
            {
                if (signs.Contains("data:image"))
                {
                    int index = signs.IndexOf('/') + 1;
                    string fileExtension = signs[index..signs.LastIndexOf(';')];
                    signs = signs[(signs.LastIndexOf(',') + 1)..];
                    if (!CommonStatic.ImageFormartAccepted.Contains(fileExtension.ToUpper()))
                        notification.AddError(DoctorStatic.SignsMsgErrorExtension);
                }
                else
                    notification.AddError(DoctorStatic.SignsMsgErrorErrorFormart);

                if (notification.HasErrors())
                    return notification;


                if (!Convert.TryFromBase64String(signs, new(new byte[signs.Length]), out _))
                    notification.AddError(DoctorStatic.SignsMsgErrorErrorBase64);

                if (notification.HasErrors())
                    return notification;
            }
            string photo = string.IsNullOrEmpty(request.Photo) ? "" : request.Photo.Trim();

            if (!string.IsNullOrEmpty(photo))
            {
                if (photo.Contains("data:image"))
                {
                    int index = photo.IndexOf('/') + 1;
                    string fileExtension = photo[index..photo.LastIndexOf(';')];
                    photo = photo[(photo.LastIndexOf(',') + 1)..];
                    if (!CommonStatic.ImageFormartAccepted.Contains(fileExtension.ToUpper()))
                        notification.AddError(DoctorStatic.PhotoMsgErrorExtension);
                }
                else
                    notification.AddError(DoctorStatic.PhotoMsgErrorErrorFormart);

                if (notification.HasErrors())
                    return notification;


                if (!Convert.TryFromBase64String(photo, new(new byte[photo.Length]), out _))
                    notification.AddError(DoctorStatic.PhotoMsgErrorErrorBase64);

                if (notification.HasErrors())
                    return notification;

            }
            string code = string.IsNullOrEmpty(request.Code) ? "" : request.Code.Trim();


            if (!string.IsNullOrEmpty(code))
            {

                if (code.Length > DoctorStatic.CodeMaxLength)
                    notification.AddError(String.Format(DoctorStatic.CodeMsgErrorMaxLength, DoctorStatic.CodeMaxLength.ToString()));

                if (notification.HasErrors())
                    return notification;


                bool CodeTakenForEdit = _doctorsRepository.CodeTakenForEdit(request.Id, code);

                if (CodeTakenForEdit)
                    notification.AddError(DoctorStatic.CodeMsgErrorDuplicate);

            }





            return notification;
        }
    }
}
