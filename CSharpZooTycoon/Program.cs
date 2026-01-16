using CSharpZooTycoonLibrary;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace CSharpZooTycoon
{
    public class Program
    {

        static HashSet<string> allowedSpecies = new() { "CAT", "DOG", "BIRD", "APE", "UNKNOWN" };
        static HashSet<string> allowedColours = new() { "BROWN", "BLACK", "WHITE", "ORANGE", "PURPLE", "PINK" };

        public static List<Animal> LoadAnimals()
        {
            using (ZooContext db = new ZooContext())
            {
                var animals = db.Animals.ToList();
                return animals;
            }
        }

        public static bool IsNumeric(string value)
        {
            // Guard clause: if the string is null or empty, it cannot be numeric.
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }

            // Use LINQ to check if all characters in the string are numeric digits.
            // char.IsNumber returns true for any Unicode numeric digit.
            return value.All(char.IsNumber);
        }

        public static bool AttributeChecker(string attributeName, string value)
        {
            //if true is returned the attribute is invalid
            switch (attributeName)
            {
                case "Name":
                    return value.Length < 2;
                case "Type":
                    return !allowedSpecies.Contains(value.ToUpper());
                case "Colour":
                    return !allowedColours.Contains(value.ToUpper());
                case "LimbCount":
                    return !IsNumeric(value) || (int.Parse(value) < 0);
                case "WhiskerCount":
                    return !IsNumeric(value) || (int.Parse(value) < 0);
                case "TailLength":
                    if (double.TryParse(value, out double result))
                    {
                        return result < 0.05;
                    }
                    return true;
                case "Wingspan":
                    return !IsNumeric(value) || (int.Parse(value) < 10);
                default:
                    return true;
            }
        }

        public static string GetAndValidateAttributeForAdding(string animalAttribute)
        {
            Console.Write($"{animalAttribute}: ");
            string value = Console.ReadLine()?.Trim() ?? string.Empty;
            while (AttributeChecker(animalAttribute, value))
            {
                Console.WriteLine($"Invalid {animalAttribute}, please try again.");
                Console.Write($"{animalAttribute}: ");
                value = Console.ReadLine()?.Trim() ?? string.Empty;
            }

            return value;
        }

        public static void AddAnimal(List<Animal> animals)
        {
            Console.WriteLine("Add a new animal");
            Animal a = null;
            string name = GetAndValidateAttributeForAdding("Name");
            string type = GetAndValidateAttributeForAdding("Type");
            string colour = GetAndValidateAttributeForAdding("Colour");
            string limbCount = GetAndValidateAttributeForAdding("LimbCount");

            switch (type.ToUpper())
            {
                case "CAT":
                    string whiskerCount = GetAndValidateAttributeForAdding("WhiskerCount");
                    a = new Cat(name: name, type: type.ToUpper(), colour: colour.ToUpper(), limbCount: Convert.ToInt32(limbCount), whiskerCount: Convert.ToInt32(whiskerCount));
                    break;
                case "DOG":
                    string tailLength = GetAndValidateAttributeForAdding("TailLength");
                    a = new Dog(name: name, type: type.ToUpper(), colour: colour.ToUpper(), limbCount: Convert.ToInt32(limbCount), tailLength: Convert.ToDouble(tailLength));
                    break;
                case "BIRD":
                    string wingspan = GetAndValidateAttributeForAdding("Wingspan");
                    a = new Bird(name: name, type: type.ToUpper(), colour: colour.ToUpper(), limbCount: Convert.ToInt32(limbCount), wingspan: Convert.ToInt32(wingspan));
                    break;
                default:
                    a = new Animal(name: name, type: type.ToUpper(), colour: colour.ToUpper(), limbCount: Convert.ToInt32(limbCount));
                    break;
            }

            using (ZooContext db = new ZooContext())
            {
                db.Animals.Add(a);
                db.SaveChanges();
            }

        }

        public static int? ChooseIndex(int maxN)
        {
            string raw = InputDetail("Choose number (or blank to cancel)");
            if (string.IsNullOrWhiteSpace(raw))
            {
                Console.WriteLine("Cancelled.");
                return null;
            }

            if (!int.TryParse(raw, out int n))
            {
                Console.WriteLine("Invalid selection");
                return null;
            }

            if (n >= 1 && n <= maxN)
                return n - 1;

            Console.WriteLine("Invalid selection");
            return null;
        }

        public static (Animal? selected, bool Quit) AnimalSelector(List<Animal> animals, string messageMode, bool quitFlag)
        {
            var title = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(messageMode ?? string.Empty);
            Console.WriteLine($"{title} animals");

            if (animals == null || animals.Count == 0)
            {
                Console.WriteLine($"No animals to {messageMode}.");
                quitFlag = true;
            }

            ListAnimals(animals);

            int? idx = ChooseIndex(animals?.Count ?? 0);
            if (!idx.HasValue)
            {
                quitFlag = true;
                return (null, quitFlag);
            }

            return (animals[idx.Value], quitFlag);
        }

        public static string GetAndValidateAttributeWhileEditing(string animalAttribute, string propertyName, string currentValue)
        {
            Console.Write($"{propertyName} [{currentValue}]: ");
            string input = Console.ReadLine()?.Trim() ?? string.Empty;

            // blank => keep existing value
            if (string.IsNullOrEmpty(input))
                return currentValue;

            while (AttributeChecker(animalAttribute, input))
            {
                Console.WriteLine($"Invalid {propertyName}, please try again.");
                Console.Write($"{propertyName} [{currentValue}]: ");
                input = Console.ReadLine()?.Trim() ?? string.Empty;
                if (string.IsNullOrEmpty(input))
                    return currentValue;
            }

            return input;
        }

        public static void EditAnimal(List<Animal> animals)
        {
            string messageMode = "edit";
            bool quitFlag = false;

            var (ani, qf) = AnimalSelector(animals, messageMode, quitFlag);
            if (qf || ani == null)
                return;

            Console.WriteLine("Current attributes (leave blank to keep):");

            ani.Name = GetAndValidateAttributeWhileEditing("Name", "name", ani.Name);
            ani.Colour = GetAndValidateAttributeWhileEditing("Colour", "colour", ani.Colour);
            string limbCount = GetAndValidateAttributeWhileEditing("LimbCount", "limb_count", ani.LimbCount.ToString());
            ani.LimbCount = Convert.ToInt32(limbCount);

            //if (ani is Cat cat)
            //{
            //    string whiskerCount = GetAndValidateAttributeWhileEditing("WhiskerCount", "whisker_count", cat.WhiskerCount.ToString());
            //    cat.WhiskerCount = Convert.ToInt32(whiskerCount);
            //}
            //if (ani is Dog dog)
            //{
            //    string tailLength = GetAndValidateAttributeWhileEditing("TailLength", "tail_length", dog.TailLength.ToString());
            //    dog.TailLength = Convert.ToDouble(tailLength);
            //}
            //if (ani is Bird bird)
            //{
            //    string wingspan = GetAndValidateAttributeWhileEditing("Wingspan", "wingspan", bird.Wingspan.ToString());
            //    bird.Wingspan = Convert.ToInt32(wingspan);
            //}

            using (ZooContext db = new ZooContext())
            {
                Animal an = db.Animals.SingleOrDefault(a => a.Id == ani.Id);
                if (an != null)
                {
                    an.Name = ani.Name;
                    an.Colour = ani.Colour;
                    an.LimbCount = ani.LimbCount;
                    an.Type = ani.Type;
                    switch (an)
                    {
                        case Cat cat:
                            string whiskerCount = GetAndValidateAttributeWhileEditing("WhiskerCount", "whisker_count", cat.WhiskerCount.ToString());
                            cat.WhiskerCount = Convert.ToInt32(whiskerCount);
                            break;
                        case Dog dog:
                            string tailLength = GetAndValidateAttributeWhileEditing("TailLength", "tail_length", dog.TailLength.ToString());
                            dog.TailLength = Convert.ToDouble(tailLength);
                            break;
                        case Bird bird:
                            string wingspan = GetAndValidateAttributeWhileEditing("Wingspan", "wingspan", bird.Wingspan.ToString());
                            bird.Wingspan = Convert.ToInt32(wingspan);
                            break;
                    }
                }
                db.SaveChanges();
            }
            Console.WriteLine("Saved changes.");
        }

        public static void RemoveAnimal(List<Animal> animals)
        {
            string messageMode = "remove";
            bool quitFlag = false;

            var (ani, qf) = AnimalSelector(animals, messageMode, quitFlag);
            if (qf || ani == null)
                return;

            using (ZooContext db = new ZooContext())
            {
                db.Animals.Remove(ani);
                db.SaveChanges();
            }

            Console.WriteLine($"Removed {ani.Name}");
        }

        public static void FeedAnimal(List<Animal> animals)
        {
            string messageMode = "feed";
            bool quitFlag = false;

            var (ani, qf) = AnimalSelector(animals, messageMode, quitFlag);
            if (qf || ani == null)
                return;

            string food = ani.Type.ToUpper() switch
            {
                "CAT" => "fish",
                "DOG" => "biscuits",
                "BIRD" => "seeds",
                _ => "sandwiches"
            };

            string msg = $"I'm a {ani.Type} called {ani.Name} using some of my {ani.LimbCount} limbs to eat {food}.";
            msg += $" You fed the {ani.Type} called {ani.Name}.";

            if (ani.Type.Equals("DOG", StringComparison.OrdinalIgnoreCase))
                msg += " It's wagging its tail happily!";
            else if (ani.Type.Equals("CAT", StringComparison.OrdinalIgnoreCase))
                msg += " It purrs contentedly.";
            else if (ani.Type.Equals("BIRD", StringComparison.OrdinalIgnoreCase))
                msg += " It chirps sweetly.";
            else
                msg += " It seems satisfied.";

            Console.WriteLine(msg);
        }

        public static void PrintSortMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Animals App - Sort Menu");
            Console.WriteLine("1) Name sequence");
            Console.WriteLine("2) Colour sequence");
            Console.WriteLine("3) Limb Count sequence");
        }

        public static void SortAnimals(List<Animal> animals)
        {
            PrintSortMenu();
            string choice = InputDetail("Choose an option");

            switch (choice)
            {
                case "1":
                    animals.Sort(Animal.NameComparer);
                    Console.WriteLine("Animals sorted by name.");
                    break;
                case "2":
                    animals.Sort(Animal.ColourComparer);
                    Console.WriteLine("Animals sorted by colour.");
                    break;
                case "3":
                    animals.Sort();
                    Console.WriteLine("Animals sorted by limb count.");
                    break;
                default:
                    Console.WriteLine("Bad input. Animals sequence unchanged.");
                    break;
            }

            ListAnimals(animals);
        }

        public static void PrintFilterMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Animals App - Filter Menu");
            Console.WriteLine("1) Name");
            Console.WriteLine("2) Colour");
            Console.WriteLine("3) Limb Count");
            Console.WriteLine("4) Type");
            Console.WriteLine("5) Most Popular Colour");
        }

        public static string GetFilterValue(string filterType)
        {
            Console.Write($"Enter value to filter by {filterType}: ");
            return Console.ReadLine()?.Trim() ?? string.Empty;
        }

        public static (List<string> Colours, int Count) GetMostPopularColours(List<Animal> animals)
        {
            if (animals == null || animals.Count == 0)
                return (new List<string>(), 0);

            var groups = animals
                .GroupBy(a => (a.Colour ?? string.Empty).ToUpperInvariant())
                .Select(g => new { Colour = g.Key, Count = g.Count() })
                .ToList();

            int max = groups.Max(g => g.Count);
            var top = groups.Where(g => g.Count == max).Select(g => g.Colour).ToList();
            return (top, max);
        }

        public static void FilterAnimals(List<Animal> animals)
        {
            PrintFilterMenu();
            string choice = InputDetail("Choose an option");

            switch (choice)
            {
                case "1":
                    string nameFilter = GetFilterValue("Name");
                    animals = animals.Where(a => a.Name.StartsWith(nameFilter, StringComparison.OrdinalIgnoreCase)).ToList();
                    Console.WriteLine("Animals filtered by name.");
                    break;
                case "2":
                    string colourFilter = GetFilterValue("Colour");
                    animals = animals.Where(a => a.Colour.Equals(colourFilter, StringComparison.OrdinalIgnoreCase)).ToList();
                    Console.WriteLine("Animals filtered by colour.");
                    break;
                case "3":
                    string limbCountFilter = GetFilterValue("Limb Count");
                    animals = animals.Where(a => a.LimbCount.ToString() == limbCountFilter).ToList();
                    Console.WriteLine("Animals filtered by limb count.");
                    break;
                case "4":
                    string typeFilter = GetFilterValue("Type");
                    animals = animals.Where(a => a.Type.ToString() == typeFilter.ToUpper()).ToList();
                    Console.WriteLine("Animals filtered by type.");
                    break;
                case "5":
                    var analytics = GetMostPopularColours(animals);
                    if (analytics.Colours.Count == 1)
                    {
                        Console.WriteLine($"The most popular animal colour is {analytics.Colours[0]} and there are {analytics.Count} {analytics.Colours[0]} animals in the collection");
                        return;
                    }
                    else if (analytics.Colours.Count > 1)
                    {
                        string colourString = analytics.Colours.Count switch
                        {
                            2 => $"{analytics.Colours[0]} and {analytics.Colours[1]}",
                            _ => string.Join(", ", analytics.Colours[..^1]) + $" and {analytics.Colours[^1]}"
                        };
                        Console.WriteLine($"The most popular animal colours are {colourString} and there are {analytics.Count} of each in the animals collection");
                        return;
                    }
                    break;
                default:
                    Console.WriteLine("Bad input. Animals are unfiltered.");
                    break;
            }

            ListAnimals(animals);
        }

        public static void ListAnimals(List<Animal> animals)
        {
            for (int i = 0; i < animals.Count; i++)
            {
                Console.Write($"{i + 1}) Name: {animals[i].Name}, Colour: {animals[i].Colour}, LimbCount: {animals[i].LimbCount}, Type: {animals[i].Type}");
                if (animals[i] is Cat cat)
                {
                    Console.Write($", WhiskerCount: {cat.WhiskerCount}\n");
                }
                else if (animals[i] is Dog dog)
                {
                    Console.Write($", TailLength: {dog.TailLength}\n");
                }
                else if (animals[i] is Bird bird)
                {
                    Console.Write($", Wingspan: {bird.Wingspan}\n");
                }
                else
                {
                    Console.Write("\n");
                }
            }
        }

        public static void PrintMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Animals App - Menu");
            Console.WriteLine("1) List animals");
            Console.WriteLine("2) Add animal");
            Console.WriteLine("3) Edit animal");
            Console.WriteLine("4) Remove animal");
            Console.WriteLine("5) Feed animal");
            Console.WriteLine("6) Sort animals");
            Console.WriteLine("7) Filter animals");
            Console.WriteLine("8) Exit");
        }

        public static string InputDetail(string prompt)
        {
            Console.Write($"{prompt}: ");
            return Console.ReadLine()?.Trim() ?? string.Empty;
        }

        public static void MainMenu()
        {
            while (true)
            {
                List<Animal> animals = LoadAnimals();
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
                        EditAnimal(animals); ;
                        break;
                    case "4":
                        RemoveAnimal(animals);
                        break;
                    case "5":
                        FeedAnimal(animals);
                        break;
                    case "6":
                        SortAnimals(animals);
                        break;
                    case "7":
                        FilterAnimals(animals);
                        break;
                    case "8":
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