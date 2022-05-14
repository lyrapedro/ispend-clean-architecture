using iSpend.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace iSpend.Application.DTOs;

public class InstallmentDTO
{
    public int Id { get; set; }

    public int Sequence { get; set; }

    [Required(ErrorMessage = "The price is required")]
    [Column(TypeName = "decimal(18, 2")]
    [DataType(DataType.Currency)]
    public decimal Price { get; private set; }

    public bool Paid { get; private set; }

    public DateTime ExpiresAt { get; set; }

    [Required(ErrorMessage = "The purchase is required")]
    public int PurchaseId { get; set; }

    [JsonIgnore]
    public Purchase Purchase { get; set; }
}
