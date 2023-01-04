using System.ComponentModel.DataAnnotations;
using iSpend.Domain.Entities;

namespace iSpend.Application.DTOs;

public class GoalDto
{
    public int Id { get; private set; }

    public string? UserId { get; set; }

    [Required(ErrorMessage = "The name is required")]
    [MinLength(2)]
    [MaxLength(30)]
    public string Name { get; set; }

    [MinLength(2)]
    [MaxLength(200)]
    public string Description { get; set; }

    [Required(ErrorMessage = "The value is required")]
    [DataType(DataType.Currency)]
    public decimal GoalValue { get; set; }

    [DataType(DataType.Currency)]
    public decimal? ValueSaved { get; set; }

    [Required(ErrorMessage = "The start date is required")]
    [DataType(DataType.DateTime)]
    public DateTime StartDate { get; set; }

    [Required(ErrorMessage = "The end date is required")]
    [DataType(DataType.DateTime)]
    public DateTime EndDate { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime ModifiedAt { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime RegisteredAt { get; set; }

    public static explicit operator GoalDto(Goal goal)
    {
        return new GoalDto
        {
            Id = goal.Id,
            UserId = goal.UserId,
            Name = goal.Name,
            Description = goal.Description,
            GoalValue = goal.GoalValue,
            ValueSaved = goal.ValueSaved,
            StartDate = goal.StartDate,
            EndDate = goal.EndDate,
            ModifiedAt = goal.ModifiedAt,
            RegisteredAt = goal.RegisteredAt
        };
    }

    public static explicit operator Goal(GoalDto goal)
    {
        return new Goal(goal.UserId, goal.Name, goal.Description, goal.GoalValue, goal.ValueSaved, goal.StartDate, goal.EndDate);
    }
}
