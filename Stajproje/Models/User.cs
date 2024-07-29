namespace Stajproje.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Surname{ get; set; }
        public string PhoneNumber { get; set; }
        public int RegStatus { get; set; }

        public string Title { get; set; }
        public string TaxAdmin { get; set; }
        public string TaxNo { get; set; }


    }
}
