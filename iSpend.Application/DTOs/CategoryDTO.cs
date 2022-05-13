using System.ComponentModel.DataAnnotations;

namespace iSpend.Application.DTOs;

public class CategoryDTO
{
    public int Id { get; set; }
    public Guid UserId { get; set; }

    [Required(ErrorMessage = "The name is required")]
    [MinLength(2)]
    [MaxLength(50)]
    public string Name { get; private set; }
    public string Color { get; set; }
}
