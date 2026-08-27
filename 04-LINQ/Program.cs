/*
    Seja um arquivo com músicas em formato CSV (Comma Separated Values). 

    Implemente as funções abaixo:
    //     [x] Leia-o como uma coleção de músicas
    //     [x] Filtre a coleção por artista (por ex. Coldplay, Metallica, AC/DC)
    //     [x] Filtre a coleção por gênero (por ex. rock)
    //     [x] Filtre a coleção por duração (por ex. maiores que 5 minutos)
    //     [x] Ordene a coleção por artista
    //     [x] Ordene a coleção por artista e em seguida por músicas com duração crescente
    //     [ ] Crie uma coleção de artistas e suas músicas
    //     [ ] Informe a duração média das músicas da coleção
    //     [ ] Informe a duração total das músicas da coleção
    //     [ ] Informe qual artista tem mais músicas na coleção
 
*/

using System.Runtime.CompilerServices;

using var arquivo = new FileStream("C:\\Users\\kawan\\source\\repos\\DataManipulation\\03-AbstraindoAFonteDeDados\\musicas.csv", FileMode.Open, FileAccess.Read);
using var stream = new StreamReader(arquivo);


var generos = ObterArquivo(stream)
                .SelectMany(m => m.Genero) // projecao ou transformacao 
                .Distinct() // filtragem
                .OrderBy(g => g);

foreach (var genero in generos)
{
    Console.WriteLine(genero);
}

void OperacoesComProjecoes(StreamReader stream)
{
    var artistas = ObterArquivo(stream)
                .Select(m => m.Artista) // projecao ou transformacao 
                .Distinct() // filtragem5
                .OrderBy(a => a);

    foreach (var artista in artistas)
    {
        Console.WriteLine(artista);
    }
}
void OperacoesDeFiltroEOrdenacao(StreamReader stream)
{
    var musicasColdplay =
    ObterArquivo(stream)
    .Where(m => m.Artista == "Coldplay")
    .OrderBy(m => m.Titulo)   // ThenBy (m => m.Duracao)                             
                              //.OrderBy(musica => musica.Artista)   isso permite que a gente ordene por mais de um critério, primeiro por artista e depois por duração.
                              // Skip(5) pula os 5 primeiros elementos da coleção
    .Take(5);
    //.OrderBy(m => m.Duracao); // mais conciso e legivel! isso se chama expressao lambda com LINQ (Language Integrated Query) - uma forma de consultar coleções de dados em C#. 
    ExibirMusicas(musicasColdplay);
}

void ExibirMusicas(IEnumerable<Musica> musicas)
{
    var contador = 0;
    Console.WriteLine("Exibindo músicas:");
    foreach (var musica in musicas)
    {
        Console.WriteLine($"\t - {musica.Titulo} ({musica.Artista}), Duração: {musica.Duracao} segundos");
        contador++;
        if (contador > 10) break;
    }
}
IEnumerable<Musica> ObterArquivo(StreamReader stream)
{
    var linha = stream.ReadLine();
    while (linha is not null)
    {
        var separador = linha.Split(';');
        var musica = new Musica
        {
            Titulo = separador[0],
            Artista = separador[1],
            Duracao = Convert.ToInt32(separador[2]),
            Genero = separador[3].Split(',').Select(g => g.Trim())
        };
        yield return musica;
        linha = stream.ReadLine();
    }
}

//bool FiltrarPorArtista(Musica musica) => musica.Artista == "ColdPlay";
//bool FiltrarPorDuracao(Musica musica) => musica.Duracao > 400; //delegate = tipos que represetam metodos com a mesma assinatura.  

static class MusicaExtensions
{
    public static IEnumerable<T> FiltrarPor<T>(this IEnumerable<T> colecoes, Func<T, bool> condicao)
    {
        foreach (var elementos in colecoes)
        {
            if (condicao(elementos)) yield return elementos;
        }
    }
}
class Musica
{
    public string Titulo { get; set; }
    public string Artista { get; set; }
    public int Duracao { get; set; }
    public IEnumerable<string> Genero { get; set; }
}


//public delegate int Operacao(int a, int b);

//class Program
//{
//    static int Somar(int x, int y) => x + y;
//    static int Subtrair(int x, int y) => x - y;

//    static void Main()
//    {
//        Operacao op = Somar;                             isso e um delegate, criado manualmente (raramente criamos um delegate do zero)
//        Console.WriteLine(op(3, 4)); // Saída: 7

//        op = Subtrair;
//        Console.WriteLine(op(10, 5)); // Saída: 5
//    }
//}