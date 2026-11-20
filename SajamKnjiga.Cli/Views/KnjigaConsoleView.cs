using SajamKnjiga.Core.Models;
using SajamKnjiga.Core.DAO;

namespace SajamKnjiga.Cli.Views
{
    class KnjigaConsoleView
    {
        private readonly KnjigaDAO _knjigaDAO;
        private readonly IzdavacDAO _izdavacDAO;
        private readonly KupovinaDAO _kupvinaDAO;
        private readonly AutorKnjigaDAO _autorKnjigaDAO;

        public KnjigaConsoleView(KnjigaDAO knjigaDAO, IzdavacDAO izdavacDAO, KupovinaDAO kupvinaDAO, AutorKnjigaDAO autorKnjigaDAO)
        {
            _knjigaDAO = knjigaDAO;
            _izdavacDAO = izdavacDAO;
            _kupvinaDAO = kupvinaDAO;
            _autorKnjigaDAO = autorKnjigaDAO;
        }

        public void RunMenu()
        {
            while (true)
            {
                ShowMenu();
                string userInput = Console.ReadLine() ?? "0";

                if (userInput == "0") break;
                HandleMenuInput(userInput);
            }
        }

        private void ShowMenu()
        {
            Console.WriteLine("\n----- KNJIGE -----");
            Console.WriteLine("Izaberite opciju: ");
            Console.WriteLine("1: Prikaz svih knjiga");
            Console.WriteLine("2: Dodavanje knjige");
            Console.WriteLine("3: Izmjena knjige");
            Console.WriteLine("4: Brisanje knjige");
            Console.WriteLine("0: Nazad");
        }

        private void HandleMenuInput(string input)
        {
            switch (input)
            {
                case "1":
                    ShowAllKnjige();
                    break;
                case "2":
                    AddKnjiga();
                    break;
                case "3":
                    UpdateKnjiga();
                    break;
                case "4":
                    RemoveKnjiga();
                    break;
                default:
                    Console.WriteLine("Nepostojeća opcija.");
                    break;
            }
        }

        private void ShowAllKnjige()
        {
            List<Knjiga> knjige = _knjigaDAO.GetAllKnjige();

            if(knjige.Count == 0)
            {
                Console.WriteLine("Nema unijetih knjiga.");
                return;
            }

            Console.WriteLine("\nID | ISBN, Naziv, Žanr, Godina izdanja, Cijena, Broj strana, Izdavač ID");

            foreach(Knjiga knjiga in knjige)
            {
                Console.WriteLine(knjiga);
            }
        }

        private void AddKnjiga()
        {
            if (!PostojeIzdavaci()) return;

            Knjiga knjiga = InputKnjiga();
            _knjigaDAO.AddKnjiga(knjiga);
            Console.WriteLine($"Knjiga je dodata (ID {knjiga.Id}).");
        }

        private Knjiga InputKnjiga()
        {
            Console.Write("ISBN: ");
            string isbn = ConsoleViewUtils.SafeInputString();

            Console.Write("Naziv: ");
            string naziv = ConsoleViewUtils.SafeInputString();

            Console.Write("Žanr: ");
            string zanr = ConsoleViewUtils.SafeInputString();

            int godinaIzdanja = InputGodinaIzdanja();

            decimal cijena = InputCijena();

            int brojStrana = InputBrojStrana();

            int izdavacId = InputIzdavacId();

            return new Knjiga(isbn, naziv, zanr, godinaIzdanja, cijena, brojStrana, izdavacId);
          
        }

        private int InputGodinaIzdanja()
        {
            int trenutnaGodina = DateTime.Now.Year;

            Console.Write("Godina izdanja: ");
            int godina = ConsoleViewUtils.SafeInputInt();

            while(godina < 1 || godina > trenutnaGodina)
            {
                Console.Write($"Godina mora biti između 1 i {trenutnaGodina}, poušajte ponovo: ");
                godina = ConsoleViewUtils.SafeInputInt();
            }

            return godina;
        }

        private decimal InputCijena()
        {
            Console.Write("Cijena: ");
            decimal cijena = ConsoleViewUtils.SafeInputDecimal();

            while(cijena < 0)
            {
                Console.Write("Cijena ne može biti negativna, pokušajte ponovo: ");
                cijena = ConsoleViewUtils.SafeInputDecimal();
            }

            return cijena;
        }

        private int InputBrojStrana()
        {
            Console.Write("Broj strana: ");
            int brStrana = ConsoleViewUtils.SafeInputInt();

            while(brStrana <= 0)
            {
                Console.Write("Broj strana mora biti veći od 0, pokušajte ponovo: ");
                brStrana = ConsoleViewUtils.SafeInputInt();
            }

            return brStrana;
        }

        private int InputIzdavacId()
        {
            List<Izdavac> izdavaci = _izdavacDAO.GetAllIzdavaci();

            Console.WriteLine("Dostupni izdavači: ");
           
            foreach(Izdavac izdavac in izdavaci)
            {
                Console.WriteLine(izdavac);
            }

            Console.Write("ID izdavača: ");
            int izdavacId = ConsoleViewUtils.SafeInputInt();

            while(!izdavaci.Exists(i => i.Id == izdavacId))
            {
                Console.Write("Izdavač sa tim ID-em ne postoji, pokušajte ponovo: ");
                izdavacId = ConsoleViewUtils.SafeInputInt();
            }

            return izdavacId;
        }
        private bool PostojeIzdavaci()
        {
            if(_izdavacDAO.GetAllIzdavaci().Count == 0)
            {
                Console.WriteLine("Nema unijetih izdavača. Prvo dodajte izdavače u meniju za izdavače.");
                return false;
            }

            return true;
        }

        private void UpdateKnjiga()
        {
            if (!PostojeIzdavaci()) return;

            int id = InputId();
            Knjiga knjiga = InputKnjiga();
            knjiga.Id = id;

            Knjiga? updatedKnjiga = _knjigaDAO.UpdateKnjiga(knjiga);

            if(updatedKnjiga == null)
            {
                Console.WriteLine("Knjiga nije pronađena.");
                return;
            }

            Console.WriteLine("Knjiga je izmijenjena.");
        }

        private int InputId()
        {
            Console.Write("Unesi ID knjige: ");
            return ConsoleViewUtils.SafeInputInt();
        }

        private void RemoveKnjiga()
        {
            int id = InputId();

            Knjiga? removedKnjiga = _knjigaDAO.RemoveKnjiga(id);

            if(removedKnjiga == null)
            {
                Console.WriteLine("Knjiga nije pronađena.");
                return;
            }

            if(_kupvinaDAO.GetAllKupovine().Exists(k => k.KnjigaId == id))
            {
                Console.WriteLine("Knjiga se ne može obrisati jer postoje njene kupovine.");
                return;
            }

            _autorKnjigaDAO.RemoveAllByKnjiga(id);

            Console.WriteLine($"Knjiga je obrisana: {removedKnjiga.Naziv}");
        }
    }

}
