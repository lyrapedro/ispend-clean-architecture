using AutoMapper;
using iSpend.Application.DTOs;
using iSpend.Application.Interfaces;
using iSpend.Domain.Entities;
using iSpend.Domain.Interfaces;

namespace iSpend.Application.Services;

public class PurchaseService : IPurchaseService
{
    private IPurchaseRepository _purchaseRepository;
    private readonly IMapper _mapper;

    public PurchaseService(IPurchaseRepository purchaseRepository, IMapper mapper)
    {
        _purchaseRepository = purchaseRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PurchaseDTO>> GetPurchases(string userId)
    {
        var purchases = await _purchaseRepository.GetPurchases(userId);
        return _mapper.Map<IEnumerable<PurchaseDTO>>(purchases);
    }

    public async Task<IEnumerable<PurchaseDTO>> GetPurchasesFromCreditCard(string userId, int creditCardId)
    {
        var purchases = await _purchaseRepository.GetPurchasesFromCreditCard(userId, creditCardId);
        return _mapper.Map<IEnumerable<PurchaseDTO>>(purchases);
    }

    public async Task<PurchaseDTO> GetById(string userId, int? id)
    {
        var purchase = await _purchaseRepository.GetById(userId, id);
        return _mapper.Map<PurchaseDTO>(purchase);
    }

    public async Task<IEnumerable<PurchaseDTO>> GetByName(string userId, string name)
    {
        IEnumerable<PurchaseDTO> purchases;

        if (!string.IsNullOrEmpty(name))
        {
            var query = await _purchaseRepository.GetByName(userId, name);
            purchases = query.Select(c => _mapper.Map<PurchaseDTO>(c)).ToList();
        }
        else
        {
            purchases = await GetPurchases(userId);
        }

        return purchases;
    }

    public async Task<CreditCardDTO> GetPurchaseCreditCard(string userId, int? id)
    {
        var creditCard = await _purchaseRepository.GetPurchaseCreditCard(userId, id);
        return _mapper.Map<CreditCardDTO>(creditCard);
    }

    public async Task Add(PurchaseDTO purchaseDTO)
    {
        var purchase = _mapper.Map<Purchase>(purchaseDTO);
        await _purchaseRepository.Create(purchase);
    }

    public async Task Update(PurchaseDTO purchaseDTO)
    {
        var purchase = _mapper.Map<Purchase>(purchaseDTO);
        await _purchaseRepository.Update(purchase);
    }

    public async Task Remove(string userId, int? id)
    {
        var purchase = _purchaseRepository.GetById(userId, id).Result;
        await _purchaseRepository.Remove(purchase);
    }
}
