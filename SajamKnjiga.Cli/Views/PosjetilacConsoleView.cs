using System;
using System.Collections.Generic;
using System.Text;

using System.Globalization;

using SajamKnjiga.Core.DAO;
using SajamKnjiga.Core.Models;

namespace SajamKnjiga.Cli.Views
{
    class PosjetilacConsoleView
    {
        private readonly PosjetilacDAO _posjetilacDAO;
        private readonly AdresaDAO _adresaDAO;

        public PosjetilacConsoleView(
            PosjetilacDAO posjetilacDAO,
            AdresaDAO adresaDAO)
        {
            _posjetilacDAO = posjetilacDAO;
            _adresaDAO = adresaDAO;
        }

        public void RunMenu()
        {
            while (true)
            {
                ShowMenu();
                string izbor = Console.ReadLine() ?? "0";

                if (izbor == "0") break;
                HandleMenuInput(izbor);
            }
        }

        private void ShowMenu()
        {
            Console.WriteLine("\n----- POSJETIOCI -----");
            Console.WriteLine("1: Prikaz svih posjetilaca");
            Console.WriteLine("2: Dodavanje posjetioca");
            Console.WriteLine("3: Izmjena posjetioca");
            Console.WriteLine("4: Brisanje posjetioca");
            Console.WriteLine("0: Nazad");
        }

        private void HandleMenuInput(string izbor)
        {
            switch (izbor)
            {
                case "1":
                    ShowAllPosjetioci();
                    break;
                // Dodajemo slučajeve 2–4 u sljedećem koraku.
                case "2":
                    AddPosjetilac();
                    break;
                case "3":
                    UpdatePosjetilac();
                    break;
                case "4":
                    RemovePosjetilac();
                    break;

                default:
                    Console.WriteLine("Nepostojeća opcija.");
                    break;
            }
        }

        private void ShowAllPosjetioci()
        {
            List<Posjetilac> posjetioci =
                _posjetilacDAO.GetAllPosjetioci();

            if (posjetioci.Count == 0)
            {
                Console.WriteLine("Nema unijetih posjetilaca.");
                return;
            }

            foreach (Posjetilac posjetilac in posjetioci)
            {
                Console.WriteLine(posjetilac);
            }
        }

        private void AddPosjetilac()
        {
            List<Adresa> adrese = _adresaDAO.GetAllAdrese();

            if (adrese.Count == 0)
            {
                Console.WriteLine("Nema unijetih adresa. Prvo dodajte adresu u meniju za adrese.");
                return;
            }

            Posjetilac posjetilac = InputPosjetilac(adrese);
            _posjetilacDAO.AddPosjetilac(posjetilac);

            Console.WriteLine($"Posjetilac je dodat (ID {posjetilac.Id}).");
        }

        private Posjetilac InputPosjetilac(List<Adresa> adrese)
        {
            Console.Write("Ime: ");
            string ime = ConsoleViewUtils.SafeInputString();

            Console.Write("Prezime: ");
            string prezime = ConsoleViewUtils.SafeInputString();

            Console.Write("Datum rođenja (dd.MM.yyyy): ");
            DateTime datumRodjenja = ConsoleViewUtils.SafeInputDate();

            Console.WriteLine("Dostupne adrese:");
            foreach (Adresa adresa in adrese)
            {
                Console.WriteLine(adresa);
            }

            Console.Write("ID adrese: ");
            int adresaId = ConsoleViewUtils.SafeInputInt();

            while (!adrese.Exists(a => a.Id == adresaId))
            {
                Console.Write("Adresa sa tim ID-em ne postoji. Pokušajte ponovo: ");
                adresaId = ConsoleViewUtils.SafeInputInt();
            }

            Adresa izabranaAdresa = adrese.Find(a => a.Id == adresaId)!;

            Console.Write("Kontakt telefon: ");
            string telefon = ConsoleViewUtils.SafeInputString();

            Console.Write("E-mail: ");
            string email = ConsoleViewUtils.SafeInputString();

            Console.Write("Broj članske karte: ");
            string brojKarte = ConsoleViewUtils.SafeInputString();

            Console.Write("Trenutna godina članstva: ");
            int godinaClanstva = ConsoleViewUtils.SafeInputInt();

            Console.WriteLine("Status: 0 — Redovan, 1 — VIP");
            int statusIzbor = ConsoleViewUtils.SafeInputInt();
            while (statusIzbor != 0 && statusIzbor != 1)
            {
                Console.Write("Unesite 0 za Redovan ili 1 za VIP: ");
                statusIzbor = ConsoleViewUtils.SafeInputInt();
            }

            StatusPosjetioca status = (StatusPosjetioca)statusIzbor;

            Console.Write("Prosječna ocjena recenzija (0–5): ");
            double ocjena;
            while (!double.TryParse(
                       Console.ReadLine(),
                       NumberStyles.Number,
                       CultureInfo.CurrentCulture,
                       out ocjena)
                   || ocjena < 0
                   || ocjena > 5)
            {
                Console.Write("Unesite broj od 0 do 5: ");
            }

            return new Posjetilac(
                ime,
                prezime,
                datumRodjenja,
                izabranaAdresa,
                telefon,
                email,
                brojKarte,
                godinaClanstva,
                status,
                ocjena,
                new List<Kupovina>(),
                new List<Knjiga>());
        }

        private int InputId()
        {
            Console.Write("Unesite ID posjetioca: ");
            return ConsoleViewUtils.SafeInputInt();
        }

        private void UpdatePosjetilac()
        {
            List<Posjetilac> posjetioci = _posjetilacDAO.GetAllPosjetioci();
            int id = InputId();

            if (!posjetioci.Exists(p => p.Id == id))
            {
                Console.WriteLine("Posjetilac nije pronađen.");
                return;
            }

            List<Adresa> adrese = _adresaDAO.GetAllAdrese();
            if (adrese.Count == 0)
            {
                Console.WriteLine("Nema unijetih adresa.");
                return;
            }

            Posjetilac posjetilac = InputPosjetilac(adrese);
            posjetilac.Id = id;

            if (_posjetilacDAO.UpdatePosjetilac(posjetilac) is null)
            {
                Console.WriteLine("Posjetilac nije pronađen.");
                return;
            }

            Console.WriteLine("Posjetilac je izmijenjen.");
        }

        private void RemovePosjetilac()
        {
            int id = InputId();
            Posjetilac? obrisani = _posjetilacDAO.RemovePosjetilac(id);

            if (obrisani is null)
            {
                Console.WriteLine("Posjetilac nije pronađen.");
                return;
            }

            Console.WriteLine($"Obrisan je posjetilac: {obrisani.Ime} {obrisani.Prezime}");
        }
    }
}