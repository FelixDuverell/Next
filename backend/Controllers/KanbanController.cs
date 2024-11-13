using System.Data.Common;
using System.Security.Claims;
using backend.Data;
using backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

    public record GetKanbanpostRequestDto(int Id, string Title, string Message, string UserId);

    [HttpGet]
    public ActionResult<List<GetKanbanpostRequestDto>> GetAllKanbanposts()
    {
        return _db.Kanbanposts
            .Select(kanban => new GetKanbanpostRequestDto(kanban.Id, kanban.Title, kanban.Message, kanban.UserId))
            .ToList();
    } 

    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Produces("application/json")]
    public ActionResult<GetKanbanpostRequestDto> GetKanbanpostById([FromRoute] int id)
    {
        Kanbanpost? kanbanpost = _db.Kanbanposts.Find(id);

        if(kanbanpost == null)
        {
            return NotFound();
        }

        return Ok(new GetKanbanpostRequestDto(kanbanpost.Id, kanbanpost.Title, kanbanpost.Message, kanbanpost.UserId));
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

    public record CreateKanbanpostDto(string Title, string Message, string UserId);

    [HttpPost]
    public ActionResult<Kanbanpost> CreatePost([FromBody] CreateKanbanpostDto kanbanpostDto)
    {
        Kanbanpost kanbanpost = new Kanbanpost(kanbanpostDto.Title, kanbanpostDto.Message, kanbanpostDto.UserId);
        _db.Kanbanposts.Add(kanbanpost);
        _db.SaveChanges();
        return CreatedAtAction(nameof(GetKanbanpostById), new { Id = kanbanpost.Id }, kanbanpost);
    }

}