namespace Backend.API.Models
{
    public class UserInformation
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public Address? Address { get; set; }
        public string Phone { get; set; } = string.Empty;
        public string Website { get; set; } = string.Empty;
        public Company? Company { get; set; }

    }
}
