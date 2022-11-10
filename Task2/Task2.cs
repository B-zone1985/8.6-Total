using System;
using System.IO;

namespace Task2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string pathDir = @"C:\\newDirectory";
            if (!Directory.Exists(pathDir)) 
            {
                Console.WriteLine("По данному пути папок не обнаружено!");
                Console.ReadKey();
                return;
            }
            long size = CalcSizeFolders(pathDir);
            Console.WriteLine("Размер папки " + size + " байт");
            Console.ReadKey();
        }
        public static long CalcSizeFolders(string dirName)
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
                    size = size + CalcSizeFolders(dir);
                }
            }
            return size;
        }
    }
}