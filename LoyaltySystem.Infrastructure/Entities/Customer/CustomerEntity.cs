namespace LoyaltySystem.Infrastructure.Entities.User;

public class CustomerEntity
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Email { get; set; }
    public int Points { get; set; }
    public required string Phone { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsConfirmed { get; set; }
}