using iSpend.Application.DTOs;

namespace iSpend.Application.Interfaces;

public interface IInstallmentService
{
    Task<IEnumerable<InstallmentDTO>> GetInstallments(int purchaseId);
    Task<InstallmentDTO> GetById(int? id);
    Task<InstallmentDTO> GetInstallmentPurchase(int? id);
    Task Add(InstallmentDTO installmentDTO);
    Task Update(InstallmentDTO installmentDTO);
    Task Remove(int? id);
}
