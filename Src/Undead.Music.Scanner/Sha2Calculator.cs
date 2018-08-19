using System;
using System.Collections.Specialized;
using System.IO;
using System.Security.Cryptography;

namespace Undead.Music.Scanner
{
    public class Sha2Calculator
    {
        private static string PrintByteArray(byte[] array)
        {
            int i;
            string hash = string.Empty;
            for (i = 0; i < array.Length; i++)
            {
                hash += string.Format("{0:X2}", array[i]);
                if ((i % 4) == 3) Console.Write(" ");
            }

            return hash;
        }

        public static string ComputeFileHash(FileInfo info)
        {
            var mySha256 = SHA256.Create();
            FileStream fileStream = info.Open(FileMode.Open);
            fileStream.Position = 0;
            byte[] hashValue = mySha256.ComputeHash(fileStream);
            fileStream.Close();
            return PrintByteArray(hashValue);
        }

        public static StringDictionary ComputeFolderHash(string folderPath)
        {
            var dir = new DirectoryInfo(folderPath);
            var files = dir.GetFiles("*.mp3");
            var mySha256 = SHA256.Create();
            var filesHash = new StringDictionary();

            foreach (FileInfo info in files)
            {
                FileStream fileStream = info.Open(FileMode.Open);
                fileStream.Position = 0;
                byte[] hashValue = mySha256.ComputeHash(fileStream);
                filesHash.Add(info.Name, PrintByteArray(hashValue));
                fileStream.Close();
            }

            return filesHash;
        }
    }
}