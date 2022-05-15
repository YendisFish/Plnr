using Newtonsoft.Json;

namespace Plnr.Types
{
    public class Entry
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public DateTime DueDate { get; set; }

        public Entry(string name, string content, DateTime duedate)
        {
            Name = name;
            Content = content;
            DueDate = duedate;
        }

        public static async Task<Entry> Factory()
        {
            Console.Write("Enter entry name > ");
            string name = Console.ReadLine() ?? "Unnamed Entry";

            Console.Write("Enter notes about message > ");
            string content = Console.ReadLine() ?? "Empty";

            Console.Write("Enter an end date (Example m/dd/yyyy ) > ");
            DateTime dt; /*=*/ DateTime.TryParseExact(Console.ReadLine(), "M/d/yyyy", null, System.Globalization.DateTimeStyles.None, out dt);

            Entry entry = new Entry(name, content, dt);
            return entry;
        }

        public static async Task<List<Entry>> Import()
        {
            List<Entry> entries = JsonConvert.DeserializeObject<List<Entry>>(await File.ReadAllTextAsync($"/home/{Environment.UserName}/.plnr.json")) ?? new List<Entry>();
            return entries;
        }

        public async Task Write()
        {
            if(!File.Exists($"/home/{Environment.UserName}/.plnr.json"))
            {
                File.Create($"/home/{Environment.UserName}/.plnr.json").Close();
            }

            List<Entry> entries = JsonConvert.DeserializeObject<List<Entry>>(await File.ReadAllTextAsync($"/home/{Environment.UserName}/.plnr.json")) ?? new List<Entry>();
            entries.Add(this);

            string ToWrite = JsonConvert.SerializeObject(entries);
            await File.WriteAllTextAsync($"/home/{Environment.UserName}/.plnr.json", ToWrite);
        }

        public async Task Stdout()
        {
            Console.WriteLine("--------------------");
            Console.WriteLine(this.Name + " | " + this.DueDate.ToString());
            Console.WriteLine(this.Content);
            Console.WriteLine("--------------------");
        }
    }
}