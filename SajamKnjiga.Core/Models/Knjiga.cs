using System;
using System.Collections.Generic;
using System.Text;

namespace SajamKnjiga.Core.Models
{
    public class Knjiga
    {
        public int Id { get; set; }
        public string Isbn { get; set; } = string.Empty;
        public string Naziv { get; set; } = string.Empty;
        public string Zanr {  get; set; } = string.Empty;
        public int GodinaIzdanja { get; set; }
        public decimal Cena {  get; set; }
        public int BrojStrana { get; set; }
        public List<int> AutoriIds = new List<int>();
        public int? IzdavacId { get; set; }

        public List<int> KupciIds { get; set; } = new List<int>();
        public List<int> ListaZeljaIds { get; set; } = new List<int>();

        public Knjiga() { }

        public Knjiga(int id, string isbn, string naziv, string zanr, int godinaIzdanja, decimal cena, int brojStrana)
        {
            Id = id;
            Isbn = isbn;
            Naziv = naziv;
            Zanr = zanr;
            GodinaIzdanja = godinaIzdanja;
            Cena = cena;
            BrojStrana = brojStrana;
        }

        public override string ToString()
        {
            return $"{Id} | {Isbn} | {Naziv} | {Zanr} | {GodinaIzdanja} | {Cena:0.00} din | {BrojStrana} str.";
        }
    }
}
