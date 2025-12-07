using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.Common.Application.Dtos
{
    public class EnvelopeDto
    {
        public object? Result { get; set; }
        public List<Error>? Errors { get; set; }
    }
}
