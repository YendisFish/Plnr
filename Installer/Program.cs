using System.Net;
using System.Diagnostics;

namespace Installer
{
    internal class EntryPoint
    {
        public static async Task Main()
        {
            if(OperatingSystem.IsLinux())
            {
                Console.WriteLine("Running linux install...");
                
                WebClient cli = new WebClient();
                byte[] dat = cli.DownloadData(new Uri("https://github.com/YendisFish/Plnr/releases/download/2022-15-5/plnr-linux"));
                File.Create($"/home/{Environment.UserName}/Plnr/Plnr").Close();

                await File.WriteAllBytesAsync($"/home/{Environment.UserName}/Plnr/Plnr", dat);
            } else {
                Console.WriteLine("There is not yet a version of Plnr made for your operating system.");
                Console.WriteLine("The source code for the project is on github: https://github.com/YendisFish/Plnr");
                Console.WriteLine("You can try modifying it to work with your system.");
            }
        }
    }
}