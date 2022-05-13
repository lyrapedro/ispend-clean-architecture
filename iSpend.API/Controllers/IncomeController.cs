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
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        try
        {
            var incomes = await _incomeService.GetIncomes(userId);
            return Ok(incomes);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error on getting incomes");
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<IncomeDTO>> GetIncome(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        try
        {
            var income = await _incomeService.GetById(userId, id);

            if (income == null)
                NotFound($"Not income with id {id}");

            return Ok(income);
        }
        catch
        {
            return BadRequest("Invalid request");
        }
    }

    [HttpGet("Find")]
    public async Task<ActionResult<IAsyncEnumerable<IncomeDTO>>> GetIncomesByName([FromQuery] string name)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        try
        {
            var incomes = await _incomeService.GetByName(userId, name);

            if (incomes == null)
                return NotFound($"Not to show with name {name}");

            return Ok(incomes);
        }
        catch
        {
            return BadRequest("Invalid request");
        }
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] IncomeDTO income)
    {
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await _incomeService.Add(income);

            return CreatedAtRoute(nameof(GetIncome), new { id = income.Id }, income);
        }
        catch
        {
            return BadRequest("Invalid request");
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Edit(int id, [FromBody] IncomeDTO income)
    {
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (income.Id == id)
            {
                Guid validGuid;
                Guid.TryParse(userId, out validGuid);

                if (validGuid == income.UserId)
                {
                    await _incomeService.Update(income);
                    return Ok($"\"{income.Name}\" successfully updated.");
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
            var income = await _incomeService.GetById(userId, id);

            if (income == null)
                return NotFound($"Not exists");

            if (income.UserId.ToString() == userId)
            {
                var incomeName = income.Name;

                await _incomeService.Remove(userId, id);
                return Ok($"\"{incomeName}\" successfully removed");
            }

            return Unauthorized("You do not have permissions to do that");
        }
        catch
        {
            return BadRequest("Invalid request");
        }
    }
}
