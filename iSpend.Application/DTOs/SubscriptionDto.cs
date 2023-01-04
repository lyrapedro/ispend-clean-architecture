using iSpend.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace iSpend.Application.DTOs;

public class SubscriptionDto
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
    public int BillingDay { get; set; }

    public IEnumerable<SubscriptionNodeDto> Nodes { get; set; }

    public DateTime ModifiedAt { get; set; }
    public DateTime RegisteredAt { get; set; }

    [JsonIgnore] public bool Late { get; set; }

    [JsonIgnore] public CreditCard? CreditCard { get; set; }

    public static explicit operator SubscriptionDto(Subscription subscription)
    {
        return new SubscriptionDto
        {
            Id = subscription.Id,
            CreditCardId = subscription.CreditCardId,
            Name = subscription.Name,
            Price = subscription.Price,
            BillingDay = subscription.BillingDay,
            ModifiedAt = subscription.ModifiedAt,
            RegisteredAt = subscription.RegisteredAt
        };
    }

    public static explicit operator Subscription(SubscriptionDto subscription)
    {
        return new Subscription(subscription.CreditCardId, subscription.Name, subscription.Price,
            subscription.BillingDay);
    }
}

public class SubscriptionNodeDto
{
    public int Id { get; set; }
    public int SubscriptionId { get; set; }
    public bool Paid { get; set; }
    public DateOnly ReferenceDate { get; set; }

    public static explicit operator SubscriptionNodeDto(SubscriptionNode subscriptionNode)
    {
        return new SubscriptionNodeDto
        {
            Id = subscriptionNode.Id,
            SubscriptionId = subscriptionNode.SubscriptionId,
            Paid = subscriptionNode.Paid,
            ReferenceDate = subscriptionNode.ReferenceDate
        };
    }
}