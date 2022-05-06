using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iSpend.Application.DTOs;

public class IncomeDTO
{
    public int Id { get; set; }

    [Required(ErrorMessage = "The user is required")]
    public Guid UserId { get; set; }

    [Required(ErrorMessage = "The name is required")]
    [MinLength(2)]
    [MaxLength(50)]
    public string Name { get; private set; }

    [Required(ErrorMessage = "The recurrent is required")]
    public bool Recurrent { get; private set; }

    [Required(ErrorMessage = "The value is required")]
    [Column(TypeName = "decimal(18, 2")]
    [DataType(DataType.Currency)]
    public decimal Value { get; private set; }

    public bool Active { get; private set; }

    public DateTime ModifiedAt { get; set; }

    public DateTime RegisteredAt { get; set; }
}
