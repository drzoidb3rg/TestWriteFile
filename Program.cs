using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using ChinhDo.Transactions;

namespace TestWriteFile
{
    class Program
    {
        const string ORIGINAL = "C:\\temp\\LoadPubClient\\Content\\test.pdf";
        const string TEMP = "C:\\temp\\LoadPubClient\\Content\\temp\\test.pdf";

        static void Main(string[] args)
        {

            Console.WriteLine("Press ESC to stop");

            do
            {
                while (!Console.KeyAvailable)
                {
                    WriteSafeFile();

                }
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);


        }


        private static void WriteSafeFile()
        {
            try
            {
                Thread.Sleep(100);

                IFileManager fileManager = new TxFileManager();

                using (TransactionScope scope1 = new TransactionScope())
                {



                    File.Delete(TEMP);

                    FileInfo fi = new FileInfo(ORIGINAL);
                    fi.CopyTo(TEMP);

                    Console.WriteLine("Created temp");

                    File.Replace(TEMP, ORIGINAL, "monkey");
                    
                    //fileManager.Copy(ORIGINAL,TEMP,true);

                    //Console.WriteLine("Created temp");


                    //fileManager.Delete(ORIGINAL);

                    //Console.WriteLine("Deleted original");


                    //fileManager.Move(TEMP,ORIGINAL);

                    //Console.WriteLine("Rename temop to original");

                    scope1.Complete();
                }


            }
            catch (Exception ex)
            {
                
                Console.WriteLine(ex.Message);
            }
        }

        private static void WriteUnsafeFile()
        {
            try
            {

                File.Delete(TEMP);

                FileInfo fi = new FileInfo(ORIGINAL);
                fi.CopyTo(TEMP);

                Console.WriteLine("Created temp");

                File.Replace(TEMP,ORIGINAL,"monkey");

                //File.Delete(ORIGINAL);

                //Console.WriteLine("Deleted original");

                //File.Move(TEMP, ORIGINAL);

                //Console.WriteLine("Rename temop to original");
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }
    }
}
