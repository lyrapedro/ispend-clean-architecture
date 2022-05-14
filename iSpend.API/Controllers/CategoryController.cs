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
        var userId = User.FindFirstValue("UserId");

        try
        {
            var categories = await _categoryService.GetCategories(userId);
            return Ok(categories);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error on getting categories");
        }
    }

    [HttpGet("Purchases/{categoryId:int}")]
    public async Task<ActionResult<IAsyncEnumerable<CategoryDTO>>> GetPurchasesFromCategory(int categoryId)
    {
        var userId = User.FindFirstValue("UserId");

        try
        {
            var categories = await _categoryService.GetPurchasesFromCategory(userId, categoryId);
            return Ok(categories);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error on getting categories");
        }
    }

    [HttpGet("{id:int}", Name = "GetCategory")]
    public async Task<ActionResult<CategoryDTO>> GetCategory(int id)
    {
        var userId = User.FindFirstValue("UserId");

        try
        {
            var category = await _categoryService.GetById(userId, id);

            if (category == null)
                NotFound($"Not category with id {id}");

            return Ok(category);
        }
        catch
        {
            return BadRequest("Invalid request");
        }
    }

    [HttpGet("Find")]
    public async Task<ActionResult<IAsyncEnumerable<CategoryDTO>>> GetCategoriesByName([FromQuery] string name)
    {
        var userId = User.FindFirstValue("UserId");

        try
        {
            var categories = await _categoryService.GetByName(userId, name);

            if (categories == null)
                return NotFound($"Not to show with name {name}");

            return Ok(categories);
        }
        catch
        {
            return BadRequest("Invalid request");
        }
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CategoryDTO category)
    {
        try
        {
            var userId = User.FindFirstValue("UserId");

            if (userId == category.UserId)
                await _categoryService.Add(category);
            else
                return Unauthorized("You do not have permissions to do that.");

            return CreatedAtRoute("GetCategory", new { id = category.Id }, category);
        }
        catch
        {
            return BadRequest("Invalid request");
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Edit(int id, [FromBody] CategoryDTO category)
    {
        try
        {
            var userId = User.FindFirstValue("UserId");

            if (category.Id == id)
            {

                if (userId == category.UserId)
                {
                    await _categoryService.Update(category);
                    return Ok($"\"{category.Name}\" successfully updated.");
                }
                else
                {
                    return Unauthorized("You do not have permissions to do that");
                }
            }
            else
            {
                return BadRequest("Invalid request");
            }
        }
        catch
        {
            return BadRequest("Invalid request");
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var userId = User.FindFirstValue("UserId");
        try
        {
            var category = await _categoryService.GetById(userId, id);

            if (category == null)
                return NotFound($"Not exists");

            if (category.UserId == userId)
            {
                var categoryName = category.Name;

                await _categoryService.Remove(userId, id);
                return Ok($"\"{categoryName}\" successfully removed");
            }

            return Unauthorized("You do not have permissions to do that");
        }
        catch
        {
            return BadRequest("Invalid request");
        }
    }
}
