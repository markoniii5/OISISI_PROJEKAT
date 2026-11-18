// Preuzeto sa vježbi: Termin 3 - Kolekcije i serijalizacija, primjer CRUDExample (Storage/Storage.cs)
// Izmjenjeno: putanja se pravi pomoću Path.DirectorySeparatorChar i pravi se Data folder ako ne postoji
using SajamKnjiga.Core.Serialization;

namespace SajamKnjiga.Core.Storage
{
    public class Storage<T> where T : ISerializable, new()
    {
        private readonly string _filePath;
        private readonly Serializer<T> _serializer = new();

        public Storage(string fileName)
        {
            char s = Path.DirectorySeparatorChar;
            string dataFolder = $"..{s}..{s}..{s}..{s}Data";
            Directory.CreateDirectory(dataFolder);
            _filePath = $"{dataFolder}{s}{fileName}";
        }

        public List<T> Load()
        {
            // ukoliko fajl ne postoji napravi novi fajl i
            // zatvori stream za pisanje u fajl
            if (!File.Exists(_filePath))
            {
                FileStream fs = File.Create(_filePath);
                fs.Close();
            }

            IEnumerable<string> lines = File.ReadLines(_filePath);
            List<T> objects = _serializer.FromCSV(lines);

            return objects;
        }

        public void Save(List<T> objects)
        {
            string serializedObjects = _serializer.ToCSV(objects);
            using (StreamWriter streamWriter = new StreamWriter(_filePath))
            {
                streamWriter.Write(serializedObjects);
            }
        }
    }
}