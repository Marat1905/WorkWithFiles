
using System.Runtime.Serialization.Formatters.Binary;

namespace FinalTask;
internal class Program
{
    private static void Main(string[] args)
    {
        string path = "Data/Students.dat";

        //Если на рабочем столе отсутствует каталог Students создаем
        CheckingDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "Students"));
        // Десериализация файла
        var students = DeserializeStudents(path);

        var group=students.GroupBy(x => x.Group).ToList();

    }

    /// <summary>Метод проверки на наличие каталога. Если нет то создаем</summary>
    /// <param name="directory">Путь к каталогу</param>
    private static DirectoryInfo CheckingDirectory(string directory)
    {
        DirectoryInfo info = new DirectoryInfo(directory);
        if (!info.Exists)
            info.Create();

        return info;
    }

    private static Student[]? DeserializeStudents(string path)
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