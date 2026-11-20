using System;
using System.Collections.Generic;
using System.Text;

using SajamKnjiga.Core.DAO;
using SajamKnjiga.Core.Models;

namespace SajamKnjiga.Cli.Views
{
    class IzdavacConsoleView
    {
        private readonly IzdavacDAO _izdavacDAO;
        private readonly AutorDAO _autorDAO;

        private readonly KnjigaDAO _knjigaDAO;

        public IzdavacConsoleView(IzdavacDAO izdavacDAO, AutorDAO autorDAO,KnjigaDAO knjigaDAO)
        {
            _izdavacDAO = izdavacDAO;
            _autorDAO = autorDAO;
            _knjigaDAO = knjigaDAO;
        }

        public void RunMenu()
        {
            while (true)
            {
                Console.WriteLine("\n----- IZDAVAČI -----");
                Console.WriteLine("1: Prikaz svih izdavača");
                Console.WriteLine("2: Dodavanje izdavača");
                Console.WriteLine("3: Izmjena izdavača");
                Console.WriteLine("4: Brisanje izdavača");
                Console.WriteLine("0: Nazad");

                string izbor = Console.ReadLine() ?? "0";
                if (izbor == "0") break;

                switch (izbor)
                {
                    case "1": ShowAll(); break;
                    case "2": Add(); break;
                    case "3": Update(); break;
                    case "4": Remove(); break;
                    default: Console.WriteLine("Nepostojeća opcija."); break;
                }
            }
        }

        private void ShowAll()
        {
            List<Izdavac> izdavaci = _izdavacDAO.GetAllIzdavaci();

            if (izdavaci.Count == 0)
            {
                Console.WriteLine("Nema unijetih izdavača.");
                return;
            }

            foreach (Izdavac izdavac in izdavaci)
            {
                Autor? sef = _autorDAO.GetAllAutori()
                    .Find(a => a.Id == izdavac.SefIzdavacaId);

                string imeSefa = sef is null
                    ? $"ID {izdavac.SefIzdavacaId}"
                    : $"{sef.Ime} {sef.Prezime}";

                Console.WriteLine(
                    $"ID: {izdavac.Id} | Šifra: {izdavac.Sifra} | " +
                    $"Naziv: {izdavac.Naziv} | Šef: {imeSefa}");
            }
        }

        private void Add()
        {
            Izdavac? izdavac = InputIzdavac();
            if (izdavac is null) return;

            _izdavacDAO.AddIzdavac(izdavac);
            Console.WriteLine($"Izdavač je dodat (ID {izdavac.Id}).");
        }

        private void Update()
        {
            int id = InputId();
            if (!_izdavacDAO.GetAllIzdavaci().Exists(i => i.Id == id))
            {
                Console.WriteLine("Izdavač nije pronađen.");
                return;
            }

            Izdavac? izmijenjen = InputIzdavac();
            if (izmijenjen is null) return;

            izmijenjen.Id = id;
            _izdavacDAO.UpdateIzdavac(izmijenjen);
            Console.WriteLine("Izdavač je izmijenjen.");
        }

        private void Remove()
        {
            int id = InputId();
            if (_knjigaDAO.GetAllKnjige().Exists(k => k.IzdavacId == id))
            {
                Console.WriteLine("Izdavač ne može biti obrisan jer ima knjige.");
                return;
            }
            Izdavac? obrisan = _izdavacDAO.RemoveIzdavac(id);

            if (obrisan is null)
            {
                Console.WriteLine("Izdavač nije pronađen.");
                return;
            }

            Console.WriteLine($"Obrisan je izdavač: {obrisan.Naziv}");
        }

        private Izdavac? InputIzdavac()
        {
            List<Autor> kandidati = _autorDAO.GetAllAutori()
                .FindAll(a => a.GodineIskustva >= 5);

            if (kandidati.Count == 0)
            {
                Console.WriteLine(
                    "Nema autora sa najmanje 5 godina iskustva. " +
                    "Prvo dodajte odgovarajućeg autora.");
                return null;
            }

            Console.Write("Šifra izdavača: ");
            int sifra = ConsoleViewUtils.SafeInputInt();

            Console.Write("Naziv: ");
            string naziv = ConsoleViewUtils.SafeInputString();

            Console.WriteLine("Autori koji mogu biti šefovi:");
            foreach (Autor autor in kandidati)
                Console.WriteLine($"{autor.Id}: {autor.Ime} {autor.Prezime}");

            Console.Write("Unesite ID šefa izdavača: ");
            int sefId = ConsoleViewUtils.SafeInputInt();
            Autor? sef = kandidati.Find(a => a.Id == sefId);

            while (sef is null)
            {
                Console.Write("Autor ne postoji ili nema 5 godina iskustva. Unesite drugi ID: ");
                sefId = ConsoleViewUtils.SafeInputInt();
                sef = kandidati.Find(a => a.Id == sefId);
            }

            return new Izdavac(
                sifra,
                naziv,
                sef,
                new List<Autor>(),
                new List<Knjiga>());
        }

        private int InputId()
        {
            Console.Write("Unesite ID izdavača: ");
            return ConsoleViewUtils.SafeInputInt();
        }
    }
}
