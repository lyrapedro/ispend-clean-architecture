using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iSpend.Application.DTOs;

public class GoalDTO
{
    public int Id { get; private set; }

    public string UserId { get; set; }

    [Required(ErrorMessage = "The name is required")]
    [MinLength(2)]
    [MaxLength(30)]
    public string Name { get; set; }

    [MinLength(2)]
    [MaxLength(200)]
    public string Description { get; set; }

    [Required(ErrorMessage = "The value is required")]
    [DataType(DataType.Currency)]
    public decimal GoalValue { get; set; }

    [DataType(DataType.Currency)]
    public decimal? ValueSaved { get; set; }

    [Required(ErrorMessage = "The start date is required")]
    [DataType(DataType.DateTime)]
    public DateTime StartDate { get; set; }

    [Required(ErrorMessage = "The end date is required")]
    [DataType(DataType.DateTime)]
    public DateTime EndDate { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime ModifiedAt { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime RegisteredAt { get; set; }
}
