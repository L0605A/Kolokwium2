using System.ComponentModel.DataAnnotations;

namespace ExampleTest2.DTOs;

public class CharacterDTO
{
    public string firstName { get; set; } = string.Empty;
    public string lastName { get; set; } = string.Empty;
    public int currentWeight { get; set; }
    public int maxWeight { get; set; }

    public List<BackpackResponseDTO> backpacks = new List<BackpackResponseDTO>();
    public List<TitlesDTO> titles = new List<TitlesDTO>();
}

public class BackpackResponseDTO
{
    public string itemName { get; set; } = string.Empty;
    public int itemWeight { get; set; }
    public int amount { get; set; }

}

public class TitlesDTO
{
    public string title { get; set; } = string.Empty;
    public DateTime AcquiredAt { get; set; }
}