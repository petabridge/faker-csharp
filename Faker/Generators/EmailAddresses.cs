using Faker.Helpers;

namespace Faker.Generators
{
    /// <summary>
    ///     Value generator for creating fake email addresses
    /// </summary>
    public static class EmailAddresses
    {
        /// <summary>
        ///     Returns a randomly generated email address
        /// </summary>
        /// <param name="majorDomainExtensionsOnly">
        ///     If true, only uses major domain extensions (.com, .net, .org, and .edu) when it
        ///     generates email address. Default is false.
        /// </param>
        /// <param name="minLength">The minimum length of a generated email address</param>
        /// <param name="maxLength">The maximum length of a generated email address</param>
        /// <returns>a string containing a valid email address</returns>
        public static string Generate(bool majorDomainExtensionsOnly = false, int minLength = 10, int maxLength = 100)
        {
            var domainExtension = domain_extensions.GetRandom();

            if (majorDomainExtensionsOnly)
            {
                domainExtension = major_domain_extensions.GetRandom();
            }

            var domain = string.Format("@{0}{1}", domain_names.GetRandom(),
                domainExtension);

            var newMax = maxLength - domain.Length;

            if (newMax < minLength)
                newMax += minLength; //Screw it, we're not getting OOR errors

            var lowerPart = Strings.GenerateEmailFriendlyString(minLength, newMax);

            return string.Format("{0}{1}",
                lowerPart,
                domain);
        }

        /// <summary>
        ///     Returns a human-looking email address
        /// </summary>
        /// <param name="majorDomainExtensionsOnly">
        ///     If true, only uses major domain extensions (.com, .net, .org, and .edu) when it
        ///     generates email address. Default is false.
        /// </param>
        /// <returns>a string containing a valid email address</returns>
        public static string Human(bool majorDomainExtensionsOnly = false)
        {
            var domainExtension = domain_extensions.GetRandom();

            if (majorDomainExtensionsOnly)
            {
                domainExtension = major_domain_extensions.GetRandom();
            }

            return string.Format("{0}.{1}@{2}{3}",
                Names.First(),
                Names.Last(),
                domain_names.GetRandom(),
                domainExtension);
        }

        #region Email Address Data

        private static readonly string[] major_domain_extensions = {".com", ".net", ".org", ".edu"};

        private static readonly string[] domain_extensions =
        {
            ".com", ".net", ".org", ".edu", "co.uk", ".ly", ".co",
            ".mobi", ".me", ".info", ".biz", ".us", ".ca", ".name"
        };

        private static readonly string[] domain_names =
        {
            "gmail", "mail.google", "live", "mail.yahoo", "yahoo",
            "hotmail", "mindspring", "roadrunner", "aol", "vanderbilt", "web-co", "co.ram.web"
        };

        #endregion
    }
}