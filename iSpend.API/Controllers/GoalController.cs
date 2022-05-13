﻿using iSpend.Application.DTOs;
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
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        try
        {
            var goals = await _goalService.GetGoals(userId);
            return Ok(goals);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error on getting goals");
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<GoalDTO>> GetGoal(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        try
        {
            var goal = await _goalService.GetById(userId, id);

            if (goal == null)
                NotFound($"Not goal with id {id}");

            return Ok(goal);
        }
        catch
        {
            return BadRequest("Invalid request");
        }
    }

    [HttpGet("find")]
    public async Task<ActionResult<IAsyncEnumerable<GoalDTO>>> GetGoalsByName([FromQuery] string name)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        try
        {
            var goals = await _goalService.GetByName(userId, name);

            if (goals == null)
                return NotFound($"Not to show with name {name}");

            return Ok(goals);
        }
        catch
        {
            return BadRequest("Invalid request");
        }
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] GoalDTO goal)
    {
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await _goalService.Add(goal);

            return CreatedAtRoute(nameof(GetGoal), new { id = goal.Id }, goal);
        }
        catch
        {
            return BadRequest("Invalid request");
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Edit(int id, [FromBody] GoalDTO goal)
    {
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (goal.Id == id)
            {
                Guid validGuid;
                Guid.TryParse(userId, out validGuid);

                if (validGuid == goal.UserId)
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
            var goal = await _goalService.GetById(userId, id);

            if (goal == null)
                return NotFound($"Not exists");

            if (goal.UserId.ToString() == userId)
            {
                var goalName = goal.Name;

                await _goalService.Remove(userId, id);
                return Ok($"\"{goalName}\" successfully removed");
            }

            return Unauthorized("You do not have permissions to do that");

        }
        catch
        {
            return BadRequest("Invalid request");
        }
    }
}