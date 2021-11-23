using NUnit.Framework;
using System.Linq;
using System;

namespace Tests
{
    public class DatabaseTests
    {
        private Database database;
        [SetUp]
        public void Setup()
        {
            database = new Database();
        }

        [Test]
        public void Ctor_ThrowsExceptionWhenDBCountIsExceeded()
        {
            //Arrange

            //Act

            //Assert
            Assert.Throws<InvalidOperationException>(() => database = new Database(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17));
        }
        [Test]
        public void Ctor_AddValidItemsIntoDB()
        {
            //Arrange
            var elements = new int[] { 1, 2, 3 };

            //Act
            database = new Database(elements);
            //Assert
            Assert.That(database.Count, Is.EqualTo(elements.Length));
        }

        [Test]

        public void Add_IncrementCountWhenAddIsValid()
        {
            //Arrange
            var n = 5;
            //Act
            for (int i = 0; i < n; i++)
            {
                database.Add(i);
            }
            //Assert
            Assert.That(database.Count, Is.EqualTo(n));
        }

        [Test]

        public void Add_ThrownExceptionWhenCapacityIsExceeded()
        {
            //Arrange
            var n = 16;
            //Act
            for (int i = 0; i < n; i++)
            {
                database.Add(i);
            }
            //Assert
            Assert.Throws<InvalidOperationException>(() => database.Add(17));
        }

        [Test]

        public void Remove_DecreaseDBCapacity()
        {
            //Arrange
            var n = 3;

            //Act
            for (int i = 0; i < n; i++)
            {
                database.Add(123);
            }
            database.Remove();
            var expectedResultCount = 2;
            //Assert
            Assert.That(database.Count, Is.EqualTo(expectedResultCount));
        }

        [Test]

        public void Remove_ThrowsExceptionWhenDBIsEmpty()
        {
            //Arrange


            //Act

            //Assert
            Assert.Throws<InvalidOperationException>(() => database.Remove());
        }

        [Test]
        public void Remove_RemovesElementFromDB()
        {
            //Arrange
            var n = 3;
            var lastElement = 3;
            //Act
            for (int i = 0; i < n; i++)
            {
                database.Add(i);
            }
            database.Remove();
            var elements = database.Fetch();
            //Assert
            Assert.IsFalse(elements.Contains(lastElement));
        }

        [Test]
        public void Fetch_ReturnsDBCopyNotReference()
        {
            //Arrange


            //Act
            database.Add(1);
            database.Add(2);
            var firstCopy = database.Fetch();
            database.Add(3);
            var secondCopy = database.Fetch();
            //Assert
            Assert.That(firstCopy, Is.Not.EqualTo(secondCopy));
        }

        [Test]
        public void Count_ReturnsZeroWhenDBIsEmpty()
        {
            Assert.That(database.Count, Is.EqualTo(0));
        }


    }
}