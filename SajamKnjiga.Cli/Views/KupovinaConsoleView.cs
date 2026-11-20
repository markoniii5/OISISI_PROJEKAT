using System;
using System.Collections.Generic;
using System.Text;

using SajamKnjiga.Core.DAO;
using SajamKnjiga.Core.Models;

namespace SajamKnjiga.Cli.Views
{
    class KupovinaConsoleView
    {
        private readonly KupovinaDAO _kupovinaDAO;
        private readonly PosjetilacDAO _posjetilacDAO;
        private readonly KnjigaDAO _knjigaDAO;

        public KupovinaConsoleView(
            KupovinaDAO kupovinaDAO,
            PosjetilacDAO posjetilacDAO,
            KnjigaDAO knjigaDAO)
        {
            _kupovinaDAO = kupovinaDAO;
            _posjetilacDAO = posjetilacDAO;
            _knjigaDAO = knjigaDAO;
        }

        public void RunMenu()
        {
            while (true)
            {
                Console.WriteLine("\n----- KUPOVINE -----");
                Console.WriteLine("1: Prikaz svih kupovina");
                Console.WriteLine("2: Dodavanje kupovine");
                Console.WriteLine("3: Izmjena kupovine");
                Console.WriteLine("4: Brisanje kupovine");
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
            List<Kupovina> kupovine = _kupovinaDAO.GetAllKupovine();

            if (kupovine.Count == 0)
            {
                Console.WriteLine("Nema unijetih kupovina.");
                return;
            }

            foreach (Kupovina kupovina in kupovine)
            {
                Posjetilac? posjetilac = _posjetilacDAO.GetAllPosjetioci()
                    .Find(p => p.Id == kupovina.PosjetilacId);
                Knjiga? knjiga = _knjigaDAO.GetAllKnjige()
                    .Find(k => k.Id == kupovina.KnjigaId);

                string imePosjetioca = posjetilac is null
                    ? $"ID {kupovina.PosjetilacId}"
                    : $"{posjetilac.Ime} {posjetilac.Prezime}";
                string nazivKnjige = knjiga is null
                    ? $"ID {kupovina.KnjigaId}"
                    : knjiga.Naziv;

                Console.WriteLine(
                    $"ID: {kupovina.Id} | Posjetilac: {imePosjetioca} | " +
                    $"Knjiga: {nazivKnjige} | Datum: {kupovina.DatumKupovine:dd.MM.yyyy} | " +
                    $"Ocjena: {kupovina.Ocjena} | Komentar: {kupovina.Komentar}");
            }
        }

        private void Add()
        {
            Kupovina? kupovina = InputKupovina();
            if (kupovina is null) return;

            _kupovinaDAO.AddKupovina(kupovina);
            Console.WriteLine($"Kupovina je dodata (ID {kupovina.Id}).");
        }

        private void Update()
        {
            int id = InputId();
            if (!_kupovinaDAO.GetAllKupovine().Exists(k => k.Id == id))
            {
                Console.WriteLine("Kupovina nije pronađena.");
                return;
            }

            Kupovina? izmijenjena = InputKupovina();
            if (izmijenjena is null) return;

            izmijenjena.Id = id;
            _kupovinaDAO.UpdateKupovina(izmijenjena);
            Console.WriteLine("Kupovina je izmijenjena.");
        }

        private void Remove()
        {
            Kupovina? obrisana = _kupovinaDAO.RemoveKupovina(InputId());

            if (obrisana is null)
            {
                Console.WriteLine("Kupovina nije pronađena.");
                return;
            }

            Console.WriteLine("Kupovina je obrisana.");
        }

        private Kupovina? InputKupovina()
        {
            List<Posjetilac> posjetioci = _posjetilacDAO.GetAllPosjetioci();
            List<Knjiga> knjige = _knjigaDAO.GetAllKnjige();

            if (posjetioci.Count == 0 || knjige.Count == 0)
            {
                Console.WriteLine("Prvo unesite bar jednog posjetioca i jednu knjigu.");
                return null;
            }

            Console.WriteLine("Posjetioci:");
            foreach (Posjetilac p in posjetioci)
                Console.WriteLine($"{p.Id}: {p.Ime} {p.Prezime}");

            Console.Write("ID posjetioca: ");
            int posjetilacId = ConsoleViewUtils.SafeInputInt();
            Posjetilac? posjetilac = posjetioci.Find(p => p.Id == posjetilacId);
            while (posjetilac is null)
            {
                Console.Write("Posjetilac nije pronađen. Ponovo unesite ID: ");
                posjetilacId = ConsoleViewUtils.SafeInputInt();
                posjetilac = posjetioci.Find(p => p.Id == posjetilacId);
            }

            Console.WriteLine("Knjige:");
            foreach (Knjiga k in knjige)
                Console.WriteLine($"{k.Id}: {k.Naziv} ({k.Isbn})");

            Console.Write("ID knjige: ");
            int knjigaId = ConsoleViewUtils.SafeInputInt();
            Knjiga? knjiga = knjige.Find(k => k.Id == knjigaId);
            while (knjiga is null)
            {
                Console.Write("Knjiga nije pronađena. Ponovo unesite ID: ");
                knjigaId = ConsoleViewUtils.SafeInputInt();
                knjiga = knjige.Find(k => k.Id == knjigaId);
            }

            Console.Write("Datum kupovine (dd.MM.yyyy): ");
            DateTime datum = ConsoleViewUtils.SafeInputDate();

            Console.Write("Ocjena (1–5): ");
            int ocjena = ConsoleViewUtils.SafeInputInt();
            while (ocjena < 1 || ocjena > 5)
            {
                Console.Write("Ocjena mora biti od 1 do 5: ");
                ocjena = ConsoleViewUtils.SafeInputInt();
            }

            Console.Write("Komentar: ");
            string komentar = ConsoleViewUtils.SafeInputString();

            return new Kupovina(posjetilac, knjiga, datum, ocjena, komentar);
        }

        private int InputId()
        {
            Console.Write("Unesite ID kupovine: ");
            return ConsoleViewUtils.SafeInputInt();
        }
    }
}
