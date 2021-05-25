using Microsoft.VisualStudio.TestTools.UnitTesting;
using Coordinare.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Coordinare.Interfaces;
using Coordinare.Models;

namespace Coordinare.Services.Tests
{
    [TestClass()]
    public class EventCatalogTests
    {
        [TestMethod()]
        public void CreateEventTest()
        {
            //arrange
            UserCatalog UC = new UserCatalog(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CoordinareDB21;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            EventCatalog EC = new EventCatalog(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CoordinareDB21;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False", UC);
            RoomCatalog RC = new RoomCatalog(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CoordinareDB21;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            User newUser = new User("karl", "Karl54321", "54321", "22446688", "@karl.com", true, true, true);
            UC.CreateUserAsync(newUser);
            newUser = UC.GetAllUsersAsync().Result.Last();
            Room newRoom = new Room("50", 200);
            RC.CreateRoomAsync(newRoom);
            Event newEvent = new Event(TimeSpan.FromHours(5), newUser, "50", "testevent", DateTime.Today, "info", 12, DateTime.Now );
            bool result = false;
            //act
            EC.CreateEvent(newEvent);
            EC.Events = EC.GetAllEvents().Result;
            if (EC.Events.Last().EventName == newEvent.EventName)
            {
                result = true;
                newEvent = EC.GetAllEvents().Result.Last();
                EC.DeleteEvent(newEvent.Event_ID);
                UC.DeleteUserAsync(newUser.User_ID); 
                RC.DeleteRoomAsync("50");
            }
            //assert
            Assert.AreEqual(true, result);
        }
    }
}