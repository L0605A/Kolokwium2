using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExampleTest2.Models;

[Table("Characters")]
public class Character
{
    [Key]
    public int Id { get; set; }
    [MaxLength(50)]
    public string FirstName { get; set; } = string.Empty;
    [MaxLength(120)]
    public string LastName { get; set; } = string.Empty;
    
    public int CurrentWeight { get; set; }
    
    public int MaxWeight { get; set; }
    
    public ICollection<Character_Title> Character_Titles { get; set; } = new HashSet<Character_Title>();
}