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
        var userId = User.FindFirstValue("UserId");

        try
        {
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
        var userId = User.FindFirstValue("UserId");

        try
        {
            var goal = await _goalService.GetById(id);

            if (goal == null)
                NotFound($"Not goal with id {id}");

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
        var userId = User.FindFirstValue("UserId");

        try
        {
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
            var userId = User.FindFirstValue("UserId");

            if (userId == goal.UserId)
                await _goalService.Add(goal);
            else
                return Unauthorized("You do not have permissions to do that.");

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
            var userId = User.FindFirstValue("UserId");

            if (goal.Id == id)
            {

                if (userId == goal.UserId)
                {
                    await _goalService.Update(goal);
                    return Ok($"\"{goal.Name}\" successfully updated.");
                }

                return Unauthorized("You do not have permissions to do that");
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
            var goal = await _goalService.GetById(id);

            if (goal == null)
                return NotFound($"Not exists");

            if (goal.UserId == userId)
            {
                var goalName = goal.Name;

                await _goalService.Remove(goal);
                return Ok($"\"{goalName}\" successfully removed");
            }

            return Unauthorized("You do not have permissions to do that");

        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
