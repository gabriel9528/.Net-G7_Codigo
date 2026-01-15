using System.Text.Json.Serialization;

namespace DataNotationsEF.Models.OneToOne
{
    public class CarCompany
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        [JsonIgnore]
        public virtual CarModel? CarModel { get; set; }
    }
}
