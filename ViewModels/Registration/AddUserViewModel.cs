using System.ComponentModel.DataAnnotations;

namespace ViewModels.User;
public class AddUserViewModel
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string? Password { get; set; }

    [Required(ErrorMessage = "Role is required")]
    public string? Role { get; set; }
}
