using Web_API_JWT.Models;
using Web_API_JWT.Movies.Contracts;

namespace Web_API_JWT.Services.Interfaces;

public interface IMovie
{
    Task<IEnumerable<MovieModel>> GetMoviesAsync();
    Task<MovieModel> GetMovieAsync(int id);
    Task<MovieModel> CreateMovieAsync(MovieModel movie);
    Task<MovieModel> UpdateMovieAsync(int id, MovieModel movie);
    Task DeleteMovieAsync(int id);
}
