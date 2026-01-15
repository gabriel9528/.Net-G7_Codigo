namespace FluentAPI_EF.Models.OneToOne
{
    public class CarCompany
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public virtual CarModel? CarModel { get; set; }
    }
}
