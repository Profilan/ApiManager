using System;
using System.Runtime.Remoting.Messaging;
using ApiManager.Logic.Repositories;
using ApiManager.Web.Services;
using CryptSharp;
using FluentAssertions;
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

            var logs = rep.List("", 0, 25, DateTime.Now.AddDays(-2), DateTime.Now);
            //var logs = rep.List("timestamp.desc", "", 1, 10, "");
        }

        [TestMethod]
        public void ListTasks()
        {
            var rep = new TaskRepository();

            var tasks = rep.List();
        }

        [TestMethod]
        public void HashBCryptPassword()
        {
            string cryptedPassword = Crypter.MD5.Crypt("dihap");

            cryptedPassword.Should().Be("$2y$14$6RpOSPeuMrj/JEDcUcrxC.b/SSS7EHch3vjbTuwPzhKYRaSKMGQCG");
            
        }

        [TestMethod]
        public void HashMD5ApachePassword()
        {
            string cryptedPassword = Crypter.MD5.Crypt("dihap", new CrypterOptions
              {
                { CrypterOption.Variant, MD5CrypterVariant.Apache }
              });

            bool matches = Crypter.CheckPassword("dihap", "$apr1$zXNqfF77$7TtyABmQTzi3bKQXOlXRD0");

            matches.Should().Be(true);
        }

        [TestMethod]
        public void AddUserToHtpasswd()
        {
            var htpasswd = new HtpasswdService(@"C:\Temp\htpasswd.txt");

            htpasswd.AddUser("test", "test");
        }

        [TestMethod]
        public void UpdateUserInHtpasswd()
        {
            var htpasswd = new HtpasswdService(@"C:\Temp\htpasswd.txt");


            htpasswd.GetUserList();
            htpasswd.UpdateUser("administrator", "other");
        }

        [TestMethod]
        public void DeleteUserInHtpasswd()
        {
            var htpasswd = new HtpasswdService(@"C:\Temp\htpasswd.txt");

            htpasswd.GetUserList();
            htpasswd.DeleteUser("test");
        }

        [TestMethod]
        public void TaskShouldReset()
        {
            var taskRepository = new TaskRepository();

            var task = taskRepository.GetById(new Guid("4f255c5e-3b2c-4644-880f-aba30084a19e"));
            if (task != null)
            {
               
            }

            var result = taskRepository.Reset(new Guid("4f255c5e-3b2c-4644-880f-aba30084a19e"));

        }

        [TestMethod]
        public void GetTaskById()
        {
            var rep = new TaskRepository();

            var task = rep.GetById(new Guid("E7145A24-82FB-4768-A405-AD10007BFC11"));
        }

        [TestMethod]
        public void GetQueueItems()
        {
            var rep = new QueueRepository();

            var items = rep.List();
        }
    }
}
