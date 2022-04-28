﻿using iSpend.Domain.Entities;

namespace iSpend.Domain.Interfaces;

public interface IInstallmentRepository
{
    Task<IEnumerable<Installment>> GetInstallmentsAsync();
    Task<Installment> GetInstallmentByIdAsync(int? id);
    Task<Installment> CreateAsync(Installment installment);
    Task<Installment> UpdateAsync(Installment installment);
    Task<Installment> RemoveAsync(Installment installment);
}