using AutoMapper;
using BenchmarkDotNet.Attributes;
using iSpend.Application.DTOs;
using iSpend.Domain.Entities;

namespace iSpend.Benchmarks;

public class Benchmark
{
    private Category[] _categories;
    private IMapper _mapper;
    [Params(10, 100, 1000)]
    public int NumberOfElements { get; set; }

    [GlobalSetup]
    public void Init()
    {
        var config = new MapperConfiguration((cfg =>
            cfg.CreateMap<Category, CategoryDto>().ReverseMap()));
        _mapper = config.CreateMapper();
        
        _categories = Enumerable.Range(1, NumberOfElements)
            .Select(n => new Category($"Category" + n, "#FFF", null)).ToArray();
    }

    // [Benchmark]
    // public void With_Auto_Mapper_ForEach()
    // {
    //     IEnumerable<CreditCardDto> creditCards = Array.Empty<CreditCardDto>();
    //     foreach (var card in _creditCards)
    //     {
    //         creditCards.Append(_mapper.Map<CreditCardDto>(card));
    //     }
    // }
    
    // [Benchmark]
    // public void With_Auto_Mapper_Select()
    // {
    //     var creditCards = _categories.Select(c => _mapper.Map<CreditCardDto>(c));
    // }
    
    // [Benchmark]
    // public void With_Direct_Assigment_ForEach()
    // {
    //     IEnumerable<CreditCardDto> creditCards = Array.Empty<CreditCardDto>();
    //     foreach (var card in _creditCards)
    //     {
    //         creditCards.Append(new CreditCardDto
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
        var categories = _categories.Select(c => new CategoryDto
        {
            Id = c.Id,
            UserId = c.UserId,
            Name = c.Name,
            Color = c.Color,
            ModifiedAt = c.ModifiedAt,
            RegisteredAt = c.RegisteredAt
        });
    }
    
    // [Benchmark]
    // public void With_Implicit_Operator_ForEach()
    // {
    //     IEnumerable<CreditCardDto> creditCards = Array.Empty<CreditCardDto>();
    //     foreach (var creditCard in _creditCards)
    //     {
    //         CreditCardDto dto = creditCard;
    //         creditCards.Append(dto);
    //     }
    // }
    
    [Benchmark]
    public void With_Explicit_Operator_Select()
    {
        IEnumerable<CategoryDto> categories = Array.Empty<CategoryDto>();
        categories = _categories.Select(x => (CategoryDto)x);
    }
}