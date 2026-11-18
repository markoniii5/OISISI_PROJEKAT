//Napravljeno po uzoru na vježbe: Termin 3 - Kolekcije i serijalizacija, primjer CRUDExample (DAO/VechilesDAO.cs)
using SajamKnjiga.Core.Models;
using SajamKnjiga.Core.Storage;

namespace SajamKnjiga.Core.DAO;

public class AdresaDAO
{
    private readonly List<Adresa> _adrese;
    private readonly Storage<Adresa> _storage;


    public AdresaDAO()
    {
        _storage = new Storage<Adresa>("adrese.txt");
        _adrese = _storage.Load();
    }

    private int GenerateId()
    {
        if (_adrese.Count == 0) return 0;
        return _adrese[^1].Id + 1;
    }

    public Adresa AddAdresa(Adresa adresa)
    {
        adresa.Id = GenerateId();
        _adrese.Add(adresa);
        _storage.Save(_adrese);
        return adresa;
    }

    public Adresa? UpdateAdresa(Adresa adresa)
    {
        Adresa? oldAdresa = GetAdresaById(adresa.Id);
        if (oldAdresa is null) return null;

        oldAdresa.Ulica = adresa.Ulica;
        oldAdresa.Broj = adresa.Broj;
        oldAdresa.Grad = adresa.Grad;
        oldAdresa.Drzava = adresa.Drzava;

        _storage.Save(_adrese);
        return oldAdresa;
    }

    public Adresa? RemoveAdresas(int id)
    {
        Adresa? adresa = GetAdresaById(id);
        if (adresa == null) return null;

        _adrese.Remove(adresa);
        _storage.Save(_adrese);
        return adresa;
    }

    private Adresa? GetAdresaById(int id)
    {
        return _adrese.Find(a => a.Id == id);
    }

    public List<Adresa> GetAllAdrese()
    {
        return _adrese;
    }

}
