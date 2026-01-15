namespace DataNotationsEF.Models.OneToOne
{
    public class CarModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        //[ForeignKey("CarCompany")]
        public int CarCompanyId { get; set; }
        public CarCompany? CarCompany { get; set; }
    }
}
