using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Faker.Helpers;

namespace Faker.Generators
{
    /// <summary>
    /// Generator used for generating names
    /// </summary>
    public static class Names
    {
        #region Name Data
        private static string[] first_names = {
                                                  "Aaron",
                                                  "Richard",
                                                  "Paul",
                                                  "Janet",
                                                  "Ryan",
                                                  "John",
                                                  "Cameron",
                                                  "Krysta",
                                                  "Janice",
                                                  "David",
                                                  "Michael",
                                                  "Thomas",
                                                  "George",
                                                  "William",
                                                  "Albert",
                                                  "Shawna",
                                                  "Hawa",
                                                  "Malgoisa",
                                                  "Yesenia",
                                                  "Luther",
                                                  "Gabriel",
                                                  "Abe",
                                                  "Etesh",
                                                  "Sanjay",
                                                  "Carlos",
                                                  "Steven",
                                                  "Joshua",
                                                  "Caroline",
                                                  "Maya",
                                                  "Giang",
                                                  "Roberto",
                                                  "Fei",
                                                  "Luiz",
                                                  "Viktor",
                                                  "Jamie",
                                                  "Santos",
                                                  "Abrar",
                                                  "Jung",
                                                  "Beti",
                                                  "Rodney",
                                                  "Christopher",
                                                  "Alision",
                                                  "Christina",
                                                  "Rachel",
                                                  "Jennifer",
                                                  "Cecilia",
                                                  "Vincenzio",
                                                  "Mercedes",
                                                  "Dominic",
                                                  "Jade",
                                                  "Crystal"
                                              };

        private static string[] last_names = {
                                                 "Cavallo",
                                                 "Alvarez",
                                                 "Schmidt",
                                                 "Schwartz",
                                                 "Smith",
                                                 "Burris",
                                                 "Atell",
                                                 "Vasquez",
                                                 "McCarter",
                                                 "Sterling",
                                                 "Soo",
                                                 "Bek",
                                                 "Vlaskovits",
                                                 "Kravitz",
                                                 "Arland",
                                                 "Goldman",
                                                 "Sneath",
                                                 "Seow",
                                                 "Bakshi"
                                             };

        #endregion  

        /// <summary>
        /// Generates a random first name
        /// </summary>
        /// <returns>A first name in string format</returns>
        public static string First()
        {
            return first_names.GetRandom();
        }

        /// <summary>
        /// Generates a random last name
        /// </summary>
        /// <returns>A last name in string format</returns>
        public static string Last()
        {
            return last_names.GetRandom();
        }

        /// <summary>
        /// Generates a random full name (first name + last name)
        /// </summary>
        /// <returns>A full name in string format</returns>
        public static string FullName()
        {
            return string.Format("{0} {1}", First(), Last());
        }
    }
}
