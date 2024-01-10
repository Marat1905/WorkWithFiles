using WorkWithFiles.Common;

internal class Program
{
    private static async Task Main(string[] args)
    {
        СreatingDirectoriesAndFiles directoriesAndFiles = new СreatingDirectoriesAndFiles("Каталог", "Подкаталог","файл");
        while (true)
        {
            // Создаем подкаталоги через заданное время
            directoriesAndFiles.SubDirectoryLastCreate(2);
            // В данных подкаталогах пишем файл
            directoriesAndFiles.FilesLastCreate(20);

            // Здесь метод для поиска устаревших файлов
            directoriesAndFiles.deleteFolder("Каталог",5);
            await Task.Delay(1000);
        }
        

    }
}