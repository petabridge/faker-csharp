using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Faker.Helpers;
using Faker.Selectors;
using FluentAssertions;
using Xunit;

namespace Faker.Tests
{
    public class TypeTableTests
    {
        #region Setup / Teardown
        #endregion

        #region Tests

        [Fact(DisplayName = "Should be able to add new typeselectors to a fresh type table")]
        public void Should_Add_Type_Selectors_For_Single_Type_To_TypeTable()
        {
            //Create a new TypeTable
            var table = new TypeTable(false);

            //Create some selectors that we're going to add
            var stringSelector1 = new StringSelector();
            var stringSelector2 = new FullNameSelector();
            var stringSelector3 = new EmailSelector();

            Assert.Equal(0, table.CountSelectors<string>()); // "should have ZERO type selectors for type 'string' since we haven't added any yet"

            //Add the first selector (our default string selector)
            table.AddSelector<string>(stringSelector1);
            Assert.Equal(1, table.CountSelectors<string>()); // "should have ONE type selectors for type 'string'"

            var firstselector = table.GetSelectors<string>().First();
            firstselector.Should().BeOfType<StringSelector>();

            //Add the second selector (the full name selector)
            table.AddSelector<string>(stringSelector2);
            Assert.Equal(2, table.CountSelectors<string>()); // "should have TWO type selectors for type 'string'"

            firstselector = table.GetSelectors<string>().First();
            firstselector.Should().BeOfType<FullNameSelector>();  //Oh snap, the new front of the line should be our full name selector!

            //Add the thrid selector (the email address selector)
            table.AddSelector<string>(stringSelector3);
            Assert.Equal(3, table.CountSelectors<string>()); // "should have THREE type selectors for type 'string'"

            firstselector = table.GetSelectors<string>().First();
            firstselector.Should().BeOfType<EmailSelector>(); //Oh snap, the new front of the line should be our full name selector!
        }

        [Fact(DisplayName = "Should be able to add type selectors for multiple types to the table")]
        public void Should_Add_Type_Selectors_For_Multiple_Types_To_TypeTable()
        {
            //Create a new TypeTable
            var table = new TypeTable(false);

            //Create some string selectors that we're going to add
            var stringSelector1 = new StringSelector();

            //Create some long selectors that we're going to use...
            var longSelector1 = new LongSelector();
            var longSelector2 = new TimeStampSelector();

            Assert.Equal(0, table.CountSelectors<string>()); // "should have ZERO type selectors for type 'string' since we haven't added any yet"
            Assert.Equal(0, table.CountSelectors<long>()); // "should have ZERO type selectors for type 'long' since we haven't added any yet"

            //Add the first and only string selector (our default string selector)
            table.AddSelector<string>(stringSelector1);
            Assert.Equal(1, table.CountSelectors<string>()); //  "should have ONE type selectors for type 'string'"

            // "should have ZERO type selectors for type 'long' since we haven't added any yet"
            Assert.Equal(0, table.CountSelectors<long>()); //Assert that we haven't added any long selectors yet

            var firstStringSelector = table.GetSelectors<string>().First();
            firstStringSelector.Should().BeOfType<StringSelector>();

            var currentLongSelector = table.GetSelectors<long>().FirstOrDefault();
            Assert.Null(currentLongSelector); //Since we haven't added any long selectors yet, this should return null

            //Add the first long selector (our default long selector)
            table.AddSelector<long>(longSelector1);
            Assert.Equal(1, table.CountSelectors<string>()); // "should have ONE type selectors for type 'string'"
            Assert.Equal(1, table.CountSelectors<long>()); // "should have ONE type selectors for type 'long'"

            firstStringSelector = table.GetSelectors<string>().First();
            firstStringSelector.Should().BeOfType<StringSelector>();

            currentLongSelector = table.GetSelectors<long>().FirstOrDefault();
            currentLongSelector.Should().BeOfType<LongSelector>();

            //Add the final long selector (our timestamp selector)
            table.AddSelector<long>(longSelector2);
            Assert.Equal(1, table.CountSelectors<string>()); //  "should have ONE type selectors for type 'string'"
            Assert.Equal(2, table.CountSelectors<long>()); // "should have TWO type selectors for type 'long'"

            firstStringSelector = table.GetSelectors<string>().First();
            firstStringSelector.Should().BeOfType<StringSelector>();

            currentLongSelector = table.GetSelectors<long>().FirstOrDefault();
            currentLongSelector.Should().BeOfType<TimeStampSelector>();
        }

