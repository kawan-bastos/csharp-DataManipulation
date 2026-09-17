/*
    Seja um arquivo com músicas em formato CSV (Comma Separated Values). 

    Implemente as funções abaixo:
    //     [x] Leia-o como uma coleção de músicas
    //     [x] Filtre a coleção por artista (por ex. Coldplay, Metallica, AC/DC)
    //     [x] Filtre a coleção por gênero (por ex. rock)
    //     [x] Filtre a coleção por duração (por ex. maiores que 5 minutos)
    //     [x] Ordene a coleção por artista
    //     [x] Ordene a coleção por artista e em seguida por músicas com duração crescente
    //     [x] Crie uma coleção de artistas e suas músicas
    //     [x] Informe a duração média das músicas da coleção
    //     [x] Informe a duração total das músicas da coleção
    //     [x] Informe qual artista tem mais músicas na coleção
    //     [x] Artista com pelo menos uma musica acima de 6 minutos (400 segundos)
    //     [x] Artista com pelo menos uma musica de reggae
    //     [x] Existem musica de Jazz na coleção? 
 
*/

/*
Fluxo Padrão: Estágio 1 (Origem Dados) > Estágio 2 > ... > Estágio N

LINQ - Categorias de operações para manipulação de coleções
============================================================

Filtro (+)      | coleção c/ tam menor/igual atendendo condição | Where, Distinct
Projeção (+)    | coleção transformada, do mesmo tipo ou não     | Select, SelectMany
Ordenação (+)   | coleção ordenada pela expressão lambda        | OrderBy, ThenBy
Agregação (*)   | valor único a partir de operação de acúmulo   | Sum, Min, Max
Agrupamento (+) | coleção de grupos onde a chave é o argumento  | GroupBy
Elementos (*)   | elemento único T a partir do argumento        | First, Last, MinBy
Existência (*)  | booleano a partir da operação e argumento     | All, Any, Contains
Conversão (*)   | coleção em outra estrutura                    | ToList, ToArray

+ operações avalidas sob demanda (yield)
* operações avalidas imediatamente
*/


using System.Runtime.CompilerServices;

using var arquivo = new FileStream("C:\\Users\\kawan\\source\\repos\\DataManipulation\\03-AbstraindoAFonteDeDados\\musicas.csv", FileMode.Open, FileAccess.Read);
using var stream = new StreamReader(arquivo);

OperacoesDeVerificacaoDeExistencia(stream);

void OperacoesDeVerificacaoDeExistencia(StreamReader stream)
{
    var musicas = ObterArquivo(stream).ToList();

    var artistas = musicas
        .GroupBy(m => m.Artista)
        .Where(g => g.Any(m => m.Duracao > 400));
    Console.WriteLine("Artistas com pelo menos uma música acima de 400 segundos:");
    foreach (var artista in artistas)
    {
        Console.WriteLine($"\t - {artista.Key}");
    }

    var reggae = musicas
        .GroupBy(m => m.Artista)
        .Where(g => g.Any(m => m.Genero.Contains("Reggae")));
    Console.WriteLine("Artistas com pelo menos uma música de reggae:");
    foreach (var artista in reggae)
    {
        Console.WriteLine($"\t - {artista.Key}");
    }

}
void ArtistaComMaioQtde(StreamReader stream)
{
    var artistaComMaiorQtdeMusicas = ObterArquivo(stream)
    .GroupBy(m => m.Artista)
    .Select(g => new { Artista = g.Key, Musica = g, Total = g.Count() })
    .MaxBy(a => a.Total);

    if (artistaComMaiorQtdeMusicas is not null)
    {
        Console.WriteLine($"O artista com mais músicas na coleção é {artistaComMaiorQtdeMusicas.Artista} com {artistaComMaiorQtdeMusicas.Total} músicas.");
    }
    ;
}
void OperacoesDeAgrupamento(StreamReader stream)
{
    var artistas = ObterArquivo(stream)
               .GroupBy(m => m.Artista);
    Console.WriteLine("Exibindo quantas músicas cada artista tem:");
    foreach (var artista in artistas)
    {
        Console.WriteLine($"{artista.Key}: {artista.Count()} músicas");
        foreach (var musica in artista)
        {
            Console.WriteLine($"\t - {musica.Titulo} ({musica.Duracao} segundos)");
        }
    }
}
void EstatiscasDaMusica(StreamReader stream)
{
    var musica = ObterArquivo(stream).ToList();
    Console.WriteLine($"\n Existem {musica.Count()} musicas na colecao");
    Console.WriteLine($"\n Existem {musica.Count(m => m.Duracao > 600)} musicas com mais de 10 minutos na colecao");
    Console.WriteLine($"\n A musica com menor duracao tem {musica.Min(m => m.Duracao)} segundos");
    Console.WriteLine($"\n A musica com maior duracao tem {musica.Max(m => m.Duracao)} segundos");
    Console.WriteLine($"\n A media de duracao das musicas desta colecao e de {musica.Average(m => m.Duracao)} segundos");
    Console.WriteLine($"\n Uma pessoa vai levar {(musica.Sum(m => m.Duracao) / (3600 * 24))} dias para ouvir todas as musicas");
}

void OperacoesComProjecoes2(StreamReader stream)
{
    var generos = ObterArquivo(stream)
              .SelectMany(m => m.Genero) // projecao ou transformacao 
              .Distinct() // filtragem
              .OrderBy(g => g);

    foreach (var genero in generos)
    {
        Console.WriteLine(genero);
    }
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