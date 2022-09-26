using iSpend.Application.DTOs;
using iSpend.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace iSpend.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class GoalController : ControllerBase
{
    private IGoalService _goalService;

    public GoalController(IGoalService goalService)
    {
        _goalService = goalService;
    }

    [HttpGet]
    public async Task<ActionResult<IAsyncEnumerable<GoalDTO>>> GetGoals()
    {
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var goals = await _goalService.GetGoals(userId);

            return Ok(goals);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id:int}", Name = "GetGoal")]
    public async Task<ActionResult<GoalDTO>> GetGoal(int id)
    {
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var goal = await _goalService.GetById(id);

            if (goal is null)
                return NotFound($"Not goal with id {id}");

            if (goal.UserId != userId)
                return Unauthorized();

            return Ok(goal);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("Find")]
    public async Task<ActionResult<IAsyncEnumerable<GoalDTO>>> GetGoalsByName([FromQuery] string name)
    {
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var goals = await _goalService.GetByName(userId, name);

            if (goals == null)
                return NotFound($"Not to show with name {name}");

            return Ok(goals);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] GoalDTO goal)
    {
        try
        {
            if (goal is null)
                return BadRequest("Invalid request");

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            goal.UserId = userId;

            await _goalService.Add(goal);

            return CreatedAtRoute("GetGoal", new { id = goal.Id }, goal);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Edit(int id, [FromBody] GoalDTO goal)
    {
        try
        {
            if (goal is null)
                return BadRequest("Invalid request");

            if (goal.Id != id)
                return BadRequest("Invalid request");

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId != goal.UserId)
                return Unauthorized();

            await _goalService.Update(goal);

            return Ok($"\"{goal.Name}\" successfully updated.");
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
            var goal = await _goalService.GetById(id);

            if (goal == null)
                return NotFound($"Not exists");

            if (goal.UserId != userId)
                return Unauthorized();

            var goalName = goal.Name;

            await _goalService.Remove(goal);

            return Ok($"\"{goalName}\" successfully removed");

        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
