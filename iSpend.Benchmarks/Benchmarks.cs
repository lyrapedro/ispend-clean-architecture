using AutoMapper;
using BenchmarkDotNet.Attributes;
using iSpend.Application.DTOs;
using iSpend.Domain.Entities;

namespace iSpend.Benchmarks;

public class Benchmark
{
    private CreditCard[] _creditCards;
    private IMapper _mapper;
    [Params(10, 100, 1000)]
    public int NumberOfElements { get; set; }

    [GlobalSetup]
    public void Init()
    {
        var config = new MapperConfiguration((cfg =>
            cfg.CreateMap<CreditCard, CreditCardDTO>().ReverseMap()));
        _mapper = config.CreateMapper();
        
        _creditCards = Enumerable.Range(1, NumberOfElements)
            .Select(n => new CreditCard(Guid.NewGuid().ToString(), $"Card" + n, n * 1000, 10, 1)).ToArray();
    }

    // [Benchmark]
    // public void With_Auto_Mapper_ForEach()
    // {
    //     IEnumerable<CreditCardDTO> creditCards = Array.Empty<CreditCardDTO>();
    //     foreach (var card in _creditCards)
    //     {
    //         creditCards.Append(_mapper.Map<CreditCardDTO>(card));
    //     }
    // }
    
    [Benchmark]
    public void With_Auto_Mapper_Select()
    {
        var creditCards = _creditCards.Select(c => _mapper.Map<CreditCardDTO>(c));
    }
    
    // [Benchmark]
    // public void With_Direct_Assigment_ForEach()
    // {
    //     IEnumerable<CreditCardDTO> creditCards = Array.Empty<CreditCardDTO>();
    //     foreach (var card in _creditCards)
    //     {
    //         creditCards.Append(new CreditCardDTO
    //         {
    //             UserId = card.UserId,
    //             Name = card.Name,
    //             Limit = card.Limit,
    //             ExpirationDay = card.ExpirationDay,
    //             ClosingDay = card.ClosingDay,
    //             ModifiedAt = card.ModifiedAt,
    //             RegisteredAt = card.RegisteredAt
    //         });
    //     }
    // }
    
    [Benchmark]
    public void With_Direct_Assigment_Select()
    {
        var creditCards = _creditCards.Select(c => new CreditCardDTO
        {
            UserId = c.UserId,
            Name = c.Name,
            Limit = c.Limit,
            ExpirationDay = c.ExpirationDay,
            ClosingDay = c.ClosingDay,
            ModifiedAt = c.ModifiedAt,
            RegisteredAt = c.RegisteredAt
        });
    }
    
    // [Benchmark]
    // public void With_Implicit_Operator_ForEach()
    // {
    //     IEnumerable<CreditCardDTO> creditCards = Array.Empty<CreditCardDTO>();
    //     foreach (var creditCard in _creditCards)
    //     {
    //         CreditCardDTO dto = creditCard;
    //         creditCards.Append(dto);
    //     }
    // }
    
    // [Benchmark]
    // public void With_Implicit_Operator_Select()
    // {
    //     IEnumerable<CreditCardDTO> creditCards = Array.Empty<CreditCardDTO>();
    //     creditCards = _creditCards.Select(x => (CreditCardDTO)x);
    // }
}