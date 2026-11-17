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
        
    }
}
