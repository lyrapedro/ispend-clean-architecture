using iSpend.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace iSpend.Application.DTOs;

public class InstallmentDto
{
    public int Id { get; private set; }

    [Required(ErrorMessage = "The purchase is required")]
    public int PurchaseId { get; set; }

    public int Order { get; set; }

    [Required(ErrorMessage = "The price is required")]
    [DataType(DataType.Currency)]
    public decimal Price { get; set; }

    public bool? Paid { get; set; }

    [DataType(DataType.DateTime)] public DateTime? ExpiresAt { get; set; }

    [JsonIgnore] public Purchase Purchase { get; private set; }

    public static explicit operator InstallmentDto(Installment installment)
    {
        return new InstallmentDto
        {
            Id = installment.Id,
            PurchaseId = installment.PurchaseId,
            Order = installment.Order,
            Price = installment.Price,
            Paid = installment.Paid,
            ExpiresAt = installment.ExpiresAt
        };
    }

    public static explicit operator Installment(InstallmentDto installment)
    {
        return new Installment(installment.PurchaseId, installment.Order, installment.Price, installment.Paid,
            installment.ExpiresAt.Value);
    }
}