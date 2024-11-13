using backend.Data;
using backend.Models;

namespace backend.Repository;

public interface IUserRepo
{
    AppUser? GetUserById(int id);

    AppUser SaveNew(AppUser appUser);

}

public class UserRepo : IUserRepo
{
    
    private readonly BackendContext _db;
    public UserRepo(BackendContext db)
    {
        _db = db;
    } 

    public AppUser? GetUserById(int id)
    {
        return _db.AppUsers.Find(id);
    }

    public AppUser SaveNew(AppUser appUser)
    {
        _db.AppUsers.Add(appUser);
        _db.SaveChanges();
        return appUser;
    }
}