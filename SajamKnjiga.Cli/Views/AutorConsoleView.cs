using SajamKnjiga.Core.DAO;
using SajamKnjiga.Core.Models;

namespace SajamKnjiga.Cli.Views
{
    class AutorConsoleView
    {
        private readonly AutorDAO _autorDAO;
        private readonly AdresaDAO _adresaDAO;

        public AutorConsoleView(AutorDAO autorDAO, AdresaDAO adresaDAO)
        {
            _autorDAO = autorDAO;
            _adresaDAO = adresaDAO;
        }

        public void RunMenu()
        {
            while(true)
            {
                ShowMenu();
                string userInput = Console.ReadLine() ?? "0";

                if (userInput == "0") break;
                HandleMenuInput(userInput);
            }
        }

        private void ShowMenu()
        {
            Console.WriteLine("\n----- AUTORI -----");
            Console.WriteLine("Izaberite opciju: ");
            Console.WriteLine("1: Prikaz svih autora");
            Console.WriteLine("2: Dodavanje autora");
            Console.WriteLine("3: Izmjena autora");
            Console.WriteLine("4: Brisanje autora");
            Console.WriteLine("0: Nazad");
        }

        private void HandleMenuInput(string input)
        {
            switch (input)
            {
                case "1":
                    ShowAllAutore();
                    break;
                case "2":
                    AddAutor();
                    break;
                case "3":
                    UpdateAutor();
                    break;
                case "4":
                    RemoveAutor();
                    break;
                default:
                    Console.WriteLine("Nepostojeća opcija.");
                    break;
            }
        }

        private void ShowAllAutore()
        {
            List<Autor> autori = _autorDAO.GetAllAutori();

            if(autori.Count == 0)
            {
                Console.WriteLine("Nema unijetih autora.");
                return;
            }

            Console.WriteLine("\nID | Ime Prezime, Datum rođenja, Adresa ID, Telefon, Email, Broj lične karte, Godine iskustva");

            foreach(Autor autor in autori)
            {
                Console.WriteLine(autor);
            }
        }

        private void AddAutor()
        {
            if (!PostojeAdrese()) return;

            Autor autor = InputAutor();
            _autorDAO.AddAutor(autor);
            Console.WriteLine($"Autor je dodat (ID {autor.Id}).");
        }

        private bool PostojeAdrese()
        {
            if(_adresaDAO.GetAllAdrese().Count == 0)
            {
                Console.WriteLine("Nema unijetih adresa. Prvo dodajte adresu u meniju za adrese.");
                return false;
            }
            return true;
        }

        private Autor InputAutor()
        {
            Console.Write("Ime: ");
            string ime = ConsoleViewUtils.SafeInputString();

            Console.Write("Prezime: ");
            string prezime = ConsoleViewUtils.SafeInputString();

            Console.Write("Datum rođenja (dd.MM.yyyy): ");
            DateTime datumRodjenja = ConsoleViewUtils.SafeInputDate();

            int adresaId = InputAdresaId();

            Console.Write("Telefon: ");
            string telefon = ConsoleViewUtils.SafeInputString();

            Console.Write("E-mail: ");
            string email = ConsoleViewUtils.SafeInputString();

            Console.Write("Broj lične karte: ");
            string brLicneKarte = ConsoleViewUtils.SafeInputString();

            int godineIskustva = InputGodineIskustva();

            return new Autor(ime, prezime, datumRodjenja, adresaId, telefon, email, brLicneKarte, godineIskustva);
        }

        private int InputAdresaId()
        {
            List<Adresa> adrese = _adresaDAO.GetAllAdrese();

            Console.WriteLine("Dostupne adrese:");
            
            foreach(Adresa adresa in adrese)
            {
                Console.WriteLine(adresa);
            }

            Console.Write("ID adrese: ");
            int adresaId = ConsoleViewUtils.SafeInputInt();

            while(!adrese.Exists(a => a.Id == adresaId))
            {
                Console.Write("Adresa sa time ID-em ne postoji, pokušajte ponovo: ");
                adresaId = ConsoleViewUtils.SafeInputInt();
            }

            return adresaId;
        }

        private int InputGodineIskustva()
        {
            Console.Write("Godine iskustva: ");
            int godine = ConsoleViewUtils.SafeInputInt();

            while(godine < 0)
            {
                Console.Write("Godine iskustva ne mogu biti negativne, pokušajte ponovo: ");
                godine = ConsoleViewUtils.SafeInputInt();
            }

            return godine;
        }

        private void UpdateAutor()
        {
            if (!PostojeAdrese()) return;

            int id = InputId();
            Autor autor = InputAutor();
            autor.Id = id;

            Autor? updatedAutor = _autorDAO.UpdateAutor(autor);

            if(updatedAutor == null)
            {
                Console.WriteLine("Autor nije pronađen.");
                return;
            }

            Console.WriteLine("Autor je izmijenjen.");
        }

        private int InputId()
        {
            Console.Write("Unesite ID autora: ");
            return ConsoleViewUtils.SafeInputInt();
        }

        private void RemoveAutor()
        {
            int id = InputId();

            Autor? removedAutor = _autorDAO.RemoveAutor(id);

            if(removedAutor == null)
            {
                Console.WriteLine("Autor nije pronađen.");
                return;
            }

            Console.WriteLine($"Autor je obrisan: {removedAutor.Ime} {removedAutor.Ime}");
        }
    }
}
