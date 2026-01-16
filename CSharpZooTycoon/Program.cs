namespace CSharpZooTycoon
{
    public class Program
    {
        static List<string> animals;

        static int count;

        public static List<string> CreateAnimalCollection()
        {
            List<string> animals = new List<string>();
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

            Console.WriteLine($"My count variable tells me there are {count} animals.");

            return animals;
        }

        public static void PrintDetails(List<string> animals)
        {
            Console.WriteLine("\nAnimal Collection Details:");
            Console.WriteLine(string.Join(", \n", animals));
            Console.WriteLine($"My animals collection tells me there are {animals.Count} animals.");
        }

        public static void AddNewAnimal(List<string> animals)
        {
            Console.WriteLine("\nAdd a new animal");
            Console.Write("Enter Animal Details (in form '{Name},{Type},{Colour},{LimbCount}': ");
            string? animal = Console.ReadLine()?.Trim();

            if (!string.IsNullOrEmpty(animal))
            {
                animals.Add(animal);
            }
        }

        public static void Main(string[] args)
        {
            animals = CreateAnimalCollection();

            PrintDetails(animals);
            AddNewAnimal(animals);

            PrintDetails(animals);
        }

    }
}