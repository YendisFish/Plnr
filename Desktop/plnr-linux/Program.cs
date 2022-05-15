using Newtonsoft.Json;
using Plnr.Types;

namespace Plnr
{
    public class EntryPoint
    {
        public static async Task Main(params string[] args)
        {
            foreach(string arg in args)
            {
                if(arg == "help")
                {
                    Console.WriteLine("--------------------");
                    Console.WriteLine("check - Check ToDo list");
                    Console.WriteLine("--------------------");
                }

                if(arg == "check")
                {
                    List<Entry> entries = await Entry.Import();

                    foreach(Entry entry in entries)
                    {
                        await entry.Stdout();
                    }
                }

                if(arg == "create")
                {
                    Entry entry = await Entry.Factory();
                    await entry.Write();
                }
            }
        }
    }
}
