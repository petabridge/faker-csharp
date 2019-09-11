using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Faker.Tests.MatcherTests
{
    public class TreeMatcherTests
    {
        private Matcher _matcher = new Matcher();

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

        #region Tests

        [Fact(DisplayName = "Should only create a tree structure that has a single node in this instance")]
        public void Should_Terminate_Tree_Structure_After_One_Node()
        {
            var flatObjectTree = new FlatObjectTree();

            _matcher.Match(flatObjectTree);

            Assert.NotNull(flatObjectTree.Id);
            Assert.NotNull(flatObjectTree.Name);
            Assert.Null(flatObjectTree.Parent); // "Should only create one node (the root) in a tree structure"
        }

        [Fact(DisplayName = "In an enviromment where a tree can support a list of child nodes, make sure the list gets created but don't populate it")]
        public void Should_Create_Tree_Structure_with_No_Children()
        {
            var objectTreeWithMultipleChildren = new ObjectTreeWithMultipleChildren();

            _matcher.Match(objectTreeWithMultipleChildren);

            Assert.NotNull(objectTreeWithMultipleChildren.Id);
            Assert.NotNull(objectTreeWithMultipleChildren.Name);
            Assert.Null(objectTreeWithMultipleChildren.Parent); // "Should only create one node (the root) in a tree structure"
            Assert.NotNull(objectTreeWithMultipleChildren.Children); // "Should have the child element array instantiated"
            Assert.Empty(objectTreeWithMultipleChildren.Children); //  "Should not have any children in the array"
        }

        #endregion
    }
}
