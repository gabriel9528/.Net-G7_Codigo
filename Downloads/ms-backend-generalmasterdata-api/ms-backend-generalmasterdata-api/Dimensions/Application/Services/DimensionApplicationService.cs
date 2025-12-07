using CSharpFunctionalExtensions;
using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.Dimensions.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.Dimensions.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Dimensions.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Dimensions.Infrastructure.Repositories;
using System.Transactions;

namespace AnaPrevention.GeneralMasterData.Api.Dimensions.Application.Services
{
    public class DimensionApplicationService
    {
        private readonly AnaPreventionContext _context;
        private readonly RegisterDimensionValidator _registerDimensionValidator;
        private readonly RegisterCostCenterValidator _registerCostCenterValidator;
        private readonly EditDimensionValidator _editDimensionValidator;
        private readonly DimensionRepository _dimensionRepository;
        private readonly CostCenterRepository _costCenterRepository;

        public DimensionApplicationService(
       AnaPreventionContext context,
       RegisterDimensionValidator registerDimensionValidator,
       RegisterCostCenterValidator registerCostCenterValidator,
       EditDimensionValidator editDimensionValidator,
        DimensionRepository dimensionRepository,
       CostCenterRepository costCenterRepository)
        {
            _context = context;
            _registerDimensionValidator = registerDimensionValidator;
            _editDimensionValidator = editDimensionValidator;
            _registerCostCenterValidator = registerCostCenterValidator;
            _dimensionRepository = dimensionRepository;
            _costCenterRepository = costCenterRepository;
        }

        public Result<RegisterDimensionResponse, Notification> RegisterDimension(RegisterDimensionRequest request, Guid companyId, Guid userId)
        {
            Notification notification = _registerDimensionValidator.Validate(request, companyId);

            if (notification.HasErrors())
                return notification;


            string description = request.Description.Trim();
            string code = GenerateCode(companyId);

            Dimension dimension = new(description, code, companyId, Guid.NewGuid());
            List<RegisterCostCenterResponse> CostCenters = new();

            using (var scope = new TransactionScope())
            {
                _dimensionRepository.Save(dimension);
                foreach (RegisterCostCenterRequest costCenterRequest in request.costCenters)
                {
                    Notification notificationCostCenter = _registerCostCenterValidator.Validate(costCenterRequest);

                    if (notificationCostCenter.HasErrors())
                        return notificationCostCenter;

                    CostCenter costCenter = new(costCenterRequest.Description, costCenterRequest.Code, dimension.Id, Guid.NewGuid());
                    _costCenterRepository.Save(costCenter);
                    CostCenters.Add(new RegisterCostCenterResponse
                    {
                        Code = costCenter.Code,
                        Description = costCenter.Description,
                        Status = costCenter.Status
                    });
                }
                _context.SaveChanges(userId);
                scope.Complete();
            }

            var response = new RegisterDimensionResponse
            {
                Id = dimension.Id,
                Code = dimension.Code,
                Description = dimension.Description,
                Status = dimension.Status,
                CompanyId = dimension.CompanyId,
                costCenters = CostCenters
            };

            return response;
        }
        public string GenerateCode(Guid companyId)
        {
            return _dimensionRepository.GenerateCode(companyId);
        }
        public EditDimensionResponse EditDimension(EditDimensionRequest request, Dimension dimension, List<CostCenter> costCenterList, List<EditCostCenterRequest> costCenterEditRequestList, List<RegisterCostCenterRequest> costCenterInsertRequestList, Guid userId)
        {
            dimension.Description = request.Description.Trim();
            dimension.Code = request.Code.Trim();
            dimension.Status = request.Status;
            List<EditCostCenterResponse> CostCentersReponse = new();
            int loopEdit = 0;
            foreach (EditCostCenterRequest costCenterEditRequest in costCenterEditRequestList)
            {
                costCenterList[loopEdit].Description = costCenterEditRequest.Description.Trim();
                costCenterList[loopEdit].Status = costCenterEditRequest.Status;
                costCenterList[loopEdit].Code = costCenterEditRequest.Code.Trim();
                CostCentersReponse.Add(new EditCostCenterResponse
                {
                    Id = costCenterList[loopEdit].Id,
                    Code = costCenterList[loopEdit].Code,
                    Description = costCenterList[loopEdit].Description,
                    Status = costCenterList[loopEdit].Status
                });
                loopEdit++;
            }
            foreach (RegisterCostCenterRequest costCenterInsertRequest in costCenterInsertRequestList)
            {
                CostCenter costCenter = new(costCenterInsertRequest.Description, costCenterInsertRequest.Code, dimension.Id, Guid.NewGuid());
                _costCenterRepository.Save(costCenter);
                CostCentersReponse.Add(new EditCostCenterResponse
                {
                    Code = costCenter.Code,
                    Description = costCenter.Description,
                    Status = costCenter.Status
                });
            }
            _context.SaveChanges(userId);

            var response = new EditDimensionResponse
            {
                Id = dimension.Id,
                Code = dimension.Code,
                Description = dimension.Description,
                Status = dimension.Status,
                CompanyId = dimension.CompanyId,
                costCenters = CostCentersReponse
            };

            return response;
        }

        public EditDimensionResponse ActiveDimension(Dimension dimension, Guid userId)
        {
            dimension.Status = true;

            _context.SaveChanges(userId);

            var response = new EditDimensionResponse
            {
                Id = dimension.Id,
                Description = dimension.Description,
                CompanyId = dimension.CompanyId,
                Status = dimension.Status
            };

            return response;
        }
        public Notification ValidateEditDimensionRequest(EditDimensionRequest request, Guid companyId)
        {
            return _editDimensionValidator.Validate(request, companyId);
        }
        public EditDimensionResponse RemoveDimension(Dimension dimension, Guid userId)
        {
            dimension.Status = false;
            _context.SaveChanges(userId);

            var response = new EditDimensionResponse
            {
                Id = dimension.Id,
                Description = dimension.Description,
                Status = dimension.Status,
                CompanyId = dimension.CompanyId,
            };

            return response;
        }
        public Dimension? GetById(Guid id)
        {
            return _dimensionRepository.GetById(id);
        }

        public DimensionDto? GetDtoById(Guid id, Guid companyId)
        {
            return _dimensionRepository.GetDtoById(id, companyId);
        }

        public List<DimensionDto> GetListAll(Guid companyId)
        {
            return _dimensionRepository.GetListAll(companyId);
        }

        public Tuple<IEnumerable<DimensionDto>, PaginationMetadata> GetList(int pageNumber, int pageSize, Guid companyId, bool status, string descriptionSearch = "")
        {
            return _dimensionRepository.GetList(pageNumber, pageSize, companyId, status, descriptionSearch);
        }
    }
}
