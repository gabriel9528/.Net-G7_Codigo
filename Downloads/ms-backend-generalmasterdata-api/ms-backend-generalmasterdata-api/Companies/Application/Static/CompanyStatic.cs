using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Companies.Application.Dtos;
using System.Text.Json;

namespace AnaPrevention.GeneralMasterData.Api.Companies.Application.Static
{
    public class CompanyStatic
    {

        public static SettingDto ConvertJsonToSettingDto(string? json)
        {
            try
            {
                if (json == null)
                    return new();


                var list = JsonSerializer.Deserialize<SettingDto>(json, CommonStatic.Options);

                if (list == null)
                    return new();

                return list;
            }
            catch { return new(); }
        }
    }
}
