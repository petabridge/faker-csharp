using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Faker.Generators
{
    /// <summary>
    /// Value generator for creating fake email addresses
    /// </summary>
    public static class EmailAddresses
    {
        #region Email Address Data

        private static string[] domain_extensions = {".com", ".net", ".org", ".edu", "co.uk", ".ly", ".co", ".mobi", ".me", ".info", ".biz", ".us", ".ca", ".name"};

        private static string[] domain_names = {"gmail", "mail.google", "live", "mail.yahoo", "yahoo", "hotmail", "mindspring", "roadrunner", "aol", "vanderbilt", "web-co", "co.ram.web"};

        #endregion

        public static string Generate(bool majorDomainExtensionsOnly = false, int minLength = 10, int maxLength = 100)
        {
            throw new NotImplementedException();
        }

        public static string Human()
        {
            throw new NotImplementedException();
        }
    }
}
