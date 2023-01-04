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
    public async Task<ActionResult<IAsyncEnumerable<SubscriptionDto>>> GetSubscriptions()
    {
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var subscriptions = await _subscriptionService.GetSubscriptions(userId);

            return Ok(subscriptions);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("CreditCard/{creditCardId:int}")]
    public async Task<ActionResult<IAsyncEnumerable<SubscriptionDto>>> GetSubscriptionsFromCreditCard(int creditCardId)
    {
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

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
    public async Task<ActionResult<SubscriptionDto>> GetSubscription(int id)
    {
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var subscription = await _subscriptionService.GetById(id);

            if (subscription is null)
                return NotFound($"Not subscription with id {id}");

            if (subscription.CreditCard.UserId != userId)
                return Unauthorized();

            return Ok(subscription);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("find")]
    public async Task<ActionResult<IAsyncEnumerable<SubscriptionDto>>> GetSubscriptionsByName([FromQuery] string name)
    {
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var subscriptions = await _subscriptionService.GetByName(userId, name);

            if (subscriptions is null)
                return NotFound($"Not to show with name {name}");

            return Ok(subscriptions);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] SubscriptionDto subscription)
    {
        try
        {
            if (subscription is null)
                return BadRequest("Invalid requst");

            await _subscriptionService.Add(subscription);

            return CreatedAtRoute("GetSubscription", new { id = subscription.Id }, subscription);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Edit(int id, [FromBody] SubscriptionDto subscription)
    {
        try
        {
            if (subscription is null)
                return BadRequest("Invalid request");

            if (subscription.Id != id)
                return BadRequest("Invalid request");

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var creditCard = _subscriptionService.GetById(id).Result.CreditCard;

            if (creditCard.UserId != userId)
                return Unauthorized();

            await _subscriptionService.Update(subscription);

            return Ok($"\"{subscription.Name}\" successfully updated.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var subscription = await _subscriptionService.GetById(id);

            if (subscription is null)
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
