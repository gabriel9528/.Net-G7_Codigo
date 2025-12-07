using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Doctors.Application.Static;
using AnaPrevention.GeneralMasterData.Api.MedicalAreas.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.MedicalAreas.Infrastructure.Repositories;

namespace AnaPrevention.GeneralMasterData.Api.Doctors.Application.Validators
{
    public class RegisterListMedicalAreaIdsValidator
    {

        private readonly MedicalAreaRepository _medicalAreaRepository;

        public RegisterListMedicalAreaIdsValidator(MedicalAreaRepository medicalAreaRepository)
        {
            _medicalAreaRepository = medicalAreaRepository;
        }

        public Notification Validate(List<Guid>? ListMedicalAreaIds)
        {
            Notification notification = new();
            if (ListMedicalAreaIds != null)
            {
                foreach (var medicalAreaId in ListMedicalAreaIds)
                {
                    MedicalArea? medicalArea = _medicalAreaRepository.GetById(medicalAreaId);
                    if (medicalArea == null)
                    {
                        notification.AddError(DoctorStatic.MedicalAreaMsgErrorNotFound);
                        return notification;
                    }
                }
            }
            return notification;
        }
    }
}
