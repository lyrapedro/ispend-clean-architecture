﻿using iSpend.Application.DTOs;
using iSpend.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace iSpend.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class InstallmentController : ControllerBase
{
    private IInstallmentService _installmentService;

    public InstallmentController(IInstallmentService installmentService)
    {
        _installmentService = installmentService;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<InstallmentDto>> GetInstallment(int id)
    {
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var installment = await _installmentService.GetById(id);

            if (installment is null)
                NotFound($"Not installment with id {id}");

            if (installment?.Purchase.CreditCard.UserId != userId)
                return Unauthorized();

            return Ok(installment);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<IAsyncEnumerable<InstallmentDto>>> GetInstallments()
    {
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var installments = await _installmentService.GetInstallments(userId);

            return Ok(installments);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("Purchase/{purchaseId:int}")]
    public async Task<ActionResult<IAsyncEnumerable<InstallmentDto>>> GetInstallmentsFromPurchase(int purchaseId)
    {
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var installments = await _installmentService.GetInstallmentsFromPurchase(userId, purchaseId);

            return Ok(installments);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
