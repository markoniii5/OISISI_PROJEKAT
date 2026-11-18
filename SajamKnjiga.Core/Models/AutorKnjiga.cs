// Napravljeno po uzoru na vježbe: Termin 3 - Kolekcije i serijaliziacija, primjer SerializationExample (Model/StudnetSubject.cs)
using SajamKnjiga.Core.Serialization;

namespace SajamKnjiga.Core.Models
{
    public class AutorKnjiga : ISerializable
    {
        public int AutorId { get; set; }
        public int KnjigaId { get; set; }

        public AutorKnjiga() { }
        public AutorKnjiga(int autorId, int knjigaId)
        {
            AutorId = autorId;
            KnjigaId = knjigaId;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                AutorId.ToString(),
                KnjigaId.ToString()
            };
            return csvValues;

        }

        public void FromCSV(string[] values)
        {
            AutorId = int.Parse(values[0]);
            KnjigaId = int.Parse(values[1]);
        }
    }
}
