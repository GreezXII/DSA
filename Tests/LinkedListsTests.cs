namespace Tests
{
    [TestClass]
    public class LinkedListsTests
    {
        [TestMethod]
        public void AddWhenNotEmpty_Success()
        {
            var linkedList = new LinkedLists.LinkedList<int>();
            linkedList.Add(1);
            linkedList.Add(2);
            linkedList.Add(3);
            linkedList.Add(4);
            int count = 0;
            foreach(var number in linkedList)
            {
                count++;
            }
            Assert.AreEqual(count, 4);
        }
    }
}