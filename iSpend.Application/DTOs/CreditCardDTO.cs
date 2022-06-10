using System.ComponentModel.DataAnnotations;

namespace iSpend.Application.DTOs;

public class CreditCardDTO
{
    public int Id { get; private set; }

    public string? UserId { get; set; }

    [Required(ErrorMessage = "The name is required")]
    [MinLength(2)]
    [MaxLength(30)]
    public string Name { get; set; }

    [Required(ErrorMessage = "The limit is required")]
    [DataType(DataType.Currency)]
    public decimal Limit { get; set; }

    [Required(ErrorMessage = "The expiration day is required")]
    [Range(1, 31)]
    public int ExpirationDay { get; set; }

    [Required(ErrorMessage = "The closing day is required")]
    [Range(1, 31)]
    public int ClosingDay { get; set; }

    public DateTime ModifiedAt { get; set; }
    public DateTime RegisteredAt { get; set; }
}
