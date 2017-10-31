using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UserStorageServices.Exceptions;
using UserStorageServices.Interfaces;

namespace UserStorageServices.Tests
{
    [TestClass]
    public class UserStorageServiceTests
    {
        #region AddTests

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Add_NullAsUserArgument_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageService();

            // Act
            userStorageService.Add(null);

            // Assert - [ExpectedException]
        }

        [TestMethod]
        [ExpectedException(typeof(FirstNameIsNullOrEmptyException))]
        public void Add_UserFirstNameIsNull_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageService();

            // Act
            userStorageService.Add(new User
            {
                FirstName = null,
                LastName = "Olegov",
                Age = 20
            });

            // Assert - [ExpectedException]
        }

        [TestMethod]
        [ExpectedException(typeof(LastNameIsNullOrEmptyException))]
        public void Add_UserLasrtNameIsNull_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageService();

            // Act
            userStorageService.Add(new User
            {
                FirstName = "Oleg",
                LastName = null,
                Age = 20
            });

            // Assert - [ExpectedException]
        }

        [TestMethod]
        [ExpectedException(typeof(AgeExceedsLimitsException))]
        public void Add_UserAgeIncorrect_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageService();

            // Act
            userStorageService.Add(new User
            {
                FirstName = "Oleg",
                LastName = "Olegov",
                Age = -20
            });

            // Assert - [ExpectedException]
        }

        #endregion

        #region RemoveTests

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Remove_nullArgument_ExceptionTrown()
        {
            // Arrange
            var userStorageService = new UserStorageService();

            // Act
            userStorageService.Remove(null);

            // Assert - [ExpectedException]
        }

        [TestMethod]
        public void Remove_ExistingUser_True()
        {
            // Arrange
            var userStorageService = new UserStorageService();
            var user = new User
            {
                Age = 15,
                FirstName = "Oleg",
                LastName = "Olegov"
            };
            userStorageService.Add(user);

            // Act
            var deleted = userStorageService.Remove(user);

            Assert.IsTrue(deleted);
        }

        [TestMethod]
        public void Remove_NotExistingUser_False()
        {
            // Arrange
            var userStorageService = new UserStorageService();
            var user = new User { Age = 15, FirstName = "Oleg", LastName = "Olegov" };
            userStorageService.Add(user);

            // Act
            var deleted = userStorageService.Remove(new User());

            Assert.IsFalse(deleted);
        }

        #endregion

        #region SearchTests

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Search_nullStringArgument_ExceptionTrown()
        {
            // Arrange
            var userStorageService = new UserStorageService();

            // Act
            userStorageService.Search((string)null);

            // Assert - [ExpectedException]
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Search_nullPredicateArgument_ExceptionTrown()
        {
            // Arrange
            var userStorageService = new UserStorageService();

            // Act
            userStorageService.Search((Predicate<User>)null);

            // Assert - [ExpectedException]
        }

        [TestMethod]
        public void Search_NotExistingUser_NullReturned()
        {
            // Arrange
            var userStorageService = new UserStorageService();
            FillStorage(userStorageService);

            // Act
            var returned = userStorageService.Search(u => u.FirstName == "Qqq");

            // Assert
            Assert.IsNull(returned);
        }

        [TestMethod]
        public void Search_ExistingUserByPredicateFirstName_UserReturned()
        {
            // Arrange
            var userStorageService = new UserStorageService();
            var list = FillStorage(userStorageService);
            var expected = list.Find(u => u.FirstName == "Oleg");

            // Act
            var returned = userStorageService.Search(u => u.FirstName == "Oleg");
            
            // Assert
            Assert.AreEqual(returned, expected);
        }

        [TestMethod]
        public void Search_ExistingUserByPredicateLastName_UserReturned()
        {
            // Arrange
            var userStorageService = new UserStorageService();
            var list = FillStorage(userStorageService);
            var expected = list.Find(u => u.LastName == "Olegov");

            // Act
            var returned = userStorageService.Search(u => u.LastName == "Olegov");

            // Assert
            Assert.AreEqual(returned, expected);
        }

        [TestMethod]
        public void Search_ExistingUserByPredicateAge_UserReturned()
        {
            // Arrange
            var userStorageService = new UserStorageService();
            var list = FillStorage(userStorageService);
            var expected = list.Find(u => u.Age == 15);

            // Act
            var returned = userStorageService.Search(u => u.Age == 15);

            // Assert
            Assert.AreEqual(returned, expected);
        }

        [TestMethod]
        public void Search_ExistingUserByFirstName_UserReturned()
        {
            // Arrange
            var userStorageService = new UserStorageService();
            var list = FillStorage(userStorageService);
            var expected = list.Find(u => u.FirstName == "Oleg");

            // Act
            var returned = userStorageService.Search("Oleg");

            // Assert
            Assert.AreEqual(returned, expected);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SearchAll_nullStringArgument_ExceptionTrown()
        {
            // Arrange
            var userStorageService = new UserStorageService();

            // Act
            userStorageService.SearchAll((string)null);

            // Assert - [ExpectedException]
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SearchAll_nullPredicateArgument_ExceptionTrown()
        {
            // Arrange
            var userStorageService = new UserStorageService();

            // Act
            userStorageService.SearchAll((Predicate<User>)null);

            // Assert - [ExpectedException]
        }

        [TestMethod]
        public void SearchAll_ExistingUsersByFirstName_CollectionReturned()
        {
            var userStorageService = new UserStorageService();
            var list = FillStorage(userStorageService);
            var expected = list.FindAll(u => u.FirstName == "Oleg");

            // Act
            var returned = userStorageService.SearchAll("Oleg");

            // Assert
            CollectionAssert.AreEqual(returned.ToList(), expected);
        }

        [TestMethod]
        public void SearchAll_ExistingUsersByPredicateFirstName_CollectionReturned()
        {
            var userStorageService = new UserStorageService();
            var list = FillStorage(userStorageService);
            var expected = list.FindAll(u => u.FirstName == "Oleg");

            // Act
            var returned = userStorageService.SearchAll(u => u.FirstName == "Oleg");

            // Assert
            CollectionAssert.AreEqual(returned.ToList(), expected);
        }

        [TestMethod]
        public void SearchAll_ExistingUsersByPredicateLastName_CollectionReturned()
        {
            var userStorageService = new UserStorageService();
            var list = FillStorage(userStorageService);
            var expected = list.FindAll(u => u.LastName == "Olegov");

            // Act
            var returned = userStorageService.SearchAll(u => u.LastName == "Olegov");

            // Assert
            CollectionAssert.AreEqual(returned.ToList(), expected);
        }

        [TestMethod]
        public void SearchAll_ExistingUsersByPredicateAge_CollectionReturned()
        {
            var userStorageService = new UserStorageService();
            var list = FillStorage(userStorageService);
            var expected = list.FindAll(u => u.Age <= 30);

            // Act
            var returned = userStorageService.SearchAll(u => u.Age <= 30);

            // Assert
            CollectionAssert.AreEqual(returned.ToList(), expected);
        }

        [TestMethod]
        public void SearchAll_ExistingUsersByPredicateFirstNameAndAge_CollectionReturned()
        {
            var userStorageService = new UserStorageService();
            var list = FillStorage(userStorageService);
            var expected = list.FindAll(u => u.FirstName == "Oleg" && u.Age == 25);

            // Act
            var returned = userStorageService.SearchAll(u => u.FirstName == "Oleg" && u.Age == 25);

            // Assert
            CollectionAssert.AreEqual(returned.ToList(), expected);
        }

        [TestMethod]
        public void SearchAll_ExistingUsersByPredicateLastNameAndAge_CollectionReturned()
        {
            var userStorageService = new UserStorageService();
            var list = FillStorage(userStorageService);
            var expected = list.FindAll(u => u.LastName == "Olegov" && u.Age == 30);

            // Act
            var returned = userStorageService.SearchAll(u => u.LastName == "Olegov" && u.Age == 30);

            // Assert
            CollectionAssert.AreEqual(returned.ToList(), expected);
        }

        [TestMethod]
        public void SearchAll_ExistingUsersByPredicateFirstNameAndLastName_CollectionReturned()
        {
            var userStorageService = new UserStorageService();
            var list = FillStorage(userStorageService);
            var expected = list.FindAll(u => u.FirstName == "Sergey" && u.LastName == "Egorov");

            // Act
            var returned = userStorageService.SearchAll(u => u.FirstName == "Sergey" && u.LastName == "Egorov");

            // Assert
            CollectionAssert.AreEqual(returned.ToList(), expected);
        }

        [TestMethod]
        public void SearchAll_ExistingUsersByPredicateFirstNameAndLastNameAndAge_CollectionReturned()
        {
            var userStorageService = new UserStorageService();
            var list = FillStorage(userStorageService);
            var expected = list.FindAll(u => u.FirstName == "Sergey" && u.LastName == "Egorov" && u.Age == 40);

            // Act
            var returned = userStorageService.SearchAll(u => u.FirstName == "Sergey" && u.LastName == "Egorov" && u.Age == 40);

            // Assert
            CollectionAssert.AreEqual(returned.ToList(), expected);
        }

        #endregion

        #region Master-SlaveTests

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void Add_Slave_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageService(mode: UserStorageServiceMode.SlaveNode);

            // Act
            userStorageService.Add(new User
            {
                FirstName = "Oleg",
                LastName = "Olegov",
                Age = 20
            });

            // Assert - [ExpectedException]
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void Remove_Slave_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageService(mode: UserStorageServiceMode.SlaveNode);

            // Act
            userStorageService.Remove(new User
            {
                FirstName = "Oleg",
                LastName = "Olegov",
                Age = 20
            });

            // Assert - [ExpectedException]
        }

        [TestMethod]
        public void Add_Master_AllAdded()
        {
            // Arrange
            var alex = new User
            {
                FirstName = "Alex",
                LastName = "Black",
                Age = 25
            };

            var slave1 = new UserStorageServiceLog(new UserStorageService());
            var slave2 = new UserStorageService();

            var master = new UserStorageServiceLog(new UserStorageService(mode: UserStorageServiceMode.MasterNode, slaves: new IUserStorageService[] { slave1, slave2 }));

            // Act
            master.Add(alex);

            // Assert
            Assert.IsTrue(master.Count == 1 && slave1.Count == 1 && slave2.Count == 1);
        }

        [TestMethod]
        public void Remove_Master_AllRemoved()
        {
            // Arrange
            var alex = new User
            {
                FirstName = "Alex",
                LastName = "Black",
                Age = 25
            };

            var slave1 = new UserStorageServiceLog(new UserStorageService());
            var slave2 = new UserStorageService();

            var master = new UserStorageServiceLog(new UserStorageService(mode: UserStorageServiceMode.MasterNode, slaves: new IUserStorageService[] { slave1, slave2 }));
            master.Add(alex);

            // Act
            master.Remove(alex);

            // Assert
            Assert.IsTrue(master.Count == 0 && slave1.Count == 0 && slave2.Count == 0);
        }

        #endregion

        private List<User> FillStorage(UserStorageService storage)
        {
            var list = new List<User>();
            list.Add(new User { Age = 15, FirstName = "Oleg", LastName = "Egorov" });
            list.Add(new User { Age = 20, FirstName = "Stas", LastName = "Stanislavov" });
            list.Add(new User { Age = 25, FirstName = "Oleg", LastName = "Stanislavov" });
            list.Add(new User { Age = 30, FirstName = "Stas", LastName = "Olegov" });
            list.Add(new User { Age = 35, FirstName = "Volodya", LastName = "Olegov" });
            list.Add(new User { Age = 40, FirstName = "Sergey", LastName = "Egorov" });

            foreach (var u in list)
            {
                storage.Add(u);
            }

            return list;
        }
    }
}
