using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using SajamKnjiga.Core.Serialization;

namespace SajamKnjiga.Core.Models
{
    public class Kupovina : ISerializable
    {
        public Posjetilac PosjetilacObj { set; get; } = null!;
        public Knjiga KnjigaObj { set; get; } = null!;
        public DateTime DatumKupovine { get; set; }
        public int Ocjena { get; set; }
        public string Komentar { get; set; } = string.Empty;
        public int Id { get; set; }
        public int PosjetilacId { get; set; }
        public int KnjigaId { get; set; }

        public Kupovina() {  }
    
        public Kupovina(Posjetilac posjetilacObj, Knjiga knjigaObj, DateTime datumKupovine, int ocjena, string komentar )
        {
            PosjetilacObj = posjetilacObj;
            KnjigaObj = knjigaObj;
            DatumKupovine = datumKupovine;
            Ocjena = ocjena;
            Komentar = komentar;
            PosjetilacId = posjetilacObj.Id;
            KnjigaId = knjigaObj.Id;
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

        public string[] ToCSV()
        {
            return new string[]
            {
        Id.ToString(CultureInfo.InvariantCulture),
        PosjetilacId.ToString(CultureInfo.InvariantCulture),
        KnjigaId.ToString(CultureInfo.InvariantCulture),
        DatumKupovine.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture),
        Ocjena.ToString(CultureInfo.InvariantCulture),
        Komentar
            };
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0], CultureInfo.InvariantCulture);
            PosjetilacId = int.Parse(values[1], CultureInfo.InvariantCulture);
            KnjigaId = int.Parse(values[2], CultureInfo.InvariantCulture);
            DatumKupovine = DateTime.ParseExact(
                values[3],
                "dd.MM.yyyy",
                CultureInfo.InvariantCulture);
            Ocjena = int.Parse(values[4], CultureInfo.InvariantCulture);
            Komentar = values[5];
        }
    }
}
