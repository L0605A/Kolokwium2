using ExampleTest2.DTOs;
using ExampleTest2.Models;

namespace ExampleTest2.Services;

public interface IDbService
{
    Task<bool> DoesClientExist(int id);
    Task<bool> DoesItemExist(int id);
    Task<bool> IsWeightValid(int charId, List<int> items);
    
    Task<List<BackpackDTO>> AddItems(int charId, List<int> items);
    
    Task<CharacterDTO> GetCharacter(int charId);

}