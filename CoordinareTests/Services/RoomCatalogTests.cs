using Microsoft.VisualStudio.TestTools.UnitTesting;
using Coordinare.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Coordinare.Models;

namespace Coordinare.Services.Tests
{
    [TestClass()]
    public class RoomCatalogTests
    {
        [TestMethod()]
        public void CreateRoomAsyncTest()
        {
            //arrange
            RoomCatalog RC = new RoomCatalog(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CoordinareDB21;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            Room newroom = new Room("50", 100);
            bool result = false;
            RC.DeleteRoomAsync("50");

            //act
            if (RC.CreateRoomAsync(newroom).Result == true)
            {
                result = true;
                RC.DeleteRoomAsync("50");
            }
            //assert
            Assert.AreEqual(true, result);
        }
    }
}