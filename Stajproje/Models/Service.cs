using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stajproje.Models
{
    public class Service
    {
        public int Id { get; set; }
        
        [Required]
        public int UserId { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int SeriNo {  get; set; }
        public int Warranty {  get; set; }
        public string Fault { get; set; }
        public string Procedures { get; set; }
        public int PartsCost { get; set; }
        public int ServiceCost { get; set; }
        public string Description { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public int DeliveryStatus => DeliveryDate.HasValue ? 1 : 0;
        public User? User { get; set; } 
    }
}
