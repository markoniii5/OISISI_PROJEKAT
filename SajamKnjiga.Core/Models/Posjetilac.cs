using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using SajamKnjiga.Core.Serialization;

namespace SajamKnjiga.Core.Models
{
    public class Posjetilac : ISerializable
    {
        public string Ime { get; set; } = string.Empty;
        public string Prezime { get; set; } = string.Empty;
        public DateTime DatumRodjenja { get; set; }
        public Adresa? AdresaObj { get; set; }
        public string KontaktTelefon { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string BrojClanskeKarte { get; set; } = string.Empty;
        public int TrenutnaGodinaClanstva { get; set; }
        public StatusPosjetioca Status { get; set; }
        public double ProsecnaOcenaRecenzija { get; set; }
        public List<Kupovina> KupljeneKnjige { get; set; } = new();
        public List<Knjiga> Zelje { get; set; } = new();
        public int Id { get; set; }
        public int AdresaId { get; set; }



        public Posjetilac() { }
        public Posjetilac(string ime, string prezime, DateTime datumRodjenja, Adresa adresaObj, string kontaktTelefon, string email, string brojClanskeKarte, int trenutnaGodinaClanstva, StatusPosjetioca status, double prosecnaOcenaRecenzija, List<Kupovina> kupljeneKnjige, List<Knjiga> zelje)
        {
            Ime = ime;
            Prezime = prezime;
            DatumRodjenja = datumRodjenja;
            AdresaObj = adresaObj;
            KontaktTelefon = kontaktTelefon;
            Email = email;
            BrojClanskeKarte = brojClanskeKarte;
            TrenutnaGodinaClanstva = trenutnaGodinaClanstva;
            Status = status;
            ProsecnaOcenaRecenzija = prosecnaOcenaRecenzija;
            KupljeneKnjige = kupljeneKnjige;
            Zelje = zelje; 
            AdresaId = adresaObj.Id;

        }

        public override string ToString()
        {
            return $"POSJETILAC \n" +
          $"Ime: {Ime} \n" +
          $"Prezime: {Prezime} \n" +
          $"Datum rođenja: {DatumRodjenja} \n" +
          $"Kontakt telefon: {KontaktTelefon} \n" +
          $"Email: {Email} \n" +
          $"Broj članske karte: {BrojClanskeKarte} \n" +
          $"Trenutna godina članstva: {TrenutnaGodinaClanstva} \n" +
          $"Status: {Status} \n" +
          $"Prosječna ocjena recenzija: {ProsecnaOcenaRecenzija} \n" +
          $"Adresa: {AdresaObj} \n" +
          $"Kupljene knjige: \n {string.Join("\n", KupljeneKnjige)} \n" +
          $"Lista želja: \n {string.Join("\n", Zelje)}";
        }
    


    public string[] ToCSV()
        {
            return new string[]
            {
        Id.ToString(),
        Ime,
        Prezime,
        DatumRodjenja.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture),
        AdresaId.ToString(),
        KontaktTelefon,
        Email,
        BrojClanskeKarte,
        TrenutnaGodinaClanstva.ToString(),
        Status.ToString(),
        ProsecnaOcenaRecenzija.ToString(CultureInfo.InvariantCulture)
            };
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Ime = values[1];
            Prezime = values[2];
            DatumRodjenja = DateTime.ParseExact(
                values[3],
                "dd.MM.yyyy",
                CultureInfo.InvariantCulture);
            AdresaId = int.Parse(values[4]);
            KontaktTelefon = values[5];
            Email = values[6];
            BrojClanskeKarte = values[7];
            TrenutnaGodinaClanstva = int.Parse(values[8]);
            Status = Enum.Parse<StatusPosjetioca>(values[9]);
            ProsecnaOcenaRecenzija = double.Parse(
                values[10],
                CultureInfo.InvariantCulture);
        }

    }
}