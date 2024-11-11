namespace backend.Models;

public class AppUser(string username, string password)
{
    public int Id { get; set; }
    public string Username { get; set; } = username;
    public string Password { get; set; } = password;
}