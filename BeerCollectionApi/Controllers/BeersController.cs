using BeerCollectionAPI.DTOs;
using BeerCollectionAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BeerCollectionAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BeersController : ControllerBase
{
    private readonly BeerService _beerService;

    public BeersController(BeerService beerService)
    {
        _beerService = beerService;
    }

    // GET: api/Beers
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BeerDTO>>> GetBeers()
    {
        return Ok(await _beerService.GetAllBeers());
    }

    // GET: api/Beers/search/ipa
    [HttpGet("search/{searchTerm}")]
    public async Task<ActionResult<IEnumerable<BeerDTO>>> SearchBeers(string searchTerm)
    {
        return Ok(await _beerService.SearchBeers(searchTerm));
    }

    // GET: api/Beers/5
    [HttpGet("{id}")]
    public async Task<ActionResult<BeerDTO>> GetBeer(int id)
    {
        var beer = await _beerService.GetBeerById(id);

        if (beer == null)
        {
            return NotFound();
        }

        return beer;
    }

    // POST: api/Beers
    [HttpPost]
    public async Task<ActionResult<BeerDTO>> PostBeer(BeerDTO beerDTO)
    {
        var createdBeer = await _beerService.AddBeer(beerDTO);
        return CreatedAtAction(nameof(GetBeer), new { id = createdBeer.Id }, createdBeer);
    }

    // POST: api/Beers/rate
    [HttpPost("rate")]
    public async Task<IActionResult> RateBeer(BeerRatingDTO ratingDTO)
    {
        if (ratingDTO.Score < 1.0 || ratingDTO.Score > 5.0)
        {
            return BadRequest("Rating must be between 1.0 and 5.0");
        }

        var success = await _beerService.UpdateBeerRating(ratingDTO);
        if (!success)
        {
            return NotFound();
        }

        return NoContent();
    }


    // GET: api/Beers/details/all
    [HttpGet("details/all")]
    public async Task<ActionResult<IEnumerable<BeerDetailedDTO>>> GetAllBeersWithRatings()
    {
        return Ok(await _beerService.GetAllBeersWithRatings());
    }

    // GET: api/Beers/details/5
    [HttpGet("details/{id}")]
    public async Task<ActionResult<BeerDetailedDTO>> GetBeerDetails(int id)
    {
        var beer = await _beerService.GetBeerDetailsById(id);

        if (beer == null)
        {
            return NotFound();
        }

        return beer;
    }

}