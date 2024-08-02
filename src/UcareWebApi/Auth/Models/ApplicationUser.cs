using Microsoft.AspNetCore.Identity;

namespace UcareApp.Auth.Models;

public class ApplicationUser : IdentityUser
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
}
