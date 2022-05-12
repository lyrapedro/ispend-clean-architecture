using AutoMapper;
using iSpend.Application.DTOs;
using iSpend.Application.Interfaces;
using iSpend.Domain.Entities;
using iSpend.Domain.Interfaces;

namespace iSpend.Application.Services;

public class InstallmentService : IInstallmentService
{
    private IInstallmentRepository _installmentRepository;
    private readonly IMapper _mapper;

    public InstallmentService(IInstallmentRepository installmentRepository, IMapper mapper)
    {
        _installmentRepository = installmentRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<InstallmentDTO>> GetInstallments(string userId)
    {
        var installments = await _installmentRepository.GetInstallments(userId);
        return _mapper.Map<IEnumerable<InstallmentDTO>>(installments);
    }

    public async Task<InstallmentDTO> GetById(string userId, int? id)
    {
        var installment = await _installmentRepository.GetById(userId, id);
        return _mapper.Map<InstallmentDTO>(installment);
    }

    public async Task<IEnumerable<InstallmentDTO>> GetInstallmentsFromPurchase(string userId, int? purchaseId)
    {
        var installment = await _installmentRepository.GetInstallmentsFromPurchase(userId, purchaseId);
        return _mapper.Map<IEnumerable<InstallmentDTO>>(installment);
    }

    public async Task<InstallmentDTO> GetInstallmentPurchase(string userId, int? id)
    {
        var installment = await _installmentRepository.GetInstallmentPurchase(userId, id);
        return _mapper.Map<InstallmentDTO>(installment);
    }

    public async Task Add(InstallmentDTO installmentDTO)
    {
        var installment = _mapper.Map<Installment>(installmentDTO);
        await _installmentRepository.Create(installment);
    }

    public async Task Update(InstallmentDTO installmentDTO)
    {
        var installment = _mapper.Map<Installment>(installmentDTO);
        await _installmentRepository.Update(installment);
    }

    public async Task Remove(string userId, int? id)
    {
        var installment = _installmentRepository.GetById(userId, id).Result;
        await _installmentRepository.Remove(installment);
    }
}
