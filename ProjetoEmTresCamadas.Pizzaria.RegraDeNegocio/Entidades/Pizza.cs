using FluentValidation;
using ProjetoEmTresCamadas.Pizzaria.DAO.ValueObjects;

namespace ProjetoEmTresCamadas.Pizzaria.RegraDeNegocio.Entidades;


public class Pizza : EntidadeBase
{
    public string Sabor { get; set; }
    public TamanhoDePizza TamanhoDePizza { get; set; }
    public string Descricao { get; set; }
    public double Valor { get; set; }

    public Pizza()
    {
        Descricao = string.Empty;
        Valor = 0;
        TamanhoDePizza = TamanhoDePizza.Pequena;
        Sabor = string.Empty;
    }

    public override string ToString()
    {
        return $"Sua Pizza é de sabor {Sabor} e tamanho {TamanhoDePizza}";
    }

    public PizzaVo ToPizzaVo()
    {
        return new PizzaVo()
        {
            Id = Id,
            Descricao = Descricao,
            Sabor = Sabor,
            TamanhoDePizza = Convert.ToInt32(TamanhoDePizza),
            Valor = Valor
        };
    }
}


public class PizzaValidator : AbstractValidator<Pizza>
{
    public PizzaValidator()
    {
        RuleFor( pizza =>  pizza.Sabor ).NotEmpty();
        RuleFor( pizza =>  pizza.Valor ).NotEmpty();
        RuleFor( pizza =>  pizza.TamanhoDePizza ).NotEmpty();
        RuleFor( pizza =>  pizza.Descricao ).NotEmpty();
    }
}