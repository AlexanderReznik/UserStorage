using System;
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
    }
}
