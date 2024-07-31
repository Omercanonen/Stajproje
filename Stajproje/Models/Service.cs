using System.ComponentModel.DataAnnotations.Schema;

namespace Stajproje.Models
{
    public class Service
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        //[ForeignKey("UserId")]
        //public virtual User User { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int SeriNo {  get; set; }
        public int Warranty {  get; set; }
        public string Fault { get; set; }
        public string Procedures { get; set; }
        public int PartsCost { get; set; }
        public int ServiceCost { get; set; }
        public string Description { get; set; }
        public DateTime DeliveryDate { get; set; }

       
    }
}
