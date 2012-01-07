using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Faker.Generators
{
    /// <summary>
    /// Generator class used for creating random stings
    /// </summary>
    public static class Strings
    {
        #region String Data

        private const string AlphaChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private const string EmailFriendlyChars = "!#$%&'*+-/=?^_`{|}~.abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        #endregion

        private static readonly Random R = new Random();

        /// <summary>
        /// Generates a random alpha numeric string of the specified maxLength
        /// </summary>
        /// <param name="minLength">The minimum lenght of the random string</param>
        /// <param name="maxLength">The maxLength of the random string</param>
        /// <returns>A string</returns>
        public static string GenerateAlphaNumericString(int minLength = 10, int maxLength = 40)
        {
            var stringLength = R.Next(minLength, maxLength);

            return new string(Enumerable.Repeat(AlphaChars, stringLength).Select(x => x[R.Next(x.Length)]).ToArray());
        }

        /// <summary>
        /// Generates a random email address-compatible string of the specified maxLength
        /// </summary>
        /// <param name="minLength">The minimum lenght of the random string</param>
        /// <param name="maxLength">The maxLength of the random string</param>
        /// <returns>A string</returns>
        public static string GenerateEmailFriendlyString(int minLength = 10, int maxLength = 40)
        {
            var stringLength = R.Next(minLength, maxLength);

            return new string(Enumerable.Repeat(EmailFriendlyChars, stringLength).Select(x => x[R.Next(x.Length)]).ToArray());
        }
    }
}
