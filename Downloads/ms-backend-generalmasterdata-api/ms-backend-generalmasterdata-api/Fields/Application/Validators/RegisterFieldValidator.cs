using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Fields.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.Fields.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Fields.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Fields.Domain.Enums;
using AnaPrevention.GeneralMasterData.Api.Fields.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Application.Static;
using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Infrastructure.Repositories;

namespace AnaPrevention.GeneralMasterData.Api.Fields.Application.Validators
{
    public class RegisterFieldValidator : Validator
    {
        private readonly ServiceCatalogRepository _serviceCatalogRepository;

        public RegisterFieldValidator(ServiceCatalogRepository serviceCatalogRepository)
        {
            _serviceCatalogRepository = serviceCatalogRepository;
        }

        public Notification Validate(RegisterFieldRequest request)
        {
            Notification notification = new();

            ValidatorString(notification, request.Description, CommonStatic.DescriptionMaxLength, CommonStatic.DescriptionMsgErrorMaxLength, CommonStatic.DescriptionMsgErrorRequiered, true);

            ValidatorString(notification, request.Uom, FieldStatic.UomMaxLength, FieldStatic.UomMsgErrorMaxLength, FieldStatic.UomMsgErrorRequiered, true);
            ValidatorString(notification, request.Legend, FieldStatic.LegendMaxLength, FieldStatic.LegendMsgErrorMaxLength);

            if (request.FieldType is not FieldType.VARCHAR and not FieldType.INT and not FieldType.DECIMAL and not FieldType.BOOL and not FieldType.SECCTION and not FieldType.LIST)
                notification.AddError(FieldStatic.FieldTypeMsgErrorNotFormat);

            if (request.ListServiceCatalogIds != null)
            {
                foreach (var ServiceCatalogId in request.ListServiceCatalogIds)
                {
                    var serviceCatalog = _serviceCatalogRepository.GetById(ServiceCatalogId);
                    if (serviceCatalog == null)
                    {
                        notification.AddError(ServiceCatalogStatic.ServiceCatalogMsgErrorNoFound);
                    }
                }
            }



            return notification;
        }
    }
}
