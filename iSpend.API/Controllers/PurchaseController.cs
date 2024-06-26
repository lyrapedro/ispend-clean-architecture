﻿using iSpend.Application.DTOs;
using iSpend.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace iSpend.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class PurchaseController : ControllerBase
{
    private IPurchaseService _purchaseService;
    private readonly ICreditCardService _creditCardService;

    public PurchaseController(IPurchaseService purchaseService, ICreditCardService creditCardService)
    {
        _purchaseService = purchaseService;
        _creditCardService = creditCardService;
    }

    [HttpGet]
    public async Task<ActionResult<IAsyncEnumerable<PurchaseDto>>> GetPurchases()
    {
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var purchases = await _purchaseService.GetPurchases(userId);

            return Ok(purchases);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("CreditCard/{creditCardId:int}")]
    public async Task<ActionResult<IAsyncEnumerable<PurchaseDto>>> GetPurchasesFromCreditCard(int creditCardId)
    {
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var creditCard = await _creditCardService.GetById(creditCardId);

            if (creditCard.UserId != userId)
                return Unauthorized();

            var purchases = await _purchaseService.GetPurchasesFromCreditCard(creditCardId);

            return Ok(purchases);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id:int}", Name = "GetPurchase")]
    public async Task<ActionResult<PurchaseDto>> GetPurchase(int id)
    {
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var purchase = await _purchaseService.GetById(id);

            if (purchase is null)
                NotFound($"Not purchase with id {id}");

            if (purchase?.CreditCard?.UserId != userId)
                return Unauthorized();

            return Ok(purchase);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("Find")]
    public async Task<ActionResult<IAsyncEnumerable<PurchaseDto>>> GetPurchasesByName([FromQuery] string name)
    {
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var purchases = await _purchaseService.GetByName(userId, name);

            if (purchases is null)
                return NotFound($"Not to show with name {name}");

            return Ok(purchases);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] PurchaseDto purchase)
    {
        try
        {
            if (purchase is null)
                return BadRequest("Invalid request");

            await _purchaseService.Add(purchase);

            return CreatedAtRoute("GetPurchase", new { id = purchase.Id }, purchase);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Edit(int id, [FromBody] PurchaseDto purchase)
    {
        try
        {
            if (purchase is null)
                return BadRequest("Invalid request");

            if (purchase.Id != id)
                return BadRequest("Invalid request");

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var creditCard = _purchaseService.GetById(id).Result.CreditCard;

            if (creditCard?.UserId != userId)
                return Unauthorized();

            await _purchaseService.Update(purchase);

            return Ok($"\"{purchase.Name}\" successfully updated.");
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
            var purchase = await _purchaseService.GetById(id);

            if (purchase is null)
                return NotFound($"Not exists");

            if (purchase?.CreditCard?.UserId != userId)
                return Unauthorized();

            var purchaseName = purchase.Name;

            await _purchaseService.Remove(purchase);

            return Ok($"\"{purchaseName}\" successfully removed");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
