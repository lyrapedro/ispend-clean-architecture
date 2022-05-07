using iSpend.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace iSpend.WebUI.Controllers;

public class SubscriptionController : Controller
{
    private readonly ISubscriptionService _subscriptionService;
    public SubscriptionController(ISubscriptionService subscriptionService)
    {
        _subscriptionService = subscriptionService;
    }

    [HttpGet]
    public async Task<IActionResult> Index(int creditCardId)
    {
        var subscriptions = await _subscriptionService.GetSubscriptions(creditCardId);

        return View(subscriptions);
    }
}
