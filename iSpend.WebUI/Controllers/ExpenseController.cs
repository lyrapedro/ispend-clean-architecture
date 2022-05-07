
using iSpend.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace iSpend.WebUI.Controllers;

public class ExpenseController : Controller
{
    private readonly IExpenseService _expenseService;
    public ExpenseController(IExpenseService expenseService)
    {
        _expenseService = expenseService;
    }

    [HttpGet]
    public async Task<IActionResult> Index(HttpContext http)
    {
        //verificar depois se essa maneira de pegar o id do usuario logado esta correta
        var userId = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        var expenses = await _expenseService.GetExpenses(userId);

        return View(expenses);
    }
}
