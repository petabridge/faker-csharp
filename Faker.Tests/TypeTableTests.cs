using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Faker.Selectors;
using NUnit.Framework;

namespace Faker.Tests
{
    [TestFixture(Description = "Ensures that our type table behaves as expected")]
    public class TypeTableTests
    {
        #region Setup / Teardown
        #endregion

        #region Tests

        [Test(Description = "Should be able to add new typeselectors to a fresh type table")]
        public void Should_Add_Type_Selectors_For_Single_Type_To_TypeTable()
        {
            //Create a new TypeTable
            var table = new TypeTable();

            //Create some selectors that we're going to add
            var stringSelector1 = new StringSelector();
            var stringSelector2 = new FullNameSelector();
            var stringSelector3 = new EmailSelector();

            Assert.AreEqual(0, table.CountSelectors<string>(), "should have ZERO type selectors for type 'string' since we haven't added any yet");

            //Add the first selector (our default string selector)
            table.AddSelector<string>(stringSelector1);
            Assert.AreEqual(1, table.CountSelectors<string>(), "should have ONE type selectors for type 'string'");

            var firstselector = table.GetSelectors<string>().First();
            Assert.IsInstanceOf<StringSelector>(firstselector);

            //Add the second selector (the full name selector)
            table.AddSelector<string>(stringSelector2);
            Assert.AreEqual(2, table.CountSelectors<string>(), "should have TWO type selectors for type 'string'");

            firstselector = table.GetSelectors<string>().First();
            Assert.IsInstanceOf<FullNameSelector>(firstselector); //Oh snap, the new front of the line should be our full name selector!

            //Add the thrid selector (the email address selector)
            table.AddSelector<string>(stringSelector3);
            Assert.AreEqual(3, table.CountSelectors<string>(), "should have THREE type selectors for type 'string'");

            firstselector = table.GetSelectors<string>().First();
            Assert.IsInstanceOf<EmailSelector>(firstselector); //Oh snap, the new front of the line should be our full name selector!
        }

        #endregion
    }
}
