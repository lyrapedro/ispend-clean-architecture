using iSpend.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace iSpend.Application.DTOs;

public class SubscriptionDTO
{
    public int Id { get; set; }

    [Required(ErrorMessage = "The Name is required")]
    [MinLength(2)]
    [MaxLength(50)]
    public string Name { get; set; }

    [Required(ErrorMessage = "The Price is required")]
    public decimal Price { get; set; }
    public bool Active { get; set; }
    public DateTime ModifiedAt { get; set; }
    public DateTime RegisteredAt { get; set; }


    [Required(ErrorMessage = "The credt card is required")]
    public int CreditCardId { get; set; }

    [JsonIgnore]
    public CreditCard? CreditCard { get; set; }
}
