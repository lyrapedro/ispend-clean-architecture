﻿using AutoMapper;
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

    public async Task<IEnumerable<CreditCardDTO>> GetCreditCards(string userId)
    {
        var creditCards = await _creditCardRepository.GetCreditCards(userId);
        return _mapper.Map<IEnumerable<CreditCardDTO>>(creditCards);
    }

    public async Task<CreditCardDTO> GetById(int id)
    {
        var creditCard = await _creditCardRepository.GetById(id);
        return _mapper.Map<CreditCardDTO>(creditCard);
    }

    public async Task<IEnumerable<CreditCardDTO>> GetByName(string userId, string name)
    {
        IEnumerable<CreditCardDTO> creditCards;

        if (!string.IsNullOrEmpty(name))
        {
            var query = await _creditCardRepository.GetByName(userId, name);
            creditCards = query.Select(c => _mapper.Map<CreditCardDTO>(c)).ToList();
        }
        else
        {
            creditCards = await GetCreditCards(userId);
        }

        return creditCards;
    }

    public async Task<CreditCardDTO> GetCreditCardFromSubscription(int subscriptionId)
    {
        var subscription = await _creditCardRepository.GetCreditCardFromSubscription(subscriptionId);
        return _mapper.Map<CreditCardDTO>(subscription);
    }

    public async Task<CreditCardDTO> GetCreditCardFromPurchase(int purchaseId)
    {
        var purchase = await _creditCardRepository.GetCreditCardFromPurchase(purchaseId);
        return _mapper.Map<CreditCardDTO>(purchase);
    }

    public async Task Add(CreditCardDTO creditCardDTO)
    {
        var creditCard = _mapper.Map<CreditCard>(creditCardDTO);
        await _creditCardRepository.Create(creditCard);
    }

    public async Task Update(CreditCardDTO creditCardDTO)
    {
        var creditCard = _mapper.Map<CreditCard>(creditCardDTO);
        await _creditCardRepository.Update(creditCard);
    }

    public async Task Remove(CreditCardDTO creditCardDTO)
    {
        var creditCard = _mapper.Map<CreditCard>(creditCardDTO);
        await _creditCardRepository.Remove(creditCard);
    }
}
