using iSpend.Application.DTOs;
using iSpend.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace iSpend.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ExpenseController : ControllerBase
{
    private IExpenseService _expenseService;

    public ExpenseController(IExpenseService expenseService)
    {
        _expenseService = expenseService;
    }

    [HttpGet]
    public async Task<ActionResult<IAsyncEnumerable<ExpenseDTO>>> GetExpenses()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        try
        {
            var expenses = await _expenseService.GetExpenses(userId);
            return Ok(expenses);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error on getting expenses");
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ExpenseDTO>> GetExpense(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        try
        {
            var expense = await _expenseService.GetById(userId, id);

            if (expense == null)
                NotFound($"Not expense with id {id}");

            return Ok(expense);
        }
        catch
        {
            return BadRequest("Invalid request");
        }
    }

    [HttpGet("Find")]
    public async Task<ActionResult<IAsyncEnumerable<ExpenseDTO>>> GetExpensesByName([FromQuery] string name)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        try
        {
            var expenses = await _expenseService.GetByName(userId, name);

            if (expenses == null)
                return NotFound($"Not to show with name {name}");

            return Ok(expenses);
        }
        catch
        {
            return BadRequest("Invalid request");
        }
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] ExpenseDTO expense)
    {
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await _expenseService.Add(expense);

            return CreatedAtRoute(nameof(GetExpense), new { id = expense.Id }, expense);
        }
        catch
        {
            return BadRequest("Invalid request");
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Edit(int id, [FromBody] ExpenseDTO expense)
    {
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (expense.Id == id)
            {
                if (userId == expense.UserId)
                {
                    await _expenseService.Update(expense);
                    return Ok($"\"{expense.Name}\" successfully updated.");
                }

                return Unauthorized("You do not have permissions to do that");
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
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        try
        {
            var expense = await _expenseService.GetById(userId, id);

            if (expense == null)
                return NotFound($"Not exists");

            if (expense.UserId.ToString() == userId)
            {
                var expenseName = expense.Name;

                await _expenseService.Remove(userId, id);
                return Ok($"\"{expenseName}\" successfully removed");
            }

            return Unauthorized("You do not have permissions to do that");
        }
        catch
        {
            return BadRequest("Invalid request");
        }
    }
}
