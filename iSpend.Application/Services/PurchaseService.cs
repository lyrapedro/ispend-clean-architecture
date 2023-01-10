using iSpend.Application.DTOs;
using iSpend.Application.Interfaces;
using iSpend.Domain.Entities;
using iSpend.Domain.Interfaces;

namespace iSpend.Application.Services;

public class PurchaseService : IPurchaseService
{
    private readonly IPurchaseRepository _purchaseRepository;
    private readonly IInstallmentRepository _installmentRepository;
    private readonly ICreditCardRepository _creditCardRepository;

    public PurchaseService(IPurchaseRepository purchaseRepository, IInstallmentRepository installmentRepository,
        ICreditCardRepository creditCardRepository)
    {
        _purchaseRepository = purchaseRepository;
        _installmentRepository = installmentRepository;
        _creditCardRepository = creditCardRepository;
    }

    public async Task<IEnumerable<PurchaseDto>> GetPurchases(string userId)
    {
        var purchases = await _purchaseRepository.GetPurchases(userId);
        return purchases.Select(p => (PurchaseDto)p);
    }

    public async Task<IEnumerable<PurchaseDto>> GetPurchasesFromCreditCard(int creditCardId)
    {
        var purchases = await _purchaseRepository.GetPurchasesFromCreditCard(creditCardId);
        return purchases.Select(p => (PurchaseDto)p);
    }

    public async Task<PurchaseDto> GetById(int id)
    {
        var purchase = await _purchaseRepository.GetById(id);
        return (PurchaseDto)purchase;
    }

    public async Task<IEnumerable<PurchaseDto>> GetByName(string userId, string name)
    {
        var query = await _purchaseRepository.GetByName(userId, name);
        var purchases = query.Select(p => (PurchaseDto)p);

        return purchases;
    }

    public async Task Add(PurchaseDto purchaseDto)
    {
        var purchase = (Purchase)purchaseDto;
        var purchaseCreated = _purchaseRepository.Create(purchase).Result;
        var creditCard = await _creditCardRepository.GetById(purchaseCreated.CreditCardId);

        var purchaseInInstallments = purchase.NumberOfInstallments.HasValue;
        if (purchaseInInstallments)
        {
            var newInstallmentsList = purchaseCreated.GenerateListOfInstallments(creditCard);
            await _installmentRepository.CreateInstallments(newInstallmentsList);
        }
    }

    public async Task Update(PurchaseDto purchaseDto)
    {
        var purchase = (Purchase)purchaseDto;
        await _purchaseRepository.Update(purchase);
    }

    public async Task Remove(PurchaseDto purchaseDto)
    {
        var purchase = (Purchase)purchaseDto;
        await _purchaseRepository.Remove(purchase);
    }

    #region Util

    #endregion
}