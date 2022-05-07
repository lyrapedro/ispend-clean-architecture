using iSpend.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace iSpend.WebUI.Controllers;

public class GoalController : Controller
{
    private readonly IGoalService _goalService;
    public GoalController(IGoalService goalService)
    {
        _goalService = goalService;
    }

    [HttpGet]
    public async Task<IActionResult> Index(HttpContext http)
    {
        //verificar depois se essa maneira de pegar o id do usuario logado esta correta
        var userId = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        var goals = await _goalService.GetGoals(userId);

        return View(goals);
    }
}
