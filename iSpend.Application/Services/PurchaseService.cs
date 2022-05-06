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

    public async Task<IEnumerable<PurchaseDTO>> GetPurchases(int creditCardId)
    {
        var purchases = await _purchaseRepository.GetPurchases(creditCardId);
        return _mapper.Map<IEnumerable<PurchaseDTO>>(purchases);
    }

    public async Task<PurchaseDTO> GetById(int? id)
    {
        var purchase = await _purchaseRepository.GetById(id);
        return _mapper.Map<PurchaseDTO>(purchase);
    }

    public async Task<PurchaseDTO> GetPurchaseCreditCard(int? id)
    {
        var purchase = await _purchaseRepository.GetPurchaseCreditCard(id);
        return _mapper.Map<PurchaseDTO>(purchase);
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

    public async Task Remove(int? id)
    {
        var purchase = _purchaseRepository.GetById(id).Result;
        await _purchaseRepository.Remove(purchase);
    }
}
