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

    public async Task<IEnumerable<InstallmentDTO>> GetInstallments(int creditCardId)
    {
        var installments = await _installmentRepository.GetInstallments(creditCardId);
        return _mapper.Map<IEnumerable<InstallmentDTO>>(installments);
    }

    public async Task<InstallmentDTO> GetById(int? id)
    {
        var installment = await _installmentRepository.GetById(id);
        return _mapper.Map<InstallmentDTO>(installment);
    }

    public async Task<InstallmentDTO> GetInstallmentPurchase(int? id)
    {
        var installment = await _installmentRepository.GetInstallmentPurchase(id);
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

    public async Task Remove(int? id)
    {
        var installment = _installmentRepository.GetById(id).Result;
        await _installmentRepository.Remove(installment);
    }
}
