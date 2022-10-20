using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using webapp_travel_agency.Validations;

namespace webapp_travel_agency.Models;

public class TravelPackage
{
    public int Id { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 5)]
    public string Name { get; set; } = default!;
    
    [Required]
    [Range(0, 5)]
    public int Rating { get; set; }

    [Required]
    [DataType(DataType.Date)]
    [FutureDate]
    [Display(Name = "Start Date")]
    [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
    public DateTime StartDate { get; set; } = default!;
    
    [Required]
    [DataType(DataType.Date)]
    [FutureDate]
    [Display(Name = "End Date")]
    [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
    public DateTime EndDate { get; set; } = default!;
    
    [Required]
    [Range(0, double.MaxValue)]
    [DataType(DataType.Currency)]
    [Column(TypeName = "money")]
    [Display(Name = "Price Per Adult")]
    [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
    public decimal PricePerAdult { get; set; }
    
    [Required]
    [Range(0, double.MaxValue)]
    [DataType(DataType.Currency)]
    [Column(TypeName = "money")]
    [Display(Name = "Price Per Kid")]
    [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
    public decimal PricePerKid { get; set; }
    
    [Required]
    [Range(0, int.MaxValue)]
    [Display(Name = "Amount Of Adults")]
    public int AmountOfAdults { get; set; }
    
    [Required]
    [Range(0, int.MaxValue)]
    [Display(Name = "Amount Of Kids")]
    public int AmountOfKids { get; set; }

    [Required]
    public string State { get; set; } = default!;
    
    [Required]
    public string Region { get; set; } = default!;
    
    [Required]
    public string City { get; set; } = default!;

    [ValidateNever]
    [NotMapped] 
    [DataType(DataType.Currency)]
    [DisplayFormat(DataFormatString = "{0:C}")]
    public decimal Total => PricePerAdult * AmountOfAdults + PricePerKid * AmountOfKids;
}