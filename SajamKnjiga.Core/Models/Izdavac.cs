using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using SajamKnjiga.Core.Serialization;

namespace SajamKnjiga.Core.Models
{
    public class Izdavac : ISerializable
    {
        public int Sifra { get; set; }
        public string Naziv { get; set; } = string.Empty;
        public Autor SefIzdavaca {  get; set; } = null!;
        public List<Autor> SpisakAutora { get; set; } = new();  
        public List<Knjiga> SpisakKnjiga {  get; set; } = new();
        public int Id { get; set; }
        public int SefIzdavacaId { get; set; }
        public Izdavac() { }
        public Izdavac(int sifra, string naziv, Autor sefIzdavaca, List<Autor> spisakAutora, List<Knjiga> spisakKnjiga)
        {
            Sifra = sifra;
            Naziv = naziv;
            SefIzdavaca = sefIzdavaca;
            SpisakAutora = spisakAutora;
            SpisakKnjiga = spisakKnjiga;
            SefIzdavacaId = sefIzdavaca.Id;

        }

        public override string ToString()
        {
            return $"IZDAVAC \n" +
                   $"Sifra : {Sifra} \n" +
                   $"Naziv : {Naziv} \n" +
                   $"Sef izdavaca : {SefIzdavaca} \n" +
                   $"Spisak autora: {string.Join(", ", SpisakAutora)} \n" +
                   $"Spisak knjiga: {string.Join(", ", SpisakKnjiga)} \n";
        }

        public string[] ToCSV()
        {
            return new string[]
            {
        Id.ToString(CultureInfo.InvariantCulture),
        Sifra.ToString(CultureInfo.InvariantCulture),
        Naziv,
        SefIzdavacaId.ToString(CultureInfo.InvariantCulture)
            };
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0], CultureInfo.InvariantCulture);
            Sifra = int.Parse(values[1], CultureInfo.InvariantCulture);
            Naziv = values[2];
            SefIzdavacaId = int.Parse(values[3], CultureInfo.InvariantCulture);
        }
    }
}
