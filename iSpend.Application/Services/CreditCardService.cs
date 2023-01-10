using iSpend.Application.DTOs;
using iSpend.Application.Interfaces;
using iSpend.Domain.Entities;
using iSpend.Domain.Interfaces;

namespace iSpend.Application.Services;

public class CreditCardService : ICreditCardService
{
    private readonly ICreditCardRepository _creditCardRepository;

    public CreditCardService(ICreditCardRepository creditCardRepository)
    {
        _creditCardRepository = creditCardRepository;
    }

    public async Task<IEnumerable<CreditCardDto>> GetCreditCards(string userId)
    {
        var creditCards = await _creditCardRepository.GetCreditCards(userId);
        return creditCards.Select(c => (CreditCardDto)c);
    }

    public async Task<CreditCardDto> GetById(int id)
    {
        var creditCard = await _creditCardRepository.GetById(id);
        return (CreditCardDto)creditCard;
    }

    public async Task<IEnumerable<CreditCardDto>> GetByName(string userId, string name)
    {
        IEnumerable<CreditCardDto> creditCards;

        if (!string.IsNullOrEmpty(name))
        {
            var query = await _creditCardRepository.GetByName(userId, name);
            creditCards = query.Select(c => (CreditCardDto)c);
        }
        else
        {
            creditCards = await GetCreditCards(userId);
        }

        return creditCards;
    }

    public async Task<CreditCardDto> GetCreditCardFromSubscription(int subscriptionId)
    {
        var creditCard = await _creditCardRepository.GetCreditCardFromSubscription(subscriptionId);
        return (CreditCardDto)creditCard;
    }

    public async Task<CreditCardDto> GetCreditCardFromPurchase(int purchaseId)
    {
        var creditCard = await _creditCardRepository.GetCreditCardFromPurchase(purchaseId);
        return (CreditCardDto)creditCard;
    }

    public async Task Add(CreditCardDto creditCardDto)
    {
        var creditCard = (CreditCard)creditCardDto;
        await _creditCardRepository.Create(creditCard);
    }

    public async Task Update(CreditCardDto creditCardDto)
    {
        creditCardDto.ModifiedAt = DateTime.Now;
        var creditCard = (CreditCard)creditCardDto;
        await _creditCardRepository.Update(creditCard);
    }

    public async Task Remove(CreditCardDto creditCardDto)
    {
        var creditCard = (CreditCard)creditCardDto;
        await _creditCardRepository.Remove(creditCard);
    }
}