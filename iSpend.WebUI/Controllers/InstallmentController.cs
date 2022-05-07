using iSpend.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace iSpend.WebUI.Controllers;

public class InstallmentController : Controller
{
    private readonly IInstallmentService _installmentService;
    public InstallmentController(IInstallmentService installmentService)
    {
        _installmentService = installmentService;
    }

    [HttpGet]
    public async Task<IActionResult> Index(int purchaseId)
    {
        var installments = await _installmentService.GetInstallments(purchaseId);

        return View(installments);
    }
}
