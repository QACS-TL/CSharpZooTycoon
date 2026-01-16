using CSharpZooTycoon;
using System;
using CSharpZooTycoonLibrary;

namespace CSharpZooTycoonTests
{
    public class InterfaceTests
    {
        [Fact]
        public void TestSimilarAnimals()
        {
            //Arrange
            Animal.id = 4; // Reset static count for consistent IDs
            string _name1 = "Ted";
            string _type1 = "DOG";
            string _colour1 = "BLACK";
            int _limbCount1 = 4;
            string _id1 = "005";
            string _name2 = "Ted";
            string _type2 = "CAT";
            string _colour2 = "PINK";
            int _limbCount2 = 5;
            string _id2 = "006";
            string message1 = $"Id: {_id1}, Name: {_name1}, Species: {_type1}, Colour: {_colour1}, Limb Count: {_limbCount1}"; ;
            string message2 = $"Id: {_id2}, Name: {_name2}, Species: {_type2}, Colour: {_colour2}, Limb Count: {_limbCount2}"; ;


            //Act
            Animal animal1 = new Animal(name: _name1, type: _type1, colour: _colour1, limbCount: _limbCount1);
            Animal animal2 = new Animal(name: _name2, type: _type2, colour: _colour2, limbCount: _limbCount2);

            //Assert
            Assert.True(animal1.Equals(animal2));
            Assert.False(message1.Equals(message2));

        }

        [Fact]
        public void TestSortingOfAnimalsUsingIComparable()
        {
            //Arrange
            var animals = new List<Animal>
            {
                new Dog(name:"Fido", type:"DOG", colour:"BLACK", limbCount:4),
                new Cat(name:"Fifi", type:"CAT", colour:"WHITE", limbCount:5),
                new Bird(name:"Oscar", type:"BIRD", colour:"ORANGE", limbCount:2),
                new Animal(name:"Boris", type:"ANIMAL", colour:"PURPLE", limbCount:3)
            };
            
            //Act
            animals.Sort();

            //Assert
            Assert.Equal(2, animals[0].LimbCount);
            Assert.Equal(3, animals[1].LimbCount);
            Assert.Equal(4, animals[2].LimbCount);
            Assert.Equal(5, animals[3].LimbCount);

        }


        [Fact]
        public void TestSortingOfAnimalsUsingIColourComparer()
        {
            //Arrange
            var animals = new List<Animal>
            {
                new Dog(name:"Fido", type:"DOG", colour:"BLACK", limbCount:4),
                new Cat(name:"Fifi", type:"CAT", colour:"WHITE", limbCount:5),
                new Bird(name:"Oscar", type:"BIRD", colour:"ORANGE", limbCount:2),
                new Animal(name:"Boris", type:"ANIMAL", colour:"PURPLE", limbCount:3)
            };

            //Act
            animals.Sort(Animal.ColourComparer);

            //Assert
            Assert.Equal("BLACK", animals[0].Colour);
            Assert.Equal("ORANGE", animals[1].Colour);
            Assert.Equal("PURPLE", animals[2].Colour);
            Assert.Equal("WHITE", animals[3].Colour);

        }


        [Fact]
        public void TestSortingOfAnimalsUsingINameComparer()
        {
            //Arrange
            var animals = new List<Animal>
            {
                new Dog(name:"Fido", type:"DOG", colour:"BLACK", limbCount:4),
                new Bird(name:"Oscar", type:"BIRD", colour:"ORANGE", limbCount:2),
                new Cat(name:"Fifi", type:"CAT", colour:"WHITE", limbCount:5),
                new Animal(name:"Boris", type:"ANIMAL", colour:"PURPLE", limbCount:3)
            };

            //Act
            animals.Sort(Animal.NameComparer);

            //Assert
            Assert.Equal("Boris", animals[0].Name);
            Assert.Equal("Fido", animals[1].Name);
            Assert.Equal("Fifi", animals[2].Name);
            Assert.Equal("Oscar", animals[3].Name);

        }


        [Fact]
        public void CloneAnAnimal()
        {
            Animal ani = new Animal(name: "Boris", type: "ANIMAL", colour: "PURPLE", limbCount: 3);
            Animal aniClone = ani.Clone();
            Assert.Equal(ani, aniClone);
        }
    }
}
