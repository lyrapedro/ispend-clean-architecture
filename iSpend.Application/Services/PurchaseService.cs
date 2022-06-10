using AutoMapper;
using iSpend.Application.DTOs;
using iSpend.Application.Interfaces;
using iSpend.Domain.Entities;
using iSpend.Domain.Interfaces;

namespace iSpend.Application.Services;

public class PurchaseService : IPurchaseService
{
    private IPurchaseRepository _purchaseRepository;
    private readonly IInstallmentRepository _installmentRepository;
    private readonly ICreditCardRepository _creditCardRepository;
    private readonly IMapper _mapper;

    public PurchaseService(IPurchaseRepository purchaseRepository, IInstallmentRepository installmentRepository, ICreditCardRepository creditCardRepository, IMapper mapper)
    {
        _purchaseRepository = purchaseRepository;
        _installmentRepository = installmentRepository;
        _creditCardRepository = creditCardRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PurchaseDTO>> GetPurchases(string userId)
    {
        var purchases = await _purchaseRepository.GetPurchases(userId);
        return _mapper.Map<IEnumerable<PurchaseDTO>>(purchases);
    }

    public async Task<IEnumerable<PurchaseDTO>> GetPurchasesFromCreditCard(int creditCardId)
    {
        var purchases = await _purchaseRepository.GetPurchasesFromCreditCard(creditCardId);
        return _mapper.Map<IEnumerable<PurchaseDTO>>(purchases);
    }

    public async Task<PurchaseDTO> GetById(int id)
    {
        var purchase = await _purchaseRepository.GetById(id);
        return _mapper.Map<PurchaseDTO>(purchase);
    }

    public async Task<IEnumerable<PurchaseDTO>> GetByName(string userId, string name)
    {
        IEnumerable<PurchaseDTO> purchases;

        var query = await _purchaseRepository.GetByName(userId, name);
        purchases = query.Select(c => _mapper.Map<PurchaseDTO>(c)).ToList();

        return purchases;
    }

    public async Task Add(PurchaseDTO purchaseDTO)
    {
        var purchase = _mapper.Map<Purchase>(purchaseDTO);
        var purchaseCreated = _purchaseRepository.Create(purchase).Result;
        var creditCard = await _creditCardRepository.GetById(purchaseCreated.CreditCardId);

        var numberOfInstallments = purchase.NumberOfInstallments;
        if (numberOfInstallments.HasValue)
        {
            var newInstallmentsList = GenerateListOfInstallmentsForPurchase(numberOfInstallments.Value, creditCard, purchaseCreated).Result;
            await _installmentRepository.CreateInstallments(newInstallmentsList);
        }
    }

    public async Task Update(PurchaseDTO purchaseDTO)
    {
        var purchase = _mapper.Map<Purchase>(purchaseDTO);
        await _purchaseRepository.Update(purchase);
    }

    public async Task Remove(PurchaseDTO purchaseDTO)
    {
        var purchase = _mapper.Map<Purchase>(purchaseDTO);
        await _purchaseRepository.Remove(purchase);
    }

#region Util
    public async Task<List<Installment>> GenerateListOfInstallmentsForPurchase(int numberOfInstallments, CreditCard creditCard, Purchase purchase)
    {
        var newInstallmentsList = new List<Installment>();
        var valueByInstallment = Decimal.Round(purchase.Price / numberOfInstallments, 2);
        int expirationDay;
        int expirationMonth;
        int expirationYear = purchase.PurchasedAt.Year;

        bool purchasedAfterInvoiceClosing = purchase.PurchasedAt.Day >= creditCard.ClosingDay ? true : false;

        if (purchasedAfterInvoiceClosing)
        {
            expirationDay = creditCard.ExpirationDay;
            expirationMonth = purchase.PurchasedAt.Month + 1;
            if (expirationMonth > 12)
            {
                expirationMonth = 1;
                expirationYear += 1;
            }
        }
        else
        {
            expirationDay = creditCard.ExpirationDay;
            expirationMonth = purchase.PurchasedAt.Month;
        }

        for (var i = 1; i <= purchase.NumberOfInstallments; i++)
        {
            var installmentExpiresDate = new DateTime(expirationYear, expirationMonth, expirationDay);
            newInstallmentsList.Add(new Installment(purchase.Id, i, valueByInstallment, false, installmentExpiresDate));
            expirationMonth += 1;
            if (expirationMonth > 12)
            {
                expirationMonth = 1;
                expirationYear += 1;
            }
        }

        return newInstallmentsList;
    }
}
#endregion