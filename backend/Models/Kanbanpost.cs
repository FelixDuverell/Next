using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;

public class Kanbanpost
{
    public int Id { get; set; }
    public string Title { get; set; }

    [ForeignKey("User")]    
    public string UserId { get; set; }

    public AppUser? appUser { get; set; } 

    public Kanbanpost(string title, string userId)
    {
        Title = title;
        UserId = userId;
    }

    public override string ToString()
    {
        return $"{{Id: {Id}, Title: {Title}}}";
    }
}