using System;
using System.Collections.Generic;
using System.Text;

namespace SajamKnjiga.Core.Models
{
    public class Izdavac
    {
        public int Sifra { get; set; }
        public string Naziv { get; set; } = string.Empty;
        public Autor SefIzdavaca {  get; set; } = null!;
        public List<Autor> SpisakAutora { get; set; } = new();  
        public List<Knjiga> SpisakKnjiga {  get; set; } = new();
        
        public Izdavac() { }
        public Izdavac(int sifra, string naziv, Autor sefIzdavaca, List<Autor> spisakAutora, List<Knjiga> spisakKnjiga)
        {
            Sifra = sifra;
            Naziv = naziv;
            SefIzdavaca = sefIzdavaca;
            SpisakAutora = spisakAutora;
            SpisakKnjiga = spisakKnjiga;
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
    }
}
