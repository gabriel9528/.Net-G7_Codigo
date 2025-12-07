using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.CostCenters.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Dimensions.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.Dimensions.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Dimensions.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Dimensions.Infrastructure.Repositories;

namespace AnaPrevention.GeneralMasterData.Api.CostCenters.Application.Services
{
    public class CostCenterApplicationService
    {
        private readonly CostCenterRepository _costCenterRepository;
        private readonly EditCostCenterValidator _editCostCenterValidator;
        private readonly RegisterCostCenterValidator _registerCostCenterValidator;

        public CostCenterApplicationService(CostCenterRepository costCenterRepository, EditCostCenterValidator editCostCenterValidator, RegisterCostCenterValidator registerCostCenterValidator)
        {
            _costCenterRepository = costCenterRepository;
            _editCostCenterValidator = editCostCenterValidator;
            _registerCostCenterValidator = registerCostCenterValidator;
        }

        public CostCenter? GetById(Guid id)
        {
            return _costCenterRepository.GetById(id);
        }

        public Notification ValidateEditCostCenterRequest(EditCostCenterRequest request)
        {
            return _editCostCenterValidator.Validate(request);
        }

        public Notification ValidateRegisterCostCenterRequest(RegisterCostCenterRequest request)
        {
            return _registerCostCenterValidator.Validate(request);
        }
        public RegisterCostCenterRequest ConvertToRegisterValidator(EditCostCenterRequest request)
        {
            RegisterCostCenterRequest registerRequest = new()
            {
                Code = request.Code,
                Description = request.Description,
            };
            return registerRequest;
        }

        public List<CostCenter>? GetListAll(Guid dimensionId)
        {
            return _costCenterRepository.GetDtoByDimensionId(dimensionId);
        }

        public Tuple<IEnumerable<CostCenter>, PaginationMetadata> GetList(int pageNumber, int pageSize, Guid dimensionId, bool status, string descriptionSearch = "", string codeSearch = "")
        {
            return _costCenterRepository.GetList(pageNumber, pageSize, dimensionId, status, descriptionSearch, codeSearch);
        }

        public List<CostCenter>? GetDtoByDimensionId(Guid dimensionId)
        {
            return _costCenterRepository.GetDtoByDimensionId(dimensionId);
        }
    }
}
