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
    public async Task<ActionResult<IAsyncEnumerable<ExpenseDto>>> GetExpenses()
    {
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var expenses = await _expenseService.GetExpenses(userId);

            return Ok(expenses);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id:int}", Name = "GetExpense")]
    public async Task<ActionResult<ExpenseDto>> GetExpense(int id)
    {
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var expense = await _expenseService.GetById(id);

            if (expense == null)
                NotFound($"Not expense with id {id}");

            if (expense.UserId != userId)
                return Unauthorized();

            return Ok(expense);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("Find")]
    public async Task<ActionResult<IAsyncEnumerable<ExpenseDto>>> GetExpensesByName([FromQuery] string name)
    {
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var expenses = await _expenseService.GetByName(userId, name);

            if (expenses == null)
                return NotFound($"Not to show with name {name}");

            return Ok(expenses);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] ExpenseDto expense)
    {
        try
        {
            if (expense is null)
                return BadRequest("Invalid request");

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            expense.UserId = userId;

            await _expenseService.Add(expense);

            return CreatedAtRoute("GetExpense", new { id = expense.Id }, expense);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Edit(int id, [FromBody] ExpenseDto expense)
    {
        try
        {
            if (expense is null)
                return BadRequest("Invalid request");

            if (expense.Id != id)
                return BadRequest("Invalid request");

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId != expense.UserId)
                return Unauthorized();

            await _expenseService.Update(expense);

            return Ok($"\"{expense.Name}\" successfully updated.");
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
            var expense = await _expenseService.GetById(id);

            if (expense == null)
                return NotFound($"Not exists");

            if (expense.UserId != userId)
                return Unauthorized();

            var expenseName = expense.Name;

            await _expenseService.Remove(expense);

            return Ok($"\"{expenseName}\" successfully removed");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
