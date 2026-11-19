using System;
using System.Collections.Generic;
using System.Text;

using SajamKnjiga.Core.Models;
using SajamKnjiga.Core.Storage;

namespace SajamKnjiga.Core.DAO
{
    public class PosjetilacDAO
    {
        private readonly List<Posjetilac> _posjetioci;
        private readonly Storage<Posjetilac> _storage;

        public PosjetilacDAO()
        {
            _storage = new Storage<Posjetilac>("posjetioci.txt");
            _posjetioci = _storage.Load();
        }

        private int GenerateId()
        {
            if (_posjetioci.Count == 0) return 0;
            return _posjetioci[^1].Id + 1;
        }

        public Posjetilac AddPosjetilac(Posjetilac posjetilac)
        {
            posjetilac.Id = GenerateId();
            _posjetioci.Add(posjetilac);
            _storage.Save(_posjetioci);
            return posjetilac;
        }

        public Posjetilac? UpdatePosjetilac(Posjetilac posjetilac)
        {
            Posjetilac? postojeci = GetPosjetilacById(posjetilac.Id);
            if (postojeci is null) return null;

            postojeci.Ime = posjetilac.Ime;
            postojeci.Prezime = posjetilac.Prezime;
            postojeci.DatumRodjenja = posjetilac.DatumRodjenja;
            postojeci.AdresaId = posjetilac.AdresaId;
            postojeci.AdresaObj = posjetilac.AdresaObj;
            postojeci.KontaktTelefon = posjetilac.KontaktTelefon;
            postojeci.Email = posjetilac.Email;
            postojeci.BrojClanskeKarte = posjetilac.BrojClanskeKarte;
            postojeci.TrenutnaGodinaClanstva = posjetilac.TrenutnaGodinaClanstva;
            postojeci.Status = posjetilac.Status;
            postojeci.ProsecnaOcenaRecenzija = posjetilac.ProsecnaOcenaRecenzija;

            _storage.Save(_posjetioci);
            return postojeci;
        }

        public Posjetilac? RemovePosjetilac(int id)
        {
            Posjetilac? posjetilac = GetPosjetilacById(id);
            if (posjetilac is null) return null;

            _posjetioci.Remove(posjetilac);
            _storage.Save(_posjetioci);
            return posjetilac;
        }

        private Posjetilac? GetPosjetilacById(int id)
        {
            return _posjetioci.Find(p => p.Id == id);
        }

        public List<Posjetilac> GetAllPosjetioci()
        {
            return _posjetioci;
        }
    }
}
