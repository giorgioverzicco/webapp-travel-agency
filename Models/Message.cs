using System.ComponentModel.DataAnnotations;

namespace webapp_travel_agency.Models;

public class Message
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(50)]
    public string FirstName { get; set; } = default!;
    
    [Required]
    [StringLength(50)]
    public string LastName { get; set; } = default!;
    
    [Required]
    [EmailAddress]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = default!;
    
    [Required]
    [Phone]
    [DataType(DataType.PhoneNumber)]
    public string PhoneNumber { get; set; } = default!;
    
    [Required]
    public string Content { get; set; } = default!;
}