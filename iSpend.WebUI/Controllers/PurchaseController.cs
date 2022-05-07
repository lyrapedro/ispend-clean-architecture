
using iSpend.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace iSpend.WebUI.Controllers;

public class PurchaseController : Controller
{
    private readonly IPurchaseService _purchaseService;
    public PurchaseController(IPurchaseService purchaseService)
    {
        _purchaseService = purchaseService;
    }

    [HttpGet]
    public async Task<IActionResult> Index(int creditCardId)
    {
        var purchases = await _purchaseService.GetPurchases(creditCardId);

        return View(purchases);
    }
}
