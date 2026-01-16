using CSharpZooTycoon;
using CSharpZooTycoonLibrary;

namespace CSharpZooTycoonTests
{
    public class OtherAnimalClassTests
    {
        [Fact]
        public void TestCreateDog()
        {
            //Arrange
            string _name = "Ted";
            string _type = "DOG";
            string _colour = "BLACK";
            int _limbCount = 4;
            double _tailLength = 0.25;
            int _id = 0;
            string message = $"Id: {_id:D3}, Name: {_name}, Species: {_type}, Colour: {_colour}, Limb Count: {_limbCount}, Tail Length: {_tailLength}";

            //Act
            Dog dog = new Dog(name: _name, type: _type, colour: _colour, limbCount: _limbCount, tailLength: _tailLength);

            //Assert
            Assert.Equal(message, dog.ToString());
        }

        [Fact]
        public void TestCreateDogWithTooShortTail()
        {
            //Arrange
            string _name = "Ted";
            string _type = "DOG";
            string _colour = "BLACK";
            int _limbCount = 4;
            double _tailLength = 0.01;
            double _expectedTailLength = 0.05;
            int _id = 0;
            string expectedMessage = $"Id: {_id:D3}, Name: {_name}, Species: {_type}, Colour: {_colour}, Limb Count: {_limbCount}, Tail Length: {_expectedTailLength}";

            //Act
            Dog dog = new Dog(name: _name, type: _type, colour: _colour, limbCount: _limbCount, tailLength: _tailLength);

            //Assert
            Assert.Equal(expectedMessage, dog.ToString());
        }

        [Fact]
        public void TestDogEat()
        {
            //Arrange
            string _name = "Ted";
            string _type = "DOG";
            string _colour = "BLACK";
            int _limbCount = 4;
            double _tailLength = 0.25;
            string food = "biscuits";
            int _id = 5;
            string expectedMessage = $"I'm a {_type} called {_name} using some of my {_limbCount} limbs to eat {food}. Gobble Gobble Slobber";

            //Act
            Dog dog = new Dog(name: _name, type: _type, colour: _colour, limbCount: _limbCount, tailLength: _tailLength);

            //Assert
            Assert.Equal(expectedMessage, dog.Eat(food));
        }


        [Fact]
        public void TestDogBark()
        {
            //Arrange
            string _name = "Ted";
            string _type = "DOG";
            string _colour = "BLACK";
            int _limbCount = 4;
            double _tailLength = 0.25;
            int numberOfBarks = 5;
            int _id = 5;
            string expectedMessage = $"woof woof woof woof woof ";

            //Act
            Dog dog = new Dog(name: _name, type: _type, colour: _colour, limbCount: _limbCount, tailLength: _tailLength);

            //Assert
            Assert.Equal(expectedMessage, dog.Bark(numberOfBarks));
        }

        [Fact]
        public void TestCreateCat()
        {
            //Arrange
            string _name = "Ted";
            string _type = "Cat";
            string _colour = "BLACK";
            int _limbCount = 4;
            int _whiskerCount = 12;
            int _id = 0;
            string message = $"Id: {_id:D3}, Name: {_name}, Species: {_type.ToUpper()}, Colour: {_colour}, Limb Count: {_limbCount}, Whisker Count: {_whiskerCount}";

            //Act
            Cat cat = new Cat(name: _name, type: _type, colour: _colour, limbCount: _limbCount, whiskerCount: _whiskerCount);

            //Assert
            Assert.Equal(message, cat.ToString());
        }

        [Fact]
        public void TestCreateCatWithTooFewWhiskers()
        {
            //Arrange
            string _name = "Ted";
            string _type = "CAT";
            string _colour = "BLACK";
            int _limbCount = 4;
            int _whiskerCount = -1;
            double _expectedwhiskerCount = 0;
            int _id = 0;
            string expectedMessage = $"Id: {_id:D3}, Name: {_name}, Species: {_type}, Colour: {_colour}, Limb Count: {_limbCount}, Whisker Count: {_expectedwhiskerCount}";

            //Act
            Cat cat = new Cat(name: _name, type: _type, colour: _colour, limbCount: _limbCount, whiskerCount: _whiskerCount);

            //Assert
            Assert.Equal(expectedMessage, cat.ToString());
        }

