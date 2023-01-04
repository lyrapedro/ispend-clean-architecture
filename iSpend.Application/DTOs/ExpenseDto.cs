using System.Collections;
using iSpend.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace iSpend.Application.DTOs;

public class ExpenseDto
{
    public int Id { get; private set; }

    public string? UserId { get; set; }

    public int? CategoryId { get; set; }

    [Required(ErrorMessage = "The name is required")]
    [MinLength(2)]
    [MaxLength(30)]
    public string Name { get; set; }

    [Required(ErrorMessage = "The value is required")]
    [DataType(DataType.Currency)]
    public decimal Value { get; set; }

    [Required(ErrorMessage = "The recurrent field is required")]
    public bool Recurrent { get; set; }

    [Required(ErrorMessage = "The billing day is required")]
    [Range(1, 31)]
    public int BillingDay { get; set; }

    public bool Paid { get; set; }
    public Expense.ExpenseType? Type { get; set; }
    
    public IEnumerable<ExpenseNodeDto> Nodes { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime ModifiedAt { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime RegisteredAt { get; set; }

    [JsonIgnore]
    public bool Late { get; set; }

    public static explicit operator ExpenseDto(Expense expense)
    {
        return new ExpenseDto
        {
            Id = expense.Id,
            UserId = expense.UserId,
            CategoryId = expense.CategoryId,
            Name = expense.Name,
            Value = expense.Value,
            Recurrent = expense.Recurrent,
            BillingDay = expense.BillingDay,
            Paid = expense.Paid,
            Type = expense.Type,
            Nodes = expense.ExpenseNodes.Select(en => (ExpenseNodeDto)en)
        };
    }

    public static explicit operator Expense(ExpenseDto expenseDto)
    {
        return new Expense(expenseDto.UserId, expenseDto.CategoryId, expenseDto.Name, expenseDto.Value,
            expenseDto.Recurrent, expenseDto.BillingDay);
    }
}

public class ExpenseNodeDto
{
    public int Id { get; set; }
    public int ExpenseId { get; set; }
    public bool Paid { get; set; }
    public DateOnly ReferenceDate { get; set; }

    public static explicit operator ExpenseNodeDto(ExpenseNode expenseNode)
    {
        return new ExpenseNodeDto
        {
            Id = expenseNode.Id,
            ExpenseId = expenseNode.ExpenseId,
            Paid = expenseNode.Paid,
            ReferenceDate = expenseNode.ReferenceDate
        };
    }
}
