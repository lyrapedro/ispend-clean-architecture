using System.ComponentModel.DataAnnotations;
using iSpend.Domain.Entities;

namespace iSpend.Application.DTOs;

public class CreditCardDto
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

    public static explicit operator CreditCardDto(CreditCard creditCard)
    {
        return new CreditCardDto
        {
            Id = creditCard.Id,
            UserId = creditCard.UserId,
            Name = creditCard.Name,
            Limit = creditCard.Limit,
            ExpirationDay = creditCard.ExpirationDay,
            ClosingDay = creditCard.ClosingDay,
            ModifiedAt = creditCard.ModifiedAt,
            RegisteredAt = creditCard.RegisteredAt
        };
    }
    
    public static explicit operator CreditCard(CreditCardDto creditCardDto)
    {
        return new CreditCard(creditCardDto.UserId, creditCardDto.Name, creditCardDto.Limit,
            creditCardDto.ExpirationDay, creditCardDto.ClosingDay);
    }
}
