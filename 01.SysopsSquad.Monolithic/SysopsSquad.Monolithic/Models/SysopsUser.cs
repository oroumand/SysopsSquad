namespace SysopsSquad.Monolithic.Models;

public class SysopsUser
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string HashedPassword { get; set; }
    public string Role { get; set; } // "Expert", "Admin", "Manager"
    public string? Location { get; set; }
    public string? Skills { get; set; } // Comma-separated skills
    public bool IsAvailable { get; set; }
}
