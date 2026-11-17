using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace SajamKnjiga.Core.Models
{
    public class Posjetilac
    {
        public string Ime { get; set; } = string.Empty;
        public string Prezime { get; set; } = string.Empty;
        public DateTime DatumRodjenja { get; set; }
        public Adresa AdresaObj { get; set; } = null!;
        public string KontaktTelefon { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string BrojClanskeKarte { get; set; } = string.Empty;
        public int TrenutnaGodinaClanstva { get; set; }
        public StatusPosjetioca Status { get; set; }
        public double ProsecnaOcenaRecenzija { get; set; }
        public List<Kupovina> KupljeneKnjige { get; set; } = new();
        public List<Knjiga> Zelje { get; set; } = new();


    }
}