        [Fact]
        public void TestCatEat()
        {
            //Arrange
            string _name = "Ted";
            string _type = "CAT";
            string _colour = "BLACK";
            int _limbCount = 4;
            int _whiskerCount = 10;
            string food = "fish";
            int _id = 5;
            string expectedMessage = $"I'm a {_type} called {_name} ignoring {food}.";

            //Act
            Cat cat = new Cat(name: _name, type: _type, colour: _colour, limbCount: _limbCount, whiskerCount: _whiskerCount);

            //Assert
            Assert.Equal(expectedMessage, cat.Eat(food));
        }


        [Fact]
        public void TestCatMeow()
        {
            //Arrange
            string _name = "Ted";
            string _type = "CAT";
            string _colour = "BLACK";
            int _limbCount = 4;
            int _whiskerCount = 10;
            int numberOfMeows = 5;
            int _id = 5;
            string expectedMessage = $"meow meow meow meow meow ";

            //Act
            Cat cat = new Cat(name: _name, type: _type, colour: _colour, limbCount: _limbCount, whiskerCount: _whiskerCount);

            //Assert
            Assert.Equal(expectedMessage, cat.Meow(numberOfMeows));
        }


        [Fact]
        public void TestCreateBird()
        {
            //Arrange
            string _name = "Ted";
            string _type = "Bird";
            string _colour = "BLACK";
            int _limbCount = 4;
            int _wingspan = 12;
            int _id = 0;
            string message = $"Id: {_id:D3}, Name: {_name}, Species: {_type.ToUpper()}, Colour: {_colour}, Limb Count: {_limbCount}, Wingspan: {_wingspan}";

            //Act
            Bird bird = new Bird(name: _name, type: _type, colour: _colour, limbCount: _limbCount, wingspan: _wingspan);

            //Assert
            Assert.Equal(message, bird.ToString());
        }

        [Fact]
        public void TestCreateBirdWithTooShortWingSpan()
        {
            //Arrange
            string _name = "Ted";
            string _type = "Bird";
            string _colour = "BLACK";
            int _limbCount = 4;
            int _wingspan = 2;
            double _expectedWingspan = 10;
            int _id = 0;
            string expectedMessage = $"Id: {_id:D3}, Name: {_name}, Species: {_type.ToUpper()}, Colour: {_colour}, Limb Count: {_limbCount}, Wingspan: {_expectedWingspan}";

            //Act
            Bird bird = new Bird(name: _name, type: _type, colour: _colour, limbCount: _limbCount, wingspan: _wingspan);

            //Assert
            Assert.Equal(expectedMessage, bird.ToString());
        }

        [Fact]
        public void TestBirdEat()
        {
            //Arrange
            string _name = "Ted";
            string _type = "Bird";
            string _colour = "BLACK";
            int _limbCount = 4;
            int _wingspan = 10;
            string food = "fish";
            string expectedMessage = $"I'm a {_type.ToUpper()} called {_name} pecking at {food}.";

            //Act
            Bird bird = new Bird(name: _name, type: _type, colour: _colour, limbCount: _limbCount, wingspan: _wingspan);

            //Assert
            Assert.Equal(expectedMessage, bird.Eat(food));
        }


        [Fact]
        public void TestBirdTweet()
        {
            //Arrange
            string _name = "Ted";
            string _type = "Bird";
            string _colour = "BLACK";
            int _limbCount = 4;
            int _wingspan = 10;
            int numberOfTweets = 5;
            string expectedMessage = $"tweet tweet tweet tweet tweet ";

            //Act
            Bird bird = new Bird(name: _name, type: _type, colour: _colour, limbCount: _limbCount, wingspan: _wingspan);

            //Assert
            Assert.Equal(expectedMessage, bird.Tweet(numberOfTweets));
        }
    }
}
