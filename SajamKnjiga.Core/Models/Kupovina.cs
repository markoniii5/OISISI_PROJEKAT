using System;
using System.Collections.Generic;
using System.Text;

namespace SajamKnjiga.Core.Models
{
    public class Kupovina
    {
        public Posjetilac PosjetilacObj { set; get; } = null!;
        public Knjiga KnjigaObj { set; get; } = null!;
        public DateTime DatumKupovine { get; set; }
        public int Ocjena { get; set; }
        public string Komentar { get; set; } = string.Empty;

    }
}
