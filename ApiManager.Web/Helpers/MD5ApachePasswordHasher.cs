using CryptSharp;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiManager.Web.Helpers
{
    public class MD5ApachePasswordHasher : IPasswordHasher
    {
        public string HashPassword(string password)
        {
            string cryptedPassword = Crypter.MD5.Crypt(password, new CrypterOptions
              {
                { CrypterOption.Variant, MD5CrypterVariant.Apache }
              });

            return cryptedPassword;
        }

        public PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            var matches = Crypter.CheckPassword(providedPassword, hashedPassword);

            if (matches)
            {
                return PasswordVerificationResult.Success;
            }
            else
            {
                return PasswordVerificationResult.Failed;
            }
        }
    }
}