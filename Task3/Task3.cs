using System;
using System.IO;



namespace Task3
{
    internal class Program
    {
        private static string pathDir;
        private static DateTime startTime;
        private static long sizeDelFiles;
        
        static void Main(string[] args)
        {
            startTime = DateTime.Now;
            sizeDelFiles = 0;
            pathDir = @"C:\\newDirectory";
            if (!Directory.Exists(pathDir))
            {
                Console.WriteLine("По данному пути папки не существует!");
                Console.ReadKey();
                return;
            }
            long size = CalcSizeFolder(pathDir);
            Console.WriteLine("Исходный размер папки: " + size + "байт");
            ClearFilesAndFolders(pathDir, ref sizeDelFiles);
            Console.WriteLine("Освобождено: " + sizeDelFiles + "байт");
            size = CalcSizeFolder(pathDir);
            Console.WriteLine("Текущий размер размер папки: " + size + "байт");
            Console.ReadKey();
        }
        public static long CalcSizeFolder(string dirName)
        {
            long size = 0;
            string[] dirs = Directory.GetDirectories(dirName); 
            string[] files = Directory.GetFiles(dirName);
            foreach (string file in files)
            {
                FileInfo fileInfo = new FileInfo(file);
                size = size + fileInfo.Length;
            }
            if (dirs.Length > 0)
            {
                foreach (string dir in dirs)
                {
                    size = size + CalcSizeFolder(dir);
                }
            }
            return size;
        }

        public static void ClearFilesAndFolders(string dirName, ref long sizeDeleteFiles)
        {
            string[] dirs = Directory.GetDirectories(dirName); 
            string[] files = Directory.GetFiles(dirName);
            foreach (string file in files)
            {
                DateTime lastWorkFile = System.IO.File.GetLastWriteTime(file);
                if ((startTime - lastWorkFile) > TimeSpan.FromMinutes(0.01))
                {
                    try
                    {
                        FileInfo fileInfo = new FileInfo(file);
                        sizeDeleteFiles += fileInfo.Length;
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
                    ClearFilesAndFolders(dir, ref sizeDeleteFiles);
                }
            }
        }
    }
}