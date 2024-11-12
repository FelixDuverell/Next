using System.Data.Common;
using backend.Data;
using backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Route("api/kanbanposts")]
[ApiController]
[Authorize]
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

    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Produces("application/json")]
    public ActionResult<Kanbanpost> GetKanbanpostById([FromRoute] int id)
    {
        Kanbanpost? kanbanpost = _db.Kanbanposts.Find(id);

        if(kanbanpost == null)
        {
            return NotFound();
        }

        return Ok(kanbanpost);
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteById([FromRoute] int id)
    {
        Kanbanpost? kanbanpost = _db.Kanbanposts.Find(id);
        if (kanbanpost == null)
        {
            return NotFound();
        }

        _db.Kanbanposts.Remove(kanbanpost);
        _db.SaveChanges();
        return Ok();
    }

    public record CreateKanbanpostDto(string Title, string Message);

    [HttpPost]
    public ActionResult<Kanbanpost> CreatePost([FromBody] CreateKanbanpostDto kanbanpostDto)
    {
        Kanbanpost kanbanpost = new Kanbanpost(kanbanpostDto.Title, kanbanpostDto.Message);
        _db.Kanbanposts.Add(kanbanpost);
        _db.SaveChanges();
        return CreatedAtAction(nameof(GetKanbanpostById), new { Id = kanbanpost.Id }, kanbanpost);
    }
}