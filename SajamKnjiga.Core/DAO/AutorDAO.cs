//Napravljeno po uzoru na vježbe: Termin 3 - Kolekcije i serijalizacija, primjer CRUDExample (DAO/VechilesDAO.cs)
using SajamKnjiga.Core.Models;
using SajamKnjiga.Core.Storage;

namespace SajamKnjiga.Core.DAO;

public class AutorDAO
{
    private readonly List<Autor> _autori;
    private readonly Storage<Autor> _storage;


    public AutorDAO()
    {
        _storage = new Storage<Autor>("autori.txt");
        _autori = _storage.Load();
    }

    private int GenerateId()
    {
        if (_autori.Count == 0) return 0;
        return _autori[^1].Id + 1;
    }

    public Autor AddAutor(Autor Autor)
    {
        Autor.Id = GenerateId();
        _autori.Add(Autor);
        _storage.Save(_autori);
        return Autor;
    }

    public Autor? UpdateAutor(Autor Autor)
    {
        Autor? oldAutor = GetAutorById(Autor.Id);
        if (oldAutor is null) return null;

        oldAutor.Ime = Autor.Ime;
        oldAutor.Prezime = Autor.Prezime;
        oldAutor.DatumRodjenja = Autor.DatumRodjenja;
        oldAutor.Adresa = Autor.Adresa;
        oldAutor.Telefon = Autor.Telefon;
        oldAutor.Email = Autor.Email;
        oldAutor.BrojLicneKarte = Autor.BrojLicneKarte;
        oldAutor.GodineIskustva = Autor.GodineIskustva;

        _storage.Save(_autori);
        return oldAutor;
    }

    public Autor? RemoveAutor(int id)
    {
        Autor? Autor = GetAutorById(id);
        if (Autor == null) return null;

        _autori.Remove(Autor);
        _storage.Save(_autori);
        return Autor;
    }

    private Autor? GetAutorById(int id)
    {
        return _autori.Find(a => a.Id == id);
    }

    public List<Autor> GetAllAutori()
    {
        return _autori;
    }
   
}
