using FilesCompare;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var result = new Files().Compare(new string[] { "TestFiles" });
            var first = result.First();
            Assert.IsNotNull(first);
            Assert.IsNotNull(first.Item1);
            Assert.AreEqual(first.Item2.Count, 2);
        }
    }
}