using iSpend.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace iSpend.Application.DTOs;

public class InstallmentDTO
{
    public int Id { get; private set; }

    [Required(ErrorMessage = "The purchase is required")]
    public int PurchaseId { get; set; }

    public int Order { get; set; }

    [Required(ErrorMessage = "The price is required")]
    [DataType(DataType.Currency)]
    public decimal Price { get; set; }

    public bool? Paid { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime? ExpiresAt { get; set; }

    [JsonIgnore]
    public Purchase Purchase { get; private set; }
}
