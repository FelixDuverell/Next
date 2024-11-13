using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;

public class Kanbanpost
{
    private string v;
    private object id;

    public int Id { get; set; }
    public string Title { get; set; }
    public string Message { get; set; }


     [ForeignKey("User")]    
    public string UserId { get; set; }

    public AppUser? appUser { get; set; } 

    public Kanbanpost(string title, string message, string userId)
    {
        Title = title;
        Message = message;
        UserId = userId;
    }

    public Kanbanpost(string v, object id)
    {
        this.v = v;
        this.id = id;
    }

    public override string ToString()
    {
        return $"{{Id: {Id}, Title: {Title}, Message: {Message}}}";
    }
}