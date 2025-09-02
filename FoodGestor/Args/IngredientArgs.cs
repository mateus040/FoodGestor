using FluentValidation;
using FoodGestor.Enums;
using System.Text.Json.Serialization;

namespace FoodGestor.Args
{
    public class IngredientArgs
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("unit_measure")]
        public UnitMeasureEnum UnitMeasure { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        public class Validator : AbstractValidator<IngredientArgs>
        {
            public Validator()
            {
                RuleFor(x => x.Name)
                    .NotEmpty()
                    .WithMessage("O nome é obrigatório")
                    .MaximumLength(128)
                    .WithMessage("O nome deve ter no máximo 128 caracteres");

                RuleFor(x => x.UnitMeasure)
                    .NotEmpty()
                    .WithMessage("A unidade de medida é obrigatória")
                    .IsInEnum()
                    .WithMessage("A unidade de medida informada é inválida");

                RuleFor(x => x.Quantity)
                    .GreaterThan(0)
                    .WithMessage("A quantidade deve ser maior que zero");
            }
        }
    }
}
