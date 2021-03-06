﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Xunit;

namespace Faker.Tests.MatcherTests
{
    public class NestedPocoMatcherTests
    {
        public const string ValidEmailRegex = @"(\b[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}\b)";

        public Regex _valid_email_regex = new Regex(ValidEmailRegex, RegexOptions.IgnoreCase);

        private Matcher _matcher = new Matcher();

        #region Nested POCO Test Classes...

        public class SpecialFieldsTestClass
        {
            public int UserID { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public long Timestamp { get; set; }
            public DateTime DateRegistered { get; set; }
        }

        public class ComplexPocoTestClass
        {
            public float Float1 { get; set; }
            public float Float2 { get; set; }
            public long Long1 { get; set; }
            public double Double1 { get; set; }
            public Guid Guid1 { get; set; }
            public SpecialFieldsTestClass SpecialClass { get; set; }
            public string SampleString { get; set; }
        }

        public class ComplexPrivatePocoTestClass
        {
            public float Float1 { get; set; }
            public float Float2 { get; set; }
            public long Long1 { get; set; }
            public double Double1 { get; set; }
            public Guid Guid1 { get; set; }
            private SpecialFieldsTestClass SpecialClass { get; set; }
            public string SampleString { get; set; }
        }

        public class NestedPocoTestClass
        {
            public class NestedNestedClass
            {
                public string Email { get; set; }
                public long Timestamp { get; set; }
                public DateTime DateRegistered { get; set; }
            }

            public float Float1 { get; set; }
            public float Float2 { get; set; }
            public long Long1 { get; set; }
            public double Double1 { get; set; }
            public Guid Guid1 { get; set; }
            public NestedNestedClass NestedClassInstance { get; set; }
            public string SampleString { get; set; }
        }

        public class PrivateNestedPocoTestClass
        {
            public class NestedNestedClass
            {
                public string Email { get; set; }
                public long Timestamp { get; set; }
                public DateTime DateRegistered { get; set; }
            }

            public float Float1 { get; set; }
            public float Float2 { get; set; }
            public long Long1 { get; set; }
            public double Double1 { get; set; }
            public Guid Guid1 { get; set; }
            private NestedNestedClass NestedClassInstance { get; set; }
            public string SampleString { get; set; }
        }

        #endregion

        #region Tests

        [Fact(DisplayName = "Matcher should inject the values of a subclass in addition to those of the parent class")]
        public void Should_Populate_Fields_Of_SubClass()
        {
            //Create a new instance of our test class
            var testInstance = new ComplexPrivatePocoTestClass();

            //Match the fields...
            _matcher.Match(testInstance);

            /* ASSERTIONS */

            //Assert that all of the fields on the main class have been injected and instantiated
            Assert.NotEqual(default(double), testInstance.Double1);
            Assert.NotEqual(default(float), testInstance.Float1);
            Assert.NotEqual(default(float), testInstance.Float2);
            Assert.NotEqual(default(long), testInstance.Long1);
            Assert.NotEqual(default(Guid), testInstance.Guid1);
            Assert.NotNull(testInstance.SampleString);
        }

        [Fact(DisplayName = "Matcher shouldn't throw any errors by trying to access a private member")]
        public void Should_Not_Populate_Fields_Of_Private_SubClass()
        {
            //Create a new instance of our test class
            var testInstance = new ComplexPocoTestClass();

            //Match the fields...
            _matcher.Match(testInstance);

            /* ASSERTIONS */

            //Assert that all of the fields on the sub-class have been injected and instantiated
            Assert.NotNull(testInstance.SpecialClass);
            Assert.NotEqual(default(DateTime), testInstance.SpecialClass.DateRegistered);
            Assert.NotEqual(default, testInstance.SpecialClass.UserID);
            Assert.NotEqual(default(long), testInstance.SpecialClass.Timestamp);
            Assert.NotNull(testInstance.SpecialClass.Name);
            Assert.NotNull(testInstance.SpecialClass.Email);
            Assert.Matches(_valid_email_regex, testInstance.SpecialClass.Email);

            //Assert that all of the fields on the main class have been injected and instantiated
            Assert.NotEqual(default(double), testInstance.Double1);
            Assert.NotEqual(default(float), testInstance.Float1);
            Assert.NotEqual(default(float), testInstance.Float2);
            Assert.NotEqual(default(long), testInstance.Long1);
            Assert.NotEqual(default(Guid), testInstance.Guid1);
            Assert.NotNull(testInstance.SampleString);
        }

        [Fact(DisplayName = "Should be able to populate the fields of a class defined within another class and included as an instance member")]
        public void Should_Populate_Fields_Of_Nested_Subclass()
        {
            //Create a new instance of our test class
            var testInstance = new NestedPocoTestClass();

            //Match the fields...
            _matcher.Match(testInstance);

            /* ASSERTIONS */

            //Assert that all of the fields on the sub-class have been injected and instantiated
            Assert.NotNull(testInstance.NestedClassInstance);
            Assert.NotEqual(default(DateTime), testInstance.NestedClassInstance.DateRegistered);
            Assert.NotEqual(default(long), testInstance.NestedClassInstance.Timestamp);
            Assert.NotNull(testInstance.NestedClassInstance.Email);
            Assert.Matches(_valid_email_regex, testInstance.NestedClassInstance.Email);

            //Assert that all of the fields on the main class have been injected and instantiated
            Assert.NotEqual(testInstance.Double1, default(double));
            Assert.NotEqual(testInstance.Float1, default(float));
            Assert.NotEqual(testInstance.Float2, default(float));
            Assert.NotEqual(testInstance.Long1, default(long));
            Assert.NotEqual(testInstance.Guid1, default(Guid));
            Assert.NotNull(testInstance.SampleString);
        }

        [Fact(DisplayName = "Matcher shouldn't throw any errors by trying to access a private member (nested subclass)")]
        public void Should_Not_Populate_Fields_Of_Private_Nested_SubClass()
        {
            //Create a new instance of our test class
            var testInstance = new PrivateNestedPocoTestClass();

            //Match the fields...
            _matcher.Match(testInstance);

            /* ASSERTIONS */
            //Assert that all of the fields on the main class have been injected and instantiated
            Assert.NotEqual(testInstance.Double1, default(double));
            Assert.NotEqual(testInstance.Float1, default(float));
            Assert.NotEqual(testInstance.Float2, default(float));
            Assert.NotEqual(testInstance.Long1, default(long));
            Assert.NotEqual(testInstance.Guid1, default(Guid));
            Assert.NotNull(testInstance.SampleString);
        }

        #endregion
    }
}
