using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Faker.Selectors
{
    /// <summary>
    /// Class which contains all of the special field regular expressions
    /// </summary>
    public static class SpecialFieldsRegex
    {
        public const string FullNameRegex = "\b[fF]ull(_)?[nN]ame|\b[nN]ame";
        public const string FirstNameRegex = "\b[fF]irst(_)?[nN]ame";
        public const string LastNameRegex = "\b[lL]ast(_)?[nN]ame";
        public const string EmailRegex = "\b[eE]mail(_)?[aA]ddress|\b[eE]mail";
    }
}
