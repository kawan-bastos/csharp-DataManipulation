/*
    Seja um arquivo com músicas em formato CSV (Comma Separated Values). 

    Implemente as funções abaixo:
    //     [x] Leia-o como uma coleção de músicas
    //     [ ] Filtre a coleção por artista (por ex. Coldplay, Metallica, AC/DC)
    //     [ ] Filtre a coleção por gênero (por ex. rock)
    //     [ ] Filtre a coleção por duração (por ex. maiores que 5 minutos)
    //     [ ] Ordene a coleção por artista
    //     [ ] Ordene a coleção por artista e em seguida por músicas com duração crescente
    //     [ ] Crie uma coleção de artistas e suas músicas
    //     [ ] Informe a duração média das músicas da coleção
    //     [ ] Informe a duração total das músicas da coleção
    //     [ ] Informe qual artista tem mais músicas na coleção
 
*/

using var arquivo = new FileStream("musicas.csv", FileMode.Open, FileAccess.Read);
using var stream = new StreamReader(arquivo);

var musicas = ObterArquivo(stream);
ExibirMusicas(musicas);

void ExibirMusicas(IEnumerable<Musica> musicas)
{
    var contador = 0;
    Console.WriteLine("Exibindo músicas:");
    foreach (var musica in musicas)
    {
        Console.WriteLine($"\t - {musica.Titulo}");
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
            Duracao = Convert.ToInt32(separador[2])
        };
        yield return musica;
        linha = stream.ReadLine();
    }
}

class Musica
{
    public string Titulo { get; set; }
    public string Artista { get; set; }
    public int Duracao { get; set; }
}