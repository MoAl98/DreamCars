namespace Cars.Models.Domain
{
    public class Car
    {
        public Guid Id { get; set; } 
        public string Modell { get; set; }
        public string Brand { get; set; }
        public int Price { get; set; }
        public DateTime ManufactureDate { get; set; }
        public string category { get; set; }         
       
    }
}
