// Preuzeto sa vježbi: Termin 3 - Kolekcije i serijalizacija, primjer CRUDExample (Console/ConsoleViewUtils.cs)
// Dodato: unos teksta, decimalnog broja i datuma
using System.Globalization;

namespace SajamKnjiga.Cli.Views
{
    static class ConsoleViewUtils
    {
        public static int SafeInputInt()
        {
            int input;

            string rawInput = Console.ReadLine() ?? string.Empty;

            while (!int.TryParse(rawInput, out input))
            {
                Console.Write("Neispravan broj, pokušajte ponovo: ");
                rawInput = Console.ReadLine() ?? string.Empty;
            }

            return input;
        }

        public static decimal SafeInputDecimal()
        {
            decimal input;

            string rawInput = (Console.ReadLine() ?? string.Empty).Replace(',', '.');

            while (!decimal.TryParse(rawInput, NumberStyles.Number, CultureInfo.InvariantCulture, out input))
            {
                Console.Write("Neispravan broj, pokušajte ponovo: ");
                rawInput = (Console.ReadLine() ?? string.Empty).Replace(',', '.');
            }

            return input;
        }

        public static DateTime SafeInputDate()
        {
            DateTime input;

            string rawInput = Console.ReadLine() ?? string.Empty;

            while (!DateTime.TryParseExact(rawInput, "dd.MM.yyyy", CultureInfo.InvariantCulture,
                                           DateTimeStyles.None, out input))
            {
                Console.Write("Neispravan datum, unesite ponovo u ispravnom formatu (dd.MM.yyyy): ");
                rawInput = Console.ReadLine() ?? string.Empty;
            }

            return input;
        }

        public static string SafeInputString()
        {
            string input = Console.ReadLine() ?? string.Empty;

            while (string.IsNullOrWhiteSpace(input) || input.Contains('|'))
            {
                Console.Write("Unos ne smije biti prazan niti sadržati znak '|', pokušajte ponovo: ");
                input = Console.ReadLine() ?? string.Empty;
            }

            return input.Trim();
        }
    }
}