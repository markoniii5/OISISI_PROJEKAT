using System;
using System.Collections.Generic;
using System.Text;
using SajamKnjiga.Core.Models;
using SajamKnjiga.Core.Storage;

namespace SajamKnjiga.Core.DAO
{
    public class IzdavacDAO
    {
        private readonly List<Izdavac> _izdavaci;
        private readonly Storage<Izdavac> _storage;


        public IzdavacDAO()
        {
            _storage = new Storage<Izdavac>("izdavaci.txt");
            _izdavaci = _storage.Load();
        }
        private int GenerateId()
        {
            if (_izdavaci.Count == 0) return 0;
            return _izdavaci[^1].Id + 1;
        }

        public Izdavac AddIzdavac(Izdavac izdavac)
        {
            izdavac.Id = GenerateId();
            _izdavaci.Add(izdavac);
            _storage.Save(_izdavaci);
            return izdavac;
        }

        public Izdavac? UpdateIzdavac(Izdavac izdavac)
        {
            Izdavac? postojeci = GetIzdavacById(izdavac.Id);
            if (postojeci is null) return null;

            postojeci.Sifra = izdavac.Sifra;
            postojeci.Naziv = izdavac.Naziv;
            postojeci.SefIzdavacaId = izdavac.SefIzdavacaId;
            postojeci.SefIzdavaca = izdavac.SefIzdavaca;

            _storage.Save(_izdavaci);
            return postojeci;
        }

        public Izdavac? RemoveIzdavac(int id)
        {
            Izdavac? izdavac = GetIzdavacById(id);
            if (izdavac is null) return null;

            _izdavaci.Remove(izdavac);
            _storage.Save(_izdavaci);
            return izdavac;
        }

        private Izdavac? GetIzdavacById(int id)
        {
            return _izdavaci.Find(i => i.Id == id);
        }

        public List<Izdavac> GetAllIzdavaci()
        {
            return _izdavaci;
        }
    }
}
