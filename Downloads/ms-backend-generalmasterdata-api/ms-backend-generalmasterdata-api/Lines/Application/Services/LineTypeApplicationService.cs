using AnaPrevention.GeneralMasterData.Api.Lines.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.Lines.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Lines.Infrastructure.Repositories;

namespace AnaPrevention.GeneralMasterData.Api.Lines.Application.Services
{
    public class LineTypeApplicationService
    {
        private readonly LineTypeRepository _lineTyeRepository;

        public LineTypeApplicationService(LineTypeRepository linetyeRepository)
        {
            _lineTyeRepository = linetyeRepository;
        }

        public List<LineType> GetListAll()
        {
            return _lineTyeRepository.GetListAll();
        }
    }
}
