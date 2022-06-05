using iSpend.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace iSpend.Application.DTOs;

public class PurchaseDTO
{
    public int Id { get; private set; }

    [Required(ErrorMessage = "The credit card is required")]
    public int CreditCardId { get; set; }

    public int? CategoryId { get; set; }

    [Required(ErrorMessage = "The name is required")]
    [MinLength(2)]
    [MaxLength(30)]
    public string Name { get; set; }

    [Required(ErrorMessage = "The price is required")]
    [DataType(DataType.Currency)]
    public decimal Price { get; set; }
    public int? NumberOfInstallments { get; set; }
    public bool? Paid { get; set; }

    [Required(ErrorMessage = "The purchase date is required")]
    [DataType(DataType.DateTime)]
    public DateTime PurchasedAt { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime RegisteredAt { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime ModifiedAt { get; set; }

    [JsonIgnore]
    public CreditCard? CreditCard { get; set; }

    [JsonIgnore]
    public Category? Category { get; set; }
}
