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
        public Adresa Adresa { get; set; } = new Adresa();
        public string Telefon { get; set; } = string.Empty;
        public string Email {  get; set; } = string.Empty;
        public string BrojLicneKarte { get; set; } = string.Empty;
        public int GodineIskustva { get; set; }
        public List<int> KnjigeIds { get; set; } = new List<int>();

        public Autor() { }

        public Autor(int id, string ime, string prezime, DateTime datumRodjenja, Adresa adresa, string telefon, string email, string brojLicneKarte, int godineIskustva)
        {
            Id = id;
            Ime = ime;
            Prezime = prezime;
            DatumRodjenja = datumRodjenja;
            Adresa = adresa;
            Telefon = telefon;
            Email = email;
            BrojLicneKarte = brojLicneKarte;
            GodineIskustva = godineIskustva;
        }

        public override string ToString()
        {
            return $"{Id} | {Ime} {Prezime} | LK: {BrojLicneKarte} | {DatumRodjenja:dd.MM.yyyy} | {Email}";
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Ime,
                Prezime,
                DatumRodjenja.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture),
                Adresa.Ulica,
                Adresa.Broj,
                Adresa.Grad,
                Adresa.Drzava,
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
            Adresa = new Adresa(values[4], values[5], values[6], values[7]);
            Telefon = values[8];
            Email = values[9];
            BrojLicneKarte = values[10];
            GodineIskustva = int.Parse(values[11]);
        }
    }
}

