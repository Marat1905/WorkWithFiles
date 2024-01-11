using WorkWithFiles.Common;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.Write("Введите полный путь до каталога которому нужно посчитать размер: ");
        string? put=Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(put))
        {
            long size = DirectoryExtensions.SizeOfFolder(put);
            Console.WriteLine($"Размер каталога: {size} байт");
            Console.WriteLine($"Размер каталога: {Math.Round( (double)size/1024,2)} Kb");
            Console.WriteLine($"Размер каталога: {Math.Round((double)size / 1024 / 1024, 2)} Mb");
            Console.WriteLine($"Размер каталога: {Math.Round((double)size / 1024 / 1024 / 1024, 2)} Gb");
          
        }
        else
        {
            Console.WriteLine("Путь к каталогу не может быть пустым");
        }
        Console.ReadKey();
    }
}