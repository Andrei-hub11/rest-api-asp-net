using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;
using Web_API_JWT.Models;

using Web_API_JWT.Services;

using Web_API_JWT.Validators.Movies;

namespace Web_API_JWT.Controllers
{
    [Route("api/v1/")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly MovieService _movieService;
        public MovieController(MovieService movieService) {
        _movieService = movieService;   
        }

        [HttpGet("movie/{id}")]
        public async Task<IActionResult> GetProducts(int id)
        {
            try
            {
                var movie = await _movieService.GetMovieAsync(id);

                if (movie != null)
                {
                    return Ok(movie);
                }
                else
                {

                    return NotFound(new { Message = $"Nenhum filme encontrado com id {id}." });
                }
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { Message = "Ocorreu um erro ao buscar os filmes.", Error = ex.Message });
            }
        }


        [HttpGet("movies")]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                var movies = await _movieService.GetMoviesAsync();

                if (movies != null)
                {
                    return Ok(movies);
                }
                else
                {
                  
                    return NotFound(new { Message = "Nenhum filme encontrado." });
                }
            }
            catch (Exception ex)
            {
               
                return StatusCode(500, new { Message = "Ocorreu um erro ao buscar os filmes.", Error = ex.Message });
            }
        }

        [Authorize(Policy = "Admin")]
        [HttpPost("add-movie")]
        public async Task<IActionResult> CreateMovie([FromBody] MovieModel  movie)
        {
            var validator = new MovieValidator();
            var validationResult = validator.Validate(movie);

            if (!validationResult.IsValid)
            {
                var erros = validationResult.Errors.Select(erro => erro.ErrorMessage).ToList();
                return BadRequest(new {Message = "Os campos não foram corretamente preenchidos", Erros = erros });
            }
            try
            {

            var result = await _movieService.CreateMovieAsync(movie);
            return Ok(new { Message = "O filme foi adicionado com sucesso", Movie = result});
            } catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Erro durante a adição do filme", Error = ex.Message });
            }


        }

        [Authorize(Policy = "Admin")]
        [HttpPut("update-movie/{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] MovieModel movieEdit)
        {
            var validator = new MovieValidator();
            var validationResult = validator.Validate(movieEdit);
            if (!validationResult.IsValid)
            {
                var erros = validationResult.Errors.Select(erro => erro.ErrorMessage).ToList();
                return BadRequest(new { Message = "Os campos não foram corretamente preenchidos", Erros = erros });
            }

            try
            {
                var result = await _movieService.UpdateMovieAsync(id, movieEdit);
                return Ok(new { Message = "Movie atualizado com sucesso.", Movie = result });
            }
            catch (Exception ex)
            {
               
                return StatusCode(500, new { Message = "Erro durante a atualização do filme", Error = ex.Message });
            }
        }

        [Authorize(Policy = "Admin")]
        [HttpDelete("delete-movie/{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {

            try
            {

            await _movieService.DeleteMovieAsync(id);
                return Ok(new { Message = $"O filme com id {id} foi deletado com sucesso" });

            } catch (Exception ex)
            {

            return StatusCode(500, new { Message = $"Erro ao tentar deletar o filme com id {id}", Error = ex.Message });
            }

        }

    }
}
