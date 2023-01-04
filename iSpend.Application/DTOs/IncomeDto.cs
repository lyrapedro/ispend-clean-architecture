using iSpend.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace iSpend.Application.DTOs;

public class IncomeDto
{
    public int Id { get; private set; }

    public string? UserId { get; set; }

    public int? CategoryId { get; set; }

    [Required(ErrorMessage = "The name is required")]
    [MinLength(2)]
    [MaxLength(30)]
    public string Name { get; set; }

    [Required(ErrorMessage = "The recurrent field is required")]
    public bool Recurrent { get; set; }

    [Required(ErrorMessage = "The value is required")]
    [DataType(DataType.Currency)]
    public decimal Value { get; set; }

    [Required(ErrorMessage = "The name is required")]
    [Range(1, 31)]
    public int Payday { get; set; }

    public DateTime ModifiedAt { get; set; }

    public DateTime RegisteredAt { get; set; }

    [JsonIgnore] public Category? Category { get; set; }

    public static explicit operator IncomeDto(Income income)
    {
        return new IncomeDto
        {
            Id = income.Id,
            UserId = income.UserId,
            CategoryId = income.CategoryId,
            Name = income.Name,
            Recurrent = income.Recurrent,
            Value = income.Value,
            Payday = income.Payday,
            ModifiedAt = income.ModifiedAt,
            RegisteredAt = income.RegisteredAt
        };
    }

    public static explicit operator Income(IncomeDto income)
    {
        return new Income(income.UserId, income.CategoryId, income.Name, income.Recurrent, income.Value, income.Payday);
    }
}