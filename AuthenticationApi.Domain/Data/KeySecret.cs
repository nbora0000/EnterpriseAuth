using System.ComponentModel.DataAnnotations;
namespace AuthenticationApi.Domain;
public class KeySecret
{
    [Key]
    public Guid ClientId {get;set;}
    [Required]
    [StringLength(32)]
    public string? ClientSecret {get;set;}
    [Required]
    public string? AudId {get;set;}
}

