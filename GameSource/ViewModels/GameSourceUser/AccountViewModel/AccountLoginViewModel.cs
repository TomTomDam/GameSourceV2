using GameSource.Models.GameSourceUser;
using System.ComponentModel.DataAnnotations;

namespace GameSource.ViewModels.GameSourceUser.AccountViewModel
{
    public class AccountLoginViewModel
    {
        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
