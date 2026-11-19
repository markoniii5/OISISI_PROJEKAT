using SajamKnjiga.Core.DAO;
using SajamKnjiga.Core.Models;

namespace SajamKnjiga.Cli.Views
{
    class AdresaConsoleView
    {
        private readonly AdresaDAO _adresaDAO;

        public AdresaConsoleView(AdresaDAO adresaDAO)
        {
            _adresaDAO = adresaDAO;
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
            Console.WriteLine("\n----- ADRESE -----");
            Console.WriteLine("Izaberite opciju: ");
            Console.WriteLine("1: Prikaz svih adresa");
            Console.WriteLine("2: Dodavanje adrese");
            Console.WriteLine("3: Izmjena adrese");
            Console.WriteLine("4: Brisanje adrese");
            Console.WriteLine("0: Nazad");
        }

        private void HandleMenuInput(string input)
        {
            switch(input)
            {
                case "1":
                    ShowAllAdrese();
                    break;
                case "2":
                    AddAdresa();
                    break;
                case "3":
                    UpdateAdresa();
                    break;
                case "4":
                    RemoveAdresa();
                    break;
                default:
                    Console.WriteLine("Nepostojeća opcija.");
                    break;
            }
        }

        private void ShowAllAdrese()
        {
            List<Adresa> adrese = _adresaDAO.GetAllAdrese();

            if(adrese.Count == 0)
            {
                Console.WriteLine("Nema unijetih adresa.");
                return;
            }

            Console.WriteLine("\nID | Ulica Broj, Grad, Država");

            foreach(Adresa adresa in adrese)
            {
                Console.WriteLine(adresa);
            }
        }

        private void AddAdresa()
        {
            Adresa adresa = InputAdresa();
            _adresaDAO.AddAdresa(adresa);
            Console.WriteLine($"Adresa je dodata (ID {adresa.Id}).");
        }

        private Adresa InputAdresa()
        {
            Console.Write("Ulica: ");
            string ulica = ConsoleViewUtils.SafeInputString();

            Console.Write("Broj: ");
            string broj = ConsoleViewUtils.SafeInputString();

            Console.Write("Grad: ");
            string grad = ConsoleViewUtils.SafeInputString();

            Console.Write("Država: ");
            string država = ConsoleViewUtils.SafeInputString();

            return new Adresa(ulica, broj, grad, država);
        }

        private void UpdateAdresa()
        {
            int id = InputId();
            Adresa adresa = InputAdresa();
            adresa.Id = id;

            Adresa? updatedAdresa = _adresaDAO.UpdateAdresa(adresa);

            if (updatedAdresa == null) 
            {
                Console.WriteLine("Adresa nije pronađena.");
                return;
            }

            Console.WriteLine("Adresa je izmijenjena.");

        }

        private int InputId()
        {
            Console.Write("Unesite ID adrese: ");
            return ConsoleViewUtils.SafeInputInt();
        }

        private void RemoveAdresa()
        {
            int id = InputId();

            Adresa? removedAdresa = _adresaDAO.RemoveAdresa(id);
            
            if(removedAdresa == null)
            {
                Console.WriteLine("Adresa nije pronađena.");
                return;
            }

            Console.WriteLine($"Adresa je obrisana: {removedAdresa.Id}");
        }
    }
}
