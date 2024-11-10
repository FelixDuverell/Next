namespace backend.Models;

public class Kanbanpost
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Message { get; set; }

    public Kanbanpost(string title, string message)
    {
        Title = title;
        Message = message;
    }

    public override string ToString()
    {
        return $"{{Id: {Id}, Title: {Title}, Message: {Message}}}";
    }
}