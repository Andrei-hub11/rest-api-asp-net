using Microsoft.EntityFrameworkCore;
using Web_API_JWT.Context;
using Web_API_JWT.Exceptons;
using Web_API_JWT.Models;
using Web_API_JWT.Movies.Contracts;
using Web_API_JWT.Services.Interfaces;

namespace Web_API_JWT.Services;

public class MovieService: IMovie
{
    private readonly AppDBContext _context;

    public MovieService(AppDBContext context) => _context = context;

    public async Task<IEnumerable<MovieModel>> GetMoviesAsync()
    {
        return await _context.Movies.ToListAsync();
    }

    public async Task<MovieModel> GetMovieAsync(int id)
    {
        return await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);
    
    }

    public async Task<MovieModel> CreateMovieAsync(MovieModel movie)
    {
        _context.Movies.Add(movie);
        await _context.SaveChangesAsync();
        return movie;
    }
 

    public async Task<MovieModel> UpdateMovieAsync(int id, MovieModel movie)
    {

        var existingMovie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);

        if (existingMovie != null)
        {
            existingMovie.Name = movie.Name ?? existingMovie.Name;
            existingMovie.Director = movie.Director ?? existingMovie.Director;
            if (movie.Year != null)
            {
                existingMovie.Year = movie.Year;
            }

            await _context.SaveChangesAsync();
            return existingMovie;
        }

        throw new Exception($"O usuário com o id {id} não foi encontrado");
    }

    public async Task DeleteMovieAsync(int id)
    {
        var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);

        if (movie != null)
        {
            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
        }
        else
        {
            throw new NotFoundException($"O filme com o id {id} não foi encontrado");
        }
    }
}
