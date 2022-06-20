using iSpend.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace iSpend.Application.DTOs;

public class SubscriptionDTO
{
    public int Id { get; private set; }

    [Required(ErrorMessage = "The credit card is required")]
    public int CreditCardId { get; set; }

    [Required(ErrorMessage = "The name is required")]
    [MinLength(2)]
    [MaxLength(30)]
    public string Name { get; set; }

    [Required(ErrorMessage = "The price is required")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "The payment date is required")]
    public DateTime PaymentAt { get; set; }

    [Required(ErrorMessage = "The active field is required")]
    public bool Active { get; set; }
    public DateTime ModifiedAt { get; set; }
    public DateTime RegisteredAt { get; set; }

    [JsonIgnore]
    public bool Late { get; set; }

    [JsonIgnore]
    public CreditCard? CreditCard { get; set; }
}
