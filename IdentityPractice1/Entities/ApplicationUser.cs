using Microsoft.AspNetCore.Identity;
namespace Entities;

public class ApplicationUser :IdentityUser<Guid>
{
    public string? personName {  get; set; }
    public string? identificationNo { get; set; }
}