        [Fact(DisplayName = "If a user wants to explicitly add a new type selector to the back of the processing order, they should be able to do it.")]
        public void Should_Add_Selector_To_Back_Of_List()
        {
            //Create a new TypeTable
            var table = new TypeTable(false);

            //Create some selectors that we're going to add
            var stringSelector1 = new StringSelector();
            var stringSelector2 = new FullNameSelector();

            Assert.Equal(0, table.CountSelectors<string>()); // "should have ZERO type selectors for type 'string' since we haven't added any yet"

            //Add the first selector (our default string selector)
            table.AddSelector<string>(stringSelector1);
            Assert.Equal(1, table.CountSelectors<string>()); // "should have ONE type selectors for type 'string'"

            var firstselector = table.GetSelectors<string>().First();
            firstselector.Should().BeOfType<StringSelector>();

            //Add the second selector (FullNameSelector) to the back of the processing queue
            table.AddSelector(stringSelector2, SelectorPosition.Last);
            Assert.Equal(2, table.CountSelectors<string>()); //  "should have TWO type selectors for type 'string'"

            firstselector = table.GetSelectors<string>().First();
            firstselector.Should().BeOfType<StringSelector>();

            var lastselector = table.GetSelectors<string>().Last();
            lastselector.Should().BeOfType<FullNameSelector>();
        }

        [Fact(DisplayName = "A user should be able to clear a list of typeselectors for a given type if they wish")]
        public void Should_Clear_TypeSelector_List()
        {
            //Create a new TypeTable
            var table = new TypeTable(false);

            //Create some selectors that we're going to add
            var stringSelector1 = new StringSelector();
            var stringSelector2 = new FullNameSelector();

            Assert.Equal(0, table.CountSelectors<string>()); // "should have ZERO type selectors for type 'string' since we haven't added any yet"

            //Add some type selectors to our typetable
            table.AddSelector(stringSelector1);
            table.AddSelector(stringSelector2);

            //Check to see that our table contains at least two items...
            Assert.Equal(2, table.CountSelectors<string>()); // "should have TWO type selectors for type 'string'"

            //Clear all of the string selectors
            table.ClearSelectors<string>(); 

            //Count the new number of string selectors (should equal zero)
            Assert.Equal(0, table.CountSelectors<string>());
        }

        [Fact(DisplayName = "We should be able to get ahold of the base selector for primitive types we're working with")]
        public void Should_Get_Base_Selector_For_Primitive_Types()
        {
            //Create a new type table which uses all of the system defaults
            var table = new TypeTable();

            //Try to grab the integer selector...
            var intSelector = table.GetBaseSelector(typeof (int));
            intSelector.Should().BeOfType<IntSelector>();

            //Try to grab the float selector
            var floatSelector = table.GetBaseSelector(typeof (float));
            floatSelector.Should().BeOfType<FloatSelector>();

            //Try to grab the double selector
            var doubleSelector = table.GetBaseSelector(typeof(double));
            doubleSelector.Should().BeOfType<DoubleSelector>();

            //Try to grab the string selector
            var stringSelector = table.GetBaseSelector(typeof(string));
            stringSelector.Should().BeOfType<StringSelector>();

            //Try to grab the DateTime selector
            var dateTimeSelector = table.GetBaseSelector(typeof(DateTime));
            dateTimeSelector.Should().BeOfType<DateTimeSelector>();

            //Try to grab the Guid selector
            var guidSelector = table.GetBaseSelector(typeof(Guid));
            guidSelector.Should().BeOfType<GuidSelector>();
        }

        [Fact]
        public void TypeTable_changes_to_clone_should_not_modify_original()
        {
            var typeTable = new TypeTable();
            var typeTableIntSelectorCount = typeTable.CountSelectors(typeof (int));
            var clone = typeTable.Clone();
            var oldCloneTableIntSelectorCount = clone.CountSelectors(typeof(int));
            clone.AddSelector(new IntSelector());
            var newCloneTableSelectorCount = clone.CountSelectors(typeof (int));

            // sanity check to make sure we added the new type selector
            Assert.Equal(typeTableIntSelectorCount, oldCloneTableIntSelectorCount);
            Assert.NotEqual(oldCloneTableIntSelectorCount, newCloneTableSelectorCount);
            Assert.True(newCloneTableSelectorCount > oldCloneTableIntSelectorCount);

            // make sure we didn't modify old originak
            var newTypeTableSelectorCount = typeTable.CountSelectors(typeof(int));
            Assert.Equal(typeTableIntSelectorCount, oldCloneTableIntSelectorCount);
        }

        [Fact]
        public void TypeTable_changes_to_original_should_not_modify_clone()
        {
            var typeTable = new TypeTable();
            var typeTableIntSelectorCount = typeTable.CountSelectors(typeof(int));
            var clone = typeTable.Clone();
            var oldCloneTableIntSelectorCount = clone.CountSelectors(typeof(int));


            typeTable.AddSelector(new IntSelector());
            var newTypeTableSelectorCount = typeTable.CountSelectors(typeof(int));

            // sanity check to make sure we added the new type selector
            Assert.Equal(typeTableIntSelectorCount, oldCloneTableIntSelectorCount);
            Assert.NotEqual(typeTableIntSelectorCount, newTypeTableSelectorCount);
            Assert.True(newTypeTableSelectorCount > typeTableIntSelectorCount);

            // make sure we didn't modify old originak
            var newCloneSelectorCount = clone.CountSelectors(typeof(int));
            Assert.Equal(oldCloneTableIntSelectorCount, newCloneSelectorCount);
        }

        #endregion
    }
}
