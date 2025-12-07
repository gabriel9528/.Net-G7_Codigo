using AnaPrevention.GeneralMasterData.Api.Common.Domain.Enums;
using AnaPrevention.GeneralMasterData.Api.Persons.Domain.Enums;

namespace AnaPrevention.GeneralMasterData.Api.Persons.Application.Dtos
{
    public class OptionYesOrNot
    {
        public Guid MasterDataId { get; set; }
        public string Description { get; set; } = string.Empty;
        public YesOrNot IsChecked { get; set; } = YesOrNot.NONE;
        public InternalType InternalType { get; set; }
    }
}
