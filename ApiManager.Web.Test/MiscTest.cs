using System;
using ApiManager.Web.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApiManager.Web.Test
{
    [TestClass]
    public class MiscTest
    {
        [TestMethod]
        public void AddUserToHtpasswd()
        {
            var htpasswd = new HtpasswdService(@"C:\Temp\htpasswd.txt");

            htpasswd.AddUser("administrator", "dihap");
        }
    }
}
