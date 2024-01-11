using WorkWithFiles.Common;

internal class Program
{
    private static async Task Main(string[] args)
    {
        СreatingDirectoriesAndFiles directoriesAndFiles = new СreatingDirectoriesAndFiles("Каталог", "Подкаталог", "файл");
        while (true)
        {
            // Создаем подкаталоги через заданное время
            directoriesAndFiles.SubDirectoryLastCreate(2);
            // В данных подкаталогах пишем файл
            directoriesAndFiles.FilesLastCreate(20);

            // Здесь метод для поиска устаревших файлов
            long size = DirectoryExtensions.SizeOfFolder(directoriesAndFiles.DirectoryInfoName.FullName);
            if(directoriesAndFiles.deleteFolder("Каталог", 5))
            {
                var color = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Исходный размер каталога: {size} байт");
                var sizeDelete = DirectoryExtensions.SizeOfFolder(directoriesAndFiles.DirectoryInfoName.FullName);
                Console.WriteLine($"Освобождено: {size-sizeDelete} байт");
                Console.WriteLine($"Текущий размер : {sizeDelete} байт");
                Console.ForegroundColor = color;
            }
            await Task.Delay(1000);
        }
    }
}