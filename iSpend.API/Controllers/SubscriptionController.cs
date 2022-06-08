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
    private ICreditCardService _creditCardService;

    public SubscriptionController(ISubscriptionService subscriptionService, ICreditCardService creditCardService)
    {
        _subscriptionService = subscriptionService;
        _creditCardService = creditCardService;
    }

    [HttpGet]
    public async Task<ActionResult<IAsyncEnumerable<SubscriptionDTO>>> GetSubscriptions()
    {
        try
        {
            var userId = User.FindFirstValue("UserId");

            var subscriptions = await _subscriptionService.GetSubscriptions(userId);
            return Ok(subscriptions);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("CreditCard/{creditCardId:int}")]
    public async Task<ActionResult<IAsyncEnumerable<SubscriptionDTO>>> GetSubscriptionsFromCreditCard(int creditCardId)
    {
        var userId = User.FindFirstValue("UserId");

        try
        {
            var creditCard = await _creditCardService.GetById(creditCardId);
            if (creditCard.UserId != userId)
                return Unauthorized();

            var subscriptions = await _subscriptionService.GetSubscriptionsFromCreditCard(creditCardId);
            return Ok(subscriptions);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id:int}", Name = "GetSubscription")]
    public async Task<ActionResult<SubscriptionDTO>> GetSubscription(int id)
    {
        var userId = User.FindFirstValue("UserId");

        try
        {
            var subscription = await _subscriptionService.GetById(id);

            if (subscription.CreditCard.UserId != userId)
                return Unauthorized();

            if (subscription == null)
                NotFound($"Not subscription with id {id}");

            return Ok(subscription);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("find")]
    public async Task<ActionResult<IAsyncEnumerable<SubscriptionDTO>>> GetSubscriptionsByName([FromQuery] string name)
    {
        var userId = User.FindFirstValue("UserId");

        try
        {
            var subscriptions = await _subscriptionService.GetByName(userId, name);

            if (subscriptions == null)
                return NotFound($"Not to show with name {name}");

            return Ok(subscriptions);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] SubscriptionDTO subscription)
    {
        try
        {
            var userId = User.FindFirstValue("UserId");

            await _subscriptionService.Add(subscription);

            return CreatedAtRoute("GetSubscription", new { id = subscription.Id }, subscription);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Edit(int id, [FromBody] SubscriptionDTO subscription)
    {
        try
        {
            var userId = User.FindFirstValue("UserId");

            if (subscription.Id == id)
            {
                var creditCard = _subscriptionService.GetById(id).Result.CreditCard;

                if (creditCard.UserId != userId)
                    return Unauthorized();

                await _subscriptionService.Update(subscription);
                return Ok($"\"{subscription.Name}\" successfully updated.");
            }
            else
            {
                return BadRequest("Invalid request");
            }
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var userId = User.FindFirstValue("UserId");
        try
        {
            var subscription = await _subscriptionService.GetById(id);

            if (subscription == null)
                return NotFound($"Not exists");

            if (subscription.CreditCard.UserId != userId)
                return Unauthorized();

            var subscriptionName = subscription.Name;

            await _subscriptionService.Remove(subscription);
            return Ok($"\"{subscriptionName}\" successfully removed");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
