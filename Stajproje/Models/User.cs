using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stajproje.Models
{
    public class User
    {
        
        public int UserId { get; set; }
        [Required(ErrorMessage = "Lütfen isim giriniz.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Lütfen email giriniz.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Lütfen soyisim giriniz.")]
        public string Surname{ get; set; }
        [Required(ErrorMessage = "Lütfen telefon numarası giriniz.")]
        public string PhoneNumber { get; set; }
        public int RegStatus { get; set; }
        [Required(ErrorMessage = "Lütfen unvan giriniz.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Lütfen vergi dairesi giriniz.")]
        public string TaxAdmin { get; set; }
        [Required(ErrorMessage = "Lütfen vergi numarası giriniz.")]
        public string TaxNo { get; set; }

        public ICollection<Service> Services { get; set; } = new List<Service>();

    }
}
