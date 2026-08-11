using System.Collections;

var musica1 = new Musica{Titulo = "Bohemian Rhapsody", Artista = "Queen", Duracao = 180};
var musica2 = new Musica{Titulo = "Stairway to Heaven", Artista = "Led Zeppelin", Duracao = 480};
var musica3 = new Musica { Titulo = "Imagine", Artista = "John Lennon", Duracao = 210 };
var musica4 = new Musica { Titulo = "Hotel California", Artista = "Eagles", Duracao = 390 };
var musica5 = new Musica { Titulo = "Smells Like Teen Spirit", Artista = "Nirvana", Duracao = 300 };
var musica6 = new Musica { Titulo = "Something in the way", Artista = "Nirvana", Duracao = 360 };
var musica7 = new Musica { Titulo = "Come as you are", Artista = "Nirvana", Duracao = 240 };

var Rock = new Playlist { Titulo = "Rock" };
Rock.Add(musica1);
Rock.Add(musica2);
Rock.Add(musica3);
Rock.Add(musica4);
Rock.Add(musica5);
Rock.Add(musica6);
Rock.Add(musica7);

RemoverMusica(Rock, "Imagine");
ExibirPlaylist(Rock);

TocarMusicaAleatoria(Rock);

Rock.OrdenarPorDuracao();
ExibirPlaylist(Rock);

Rock.OrdenarPorArtista();
ExibirPlaylist(Rock);

Rock.OrdenarPorTitulo();
ExibirPlaylist(Rock);
void RemoverMusica(Playlist playlist, string titulo)
{
    var musicaEncontrada = playlist.ObterPeloTitulo(titulo);
    if (musicaEncontrada is not null)
    {
        Console.WriteLine($"Removendo a musica: {musicaEncontrada.Titulo}");
        playlist.Remove(musicaEncontrada);
    }
    else
    {
        Console.WriteLine("Musica não encontrada");
    }
}
void ExibirPlaylist(Playlist playlist)
{
    Console.WriteLine($"Playlist: {playlist.Titulo}");
    foreach (var musica in playlist)
    {
        Console.WriteLine($"Musica: {musica.Titulo}, Artista: {musica.Artista}, Duração: {musica.Duracao} segundos");
    }
}

void TocarMusicaAleatoria(Playlist playlist)
{
    var musicaAleatoria = playlist.ObterAleatoria();
    if (musicaAleatoria is not null)
    {
        Console.WriteLine($"Tocando musica aleatória: {musicaAleatoria.Titulo}");
    }
    else
    {
        Console.WriteLine("Playlist vazia");
    }
}

class PorTitulo : IComparer<Musica>
{
    public int Compare(Musica? x, Musica? y)
    {
        if (x is null || y is null) return 0;
        if (x is null) return 1;
        if (y is null) return -1;
        return x.Titulo.CompareTo(y.Titulo);
    }
}

class PorArtista : IComparer<Musica>
{
    public int Compare(Musica? x, Musica? y)
    {
        if (x is null || y is null) return 0;
        if (x is null) return 1;
        if (y is null) return -1;
        return x.Artista.CompareTo(y.Artista);
    }
}
class Musica : IComparable
{
    public string Titulo { get; set; }
    public string Artista { get; set; }
    public int Duracao { get; set; }

    public int CompareTo(object? other)
    {
        if (other is null) return 1;
        if (other is Musica outraMusica) return this.Duracao.CompareTo(((outraMusica).Duracao));
        return 0;
    }

    public override bool Equals(object? obj) // sobrescrevendo o método Equals para comparar músicas com base no título e artista
    {
        if (obj is null) return false;
        if (obj is Musica outraMusica) return this.Titulo.Equals(outraMusica.Titulo) && this.Artista.Equals(outraMusica.Artista);
        return false;
    }

    public override int GetHashCode() // sobrescrevendo o método GetHashCode para gerar um hash code baseado no título e artista
    {
        return this.Titulo.GetHashCode() ^ this.Artista.GetHashCode();
    }
}

class Playlist : ICollection<Musica>
{
    private HashSet<Musica> set = []; // evita que seja adicionada músicas duplicadas, porem se for criada uma nova instancia de Musica com o mesmo titulo e artista, ela será adicionada!
    private List<Musica> Lista = [];
    public string Titulo { get; set; }

    public int Count => Lista.Count;

    public bool IsReadOnly => false;

    public void Add(Musica musica)
    {
        if (set.Add(musica)) Lista.Add(musica);
    }

    public void Clear()
    {
        Lista.Clear();
    }

    public Musica? ObterPeloTitulo(string titulo)
    {
        foreach(var musica in Lista)
        {
            if (musica.Titulo == titulo) return musica;
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

    public void OrdenarPorDuracao()
    {
        Lista.Sort();
    }

    public void OrdenarPorArtista()
    {
        Lista.Sort(new PorArtista());
    }
    public void OrdenarPorTitulo()
    {
        Lista.Sort(new PorTitulo());
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