//Napravljeno po uzoru na vježbe: Termin 3 - Kolekcije i serijalizacija, primjer CRUDExample (DAO/VechilesDAO.cs)
using SajamKnjiga.Core.Models;
using SajamKnjiga.Core.Storage;

namespace SajamKnjiga.Core.DAO;

public class AutorKnjigaDAO
{
    private readonly List<AutorKnjiga> _autoriKnjige;
    private readonly Storage<AutorKnjiga> _storage;


    public AutorKnjigaDAO()
    {
        _storage = new Storage<AutorKnjiga>("autori_knjige.txt");
        _autoriKnjige = _storage.Load();
    }

    public AutorKnjiga? AddAutorKnjiga(AutorKnjiga autorKnjiga)
    {
        if (Postoji(autorKnjiga.AutorId, autorKnjiga.KnjigaId)) return null;

        _autoriKnjige.Add(autorKnjiga);
        _storage.Save(_autoriKnjige);
        return autorKnjiga;
    }

    public AutorKnjiga? RemoveAutorKnjiga(int autorId, int knjigaId)
    {
        AutorKnjiga? autorKnjiga = _autoriKnjige.Find(ak => ak.AutorId == autorId && ak.KnjigaId == knjigaId);
        if (autorKnjiga == null) return null;

        _autoriKnjige.Remove(autorKnjiga);
        _storage.Save(_autoriKnjige);
        return autorKnjiga;
    }
    
    public void RemoveAllByAutor(int autorId)
    {
        _autoriKnjige.RemoveAll(ak => ak.AutorId == autorId);
        _storage.Save(_autoriKnjige);
    }

    public void RemoveAllByKnjiga(int knjigaId)
    {
        _autoriKnjige.RemoveAll(ak => ak.KnjigaId == knjigaId);
        _storage.Save(_autoriKnjige);
    }

    public bool Postoji(int autordId, int knjigaId)
    {
        return _autoriKnjige.Exists(ak => ak.AutorId == autordId && ak.KnjigaId == knjigaId);
    }
    public List<int> GetAllKnjigeAutora(int autorId)
    {
        return _autoriKnjige.Where(ak => ak.AutorId == autorId).Select(ak => ak.KnjigaId).ToList();
    }

    public List<int> GetAllAutoreKnjige(int knjigaId)
    {
        return _autoriKnjige.Where(ak => ak.KnjigaId == knjigaId).Select(ak => ak.AutorId).ToList();
    }
    public List<AutorKnjiga> GetAll()
    {
        return _autoriKnjige;
    }

}
