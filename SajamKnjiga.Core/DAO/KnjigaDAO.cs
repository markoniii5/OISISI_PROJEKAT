//Napravljeno po uzoru na vježbe: Termin 3 - Kolekcije i serijalizacija, primjer CRUDExample (DAO/VechilesDAO.cs)
using SajamKnjiga.Core.Models;
using SajamKnjiga.Core.Storage;

namespace SajamKnjiga.Core.DAO;

public class KnjigaDAO
{
    private readonly List<Knjiga> _knjige;
    private readonly Storage<Knjiga> _storage;


    public KnjigaDAO()
    {
        _storage = new Storage<Knjiga>("knjige.txt");
        _knjige = _storage.Load();
    }

    private int GenerateId()
    {
        if (_knjige.Count == 0) return 0;
        return _knjige[^1].Id + 1;
    }

    public Knjiga AddKnjiga(Knjiga knjiga)
    {
        knjiga.Id = GenerateId();
        _knjige.Add(knjiga);
        _storage.Save(_knjige);
        return knjiga;
    }

    public Knjiga? UpdateKnjiga(Knjiga knjiga)
    {
        Knjiga? oldKnjiga = GetKnjigaById(knjiga.Id);
        if (oldKnjiga is null) return null;

        oldKnjiga.Isbn = knjiga.Isbn;
        oldKnjiga.Naziv = knjiga.Naziv;
        oldKnjiga.Zanr = knjiga.Zanr;
        oldKnjiga.GodinaIzdanja = knjiga.GodinaIzdanja;
        oldKnjiga.Cena = knjiga.Cena;
        oldKnjiga.BrojStrana = knjiga.BrojStrana;

        _storage.Save(_knjige);
        return oldKnjiga;
    }

    public Knjiga? RemoveKnjiga(int id)
    {
        Knjiga? knjiga = GetKnjigaById(id);
        if (knjiga == null) return null;

        _knjige.Remove(knjiga);
        _storage.Save(_knjige);
        return knjiga;
    }

    private Knjiga? GetKnjigaById(int id)
    {
        return _knjige.Find(k => k.Id == id);
    }

    public List<Knjiga> GetAllKnjige()
    {
        return _knjige;
    }

}
