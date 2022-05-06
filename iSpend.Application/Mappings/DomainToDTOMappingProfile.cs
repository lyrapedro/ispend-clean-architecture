using AutoMapper;
using iSpend.Application.DTOs;
using iSpend.Domain.Entities;

namespace iSpend.Application.Mappings;

public class DomainToDTOMappingProfile : Profile
{
    public DomainToDTOMappingProfile()
    {
        CreateMap<CreditCard, CreditCardDTO>().ReverseMap();
        CreateMap<Subscription, SubscriptionDTO>().ReverseMap();
        CreateMap<Expense, ExpenseDTO>().ReverseMap();
        CreateMap<Income, IncomeDTO>().ReverseMap();
        CreateMap<Goal, GoalDTO>().ReverseMap();
        CreateMap<Installment, InstallmentDTO>().ReverseMap();
        CreateMap<Purchase, PurchaseDTO>().ReverseMap();
    }
}
