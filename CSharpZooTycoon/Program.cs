namespace CSharpZooTycoon
{
    public class Program
    {

        public static List<string> LoadAnimals()
        {
            List<string> animals = new List<string>();
            // csv animal values in string
            // Name,Type,Colour,LimbCount
            animals.Add("Fido,Dog,BLACK,4");
            animals.Add("Fifi,Cat,WHITE,5");
            animals.Add("Oscar,Bird,ORANGE,3");
            animals.Add("Boris,Animal,PURPLE,3");
            return animals;
        }

        public static void AddAnimal(List<string> animals)
        {
            Console.WriteLine("\nAdd a new animal");
            Console.Write("Enter Animal Details (in form '{Name},{Type},{Colour},{LimbCount}': ");
            string? animal = Console.ReadLine()?.Trim();

            if (!string.IsNullOrEmpty(animal))
            {
                animals.Add(animal);
            }
        }


        public static void ListAnimals(List<string> animals)
        {
            Console.WriteLine("\nAnimal Collection Details:");
            Console.WriteLine(string.Join(", \n", animals));
            Console.WriteLine($"My animals collection tells me there are {animals.Count} animals.");
        }

        public static void PrintMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Animals App - Menu");
            Console.WriteLine("1) List animals");
            Console.WriteLine("2) Add animal");
            Console.WriteLine("3) Exit");
        }

        public static string InputDetail(string prompt)
        {
            Console.Write($"{prompt}: ");
            return Console.ReadLine()?.Trim() ?? string.Empty;
        }

        public static void MainMenu()
        {
            List<string> animals = LoadAnimals();

            while (true)
            {
                PrintMenu();
                string choice = InputDetail("Choose an option");
                switch (choice)
                {
                    case "1":
                        ListAnimals(animals);
                        break;
                    case "2":
                        AddAnimal(animals);
                        break;
                    case "3":
                        Console.WriteLine("Goodbye — saving and exiting.");
                        return;
                    default:
                        Console.WriteLine("Unknown option. Please try again.");
                        break;
                }
            }
        }

        public static void Main(string[] args)
        {
            MainMenu();
        }
    }
}


