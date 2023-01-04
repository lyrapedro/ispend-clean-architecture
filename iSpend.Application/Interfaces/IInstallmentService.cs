using iSpend.Application.DTOs;

namespace iSpend.Application.Interfaces;

public interface IInstallmentService
{
    Task<IEnumerable<InstallmentDto>> GetInstallments(string userId);
    Task<InstallmentDto> GetById(int id);
    Task<IEnumerable<InstallmentDto>> GetInstallmentsFromPurchase(string userId, int purchaseId);
    Task Add(InstallmentDto installmentDTO);
    Task Update(InstallmentDto installmentDTO);
    Task Remove(InstallmentDto installmentDTO);
}
