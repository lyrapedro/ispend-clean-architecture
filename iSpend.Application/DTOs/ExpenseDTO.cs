using iSpend.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace iSpend.Application.DTOs;

public class ExpenseDTO
{
    public int Id { get; private set; }

    public string UserId { get; set; }

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

    [DataType(DataType.DateTime)]
    public DateTime ModifiedAt { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime RegisteredAt { get; set; }

    [JsonIgnore]
    public Category? Category { get; private set; }
}
