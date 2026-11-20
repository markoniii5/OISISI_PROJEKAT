# Sajam knjiga

Projekat iz predmeta **Osnovi informacionih sistema i softverskog inženjerstva**, školska 2026/2027.

Informacioni sistem uprave sajma knjiga – aplikacija namijenjena referentu sajma.
Aplikacija je pisana u jeziku C# (.NET 10) i sastoji se od konzolnog i grafičkog (WPF) korisničkog interfejsa
koji koriste zajednički Core sloj. U ovoj fazi projekta aplikacija radi kroz konzolu.

## Pokretanje

1. Otvoriti `SajamKnjiga.slnx` u Visual Studio 2026.
2. Za pokretanje aplikacije u konzoli: desni klik na **SajamKnjiga.Cli** → **Set as Startup Project**, zatim **F5**.

## Podaci

Podaci se čuvaju u folderu `Data` u korijenu repozitorijuma (pravi se automatski pri prvom pokretanju).
Svaki tip entiteta ima svoj fajl, a vrijednosti u jednom redu su odvojene znakom `|`.

## Podjela posla

### Student 1 – Marko Nikolić

- Modeli: `Posjetilac`, `Kupovina`, `Izdavac`
- Serijalizacija i čuvanje podataka: CSV serijalizacija modela `Posjetilac`, `Kupovina` i `Izdavac`
- DAO : `IzdavacDAO`, `PosjetilacDAO`, `KupovinaDAO`
- Konzola: `PosetilacConsoleView`, `IzdavacConsoleView`, `KupovinaConsoleView`
- Glavni meni : `Program.cs`

### Student 2 – Nikola Marčeta

- Modeli: `Adresa`, `Autor`, `Knjiga`, `AutorKnjiga`
- Serijalizacija i čuvanje podataka: `ISerializable`, `Serializer`, `Storage`
- DAO: `AdresaDAO`, `AutorDAO`, `KnjigaDAO`, `AutorKnjigaDAO`
- Konzola: `ConsoleViewUtils`, `AdresaConsoleView`, `AutorConsoleView`, `KnjigaConsoleView`
- Glavni meni (`Program.cs`)