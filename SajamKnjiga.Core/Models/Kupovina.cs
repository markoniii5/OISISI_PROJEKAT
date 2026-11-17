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


        public Kupovina() {  }
    
        public Kupovina(Posjetilac posjetilacObj, Knjiga knjigaObj, DateTime datumKupovine, int ocjena, string komentar)
        {
            PosjetilacObj = posjetilacObj;
            KnjigaObj = knjigaObj;
            DatumKupovine = datumKupovine;
            Ocjena = ocjena;
            Komentar = komentar;
        }

        public override string ToString()
        {
            return $"KUPOVINA \n" +
                   $"Posjetilac  : {PosjetilacObj} \n" +
                   $"Knjiga : {KnjigaObj} \n" +
                   $"Datum kupovine : {DatumKupovine} \n" +
                   $"Ocjena : {Ocjena} \n" +
                   $"Komentar : {Komentar} \n";
        }
    }
}
