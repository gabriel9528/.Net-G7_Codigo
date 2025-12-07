using CSharpFunctionalExtensions;
using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.MeasurementUnits.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.MeasurementUnits.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.MeasurementUnits.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.MeasurementUnits.Infrastructure.Repositories;

namespace AnaPrevention.GeneralMasterData.Api.MeasurementUnits.Application.Services
{
    public class UomApplicationService
    {
        private readonly AnaPreventionContext _context;
        private readonly RegisterUomValidator _registerUomValidator;
        private readonly EditUomValidator _editUomValidator;
        private readonly UomRepository _uomRepository;

        public UomApplicationService(
       AnaPreventionContext context,
       RegisterUomValidator registerUomValidator,
       EditUomValidator editUomValidator,
       UomRepository uomRepository)
        {
            _context = context;
            _registerUomValidator = registerUomValidator;
            _editUomValidator = editUomValidator;
            _uomRepository = uomRepository;
        }

        public Result<RegisterUomResponse, Notification> RegisterUom(RegisterUomRequest request,Guid userId)
        {
            Notification notification = _registerUomValidator.Validate(request);

            if (notification.HasErrors())
                return notification;


            string description = request.Description.Trim();
            string code = GenerateCode(); 
            string fiscalCode = request.FiscalCode.Trim();
            string abbreviation = request.Abbreviation.Trim();


            Uom uom = new(description, code, fiscalCode, abbreviation,Guid.NewGuid());

            _uomRepository.Save(uom);

            _context.SaveChanges(userId);

            var response = new RegisterUomResponse
            {
                Id = uom.Id,
                Description = uom.Description,
                Code = uom.Code,
                FiscalCode= uom.FiscalCode,
                Status = uom.Status,
            };

            return response;
        }
        public string GenerateCode()
        {
            return _uomRepository.GenerateCode();
        }
        public EditUomResponse EditUom(EditUomRequest request, Uom uom,Guid userId)
        {
            uom.Description = request.Description.Trim();
            uom.Code = request.Code.Trim();
            uom.Abbreviation = request.Abbreviation.Trim();
            uom.FiscalCode = request.FiscalCode.Trim();
            uom.Status = request.Status;


            _context.SaveChanges(userId);

            var response = new EditUomResponse
            {
                Id = uom.Id,
                Description = uom.Description,
                Code = uom.Code,
                FiscalCode = uom.FiscalCode,
                Status = uom.Status
            };

            return response;
        }

        public EditUomResponse ActiveUom(Uom uom,Guid userId)
        {
            uom.Status = true;

            _context.SaveChanges(userId);

            var response = new EditUomResponse
            {
                Id = uom.Id,
                Description = uom.Description,
                Code = uom.Code,
                FiscalCode = uom.FiscalCode,
                Status = uom.Status
            };

            return response;
        }
        public Notification ValidateEditUomRequest(EditUomRequest request)
        {
            return _editUomValidator.Validate(request);
        }

        public EditUomResponse RemoveUom(Uom uom,Guid userId)
        {
            uom.Status = false;
            _context.SaveChanges(userId);

            var response = new EditUomResponse
            {
                Id = uom.Id,
                Description = uom.Description,
                Code = uom.Code,
                FiscalCode = uom.FiscalCode,
                Status = uom.Status
            };

            return response;
        }

        public Uom? GetById(Guid id)
        {
            return _uomRepository.GetById(id);
        }

        public Uom? GetDtoById(Guid id)
        {
            return _uomRepository.GetDtoById(id);
        }

        public List<Uom> GetListAll()
        {
            return _uomRepository.GetListAll();
        }
        public Tuple<IEnumerable<Uom>, PaginationMetadata> GetList(int pageNumber, int pageSize, bool status, string descriptionSearch = "", string codeSearch = "", string FiscalCodeSearch = "")
        {
            return _uomRepository.GetList(pageNumber, pageSize, status, descriptionSearch, codeSearch, FiscalCodeSearch);
        }
    }
}
