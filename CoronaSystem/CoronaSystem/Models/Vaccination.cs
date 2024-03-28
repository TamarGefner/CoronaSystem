using System.ComponentModel.DataAnnotations;

namespace CoronaSystem.Models
{
    public class Vaccination
    {
        public int ID { get; set; }
        public int IdNumber { get; set; }
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public string? Manufacturer { get; set; }
    }
}
