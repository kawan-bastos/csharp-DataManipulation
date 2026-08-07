using System.Collections;

var musica1 = new Musica{Nome = "Bohemian Rhapsody", Artista = "Queen", Duracao = 180};
var musica2 = new Musica{Nome = "Stairway to Heaven", Artista = "Led Zeppelin", Duracao = 480};
var musica3 = new Musica { Nome = "Imagine", Artista = "John Lennon", Duracao = 210 };
var musica4 = new Musica { Nome = "Hotel California", Artista = "Eagles", Duracao = 390 };
var musica5 = new Musica { Nome = "Smells Like Teen Spirit", Artista = "Nirvana", Duracao = 300 };

var Rock = new Playlist { Nome = "Rock" };
Rock.Add(musica1);
Rock.Add(musica2);
Rock.Add(musica3);
Rock.Add(musica4);
Rock.Add(musica5);

ExibirPlaylist(Rock);

var musicaEncontrada = Rock.ObterPeloTitulo("Bohemian Rhapsody");
if (musicaEncontrada is not null)
{
    Console.WriteLine($"Removendo a musica: {musicaEncontrada.Nome}");
    Rock.Remove(musicaEncontrada);
} else
{
    Console.WriteLine("Musica não encontrada");
}


void ExibirPlaylist(Playlist playlist)
{
    Console.WriteLine($"Playlist: {playlist.Nome}");
    foreach (var musica in playlist)
    {
        Console.WriteLine($"Musica: {musica.Nome}, Artista: {musica.Artista}, Duração: {musica.Duracao} segundos");
    }
}
ExibirPlaylist(Rock);

var musicaAleatoria = Rock.ObterAleatoria();
if (musicaAleatoria is not null)
{
    Console.WriteLine($"Tocando musica aleatória: {musicaAleatoria.Nome}");
}
else
{
    Console.WriteLine("Playlist vazia");
}

class Musica
{
    public string Nome { get; set; }
    public string Artista { get; set; }
    public int Duracao { get; set; }
}

class Playlist : ICollection<Musica>
{
    private List<Musica> Lista = [];
    public string Nome { get; set; }

    public int Count => Lista.Count;

    public bool IsReadOnly => false;

    public void Add(Musica musica)
    {
        Lista.Add(musica);
    }

    public void Clear()
    {
        Lista.Clear();
    }

    public Musica? ObterPeloTitulo(string titulo)
    {
        foreach(var musica in Lista)
        {
            if (musica.Nome == titulo) return musica;
        }
        return null;
    }

    public Musica? ObterAleatoria()
    {
        if (Lista.Count == 0) return null;
        var random = new Random();
        int indiceAleatorio = random.Next(0, Lista.Count);
        return Lista[indiceAleatorio];
    }

    public bool Contains(Musica musica)
    {
        return Lista.Contains(musica);
    }

    public void CopyTo(Musica[] array, int arrayIndex)
    {
        Lista.CopyTo(array, arrayIndex);
    }

    public IEnumerator<Musica> GetEnumerator()
    {
        return Lista.GetEnumerator();
    }

    public bool Remove(Musica musica)
    {
        return Lista.Remove(musica);
    }   
    
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}