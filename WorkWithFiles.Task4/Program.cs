
using System.Runtime.Serialization.Formatters.Binary;

namespace FinalTask;
internal class Program
{
    private static void Main(string[] args)
    {
        string path = "Data/Students.dat";

        var students = DeserializeStudents(path);


    }
    
    
    public static Student[]? DeserializeStudents(string path)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        Student[] students;
        formatter.Binder = new AllowAllAssemblyVersionsDeserializationBinder();
       
        // десериализация
        using (var fs = new FileStream(Path.Combine(Environment.CurrentDirectory, path), FileMode.Open))
        {
            students = formatter.Deserialize(fs) as Student[];
        }

        return students;
    }
}