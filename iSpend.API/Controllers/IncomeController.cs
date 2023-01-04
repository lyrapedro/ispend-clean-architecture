using iSpend.Application.DTOs;
using iSpend.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
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
    public async Task<ActionResult<IAsyncEnumerable<IncomeDto>>> GetIncomes()
    {
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var incomes = await _incomeService.GetIncomes(userId);

            return Ok(incomes);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id:int}", Name = "GetIncome")]
    public async Task<ActionResult<IncomeDto>> GetIncome(int id)
    {
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var income = await _incomeService.GetById(id);

            if (income is null)
                NotFound($"Not income with id {id}");

            if (income.UserId != userId)
                return Unauthorized();

            return Ok(income);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("Find")]
    public async Task<ActionResult<IAsyncEnumerable<IncomeDto>>> GetIncomesByName([FromQuery] string name)
    {
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var incomes = await _incomeService.GetByName(userId, name);

            if (incomes is null)
                return NotFound($"Not to show with name {name}");

            return Ok(incomes);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] IncomeDto income)
    {
        try
        {
            if (income is null)
                return BadRequest("Invalid Request");

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            income.UserId = userId;

            await _incomeService.Add(income);

            return CreatedAtRoute("GetIncome", new { id = income.Id }, income);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Edit(int id, [FromBody] IncomeDto income)
    {
        try
        {
            if (income is null)
                return BadRequest("Invalid request");

            if (income.Id != id)
                return BadRequest("Invalid request");

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId != income.UserId)
                return Unauthorized();

            await _incomeService.Update(income);

            return Ok($"\"{income.Name}\" successfully updated.");
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
            var income = await _incomeService.GetById(id);

            if (income is null)
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
