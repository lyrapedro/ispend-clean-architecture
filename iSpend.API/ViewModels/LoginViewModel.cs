using System.ComponentModel.DataAnnotations;

namespace iSpend.API.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [StringLength(36, ErrorMessage = "The {0} must be at least {2} and at max " +
        "{1} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}
