using System.ComponentModel.DataAnnotations;

namespace CoronaSystem.Models
{
    public class SickMember
    {
        public int ID { get; set; }
        public int IdNumber { get; set; }
        [DataType(DataType.Date)]
        public DateTime ReceivingDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime RecoveryDate { get; set; }
    }
}
