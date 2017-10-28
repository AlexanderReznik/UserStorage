﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
        [ExpectedException(typeof(ArgumentException))]
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
        [ExpectedException(typeof(ArgumentException))]
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
        [ExpectedException(typeof(ArgumentException))]
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
            var user = new User {Age = 15, FirstName = "Oleg", LastName = "Olegov"};
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
        public void SearchAll_ExistingUsersByFirstName_ExceptionTrown()
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
        public void SearchAll_ExistingUsersByPredicateFirstName_ExceptionTrown()
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
        public void SearchAll_ExistingUsersByPredicateLastName_ExceptionTrown()
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
        public void SearchAll_ExistingUsersByPredicateAge_ExceptionTrown()
        {
            var userStorageService = new UserStorageService();
            var list = FillStorage(userStorageService);
            var expected = list.FindAll(u => u.Age <= 30);

            // Act
            var returned = userStorageService.SearchAll(u => u.Age <= 30);

            // Assert
            CollectionAssert.AreEqual(returned.ToList(), expected);
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
