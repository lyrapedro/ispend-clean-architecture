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

    [HttpGet]
    public async Task<ActionResult<IAsyncEnumerable<InstallmentDTO>>> GetInstallments()
    {
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var installments = await _installmentService.GetInstallments(userId);
            return Ok(installments);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error on getting installments");
        }
    }

    [HttpGet("frompurchase/{purchaseId:int}")]
    public async Task<ActionResult<IAsyncEnumerable<InstallmentDTO>>> GetInstallmentsFromPurchase(int purchaseId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        try
        {
            var installments = await _installmentService.GetInstallmentsFromPurchase(userId, purchaseId);
            return Ok(installments);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error on getting installments");
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<InstallmentDTO>> GetInstallment(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        try
        {
            var installment = await _installmentService.GetById(userId, id);

            if (installment == null)
                NotFound($"Not installment with id {id}");

            return Ok(installment);
        }
        catch
        {
            return BadRequest("Invalid request");
        }
    }
}
