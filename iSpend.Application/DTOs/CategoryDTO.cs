using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace iSpend.Application.DTOs;

public class CategoryDTO
{
    public int Id { get; private set; }
    public string? UserId { get; set; }

    [Required(ErrorMessage = "The name is required")]
    [MinLength(2)]
    [MaxLength(30)]
    public string Name { get; set; }
    public string Color { get; set; }

    public DateTime RegisteredAt { get; set; }
    public DateTime ModifiedAt { get; set; }
}
