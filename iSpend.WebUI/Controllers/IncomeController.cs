using iSpend.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace iSpend.WebUI.Controllers;

public class IncomeController : Controller
{
    private readonly IIncomeService _incomeService;
    public IncomeController(IIncomeService incomeService)
    {
        _incomeService = incomeService;
    }

    [HttpGet]
    public async Task<IActionResult> Index(HttpContext http)
    {
        //verificar depois se essa maneira de pegar o id do usuario logado esta correta
        var userId = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        var incomes = await _incomeService.GetIncomes(userId);

        return View(incomes);
    }
}
