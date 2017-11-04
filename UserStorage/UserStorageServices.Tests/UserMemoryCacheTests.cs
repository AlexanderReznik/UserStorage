using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UserStorageServices.Tests
{
    [TestClass]
    public class UserMemoryCacheTests
    {
        [TestMethod]
        public void Set_User_Seted()
        {
            // Arrange
            var userMemoryCache = new UserMemoryCache();

            // Act
            userMemoryCache.Set(new User()
            {
                Age = 46,
                FirstName = "qwerty",
                LastName = "qwertyuiop",
                Id = Guid.NewGuid()
            });

            // Assert
            Assert.AreEqual(userMemoryCache.Count, 1);
        }

        [TestMethod]
        public void Delete_User_Deleted()
        {
            // Arrange
            var userMemoryCache = new UserMemoryCache();
            var user = new User()
            {
                Age = 46,
                FirstName = "qwerty",
                LastName = "qwertyuiop",
                Id = Guid.NewGuid()
            };
            userMemoryCache.Set(user);
            // Act
            userMemoryCache.Delete(user);

            // Assert
            Assert.AreEqual(userMemoryCache.Count, 0);
        }

        [TestMethod]
        public void Query_User_Found()
        {
            // Arrange
            var userMemoryCache = new UserMemoryCache();
            var user = new User()
            {
                Age = 46,
                FirstName = "qwerty",
                LastName = "qwertyuiop",
                Id = Guid.NewGuid()
            };
            userMemoryCache.Set(user);
            // Act
            var result = userMemoryCache.Query(u => u.Age == 46);

            // Assert
            Assert.AreEqual(result.FirstOrDefault().FirstName, "qwerty");
        }

        [TestMethod]
        public void Get_User_Found()
        {
            // Arrange
            var userMemoryCache = new UserMemoryCache();
            var user = new User()
            {
                Age = 46,
                FirstName = "qwerty",
                LastName = "qwertyuiop",
                Id = Guid.NewGuid()
            };
            userMemoryCache.Set(user);
            // Act
            var result = userMemoryCache.Get(user.Id);

            // Assert
            Assert.AreEqual(result, user);
        }
    }
}
