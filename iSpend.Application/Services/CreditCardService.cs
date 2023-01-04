using AutoMapper;
using iSpend.Application.DTOs;
using iSpend.Application.Interfaces;
using iSpend.Domain.Entities;
using iSpend.Domain.Interfaces;

namespace iSpend.Application.Services;

public class CreditCardService : ICreditCardService
{
    private ICreditCardRepository _creditCardRepository;
    private readonly IMapper _mapper;

    public CreditCardService(ICreditCardRepository creditCardRepository, IMapper mapper)
    {
        _creditCardRepository = creditCardRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CreditCardDto>> GetCreditCards(string userId)
    {
        var creditCards = await _creditCardRepository.GetCreditCards(userId);
        return _mapper.Map<IEnumerable<CreditCardDto>>(creditCards);
    }

    public async Task<CreditCardDto> GetById(int id)
    {
        var creditCard = await _creditCardRepository.GetById(id);
        return _mapper.Map<CreditCardDto>(creditCard);
    }

    public async Task<IEnumerable<CreditCardDto>> GetByName(string userId, string name)
    {
        IEnumerable<CreditCardDto> creditCards;

        if (!string.IsNullOrEmpty(name))
        {
            var query = await _creditCardRepository.GetByName(userId, name);
            creditCards = query.Select(c => _mapper.Map<CreditCardDto>(c)).ToList();
        }
        else
        {
            creditCards = await GetCreditCards(userId);
        }

        return creditCards;
    }

    public async Task<CreditCardDto> GetCreditCardFromSubscription(int subscriptionId)
    {
        var subscription = await _creditCardRepository.GetCreditCardFromSubscription(subscriptionId);
        return _mapper.Map<CreditCardDto>(subscription);
    }

    public async Task<CreditCardDto> GetCreditCardFromPurchase(int purchaseId)
    {
        var purchase = await _creditCardRepository.GetCreditCardFromPurchase(purchaseId);
        return _mapper.Map<CreditCardDto>(purchase);
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
