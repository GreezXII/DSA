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
    }
}