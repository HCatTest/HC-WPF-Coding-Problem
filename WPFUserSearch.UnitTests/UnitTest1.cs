using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using WPFUserSearch.Data.Models.DB;
using WPFUserSearch.Data.Services;

namespace WPFUserSearch.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task TestGetUsers0()
        {
            // Get all Users
            DataService dataService = new DataService();
            var set = await dataService.GetUsers();

            Assert.IsTrue(set.Count == 9);
        }

        [TestMethod]
        public async Task TestGetUsers1()
        {
            // Get all Users whos FullName contains letter 'n'
            DataService dataService = new DataService();
            var set = await dataService.GetUsers("n");

            Assert.IsTrue(set.Count == 7);
        }

        [TestMethod]
        public async Task TestGetUsersFullNames()
        {
            // Check if User's FullName was formatted properly
            DataService dataService = new DataService();
            var set = await dataService.GetUsersFullNames();

            Assert.IsTrue(set.Contains("Robert A. King"));
        }

        [TestMethod]
        public async Task TestFormatUserFullName()
        {
            // Check if User's FullName was formatted properly
            DataService dataService = new DataService();
            var set = await dataService.GetUsers("Anne");
            var fullName = dataService.FormatUserFullName(set[0]);

            Assert.IsTrue(fullName == "Anne Dodsworth");
        }
    }
}
