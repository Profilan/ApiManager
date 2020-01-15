using System;
using ApiManager.Logic.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApiManager.LogicTests
{
    [TestClass]
    public class DataTest
    {
        [TestMethod]
        public void ListUsers()
        {
            var rep = new UserRepository();

            var users = rep.List();
        }

        [TestMethod]
        public void GetUser()
        {
            var rep = new UserRepository();

            var user = rep.GetById(49);
        }

        [TestMethod]
        public void ListDebtors()
        {
            var rep = new DebtorRepository();

            var debtors = rep.List();
        }

        [TestMethod]
        public void ListLogs()
        {
            var rep = new LogRepository();

            //var logs = rep.List("timestamp.desc", "", 1, 10, "");
        }
    }
}
