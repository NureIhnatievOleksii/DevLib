using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevLib.Application.CQRS.Dtos.Commands;

public class ResetPasswordDto
{
    [Required(ErrorMessage = "Password is required")]
    public string? Password { get; set; }

    [Compare("Password", ErrorMessage = "Passwords do not match")]
    public string? ConfirmPassword { get; set;}

    public string? Email { get; set; }
    public string? Token { get; set; }
}
