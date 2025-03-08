using BankingDashboard.Domain.Entities;

namespace BankingDashboard.API.Models
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }

        public UserResponse(User user)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            CreatedAt = user.CreatedAt;
        }
    }
}
