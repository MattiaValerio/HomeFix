using HomeFix.Api.Data;
using HomeFix.Shared.Entities;
using HomeFix.Shared.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeFix.Api.Controllers;

[Route("api/[controller]")]
public class RequestController : Controller
{
    private readonly DataContext _context;

    public RequestController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetRequests()
    {
        try
        {
            var requests = await _context.Requests.ToListAsync();
            return Ok(requests);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // [HttpGet("{id}")]
    // public async Task<IActionResult> GetRequest(Guid id)
    // {
    //     try
    //     {
    //         var request = await _context.Requests.FirstOrDefaultAsync(r => r.Id == id);
    //         return Ok(request);
    //     }
    //     catch (Exception ex)
    //     {
    //         return BadRequest(ex.Message);
    //     }
    // }

    [HttpPost]
    public async Task<IActionResult> CreateRequest(Request request)
    {
        try
        {
            var newRequest = await _context.Requests.AddAsync(request);
            await _context.SaveChangesAsync();
            return Ok(newRequest.Entity);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("assign")]
    public async Task<IActionResult> AssignRequest( Guid reqId,Guid tecnichianId,DateTime app)
    {
        try
        {
            var requestToAssign = await _context.Requests.FirstOrDefaultAsync(r => r.Id == reqId);
            var tecnichian = await _context.Tecnichians.FirstOrDefaultAsync(t => t.Id == tecnichianId);

            if (requestToAssign == null)
            {
                return NotFound("Richiesta non trovata");
            }

            if (tecnichian == null)
            {
                return NotFound("Tecnico non trovato");
            }

            requestToAssign.RequestStatus = RequestStatus.Assigned;

            var newJob = requestToAssign?.AssignJob(tecnichian, app);

            if (newJob == null)
            {
                return BadRequest("Errore nell'assegnazione della richiesta");
            }

            await _context.Jobs.AddAsync(newJob);
            await _context.SaveChangesAsync();

            return Ok(newJob);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRequest(Guid id)
    {
        try
        {
            var request = await _context.Requests.FirstOrDefaultAsync(r => r.Id == id);

            if (request == null)
            {
                return NotFound();
            }

            _context.Requests.Remove(request);
            await _context.SaveChangesAsync();
            return Ok("Richiesta eliminata: " + request.Id);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

}