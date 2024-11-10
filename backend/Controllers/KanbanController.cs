using System.Data.Common;
using backend.Data;
using backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Route("api/kanbanposts")]
[ApiController]
public class KanbanController : ControllerBase
{

    private BackendContext _db;

    public KanbanController(BackendContext backendContext)
    {
        _db = backendContext;
    }

    [HttpGet]
    public ActionResult<List<Kanbanpost>> GetAllKanbanposts()
    {
        return _db.Kanbanposts.ToList();
    } 
}