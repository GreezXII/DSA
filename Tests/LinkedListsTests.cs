namespace Tests
{
    [TestClass]
    public class LinkedListsTests
    {
        [TestMethod]
        public void AddWhenNotEmpty_Success()
        {
            int maxNumber = 10;
            var linkedList = new LinkedLists.LinkedList<int>();
            for (int i = 0; i < maxNumber; i++)
                linkedList.AddLast(i);

            int count = 0;
            foreach (var node in linkedList)
            {
                Assert.AreEqual(node, count);
                count++;
            }
            Assert.AreEqual(count, maxNumber);
            Assert.AreEqual(count, linkedList.Count());
        }
    }
}