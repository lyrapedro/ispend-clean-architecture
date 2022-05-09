using iSpend.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace iSpend.WebUI.Controllers;

public class CreditCardController : Controller
{
    private readonly ICreditCardService _creditCardService;
    public CreditCardController(ICreditCardService creditCardService)
    {
        _creditCardService = creditCardService;
    }

    [HttpGet]
    [Authorize(Policy = "User")]
    public async Task<IActionResult> Index(HttpContext http)
    {
        //verificar depois se essa maneira de pegar o id do usuario logado esta correta
        var userId = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        var creditCards = await _creditCardService.GetCreditCards(userId);

        return View(creditCards);
    }
}
