namespace backend.Models;

public class User(string username, string passowrd)
{
    public int Id { get; set; }
    public string Username { get; set; } = username;
    public string Password { get; set; } = passowrd;
}