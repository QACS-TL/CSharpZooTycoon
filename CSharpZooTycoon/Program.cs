using System.Globalization;

namespace CSharpZooTycoon
{
    public class Program
    {
        private static HashSet<string> allowedSpecies = new() { "CAT", "DOG", "BIRD", "APE", "UNKNOWN" };
        private static HashSet<string> allowedColours = new() { "BROWN", "BLACK", "WHITE", "ORANGE", "PURPLE", "PINK" };

        public static List<Dictionary<string, string>> LoadAnimals()
        {
            var animals = new List<Dictionary<string, string>>
            {
                new Dictionary<string, string>{{"Name","Fido"}, { "Colour", "BLACK" }, {"LimbCount", "4"}, {"Type", "DOG" }},
                new Dictionary<string, string>{{"Name", "Fifi" }, { "Colour", "WHITE" }, {"LimbCount", "5"}, {"Type", "CAT" }},
                new Dictionary<string, string>{{"Name", "Oscar" }, { "Colour", "ORANGE" }, {"LimbCount", "3"}, {"Type", "BIRD" }},
                new Dictionary<string, string>{{"Name", "Boris" }, { "Colour", "PURPLE" }, {"LimbCount", "3"}, {"Type", "ANIMAL" }}
            };
            return animals;
        }


        public static void AddAnimal(List<Dictionary<string, string>> animals)
        {
            Console.WriteLine("Add a new animal");

            Console.Write("Name: ");
            string name = Console.ReadLine()?.Trim() ?? string.Empty;
            while (name.Length < 2)
            {
                Console.WriteLine("Invalid name, please try again.");
                Console.Write("Name: ");
                name = Console.ReadLine()?.Trim() ?? string.Empty;
            }

            Console.Write("Type: ");
            string species = Console.ReadLine()?.Trim().ToUpper() ?? string.Empty;
            while (!allowedSpecies.Contains(species))
            {
                Console.WriteLine("Invalid Type, please try again.");
                Console.Write("Type: ");
                species = Console.ReadLine()?.Trim().ToUpper() ?? string.Empty;
            }

            Console.Write("Colour: ");
            string colour = Console.ReadLine()?.Trim().ToUpper() ?? string.Empty;
            while (!allowedColours.Contains(colour))
            {
                Console.WriteLine("Invalid colour, please try again.");
                Console.Write("Colour: ");
                colour = Console.ReadLine()?.Trim().ToUpper() ?? string.Empty;
            }

            Console.Write("Limb Count: ");
            string limbCountInput = Console.ReadLine()?.Trim() ?? string.Empty;
            bool parsed = int.TryParse(limbCountInput, out int limbCount);
            while (!parsed || limbCount < 0)
            {
                Console.WriteLine("Invalid limb count, please try again.");
                Console.Write("Limb Count: ");
                limbCountInput = Console.ReadLine()?.Trim() ?? string.Empty;
                parsed = int.TryParse(limbCountInput, out limbCount);
            }

            animals.Add(new Dictionary<string, string> { { "Name", name }, { "Colour", colour }, { "LimbCount", limbCount.ToString() }, { "Type", species } });
        }



        public static void ListAnimals(List<Dictionary<string, string>> animals)
        {
            for (int i = 0; i < animals.Count; i++)
            {
                Console.WriteLine($"{i + 1}) Name: {animals[i]["Name"]}, Colour: {animals[i]["Colour"]}, LimbCount: {animals[i]["LimbCount"]}, Type: {animals[i]["Type"]}");
            }
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
            List<Dictionary<string, string>> animals = LoadAnimals();

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
