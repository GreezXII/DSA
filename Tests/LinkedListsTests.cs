namespace Tests
{
    [TestClass]
    public class LinkedListsTests
    {
        [TestMethod]
        public void AddLast_Success()
        {
            const int maxNumber = 10;
            var linkedList = new LinkedLists.LinkedList<int>();
            for (var i = 0; i < maxNumber; i++)
                linkedList.AddLast(i);

            var count = 0;
            foreach (var node in linkedList)
            {
                Assert.AreEqual(node, count);
                count++;
            }
            Assert.AreEqual(count, maxNumber);
            Assert.AreEqual(count, linkedList.Count());
        }


        [TestMethod]
        public void AddFirst_Success()
        {
            const int maxNumber = 10;
            var linkedList = new LinkedLists.LinkedList<int>();
            for (int i = 0; i < maxNumber; i++)
                linkedList.AddFirst(i);

            int index = maxNumber - 1;
            int count = 0;
            foreach (var node in linkedList)
            {
                Assert.AreEqual(node, index);
                index--;
                count++;
            }
            Assert.AreEqual(count, maxNumber);
            Assert.AreEqual(count, linkedList.Count());
        }

        [TestMethod]
        public void MoveToFront_Success()
        {
            var linkedList = new LinkedLists.LinkedList<int>();
            linkedList.AddLast(1);
            linkedList.AddLast(2);
            linkedList.AddLast(3);

            var nodeToMove = linkedList.First;
            linkedList.MoveToFront(nodeToMove!);
            var rightOrder = new[] { 1, 2, 3 };
            int index = 0;
            foreach (int node in linkedList)
                Assert.AreEqual(node, rightOrder[index++]);

            nodeToMove = linkedList.First!.Next;
            linkedList.MoveToFront(nodeToMove!);
            rightOrder = new[] { 2, 1, 3 };
            index = 0;
            foreach (int node in linkedList)
                Assert.AreEqual(node, rightOrder[index++]);
            
            nodeToMove = linkedList.First!.Next!.Next;
            linkedList.MoveToFront(nodeToMove!);
            rightOrder = new[] { 3, 2, 1 };
            index = 0;
            foreach (int node in linkedList)
                Assert.AreEqual(node, rightOrder[index++]);
        }
    }
}