namespace Webbing.Assignment.Controllers;

[ApiController]
[Route("api")]
public class UsageController : ControllerBase
{
    private readonly ILogger<UsageController> _logger;
    private readonly ApplicationDbContext _context;

    public UsageController(ILogger<UsageController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet("usages-group-by-sim")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUsagesGroupBySim(
        Guid customerId,
        DateTime FromDate,
        DateTime ToDate)
    {
        var query =
         from ne in _context.NetworkEvents
         join s in _context.Sims on ne.SimId equals s.Id
         join c in _context.Customers on s.CustomerId equals c.Id
         where s.Id == customerId && ne.CreatedOnUtc >= FromDate && ne.CreatedOnUtc <= ToDate
         group ne.Quota by new
         {
             Id = s.Id,
             Name = c.Name,
             Sims = s.Id,
             Usage = ne.Quota
         }
         into g
         select new
         {
             Id = g.Key.Id,
             Name = g.Key.Name,
             Sims = g.Key.Sims,
             Count = 2
         };

        return Ok(await query.ToListAsync());
    }

    [HttpGet("usages-group-by-customer")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUsagesGroupByCustomer(
        DateTime FromDate,
        DateTime ToDate)
    {
        var query =
            from ne in _context.NetworkEvents
            join s in _context.Sims on ne.SimId equals s.Id
            join c in _context.Customers on s.CustomerId equals c.Id
            where ne.CreatedOnUtc >= FromDate && ne.CreatedOnUtc <= ToDate
            group ne by new
            {
                c.Id,
                c.Name,
                ne.CreatedOnUtc,
                ne.Quota
            }
            into g
            orderby g.Key.Id ascending
            select new
            {
                CId = g.Key.Id,
                Name = g.Key.Name,
                Date = g.Key.CreatedOnUtc,
                Count = g.Sum(x => x.Quota)
            };

        query = query.Take(2);

        return Ok(await query.ToListAsync());
    }
}
