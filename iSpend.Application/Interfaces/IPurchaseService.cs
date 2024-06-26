﻿using iSpend.Application.DTOs;

namespace iSpend.Application.Interfaces;

public interface IPurchaseService
{
    Task<IEnumerable<PurchaseDto>> GetPurchases(string userId);
    Task<IEnumerable<PurchaseDto>> GetPurchasesFromCreditCard(int creditCardId);
    Task<PurchaseDto> GetById(int id);
    Task<IEnumerable<PurchaseDto>> GetByName(string userId, string name);
    Task Add(PurchaseDto purchaseDto);
    Task Update(PurchaseDto purchaseDto);
    Task Remove(PurchaseDto purchaseDto);
}
