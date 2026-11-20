using System.Text;
using SajamKnjiga.Cli.Views;
using SajamKnjiga.Core.DAO;
using static System.Net.Mime.MediaTypeNames;


using SajamKnjiga.Core.Models;

// Omogućava prikaz slova č, ć, š, ž, đ
Console.OutputEncoding = Encoding.UTF8;
Console.InputEncoding = Encoding.UTF8;

AdresaDAO adresaDAO = new AdresaDAO();
AutorDAO autorDAO = new AutorDAO();
KnjigaDAO knjigaDAO = new KnjigaDAO();
IzdavacDAO izdavacDAO = new IzdavacDAO();
AutorKnjigaDAO autorKnjigaDAO = new AutorKnjigaDAO();
PosjetilacDAO posjetilacDAO = new PosjetilacDAO();
KupovinaDAO kupovinaDAO = new KupovinaDAO();

AdresaConsoleView adresaView =
    new AdresaConsoleView(adresaDAO, autorDAO, posjetilacDAO);
AutorConsoleView autorView =
    new AutorConsoleView(autorDAO, adresaDAO, izdavacDAO, autorKnjigaDAO);
KnjigaConsoleView knjigaView =
    new KnjigaConsoleView(knjigaDAO, izdavacDAO, kupovinaDAO, autorKnjigaDAO);
PosjetilacConsoleView posjetilacView =
    new PosjetilacConsoleView(posjetilacDAO, adresaDAO);

while (true)
{
    Console.WriteLine("\n----- SAJAM KNJIGA -----");
    Console.WriteLine("Izaberite opciju: ");
    Console.WriteLine("1: Adrese");
    Console.WriteLine("2: Autori");
    Console.WriteLine("3: Knjige");
    Console.WriteLine("4: Posjetioci");
    Console.WriteLine("0: Izlaz");

    string izbor = Console.ReadLine() ?? "0";

    if (izbor == "0") break;

    switch (izbor)
    {
        case "1":
            adresaView.RunMenu();
            break;
        case "2":
            autorView.RunMenu();
            break;
        case "3":
            knjigaView.RunMenu();
            break;
        case "4":
            posjetilacView.RunMenu();
            break;
        default:
            Console.WriteLine("Nepostojeća opcija.");
            break;
    }
}