using System.IO;

namespace WorkWithFiles.Common
{
    /// <summary> </summary>
    public class СreatingDirectoriesAndFiles
    {
        /// <summary>Имя основного каталога</summary>
        public string DirectoryName { get; }

        /// <summary>Начальное имя подкаталогов</summary>
        public string SubDirectoryName { get; }

        /// <summary>Начальное имя файла</summary>
        public string FileName { get; }

        /// <summary>Информация о каталоге</summary>
        public DirectoryInfo DirectoryInfoName { get; private set; }

        /// <summary>Информация о последнем файле</summary>
        public FileInfo LastFilesInfo { get; private set; }

        /// <summary>Информация о последнем подкаталоге</summary>
        public DirectoryInfo LastSubDirectoryInfo { get; private set; }

        public СreatingDirectoriesAndFiles(string directoryName, string subDirectoryName, string fileName) 
        {
            if (string.IsNullOrWhiteSpace(directoryName))
                throw new ArgumentNullException(nameof(directoryName));
            else
                DirectoryName= directoryName;

            if (string.IsNullOrWhiteSpace(subDirectoryName))
                throw new ArgumentNullException(nameof(subDirectoryName));
            else
                SubDirectoryName = subDirectoryName;

            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentNullException(nameof(fileName));
            else
                FileName = fileName;

            DirectoryInfoName=CheckingDirectory(DirectoryName);
        }


        /// <summary>Создание подкаталога</summary>
        /// <param name="minute">Интервал в минутах</param>
        /// <returns></returns>
        public DirectoryInfo SubDirectoryLastCreate(int minute)
        {
            if (LastSubDirectoryInfo == null ||
                !LastSubDirectoryInfo.Exists ||
                LastSubDirectoryInfo.CreationTime < DateTime.Now.Subtract(TimeSpan.FromMinutes(minute)))
            {
                LastSubDirectoryInfo = CreateNewSubDirectoryName(DirectoryName, SubDirectoryName);
            }

            return LastSubDirectoryInfo;
        }

        /// <summary>Создание файла в подкаталоге</summary>
        /// <param name="second">Интервал создания файлов в секундах</param>
        /// <returns></returns>
        public FileInfo FilesLastCreate(int second)
        {
            var fileInfo = FilesLast(second); // Создаем объект класса FileInfo.
            //Создаем файл и записываем в него.
            using (StreamWriter sw = fileInfo.AppendText())
            {
                sw.WriteLine(DateTime.Now);
            }

            return FilesLast(second);
        }


        /// <summary>Удаление подкаталогов и вложенных файлов</summary>
        /// <param name="folder">Путь к корневому каталогу</param>
        public  bool deleteFolder(string folder,int minute)
        {
            var result = false;
            try
            {         
                DirectoryInfo di = new DirectoryInfo(folder);
                DirectoryInfo[] diA = di.GetDirectories();
                FileInfo[] fi = di.GetFiles();
                foreach (FileInfo f in fi)
                {                
                    
                    if (f.CreationTime < DateTime.Now.Subtract(TimeSpan.FromMinutes(minute)))
                    {
                        Console.WriteLine($"Удален файл {f.Name}");
                        f.Delete();
                        return true;
                    }                  
                }
                foreach (DirectoryInfo df in diA)
                {
                   return deleteFolder(df.FullName,minute);
                }
                if (di.GetDirectories().Length == 0 && di.GetFiles().Length == 0)
                {
                   
                    if (di.CreationTime < DateTime.Now.Subtract(TimeSpan.FromMinutes(minute)))
                    {
                        Console.WriteLine($"Удален каталог {di.Name}");
                        di.Delete();
                        return true;
                    }                   
                }
            }
            catch (DirectoryNotFoundException ex)
            {
                Console.WriteLine("Директория не найдена. Ошибка: " + ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine("Отсутствует доступ. Ошибка: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка: " + ex.Message);
            }
           return false;
        }

        /// <summary>Поиск последнего файла в каталоге</summary>
        /// <param name="second">Интервал создания файлов в секундах</param>
        /// <returns></returns>
        private FileInfo FilesLast( int second)
        {
            if (LastFilesInfo == null || LastFilesInfo.Exists || LastFilesInfo.CreationTime < DateTime.Now.Subtract(TimeSpan.FromSeconds(second)))
            {
                var files = LastSubDirectoryInfo.GetFiles().Where(file => file.
                                CreationTime >= (DateTime.Now.Subtract(TimeSpan.FromSeconds(second)))
                                && file.CreationTime <= DateTime.Now);
                // Проверяем есть ли соответствие условию
                if (!files.Any())
                    return LastFilesInfo=CreateNewFile(LastSubDirectoryInfo.FullName, FileName);
                else
                {
                    var file = files.OrderByDescending(file => file.CreationTime).First();
                    LastFilesInfo = file;
                    return file;
                }
            }
            else
                return LastFilesInfo;
        }





        /// <summary>Метод проверки на наличие каталога. Если нет то создаем</summary>
        /// <param name="directory">Путь к каталогу</param>
        private DirectoryInfo CheckingDirectory(string directory)
        {
            DirectoryInfo info = new DirectoryInfo(directory);
            if (!info.Exists)
                info.Create();

            return info;
        }

        /// <summary>Создаем подкаталог в каталоге </summary>
        /// <param name="directory"><inheritdoc cref="Directory" path="/summary"/></param>
        /// <param name="SubDirectoryName">Начальное название файла</param>
        /// <returns></returns>
        private DirectoryInfo CreateNewSubDirectoryName(string directory, string SubDirectoryName)
        {
            return new DirectoryInfo(directory).CreateSubdirectory($"{SubDirectoryName}_{DateTime.Now:yyyy_MM_dd__HH_mm_ss}");
        }

        /// <summary>Создаем файл в каталоге </summary>
        /// <param name="directory"><inheritdoc cref="Directory" path="/summary"/></param>
        /// <param name="fileName">Начальное название файла</param>
        /// <returns></returns>
        private FileInfo CreateNewFile(string directory, string fileName)
        {
            return new FileInfo(Path.Combine(directory, $"{fileName}_{DateTime.Now:yyyy_MM_dd__HH_mm_ss}.txt"));
        }


    }
}
