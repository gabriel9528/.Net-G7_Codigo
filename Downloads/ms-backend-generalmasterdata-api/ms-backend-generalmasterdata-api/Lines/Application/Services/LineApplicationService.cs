using CSharpFunctionalExtensions;
using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.Families.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Lines.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.Lines.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Lines.Infrastructure.Repositories;

namespace AnaPrevention.GeneralMasterData.Api.Lines.Application.Services
{
    public class LineApplicationService
    {
        private readonly AnaPreventionContext _context;
        private readonly RegisterLineValidator _registerLineValidator;
        private readonly EditLineValidator _editLineValidator;
        private readonly LineRepository _lineRepository;

        public LineApplicationService(
       AnaPreventionContext context,
       RegisterLineValidator registerLineValidator,
       EditLineValidator editLineValidator,
       LineRepository lineRepository)
        {
            _context = context;
            _registerLineValidator = registerLineValidator;
            _editLineValidator = editLineValidator;
            _lineRepository = lineRepository;
        }

        public Result<RegisterLineResponse, Notification> RegisterLine(RegisterLineRequest request, Guid companyId, Guid userId)
        {
            Notification notification = _registerLineValidator.Validate(request, companyId);

            if (notification.HasErrors())
                return notification;


            string description = request.Description.Trim();
            string code = GenerateCode(companyId);
            Guid lineTypeId = request.LineTypeId;
            int orderRow = request.OrderRow;

            Line line = new(description, code, companyId, lineTypeId, Guid.NewGuid(), orderRow);

            _lineRepository.Save(line);

            _context.SaveChanges(userId);

            var response = new RegisterLineResponse
            {
                Id = line.Id,
                Code = line.Code,
                Description = line.Description,
                Status = line.Status,
                CompanyId = line.CompanyId,
                LineTypeId = line.LineTypeId,
            };

            return response;
        }

        public string GenerateCode(Guid companyId)
        {
            return _lineRepository.GenerateCode(companyId);
        }
        public EditLineResponse EditLine(EditLineRequest request, Line line, Guid userId)
        {
            line.Description = request.Description.Trim();
            line.Code = request.Code.Trim();
            line.Status = request.Status;
            line.LineTypeId = request.LineTypeId;


            _context.SaveChanges(userId);

            var response = new EditLineResponse
            {
                Id = line.Id,
                Code = line.Code,
                Description = line.Description,
                Status = line.Status,
                CompanyId = line.CompanyId,
                LineTypeId = request.LineTypeId,
            };

            return response;
        }

        public EditLineResponse ActiveLine(Line line, Guid userId)
        {
            line.Status = true;

            _context.SaveChanges(userId);

            var response = new EditLineResponse
            {
                Id = line.Id,
                Description = line.Description,
                CompanyId = line.CompanyId,
                Status = line.Status,
                LineTypeId = line.LineTypeId
            };

            return response;
        }
        public Notification ValidateEditLineRequest(EditLineRequest request, Guid companyId)
        {
            return _editLineValidator.Validate(request, companyId);
        }
        public EditLineResponse RemoveLine(Line line, Guid userId)
        {
            line.Status = false;
            _context.SaveChanges(userId);

            var response = new EditLineResponse
            {
                Id = line.Id,
                Description = line.Description,
                Status = line.Status,
                CompanyId = line.CompanyId,
                LineTypeId = line.LineTypeId,
            };

            return response;
        }
        public Line? GetById(Guid id)
        {
            return _lineRepository.GetById(id);
        }

        public LineDto? GetDtoById(Guid id, Guid companyId)
        {
            return _lineRepository.GetDtoById(id, companyId);
        }

        public List<LineDto> GetListAll(Guid companyId)
        {
            return _lineRepository.GetListAll(companyId);
        }
        public Tuple<IEnumerable<LineDto>, PaginationMetadata> GetList(int pageNumber, int pageSize, Guid companyId, bool status, string descriptionSearch = "", string codeSearch = "")
        {
            return _lineRepository.GetList(pageNumber, pageSize, companyId, status, descriptionSearch, codeSearch);
        }
    }
}
