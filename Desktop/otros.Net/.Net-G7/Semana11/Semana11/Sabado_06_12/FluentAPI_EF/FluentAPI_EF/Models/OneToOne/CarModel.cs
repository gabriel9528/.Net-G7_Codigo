namespace FluentAPI_EF.Models.OneToOne
{
    public class CarModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int CarCompanyId { get; set; }
        public virtual CarCompany? CarCompany { get; set; }
    }
}
