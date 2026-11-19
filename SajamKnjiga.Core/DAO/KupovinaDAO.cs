using System;
using System.Collections.Generic;
using System.Text;

using SajamKnjiga.Core.Models;
using SajamKnjiga.Core.Storage;

namespace SajamKnjiga.Core.DAO
{
    public class KupovinaDAO
    {
        private readonly List<Kupovina> _kupovine;
        private readonly Storage<Kupovina> _storage;

        public KupovinaDAO()
        {
            _storage = new Storage<Kupovina>("kupovine.txt");
            _kupovine = _storage.Load();
        }

        private int GenerateId()
        {
            if (_kupovine.Count == 0) return 0;
            return _kupovine[^1].Id + 1;
        }

        public Kupovina AddKupovina(Kupovina kupovina)
        {
            kupovina.Id = GenerateId();
            _kupovine.Add(kupovina);
            _storage.Save(_kupovine);
            return kupovina;
        }

        public Kupovina? UpdateKupovina(Kupovina kupovina)
        {
            Kupovina? postojeca = GetKupovinaById(kupovina.Id);
            if (postojeca is null) return null;

            postojeca.PosjetilacId = kupovina.PosjetilacId;
            postojeca.KnjigaId = kupovina.KnjigaId;
            postojeca.DatumKupovine = kupovina.DatumKupovine;
            postojeca.Ocjena = kupovina.Ocjena;
            postojeca.Komentar = kupovina.Komentar;
            postojeca.PosjetilacObj = kupovina.PosjetilacObj;
            postojeca.KnjigaObj = kupovina.KnjigaObj;

            _storage.Save(_kupovine);
            return postojeca;
        }

        public Kupovina? RemoveKupovina(int id)
        {
            Kupovina? kupovina = GetKupovinaById(id);
            if (kupovina is null) return null;

            _kupovine.Remove(kupovina);
            _storage.Save(_kupovine);
            return kupovina;
        }

        private Kupovina? GetKupovinaById(int id)
        {
            return _kupovine.Find(k => k.Id == id);
        }

        public List<Kupovina> GetAllKupovine()
        {
            return _kupovine;
        }
    }
}
