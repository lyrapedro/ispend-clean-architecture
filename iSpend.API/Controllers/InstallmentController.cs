using iSpend.Application.DTOs;
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
    public async Task<ActionResult<InstallmentDTO>> GetInstallment(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        try
        {
            var installment = await _installmentService.GetById(id);

            if (installment == null)
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
    public async Task<ActionResult<IAsyncEnumerable<InstallmentDTO>>> GetInstallments()
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
    public async Task<ActionResult<IAsyncEnumerable<InstallmentDTO>>> GetInstallmentsFromPurchase(int purchaseId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        try
        {
            var installments = await _installmentService.GetInstallmentsFromPurchase(userId, purchaseId);
            return Ok(installments);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
