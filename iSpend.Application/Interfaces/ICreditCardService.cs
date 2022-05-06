﻿using iSpend.Application.DTOs;

namespace iSpend.Application.Interfaces;

public interface ICreditCardService
{
    Task<IEnumerable<CreditCardDTO>> GetCreditCards(string userId);
    Task<CreditCardDTO> GetById(int? id);
    Task Add(CreditCardDTO creditCardDTO);
    Task Update(CreditCardDTO creditCardDTO);
    Task Remove(int? id);
}