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

        [Test(Description = "Should only create a tree structure that has a single node in this instance")]
        public void Should_Terminate_Tree_Structure_After_One_Node()
        {
            var flatObjectTree = new FlatObjectTree();

            _matcher.Match(flatObjectTree);

            Assert.IsNotNullOrEmpty(flatObjectTree.Id);
            Assert.IsNotNullOrEmpty(flatObjectTree.Name);
            Assert.IsNull(flatObjectTree.Parent, "Should only create one node (the root) in a tree structure");
        }

        [Test(Description = "In an enviromment where a tree can support a list of child nodes, make sure the list only goes 1 tier deep")]
        public void Should_Create_Tree_Structure_with_only_one_layer_of_children()
        {
            var objectTreeWithMultipleChildren = new ObjectTreeWithMultipleChildren();

            _matcher.Match(objectTreeWithMultipleChildren);

            Assert.IsNotNullOrEmpty(objectTreeWithMultipleChildren.Id);
            Assert.IsNotNullOrEmpty(objectTreeWithMultipleChildren.Name);
            Assert.IsNull(objectTreeWithMultipleChildren.Parent, "Should only create one node (the root) in a tree structure");
            Assert.IsNotNull(objectTreeWithMultipleChildren.Children);
            Assert.IsTrue(objectTreeWithMultipleChildren.Children.All(x => x.Children == null));
        }

        #endregion
    }
}
