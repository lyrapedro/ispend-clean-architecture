using iSpend.Application.DTOs;
using iSpend.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace iSpend.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CreditCardController : ControllerBase
{
    private ICreditCardService _creditCardService;

    public CreditCardController(ICreditCardService creditCardService)
    {
        _creditCardService = creditCardService;
    }

    [HttpGet]
    public async Task<ActionResult<IAsyncEnumerable<CreditCardDTO>>> GetCreditCards()
    {
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var creditCards = await _creditCardService.GetCreditCards(userId);

            return Ok(creditCards);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id:int}", Name = "GetCard")]
    public async Task<ActionResult<CreditCardDTO>> GetCreditCard(int id)
    {
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var creditCard = await _creditCardService.GetById(id);

            if (creditCard is null)
                return NotFound($"Not credit card with id {id}");

            if (creditCard.UserId != userId)
                return Unauthorized();

            return Ok(creditCard);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("Find")]
    public async Task<ActionResult<IAsyncEnumerable<CreditCardDTO>>> GetCreditCardsByName([FromQuery] string name)
    {
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var creditCards = await _creditCardService.GetByName(userId, name);

            if (creditCards is null)
                return NotFound($"Not to show with name {name}");

            return Ok(creditCards);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreditCardDTO creditCard)
    {
        try
        {
            if (creditCard is null)
                return BadRequest("Invalid request");

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            creditCard.UserId = userId;

            await _creditCardService.Add(creditCard);

            return CreatedAtRoute("GetCreditCard", new { id = creditCard.Id }, creditCard);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Edit(int id, [FromBody] CreditCardDTO creditCard)
    {
        try
        {
            if (creditCard is null)
                return BadRequest("Invalid request");

            if (creditCard.Id != id)
                return BadRequest("Invalid request");

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId != creditCard.UserId)
                return Unauthorized();

            await _creditCardService.Update(creditCard);

            return Ok($"\"{creditCard.Name}\" successfully updated.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<CreditCardDTO>> Delete(int id)
    {
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var creditCard = await _creditCardService.GetById(id);

            if (creditCard is null)
                return NotFound($"Not exists");

            if (creditCard.UserId != userId)
                return Unauthorized();

            await _creditCardService.Remove(creditCard);

            return Ok($"\"{creditCard.Name}\" successfully removed");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
