using DupFind;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var paths = new String[]  { "TestFiles"};
            var files = new Files().GetFiles(paths);
            var result = new CompareBySize().CompareList(files);
            var sorted = new SortByPath().Sort(paths, result);

            var first = result.First();
            Assert.IsNotNull(first);



            //Assert.IsNotNull(first.Item1);
            //Assert.AreEqual(first.Item2.Count, 2);

            //Assert.AreEqual(first.Item1.DirectoryName + "\\" + first.Item1.FullName, "TestFiles\\A\\aFile-dup.txt");
            //Assert.AreEqual(first.Item2.First().DirectoryName + "\\" + first.Item2.First().FullName, "TestFiles\\B\\aFile-dup.txt");
            //Assert.AreEqual(first.Item2.Skip(1).First().DirectoryName + "\\" + first.Item2.Skip(1).First().FullName, "TestFiles\\B\\aFile.txt");
        }
    }
}