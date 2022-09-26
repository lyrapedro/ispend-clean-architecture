using iSpend.Application.DTOs;
using iSpend.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace iSpend.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CategoryController : ControllerBase
{
    private ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<ActionResult<IAsyncEnumerable<CategoryDTO>>> GetCategories()
    {
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var categories = await _categoryService.GetCategories(userId);

            return Ok(categories);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id:int}", Name = "GetCategory")]
    public async Task<ActionResult<CategoryDTO>> GetCategory(int id)
    {
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var category = await _categoryService.GetById(id);

            if (category is null)
                NotFound($"Not category with id {id}");

            if (category.UserId is not null && category.UserId != userId)
                return Unauthorized();

            return Ok(category);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("Find")]
    public async Task<ActionResult<IAsyncEnumerable<CategoryDTO>>> GetCategoriesByName([FromQuery] string name)
    {
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var categories = await _categoryService.GetByName(userId, name);

            if (categories is null)
                return NotFound($"Not to show with name {name}");

            return Ok(categories);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CategoryDTO category)
    {
        try
        {
            if (category is null)
                return BadRequest("Invalid request");

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            category.UserId = userId;

            await _categoryService.Add(category);

            return CreatedAtRoute("GetCategory", new { id = category.Id }, category);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Edit(int id, [FromBody] CategoryDTO category)
    {
        try
        {
            if (category is null)
                return BadRequest("Invalid request");

            if (category.Id != id)
                return BadRequest("Invalid request");

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId != category.UserId)
                return Unauthorized();

            await _categoryService.Update(category);

            return Ok($"\"{category.Name}\" successfully updated.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var category = await _categoryService.GetById(id);

            if (category == null)
                return NotFound($"Not exists");

            if (category.UserId == null || category.UserId != userId)
                return Unauthorized();

            var categoryName = category.Name;

            await _categoryService.Remove(category);

            return Ok($"\"{categoryName}\" successfully removed");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
