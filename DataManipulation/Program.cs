var diasDaSemana = new DiasDaSemana();

var carrinho = new List<Produto>
{
    new Produto { Nome = "Oleo", Preco = 10.0 },
    new Produto { Nome = "Leite", Preco = 5.50 },
    new Produto { Nome = "Ketchup", Preco = 7.49 }
};

PercorrendoComFor();
void PercorrendoComFor()
{
    for (int i = 0; i < carrinho.Count; i++)
    {
        Console.WriteLine($"Produto: {carrinho[i].Nome}, Preço: {carrinho[i].Preco}");
    }
}
class Produto
{
    public string Nome { get; set; }
    public double Preco { get; set; }
};

class DiasDaSemana
{
    public string[] dias = { "Domingo", "Segunda-feira", "Terça-feira", "Quarta-feira", "Quinta-feira", "Sexta-feira", "Sábado" };
}

