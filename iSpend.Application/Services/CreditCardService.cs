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

    public async Task<IEnumerable<CreditCardDTO>> GetCreditCards(string userId)
    {
        var creditCards = await _creditCardRepository.GetCreditCards(userId);
        return _mapper.Map<IEnumerable<CreditCardDTO>>(creditCards);
    }

    public async Task<CreditCardDTO> GetById(int? id)
    {
        var creditCard = await _creditCardRepository.GetById(id);
        return _mapper.Map<CreditCardDTO>(creditCard);
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

    public async Task Remove(int? id)
    {
        var creditCard = _creditCardRepository.GetById(id).Result;
        await _creditCardRepository.Remove(creditCard);
    }
}
