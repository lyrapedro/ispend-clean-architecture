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

    public PurchaseController(IPurchaseService purchaseService)
    {
        _purchaseService = purchaseService;
    }

    [HttpGet]
    public async Task<ActionResult<IAsyncEnumerable<PurchaseDTO>>> GetPurchases()
    {
        var userId = User.FindFirstValue("UserId");

        try
        {
            var purchases = await _purchaseService.GetPurchases(userId);
            return Ok(purchases);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("CreditCard/{creditCardId:int}")]
    public async Task<ActionResult<IAsyncEnumerable<PurchaseDTO>>> GetPurchasesFromCreditCard(int creditCardId)
    {
        var userId = User.FindFirstValue("UserId");

        try
        {
            var purchases = await _purchaseService.GetPurchasesFromCreditCard(userId, creditCardId);
            return Ok(purchases);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id:int}", Name = "GetPurchase")]
    public async Task<ActionResult<PurchaseDTO>> GetPurchase(int id)
    {
        var userId = User.FindFirstValue("UserId");

        try
        {
            var purchase = await _purchaseService.GetById(userId, id);

            if (purchase == null)
                NotFound($"Not purchase with id {id}");

            return Ok(purchase);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("Find")]
    public async Task<ActionResult<IAsyncEnumerable<PurchaseDTO>>> GetPurchasesByName([FromQuery] string name)
    {
        var userId = User.FindFirstValue("UserId");

        try
        {
            var purchases = await _purchaseService.GetByName(userId, name);

            if (purchases == null)
                return NotFound($"Not to show with name {name}");

            return Ok(purchases);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] PurchaseDTO purchase)
    {
        try
        {
            var userId = User.FindFirstValue("UserId");

            await _purchaseService.Add(purchase);

            return CreatedAtRoute("GetPurchase", new { id = purchase.Id }, purchase);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Edit(int id, [FromBody] PurchaseDTO purchase)
    {
        try
        {
            var userId = User.FindFirstValue("UserId");

            if (purchase.Id == id)
            {
                var creditCard = await _purchaseService.GetPurchaseCreditCard(userId, id);
                if (creditCard.UserId.ToString() == userId)
                {
                    await _purchaseService.Update(purchase);
                    return Ok($"\"{purchase.Name}\" successfully updated.");
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
            var purchase = await _purchaseService.GetById(userId, id);

            if (purchase == null)
                return NotFound($"Not exists");

            if (purchase.CreditCard.UserId.ToString() == userId)
            {
                var purchaseName = purchase.Name;

                await _purchaseService.Remove(userId, id);
                return Ok($"\"{purchaseName}\" successfully removed");
            }

            return Unauthorized("You do not have permissions to do that");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
