using ExampleTest2.Data;
using ExampleTest2.DTOs;
using ExampleTest2.Models;
using Microsoft.EntityFrameworkCore;

namespace ExampleTest2.Services;

public class DbService : IDbService
{
    private readonly DatabaseContext _context;
    public DbService(DatabaseContext context)
    {
        _context = context;
    }
    
    public async Task<bool> DoesItemExist(int id)
    {
        return await _context.Items.AnyAsync(e => e.Id == id);
    }

    public async Task<bool> IsWeightValid(int charId, List<int> items)
    {
        var maxWeight = await _context.Characters.Where(e => e.Id == charId).Select(e => e.MaxWeight).FirstOrDefaultAsync();
        
        var curWeight = await _context.Characters.Where(e => e.Id == charId).Select(e => e.CurrentWeight).FirstOrDefaultAsync();

        var validWeight = maxWeight - curWeight;
        
        
        int itemWeight = 0;
        
        foreach (var itemID in  items)
        {
            var temp = await _context.Items.Where(e => e.Id == itemID).Select(e => e.Weight).FirstOrDefaultAsync();

            itemWeight += temp;
        }

        return validWeight >= itemWeight;
    }

    public async Task<List<BackpackDTO>> AddItems(int charId, List<int> items)
    {
        List<BackpackDTO> returnee = new List<BackpackDTO>();

        int itemWeight = 0;
        
        foreach (var itemID in  items)
        {
            var temp = await _context.Items.Where(e => e.Id == itemID).Select(e => e.Weight).FirstOrDefaultAsync();

            itemWeight += temp;
        }


        var deliquent = await _context.Characters.Where(e => e.Id == charId).FirstOrDefaultAsync();

        deliquent.CurrentWeight += itemWeight;

        
        
        foreach (var itemID in  items)
        {
            var backpack = await _context.Backpacks.Where(e => e.CharacterId == charId && e.ItemId == itemID).FirstOrDefaultAsync();

            if (backpack == null)
            {
                backpack = new Backpack()
                {
                    CharacterId = charId,
                    ItemId = itemID,
                    Amount = 1
                };
                await _context.Backpacks.AddAsync(backpack);
            }
            else
            {
                backpack.Amount++;
            }
            
            returnee.Add(new BackpackDTO()
            {
                Amount = backpack.Amount,
                CharacterId = backpack.CharacterId,
                ItemId = backpack.ItemId
            });
            
        }
        
        await _context.SaveChangesAsync();

        return returnee;
    }

    public async Task<CharacterDTO> GetCharacter(int charId)
    {
        var character = await _context.Characters.Where(e => e.Id == charId).FirstOrDefaultAsync();
        
        var backpacks = _context.Backpacks.Where(e => e.CharacterId == charId);
        var titles = _context.CharacterTitles.Where(e => e.CharacterId == charId);

        List<BackpackResponseDTO> backpackDtos = new List<BackpackResponseDTO>();
        List<TitlesDTO> titlesDtos = new List<TitlesDTO>();

        foreach (var backpack in backpacks)
        {
            var name = await _context.Items.Where(e => e.Id == backpack.ItemId).Select(e => e.Name)
                .FirstOrDefaultAsync();
            var weight = await _context.Items.Where(e => e.Id == backpack.ItemId).Select(e => e.Weight)
                .FirstOrDefaultAsync();
            backpackDtos.Add(new BackpackResponseDTO()
            {
                itemName = name,
                itemWeight = weight,
                amount = backpack.Amount
            });
        }
        
        foreach (var title in titles)
        {
            var name = await _context.Titles.Where(e => e.Id == title.TitleId).Select(e => e.Name)
                .FirstOrDefaultAsync();
            titlesDtos.Add(new TitlesDTO()
            {
                title = name,
                AcquiredAt = title.AcquiredAt
            });
        }


        CharacterDTO chara = new CharacterDTO()
        {
            firstName = character.FirstName,
            lastName = character.LastName,
            currentWeight = character.CurrentWeight,
            maxWeight = character.MaxWeight,
            backpacks = backpackDtos,
            titles = titlesDtos
        };

        return chara;
    }
}