using iSpend.Application.DTOs;

namespace iSpend.Application.Interfaces;

public interface ICreditCardService
{
    Task<IEnumerable<CreditCardDTO>> GetCreditCards(string userId);
    Task<CreditCardDTO> GetById(string userId, int? id);
    Task<IEnumerable<CreditCardDTO>> GetByName(string userId, string name);
    Task Add(CreditCardDTO creditCardDTO);
    Task Update(CreditCardDTO creditCardDTO);
    Task Remove(string userId, int? id);
}
