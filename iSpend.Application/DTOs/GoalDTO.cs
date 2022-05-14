using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iSpend.Application.DTOs;

public class GoalDTO
{
    public int Id { get; set; }

    public string UserId { get; set; }

    [Required(ErrorMessage = "The name is required")]
    [MinLength(2)]
    [MaxLength(50)]
    public string Name { get; set; }

    [MinLength(2)]
    [MaxLength(200)]
    public string Description { get; set; }

    [Required(ErrorMessage = "The value is required")]
    [Column(TypeName = "decimal(18, 2")]
    [DataType(DataType.Currency)]
    public decimal GoalValue { get; set; }

    [Column(TypeName = "decimal(18, 2")]
    [DataType(DataType.Currency)]
    public decimal ValueSaved { get; set; }

    [Required(ErrorMessage = "The start date is required")]
    public DateTime StartDate { get; set; }

    [Required(ErrorMessage = "The end date is required")]
    public DateTime EndDate { get; set; }

    public DateTime ModifiedAt { get; set; }

    public DateTime RegisteredAt { get; set; }
}
