using HomeFix.Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeFix.Api.Controllers;

[Route("api/[controller]")]
public class JobController : Controller
{
    private readonly DataContext _context;

    public JobController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetJobs()
    {
        try
        {
            var jobs = await _context.Jobs
                .Include(c=> c.Tecnic)
                .Include(c=>c.Request)
                .ToListAsync();
            return Ok(jobs);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}