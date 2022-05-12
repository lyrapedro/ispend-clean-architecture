using iSpend.Application.DTOs;
using iSpend.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace iSpend.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class SubscriptionController : ControllerBase
{
    private ISubscriptionService _subscriptionService;

    public SubscriptionController(ISubscriptionService subscriptionService)
    {
        _subscriptionService = subscriptionService;
    }

    [HttpGet]
    public async Task<ActionResult<IAsyncEnumerable<SubscriptionDTO>>> GetSubscriptions()
    {
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var subscriptions = await _subscriptionService.GetSubscriptions(userId);
            return Ok(subscriptions);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error on getting subscriptions");
        }
    }

    [HttpGet("{creditCardId:int}")]
    public async Task<ActionResult<IAsyncEnumerable<SubscriptionDTO>>> GetSubscriptionsFromCreditCard(int creditCardId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        try
        {
            var subscriptions = await _subscriptionService.GetSubscriptionsFromCreditCard(userId, creditCardId);
            return Ok(subscriptions);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error on getting subscriptions");
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<SubscriptionDTO>> GetSubscription(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        try
        {
            var subscription = await _subscriptionService.GetById(userId, id);

            if (subscription == null)
                NotFound($"Not subscription with id {id}");

            return Ok(subscription);
        }
        catch
        {
            return BadRequest("Invalid request");
        }
    }

    [HttpGet]
    public async Task<ActionResult<IAsyncEnumerable<SubscriptionDTO>>> GetSubscriptionsByName([FromQuery] string name)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        try
        {
            var subscriptions = await _subscriptionService.GetByName(userId, name);

            if (subscriptions == null)
                return NotFound($"Not to show with name {name}");

            return Ok(subscriptions);
        }
        catch
        {
            return BadRequest("Invalid request");
        }
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] SubscriptionDTO subscription)
    {
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await _subscriptionService.Add(subscription);

            return CreatedAtRoute(nameof(GetSubscription), new { id = subscription.Id }, subscription);
        }
        catch
        {
            return BadRequest("Invalid request");
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Edit(int id, [FromBody] SubscriptionDTO subscription)
    {
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (subscription.Id == id)
            {
                var creditCard = await _subscriptionService.GetSubscriptionCreditCard(userId, id);
                if (creditCard.UserId.ToString() == userId)
                {
                    await _subscriptionService.Update(subscription);
                    return Ok($"\"{subscription.Name}\" successfully updated.");
                }
                else
                {
                    return Unauthorized("You do not have permissions to do that");
                }
            }
            else
            {
                return BadRequest("Invalid request");
            }
        }
        catch
        {
            return BadRequest("Invalid request");
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        try
        {
            var subscription = await _subscriptionService.GetById(userId, id);

            if (subscription == null)
                return NotFound($"Not exists");

            if (subscription.CreditCard.UserId.ToString() == userId)
            {
                var subscriptionName = subscription.Name;

                await _subscriptionService.Remove(userId, id);
                return Ok($"\"{subscriptionName}\" successfully removed");
            }

            return Unauthorized("You do not have permissions to do that");
        }
        catch
        {
            return BadRequest("Invalid request");
        }
    }
}
