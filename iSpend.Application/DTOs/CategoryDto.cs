using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using iSpend.Domain.Entities;

namespace iSpend.Application.DTOs;

public class CategoryDto
{
    public int Id { get; set; }
    public string? UserId { get; set; }

    [Required(ErrorMessage = "The name is required")]
    [MinLength(2)]
    [MaxLength(30)]
    public string Name { get; set; }
    public string Color { get; set; }

    public DateTime RegisteredAt { get; set; }
    public DateTime ModifiedAt { get; set; }

    public static explicit operator CategoryDto(Category category)
    {
        return new CategoryDto
        {
            Id = category.Id,
            UserId = category.UserId,
            Name = category.Name,
            Color = category.Color,
            RegisteredAt = category.RegisteredAt,
            ModifiedAt = category.ModifiedAt
        };
    }

    public static explicit operator Category(CategoryDto categoryDto)
    {
        return new Category(categoryDto.Name, categoryDto.Color, categoryDto.UserId);
    }
}
