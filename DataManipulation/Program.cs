using System.Collections;

var diasDaSemana = new DiasDaSemana();

var carrinho = new List<Produto>
{
    new Produto { Nome = "Oleo", Preco = 10.0 },
    new Produto { Nome = "Leite", Preco = 5.50 },
    new Produto { Nome = "Ketchup", Preco = 7.49 }
};

var pares = NumerosParesComYield();
var contador = 0;
foreach (var par in pares)
{
    contador++;
    Console.WriteLine(par);
    if (contador > 6) break;
}

IEnumerable<int> NumeroParesSemYield(int Limite)
{
    var lista = new List<int>(); 
    for (var i = 0; i < Limite; i++)
    {
        Console.WriteLine($"Processando elemento {i}...");
        lista.Add(i * 2);
    }
    return lista;
}

IEnumerable<int> NumerosParesComYield()
{
    var i = 0;
    while (true)
    {
        Console.WriteLine($"Processando elemento {i}...");
        yield return i * 2;
        i++;
    }
}


foreach (var dia in diasDaSemana)
{
    Console.WriteLine(dia);
}

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

class DiasDaSemanaEnumerator : IEnumerator<string>
{
    private int posicao = -1;
    public string[] dias = { "Domingo", "Segunda-feira", "Terça-feira", "Quarta-feira", "Quinta-feira", "Sexta-feira", "Sábado" };
    public string Current => dias[posicao];

    object IEnumerator.Current => Current;

    public void Dispose()
    {
    
    }

    public bool MoveNext()
    {
        posicao++;
        return posicao < dias.Length;
    }

    public void Reset()
    {
        posicao = -1;
    }
}
class DiasDaSemana : IEnumerable<string>
{
    public string[] dias = { "Domingo", "Segunda-feira", "Terça-feira", "Quarta-feira", "Quinta-feira", "Sexta-feira", "Sábado" };

    public IEnumerator<string> GetEnumerator()
    {
        yield return "Domingo";
        yield return "Segunda-feira";
        yield return "Terça-feira";
        yield return "Quarta-feira";
        yield return "Quinta-feira";
        yield return "Sexta-feira";
        yield return "Sábado";
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}