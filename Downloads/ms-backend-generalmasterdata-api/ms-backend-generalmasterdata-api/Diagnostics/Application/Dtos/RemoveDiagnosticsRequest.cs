namespace AnaPrevention.GeneralMasterData.Api.Diagnostics.Application.Dtos
{
    public class RemoveDiagnosticsRequest
    {
        public List<Guid> Ids { get; set; } = new();
    }
}
