﻿using iSpend.Application.DTOs;
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
        var userId = User.FindFirstValue("UserId");

        try
        {
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
        var userId = User.FindFirstValue("UserId");

        try
        {
            var creditCard = await _creditCardService.GetById(userId, id);

            if (creditCard == null)
                return NotFound($"Not credit card with id {id}");

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
        var userId = User.FindFirstValue("UserId");

        try
        {
            var creditCards = await _creditCardService.GetByName(userId, name);

            if (creditCards == null)
                return NotFound($"Not to show with name {name}");

            return Ok(creditCards);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreditCardDTO creditCardDto)
    {
        try
        {
            var userId = User.FindFirstValue("UserId");

            if (userId == creditCardDto.UserId)
                await _creditCardService.Add(creditCardDto);
            else
                return Unauthorized("You do not have permissions to do that.");

            return CreatedAtRoute("GetCard", new { id = creditCardDto.Id }, creditCardDto);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Edit(int id, [FromBody] CreditCardDTO creditCardDto)
    {
        try
        {
            var userId = User.FindFirstValue("UserId");
            if (creditCardDto.Id == id)
            {
                Guid validGuid;
                Guid.TryParse(userId, out validGuid);

                if (userId == creditCardDto.UserId.ToString())
                {
                    await _creditCardService.Update(creditCardDto);
                    return Ok($"\"{creditCardDto.Name}\" successfully updated.");
                }

                return Unauthorized("You do not have permissions to do that");
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
    public async Task<ActionResult<CreditCardDTO>> Delete(int id)
    {
        var userId = User.FindFirstValue("UserId");
        try
        {
            var creditCard = await _creditCardService.GetById(userId, id);

            if (creditCard == null)
                return NotFound($"Not exists");

            await _creditCardService.Remove(userId, id);
            return Ok($"\"{creditCard.Name}\" successfully removed");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
