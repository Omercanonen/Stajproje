using System.ComponentModel.DataAnnotations;

namespace Stajproje.Models
{
    public class LoginModel
    {
        private string? _returnurl;

        [Required(ErrorMessage ="Kullanıcı Adı Zorunludur.")]
        public string? Name { get; set; }
        [Required(ErrorMessage ="Şifre Zorunludur.")]
        public string? Password { get; set; }

        public string ReturnUrl
        {
            get
            {
                if (_returnurl == null)
                    return "/Home/Index";
                else
                    return _returnurl;
            }
            set { _returnurl = value; }
        }

    }
}
