using FluentValidation;
using Web_API_JWT.Models;

namespace Web_API_JWT.Validators.Movies
{
    public class MovieValidator: AbstractValidator<MovieModel>
    {
        public MovieValidator() {
            RuleFor(m => m.Name)
                .NotEmpty().WithMessage("O nome não pode estar em branco.")
                .MaximumLength(30).WithMessage("O nome não pode ter mais de 30 caracteres.");
            RuleFor(m => m.Director).NotEmpty().WithMessage("O nome do diretor não pode estar em branco.").
                MaximumLength(30).WithMessage("O nome do diretor não pode ter mais de 30 caracteres.");
            RuleFor(m=> m.Category).NotEmpty().WithMessage("O nome da categoria não pode estar em branco.").
                MaximumLength(20).WithMessage("A categoria não pode ter mais de 20 caracteres.");
            RuleFor(movie => movie.Year)
           .InclusiveBetween(1900, DateTime.Now.Year).WithMessage("O ano do filme deve estar entre 1900 e o ano atual.");
        }
    }
}
