using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using SajamKnjiga.Core.Serialization;

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
        public List<Autor> Autori { get; set; } = new List<Autor>();
        public int? IzdavacId { get; set; }
        public Izdavac? Izdavac { get; set; }

        public List<Posjetilac> Kupci { get; set; } = new List<Posjetilac>();
        public List<Posjetilac> ListaZeljaIds { get; set; } = new List<Posjetilac>();

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

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Isbn,
                Naziv,
                Zanr,
                GodinaIzdanja.ToString(CultureInfo.InvariantCulture),
                Cena.ToString(),
                BrojStrana.ToString(),
                IzdavacId?.ToString()  ?? string.Empty
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Isbn = values[1];
            Naziv = values[2];
            Zanr = values[3];
            GodinaIzdanja = int.Parse(values[4]);
            Cena = decimal.Parse(values[5], CultureInfo.InvariantCulture);
            BrojStrana = int.Parse(values[6]);
            IzdavacId = string.IsNullOrEmpty(values[7]) ? null : int.Parse(values[7]);
        }
    }
}
