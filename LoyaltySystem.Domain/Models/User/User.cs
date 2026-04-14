namespace LoyaltySystem.Domain.Models.User;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public int Coins { get; set; }
    public string Phone { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsConfirmed { get; set; }
}