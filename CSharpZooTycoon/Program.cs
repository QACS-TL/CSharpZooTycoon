
namespace CSharpZooTycoon
{
    public class Program
    {
        public static void Main(string[] args)
        {
            List<string> animals = new();
            // csv animal values in string
            // Name,Type,Colour,LimbCount
            int count = 0;

            animals.Add("Fido,Dog,BLACK,4");
            count += 1;
            animals.Add("Fifi,Cat,WHITE,5");
            count += 1;
            animals.Add("Oscar,Bird,ORANGE,3");
            count += 1;
            animals.Add("Boris,Animal,PURPLE,3");
            count += 1;

            Console.WriteLine(string.Join(",\n", animals));
            Console.WriteLine("[" + string.Join(", ", animals) + "]");

            Console.WriteLine($"My count variable tells me there are {count} animals.");
            Console.WriteLine($"My animals collection tells me there are {animals.Count} animals.");

            Console.WriteLine("Add a new animal");

            Console.Write("Enter Animal Details (in form '{Name},{Type},{Colour},{LimbCount}': ");
            string? animal = Console.ReadLine()?.Trim();

            if (!string.IsNullOrEmpty(animal))
            {
                animals.Add(animal);
                count += 1;
            }

            Console.WriteLine("[" + string.Join(", ", animals) + "]");
            Console.WriteLine($"My count variable tells me there are {count} animals.");
            Console.WriteLine($"My animals collection tells me there are {animals.Count} animals.");
        }
    }
}