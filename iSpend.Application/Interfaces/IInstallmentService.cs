using iSpend.Application.DTOs;

namespace iSpend.Application.Interfaces;

public interface IInstallmentService
{
    Task<IEnumerable<InstallmentDTO>> GetInstallments(string userId);
    Task<InstallmentDTO> GetById(string userId, int? id);
    Task<IEnumerable<InstallmentDTO>> GetInstallmentsFromPurchase(string userId, int? purchaseId);
    Task<InstallmentDTO> GetInstallmentPurchase(string userId, int? id);
    Task Add(InstallmentDTO installmentDTO);
    Task Update(InstallmentDTO installmentDTO);
    Task Remove(InstallmentDTO installmentDTO);
}
