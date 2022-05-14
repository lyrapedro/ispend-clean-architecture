using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iSpend.Application.DTOs;

public class ExpenseDTO
{
    public int Id { get; set; }

    public string UserId { get; set; }

    [Required(ErrorMessage = "The name is required")]
    [MinLength(2)]
    [MaxLength(50)]
    public string Name { get; set; }

    [Required(ErrorMessage = "The value is required")]
    [Column(TypeName = "decimal(18, 2")]
    [DataType(DataType.Currency)]
    public decimal Value { get; set; }

    [Required(ErrorMessage = "The active is required")]
    public bool Active { get; set; }

    public bool Recurrent { get; set; }

    [Required(ErrorMessage = "The value is required")]
    [Range(1, 31)]
    public int BillingDay { get; set; }

    public int PaidMonths { get; set; }

    public DateTime ModifiedAt { get; set; }

    public DateTime RegisteredAt { get; set; }
}
