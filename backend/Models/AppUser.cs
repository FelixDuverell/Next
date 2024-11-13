using Microsoft.AspNetCore.Identity;

namespace backend.Models;

public class AppUser : IdentityUser
{

    public List<Kanbanpost> Kanbanposts { get; set; } = [];
    
}