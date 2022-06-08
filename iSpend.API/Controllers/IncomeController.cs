using iSpend.Application.DTOs;
using iSpend.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace iSpend.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class IncomeController : ControllerBase
{
    private IIncomeService _incomeService;

    public IncomeController(IIncomeService incomeService)
    {
        _incomeService = incomeService;
    }

    [HttpGet]
    public async Task<ActionResult<IAsyncEnumerable<IncomeDTO>>> GetIncomes()
    {
        var userId = User.FindFirstValue("UserId");

        try
        {
            var incomes = await _incomeService.GetIncomes(userId);
            return Ok(incomes);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id:int}", Name = "GetIncome")]
    public async Task<ActionResult<IncomeDTO>> GetIncome(int id)
    {
        var userId = User.FindFirstValue("UserId");

        try
        {
            var income = await _incomeService.GetById(userId, id);

            if (income == null)
                NotFound($"Not income with id {id}");

            return Ok(income);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("Find")]
    public async Task<ActionResult<IAsyncEnumerable<IncomeDTO>>> GetIncomesByName([FromQuery] string name)
    {
        var userId = User.FindFirstValue("UserId");

        try
        {
            var incomes = await _incomeService.GetByName(userId, name);

            if (incomes == null)
                return NotFound($"Not to show with name {name}");

            return Ok(incomes);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] IncomeDTO income)
    {
        try
        {
            var userId = User.FindFirstValue("UserId");

            if (userId != income.UserId)
                return Unauthorized();

            await _incomeService.Add(income);

            return CreatedAtRoute("GetIncome", new { id = income.Id }, income);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Edit(int id, [FromBody] IncomeDTO income)
    {
        try
        {
            var userId = User.FindFirstValue("UserId");

            if (income.Id == id)
            {

                if (userId != income.UserId)
                    return Unauthorized();

                await _incomeService.Update(income);
                return Ok($"\"{income.Name}\" successfully updated.");
            }
            else
            {
                return BadRequest("Invalid request");
            }
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var userId = User.FindFirstValue("UserId");
        try
        {
            var income = await _incomeService.GetById(userId, id);

            if (income == null)
                return NotFound($"Not exists");

            if (income.UserId != userId)
                return Unauthorized();

            var incomeName = income.Name;

            await _incomeService.Remove(income);
            return Ok($"\"{incomeName}\" successfully removed");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
