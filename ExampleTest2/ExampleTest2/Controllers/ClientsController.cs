using System.Transactions;
using ExampleTest2.DTOs;
using ExampleTest2.Models;
using ExampleTest2.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExampleTest2.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CharactersController : ControllerBase
{
    private readonly IDbService _dbService;
    public CharactersController(IDbService dbService)
    {
        _dbService = dbService;
    }

    [HttpGet("characters/{characterId}")]
    public async Task<IActionResult> GetCharacter(int characterId)
    {
        
        if (!(await _dbService.DoesClientExist(characterId)))
        {
            return NotFound($"Character with given ID - {characterId} doesn't exist");
        }
        var chara = await _dbService.GetCharacter(characterId);
        
        return Ok(chara);
    }
    
    [HttpPost("characters/{characterId}/backpacks")]
    public async Task<IActionResult> AddNewItems(int characterId, List<int> itemIds)
    {
        foreach (var id in itemIds)
        {
            if (!(await _dbService.DoesItemExist(id)))
            {
                return NotFound($"Item with given ID - {id} doesn't exist");
            }
        }
        
        if (!(await _dbService.IsWeightValid(characterId, itemIds)))
        {
            return BadRequest("Items won't fit in the backpack");
        }

        var backpack = await _dbService.AddItems(characterId, itemIds);
        
        return Ok(backpack);
    }
    
    
    
}