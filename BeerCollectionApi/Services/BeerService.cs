﻿using BeerCollectionAPI.Data;
using BeerCollectionAPI.Models;
using BeerCollectionAPI.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BeerCollectionAPI.Services;

public class BeerService
{
    private readonly BeerCollectionContext _context;

    public BeerService(BeerCollectionContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<BeerDTO>> GetAllBeers()
    {
        return await _context.Beers
            .Select(b => new BeerDTO
            {
                Id = b.Id,
                Name = b.Name,
                Type = b.Type.ToString(),
                AverageRating = b.AverageRating
            })
            .ToListAsync();
    }

    public async Task<IEnumerable<BeerDTO>> SearchBeers(string searchTerm)
    {
        return await _context.Beers
            .Where(b => b.Name.Contains(searchTerm))
            .Select(b => new BeerDTO
            {
                Id = b.Id,
                Name = b.Name,
                Type = b.Type.ToString(),
                AverageRating = b.AverageRating
            })
            .ToListAsync();
    }

    public async Task<BeerDTO?> GetBeerById(int id)
    {
        var beer = await _context.Beers.FindAsync(id);

        if (beer == null) return null;

        return new BeerDTO
        {
            Id = beer.Id,
            Name = beer.Name,
            Type = beer.Type.ToString(),
            AverageRating = beer.AverageRating
        };
    }
    public async Task<BeerDTO> AddBeer(BeerDTO beerDto)
    {
        if (!Enum.TryParse<BeerType>(beerDto.Type, out var beerType))
        {
            throw new ArgumentException("Invalid beer type");
        }

        var beer = new Beer
        {
            Name = beerDto.Name,
            Type = beerType
        };

        _context.Beers.Add(beer);
        await _context.SaveChangesAsync();

        beerDto.Id = beer.Id;
        return beerDto;
    }

    public async Task<bool> UpdateBeerRating(BeerRatingDTO ratingDto)
    {
        if (ratingDto.Score < 1.0 || ratingDto.Score > 5.0)
        {
            throw new ArgumentException("Rating must be between 1.0 and 5.0");
        }

        var beer = await _context.Beers
            .Include(b => b.Ratings)
            .FirstOrDefaultAsync(b => b.Id == ratingDto.BeerId);

        if (beer == null) return false;

        var rating = new BeerRating
        {
            BeerId = ratingDto.BeerId,
            Score = ratingDto.Score
        };
        _context.BeerRatings.Add(rating);

        beer.RatingCount = beer.Ratings.Count + 1;
        beer.AverageRating = Math.Round(
            ((beer.AverageRating ?? 0) * (beer.RatingCount - 1) + ratingDto.Score) / beer.RatingCount,
            1
        );

        await _context.SaveChangesAsync();
        return true;
    }


    public async Task<BeerDetailedDTO?> GetBeerDetailsById(int id)
    {
        var beer = await _context.Beers
            .Include(b => b.Ratings)
            .FirstOrDefaultAsync(b => b.Id == id);

        if (beer == null) return null;

        return new BeerDetailedDTO
        {
            Id = beer.Id,
            Name = beer.Name,
            Type = beer.Type.ToString(),
            AverageRating = beer.AverageRating,
            RatingCount = beer.RatingCount,
            Ratings = beer.Ratings.Select(r => new RatingDetailDTO
            {
                Score = r.Score,
                RatedOn = r.RatedOn
            }).ToList()
        };
    }

    public async Task<IEnumerable<BeerDetailedDTO>> GetAllBeersWithRatings()
    {
        return await _context.Beers
            .Include(b => b.Ratings)
            .Select(b => new BeerDetailedDTO
            {
                Id = b.Id,
                Name = b.Name,
                Type = b.Type.ToString(),
                AverageRating = b.AverageRating,
                RatingCount = b.RatingCount,
                Ratings = b.Ratings.Select(r => new RatingDetailDTO
                {
                    Score = r.Score,
                    RatedOn = r.RatedOn
                }).ToList()
            })
            .ToListAsync();
    }

}