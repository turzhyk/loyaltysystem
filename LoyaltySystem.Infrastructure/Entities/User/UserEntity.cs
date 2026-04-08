namespace LoyaltySystem.Infrastructure.Entities.User;

public class UserEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public int Coins { get; set; }
}