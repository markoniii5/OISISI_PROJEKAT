// Preuzeto sa vježbi: Termin 3 - Kolekcije i serijalizacija, primjer CRUDExample (Storage/Serialization/Serializable.cs)
namespace SajamKnjiga.Core.Serialization
{
    public interface ISerializable
    {
        string[] ToCSV();

        void FromCSV(string[] values);
    }
}
