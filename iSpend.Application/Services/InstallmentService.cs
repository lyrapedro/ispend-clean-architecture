using iSpend.Application.DTOs;
using iSpend.Application.Interfaces;
using iSpend.Domain.Entities;
using iSpend.Domain.Interfaces;

namespace iSpend.Application.Services;

public class InstallmentService : IInstallmentService
{
    private readonly IInstallmentRepository _installmentRepository;

    public InstallmentService(IInstallmentRepository installmentRepository)
    {
        _installmentRepository = installmentRepository;
    }

    public async Task<IEnumerable<InstallmentDto>> GetInstallments(string userId)
    {
        var installments = await _installmentRepository.GetInstallments(userId);
        return installments.Select(i => (InstallmentDto)i);
    }

    public async Task<InstallmentDto> GetById(int id)
    {
        var installment = await _installmentRepository.GetById(id);
        return (InstallmentDto)installment;
    }

    public async Task<IEnumerable<InstallmentDto>> GetInstallmentsFromPurchase(string userId, int purchaseId)
    {
        var installments = await _installmentRepository.GetInstallmentsFromPurchase(userId, purchaseId);
        return installments.Select(i => (InstallmentDto)i);
    }

    public async Task Add(InstallmentDto installmentDto)
    {
        var installment = (Installment)installmentDto;
        await _installmentRepository.Create(installment);
    }

    public async Task Update(InstallmentDto installmentDto)
    {
        var installment = (Installment)installmentDto;
        await _installmentRepository.Update(installment);
    }

    public async Task Remove(InstallmentDto installmentDto)
    {
        var installment = (Installment)installmentDto;
        await _installmentRepository.Remove(installment);
    }
}