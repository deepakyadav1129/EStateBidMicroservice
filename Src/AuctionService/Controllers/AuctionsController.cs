using AuctionService.Dtos;
using AuctionService.Extensions;
using AuctionService.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AuctionService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuctionsController : ControllerBase
    {
        private readonly IAuctionRepository _auctionRepository;

        public AuctionsController(IAuctionRepository auctionRepository)
        {
            _auctionRepository = auctionRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<AuctionDto>>> GetAuctions()
        {
            return Ok(await _auctionRepository.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuctionDto>> GetAuction(Guid id)
        {
            return Ok(await _auctionRepository.GetAuctionByIdAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult<AuctionDto>> CreateAuction(CreateAuctionDto dto)
        {
            var auction = dto.ToAuctionEntity();
            _auctionRepository.AddAuction(auction);

            var result = await _auctionRepository.SaveChangesAsync();

            if (!result)

                return BadRequest("could not save auction");
            return CreatedAtAction(nameof(GetAuction), new { id = auction.Id }, auction.ToAuctionDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuctionAsync(Guid id, UpdateAuctionDto dto)
        {
            var auction = await _auctionRepository.GetAuctionByIdAsync(id);
            if (auction == null)
                return NotFound();
            auction.ToUpdateAuction(dto);
            await _auctionRepository.SaveChangesAsync();
            return Ok();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuction(Guid id)
        {
            var auction = await _auctionRepository.GetAuctionByIdAsync(id);
            if (auction == null)
                return NotFound();
            _auctionRepository.RemoveAuction(auction);
            await _auctionRepository.SaveChangesAsync();
            return Ok();
        }
    }
}
