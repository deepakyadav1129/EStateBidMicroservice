using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SearchService.Contracts;
using SearchService.Data;
using SearchService.Entities;

namespace SearchService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class SearchController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SearchController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<ItemSearch>>> SearchItems([FromQuery] SearchParams searchParams)
        {
            var query = _context.ItemSearches.AsQueryable(); 
            if(!string.IsNullOrEmpty(searchParams.SearchTerm))
            {

                query =  query.Where(x=>x.Title.Contains(searchParams.SearchTerm)
                || x.Description.Contains(searchParams.SearchTerm) 
                || x.City.Contains(searchParams.SearchTerm));
            }
            query = searchParams.OrderBy switch
            {
                "price" => query.OrderBy(x => x.ReservePrice),
                "priceDesc" => query.OrderByDescending(x => x.ReservePrice),
                "new" => query.OrderByDescending(x => x.CreatedAt),
                "city" => query.OrderBy(x => x.City),
                _ => query.OrderBy(x => x.AuctionEnd)
            };
            if(!string.IsNullOrEmpty(searchParams.FilterBy))
            {
                query = searchParams.FilterBy switch
                {
                    "finished" => query.Where(x => x.AuctionEnd < DateTime.UtcNow),
                    "endingSoon" => query.Where(x =>
                    x.AuctionEnd < DateTime.UtcNow.AddHours(6) &&
                    x.AuctionEnd > DateTime.UtcNow),

                    _ => query.Where(x => x.AuctionEnd > DateTime.UtcNow)
                };
            }
            if(!string.IsNullOrEmpty(searchParams.Seller))
            {
                query = query.Where(x=>x.Seller==searchParams.Seller); 
            }
            if(!string.IsNullOrEmpty(searchParams.Winner))
            {
                query = query.Where(x=>x.Seller==searchParams.Winner); 
            }
            var totalCount = await query.CountAsync();
            var items = await query.Skip((searchParams.PageNumber - 1) * searchParams.PageSize)
                .Take(searchParams.PageSize).ToListAsync();
            var pageCount =  (int)Math.Ceiling((double)totalCount/ searchParams.PageSize);
            return Ok(new
            {
                results = items,
                pageCount,
                totalCount
            });


        }
    }
}
