using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UserStorageServices.Repositories;
using UserStorageServices.SerializationStrategy;

namespace UserStorageServices.Tests
{
    [TestClass]
    public class UserMemoryCacheWithStateTests
    {
        [TestMethod]
        public void StatrtAndFinish_BinarySerializer_Success()
        {
            // Arrange
            var userMemoryCacheWithState = new UserMemoryCacheWithState(serializer: new BinaryUserSerializationStrategy());
            var userMemoryCacheWithState1 = new UserMemoryCacheWithState(serializer: new BinaryUserSerializationStrategy());
            var list = FillStorage(userMemoryCacheWithState);

            // Act
            userMemoryCacheWithState.Finish();
            userMemoryCacheWithState1.Start();
            
            // Assert
            var a1 = userMemoryCacheWithState1.Query(u => true).ToList();
            CollectionAssert.AreEqual(a1, list);
        }

        [TestMethod]
        public void StatrtAndFinish_XmlSerializer_Success()
        {
            // Arrange
            var userMemoryCacheWithState = new UserMemoryCacheWithState(serializer: new XmlUserSerializationStrategy());
            var userMemoryCacheWithState1 = new UserMemoryCacheWithState(serializer: new XmlUserSerializationStrategy());
            var list = FillStorage(userMemoryCacheWithState);

            // Act
            userMemoryCacheWithState.Finish();
            userMemoryCacheWithState1.Start();

            // Assert
            var a1 = userMemoryCacheWithState1.Query(u => true).ToList();
            CollectionAssert.AreEqual(a1, list);
        }

        private List<User> FillStorage(UserMemoryCacheWithState storage)
        {
            var list = new List<User>();
            list.Add(new User { Age = 15, FirstName = "Oleg", LastName = "Egorov" , Id = Guid.NewGuid()});
            list.Add(new User { Age = 20, FirstName = "Stas", LastName = "Stanislavov", Id = Guid.NewGuid() });
            list.Add(new User { Age = 25, FirstName = "Oleg", LastName = "Stanislavov", Id = Guid.NewGuid() });
            list.Add(new User { Age = 30, FirstName = "Stas", LastName = "Olegov", Id = Guid.NewGuid() });
            list.Add(new User { Age = 35, FirstName = "Volodya", LastName = "Olegov", Id = Guid.NewGuid() });
            list.Add(new User { Age = 40, FirstName = "Sergey", LastName = "Egorov", Id = Guid.NewGuid() });

            foreach (var u in list)
            {
                storage.Set(u);
            }

            return list;
        }
    }
}
