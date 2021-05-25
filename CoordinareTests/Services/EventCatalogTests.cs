using Microsoft.VisualStudio.TestTools.UnitTesting;
using Coordinare.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Coordinare.Interfaces;
using Coordinare.Models;
using Microsoft.Extensions.Configuration;

namespace Coordinare.Services.Tests
{
    [TestClass()]
    public class EventCatalogTests : Connection
    {
        private IUserCatalog _userCatalog;
        private IEventCatalog _eCatalog;
        public EventCatalogTests(IConfiguration configuration, IUserCatalog _userCatalog, IEventCatalog eservice) : base(configuration)
        {
            this._userCatalog = _userCatalog;
            _eCatalog = eservice;
        }

        public EventCatalogTests(string connectionString, IUserCatalog _userCatalog) : base(connectionString)
        {
        }
        [TestMethod()]
        public void GetAllEventsTest()
        {
            List<Event> e = _eCatalog.GetAllEvents().Result;
            Assert.IsNotNull(e);
        }

        [TestMethod()]
        public void GetEventFromIdTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CreateEventTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DeleteEventTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void UpdateEventTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetWaitingListTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void SearchByFilterTest()
        {
            Assert.Fail();
        }

    }
}