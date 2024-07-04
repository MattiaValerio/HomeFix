using HomeFix.Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeFix.Api.Controllers;

[Route("api/[controller]")]
public class TecController : Controller
{
    private readonly DataContext _context;

    public TecController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetTecs()
    {
        try
        {
            var tecs = await _context.Tecnichians.ToListAsync();
            return Ok(tecs);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}