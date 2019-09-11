using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Faker.Tests.MatcherTests
{
    [TestFixture(Description = "Tests to verify that Faker's behavior when working with tree structures doesn't cause problems")]
    public class TreeMatcherTests
    {
        private Matcher _matcher;

        #region Tree test classes

        public class FlatObjectTree
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public FlatObjectTree Parent { get; set; }
        }

        public class ObjectTreeWithMultipleChildren
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public ObjectTreeWithMultipleChildren Parent { get; set; }
            public List<ObjectTreeWithMultipleChildren> Children { get; set; } 
        }

        #endregion

        #region Setup / Teardown

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            //Create a new matcher using the default type table
            _matcher = new Matcher();
        }

        #endregion

        #region Tests

        [Fact(DisplayName = "Should only create a tree structure that has a single node in this instance")]
        public void Should_Terminate_Tree_Structure_After_One_Node()
        {
            var flatObjectTree = new FlatObjectTree();

            _matcher.Match(flatObjectTree);

            Assert.NotNull(flatObjectTree.Id);
            Assert.NotNull(flatObjectTree.Name);
            Assert.IsNull(flatObjectTree.Parent, "Should only create one node (the root) in a tree structure");
        }

        [Fact(DisplayName = "In an enviromment where a tree can support a list of child nodes, make sure the list gets created but don't populate it")]
        public void Should_Create_Tree_Structure_with_No_Children()
        {
            var objectTreeWithMultipleChildren = new ObjectTreeWithMultipleChildren();

            _matcher.Match(objectTreeWithMultipleChildren);

            Assert.NotNull(objectTreeWithMultipleChildren.Id);
            Assert.NotNull(objectTreeWithMultipleChildren.Name);
            Assert.IsNull(objectTreeWithMultipleChildren.Parent, "Should only create one node (the root) in a tree structure");
            Assert.NotNull(objectTreeWithMultipleChildren.Children, "Should have the child element array instantiated");
            Assert.AreEqual(0, objectTreeWithMultipleChildren.Children.Count, "Should not have any children in the array");
        }

        #endregion
    }
}
