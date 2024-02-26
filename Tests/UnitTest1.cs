using DupFind;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        string getLastDirectoryPart(string path)
        {
            return path.Substring(path.LastIndexOf(Path.DirectorySeparatorChar) + 1);
        }

        [TestMethod]
        public void TestMethod1()
        {
            var tmppaths = new String[] { "TestFiles/C", "TestFiles/A" };
            var paths = tmppaths.Select(x => new DirectoryInfo(x)).ToArray();
            var files = new Files().GetFiles(paths);
            var result = new FindDuplicates().Find(files);

            var first = result.First();
            Assert.IsNotNull(first);
            var orginal = first.GetOrignal(paths);

            Assert.IsTrue(getLastDirectoryPart(orginal?.DirectoryName) == "C");
            var duplicates = first.GetDuplicates(paths);
            Assert.IsTrue(getLastDirectoryPart(duplicates[0].DirectoryName) == "A");
        }


        [TestMethod]
        public void TestMethod2()
        {
            var tmppaths = new String[] { "TestFiles/A", "TestFiles/C" };
            var paths = tmppaths.Select(x => new DirectoryInfo(x)).ToArray();
            var files = new Files().GetFiles(paths);
            var result = new FindDuplicates().Find(files);

            var first = result.First();
            Assert.IsNotNull(first);
            var orginal = first.GetOrignal(paths);

            Assert.IsTrue(getLastDirectoryPart(orginal?.DirectoryName) == "A");
            var duplicates = first.GetDuplicates(paths);
            Assert.IsTrue(getLastDirectoryPart(duplicates[0].DirectoryName) == "C");
        }
    }
}