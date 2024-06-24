using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AudioFingerprinter
{
    internal class Program
    {
        public static List<string> base64 = new List<string>();
        public static int progress = 0;

        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine("Enter MP3 Files Path:");
            string path = Console.ReadLine();
            DirectoryInfo d = new DirectoryInfo(path);

            FileInfo[] Files = d.GetFiles("*.mp3");

            foreach (FileInfo file in Files)
            {
                var audioBytes = File.ReadAllBytes(file.FullName);
                var base64String = Convert.ToBase64String(audioBytes);
                string tmp = base64String.Substring(0, 5) + base64String.Substring(20, 25) + base64String.Substring(40, 45) + base64String.Substring(120, 145);
                if (!base64.Contains(tmp))
                {
                    base64.Add(tmp);
                }
                else
                {
                    File.Delete(file.FullName);
                }
                progress++;
                Console.Title = "AudioFingerprinter       Progress [ " + progress + " | " + Files.Length + " ]";
            }
            Console.WriteLine("DONE!");
            Console.ReadKey();
        }
    }
}
