using iSpend.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace iSpend.Application.DTOs;

public class PurchaseDTO
{
    public int Id { get; set; }

    [Required(ErrorMessage = "The name is required")]
    [MinLength(2)]
    [MaxLength(50)]
    public string Name { get; private set; }

    [Required(ErrorMessage = "The price is required")]
    [Column(TypeName = "decimal(18, 2")]
    [DataType(DataType.Currency)]
    public decimal Price { get; private set; }
    public DateTime PurchasedAt { get; private set; }

    [Required(ErrorMessage = "The credt card is required")]
    public int CreditCardId { get; set; }
    public CreditCard CreditCard { get; set; }
    public int CategoryId { get; set; }

    [JsonIgnore]
    public Category Category { get; set; }
}
