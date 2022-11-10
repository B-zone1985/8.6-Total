using System;
using System.IO;


namespace Task1
{
    internal class Program
    {
        private static string pathDir;
        private static DateTime startTime;
        
        static void Main(string[] args)
        {
            startTime = DateTime.Now;
            pathDir = @"C:\\newDirectory";
            if (!Directory.Exists(pathDir))
            {
                Console.WriteLine("По данному пути папки не существует!");
                Console.ReadKey();
                return;
            }
            ClearFilesAndFolders(pathDir);
        }
        public static void ClearFilesAndFolders(string dirName)
        {
            string[] dirs = Directory.GetDirectories(dirName);  /// Получим все директории корневого каталога
            string[] files = Directory.GetFiles(dirName);/// Получим все файлы корневого каталога
            foreach (string file in files)
            {
                DateTime lastWorkFile = System.IO.File.GetLastWriteTime(file);
                if ((startTime - lastWorkFile) > TimeSpan.FromMinutes(0.01))
                {
                    try
                    {
                        File.Delete(file);
                    }
                    catch
                    {
                        Console.WriteLine("Данный файл изменялся менее 30 минут назад" + file);
                    }
                }
            }
            if (dirs.Length > 0)
            {
                //Рекурсия по вложенным папкам
                foreach (string dir in dirs)
                {
                    ClearFilesAndFolders(dir);
                }
            }
        }
    }
}