using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace cut4096
{
    class Program
    {
        private static string outputFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "output");
        private static int byteToSkip = 4096;
        public static int ByteToSkip
        {
            get { return byteToSkip; }
            set { byteToSkip = value; }
        }

        static void Main(string[] args)
        {
            try
            {
                if (args.Length > 0)
                {
                    foreach (string path in args)
                    {
                        if (File.Exists(path)) processFile(path);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[ERROR] " + ex.ToString());
            }
        }

        private static void processFile(string FilePath)
        {
            Console.WriteLine("[INFO] processing file: " + FilePath);

            if (!Directory.Exists(outputFolder)) Directory.CreateDirectory(outputFolder);
            string outPutFilePath = Path.Combine(outputFolder, Path.GetFileName(FilePath));

            if (!File.Exists(outPutFilePath))
                File.WriteAllBytes(outPutFilePath, SkipBytes(FilePath, ByteToSkip));
            else
                Console.WriteLine("[ERROR] File already exists: " + outPutFilePath);
        }


        private static byte[] SkipBytes(string sourceFile, int skipBytes)
        {
            byte[] source = File.ReadAllBytes(sourceFile);

            //byte[] newArray = new byte[source.Length - 336];
            byte[] newArray = new byte[source.Length - skipBytes];
            Buffer.BlockCopy(source, skipBytes, newArray, 0, newArray.Length);
            return newArray;
        }

    }

}
