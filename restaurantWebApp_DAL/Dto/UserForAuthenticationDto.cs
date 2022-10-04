using System.ComponentModel.DataAnnotations;

namespace restaurantWebApp_DAL.Dto
{
    public class UserForAuthenticationDto
    {
        [Required(ErrorMessage = "User name is required")]
        public string? UserName { get; set; }
        [Required(ErrorMessage = "Password name is required")]
        public string? Password { get; set; }
    }
}
