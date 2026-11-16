using System;
using System.Collections.Generic;
using System.Text;

namespace SajamKnjiga.Core.Models
{
    public class Autor
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
    }
}

