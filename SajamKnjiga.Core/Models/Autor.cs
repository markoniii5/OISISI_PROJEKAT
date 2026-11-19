using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using SajamKnjiga.Core.Serialization;

namespace SajamKnjiga.Core.Models
{
    public class Autor : ISerializable
    {
        public int Id { get; set; }
        public string Ime { get; set; } = string.Empty;
        public string Prezime { get; set; } = string.Empty;
        public DateTime DatumRodjenja { get; set; }
        public int AdresaId { get; set; }
        public Adresa? Adresa { get; set; }
        public string Telefon { get; set; } = string.Empty;
        public string Email {  get; set; } = string.Empty;
        public string BrojLicneKarte { get; set; } = string.Empty;
        public int GodineIskustva { get; set; }
        public List<Knjiga> Knjige { get; set; } = new List<Knjiga>();

        public Autor() { }

        public Autor(string ime, string prezime, DateTime datumRodjenja, int adresaId, string telefon, string email, string brojLicneKarte, int godineIskustva)
        {
            Ime = ime;
            Prezime = prezime;
            DatumRodjenja = datumRodjenja;
            AdresaId = adresaId;
            Telefon = telefon;
            Email = email;
            BrojLicneKarte = brojLicneKarte;
            GodineIskustva = godineIskustva;
        }

        public override string ToString()
        {
            return $"{Id} | {Ime} {Prezime}, {DatumRodjenja:dd.MM.yyyy}, {AdresaId}, {Telefon}, {Email}, {BrojLicneKarte}, {GodineIskustva}";
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Ime,
                Prezime,
                DatumRodjenja.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture),
                AdresaId.ToString(),
                Telefon,
                Email,
                BrojLicneKarte,
                GodineIskustva.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Ime = values[1];
            Prezime = values[2];
            DatumRodjenja = DateTime.ParseExact(values[3], "dd.MM.yyyy", CultureInfo.InvariantCulture);
            AdresaId = int.Parse(values[4]);
            Telefon = values[5];
            Email = values[6];
            BrojLicneKarte = values[7];
            GodineIskustva = int.Parse(values[8]);
        }
    }
}

