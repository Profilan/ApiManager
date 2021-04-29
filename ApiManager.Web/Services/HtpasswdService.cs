using ApiManager.Web.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace ApiManager.Web.Services
{
    public class HtpasswdService
    {
        private string _fileName;

        private string _htpasswdCache;
        private Dictionary<string, string> _userListCache;

        protected const string REGULAR_USER_PASSWORD = "~^([^:]+):(.+)$~im";

        public HtpasswdService(string fileName)
        {
            _fileName = fileName;
            CreateFileIfNotExistant();
        }

        public void AddUser(string username, string password)
        {
            string newContent = GetHtpasswdContent();

            newContent += GetNewUserEncodedString(username, password);

            ReplaceHtpasswdContent(newContent);
        }

        public bool DeleteUser(string username)
        {
            string newContent = "";
            bool usernameDeleted = false;

            string content = GetHtpasswdContent();

            var records = content.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var record in records)
            {
                var cols = record.Split(new[] { ':' });
                if (cols[0] == username)
                {
                    usernameDeleted = true;
                }
                else newContent += record + "\n";
            }

            if (usernameDeleted)
            {
                ReplaceHtpasswdContent(newContent);
            }

            return usernameDeleted;
        }

        public void UpdateUser(string username, string password)
        {
            if (UserExists(username))
            {
                DeleteUser(username);
            }

            AddUser(username, password);
        }

        private bool UserExists(string username)
        {
            var userList = GetUserList();

            foreach (var user in userList)
            {
                if (user.Key == username)
                {
                    return true;
                }
            }

            return false;
        }

        private string GetHtpasswdContent()
        {
            if (_htpasswdCache == null)
            {
                _htpasswdCache = File.ReadAllText(_fileName);
            }

            return _htpasswdCache;
        }

        private void UpdateHtpasswdContent()
        {
            _htpasswdCache = null;
            _userListCache = null;
            GetUserList();
        }

        public Dictionary<string, string> GetUserList()
        {
            if (_userListCache == null)
            {
                var result = new Dictionary<string, string>();

                string content = GetHtpasswdContent();

                var records = content.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var record in records)
                {
                    var cols = record.Split(new[] { ':' });
                    result[cols[0]] = cols[1];
                }

                _userListCache = result;
            }

            return _userListCache;
        }

        private string EncodePassword(string password)
        {
            var hasher = new MD5ApachePasswordHasher();

            return hasher.HashPassword(password);
        }

        private string GetNewUserEncodedString(string username, string password)
        {
            return username + ":" + EncodePassword(password) + "\n";
        }

        private void ReplaceHtpasswdContent(string newContent)
        {
            
                File.WriteAllText(_fileName, newContent);
                

                UpdateHtpasswdContent();

            
            
        }

        private void CreateFileIfNotExistant()
        {
            if (!File.Exists(_fileName))
            {
                using (FileStream fs = File.Create(_fileName))
                {
                    fs.Close();
                }
                
            }
        }

    }
}