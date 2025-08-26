using FluentValidation;
using System.Text.Json.Serialization;

namespace FoodGestor.Args
{
    public class CategoryArgs
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        public class Validator : AbstractValidator<CategoryArgs>
        {
            public Validator()
            {
                RuleFor(x => x.Name)
                    .NotEmpty()
                    .WithMessage("O nome é obrigatório")
                    .MaximumLength(128)
                    .WithMessage("O nome deve ter no máximo 128 caracteres");
            }
        }
    }
}
