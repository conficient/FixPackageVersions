using FixPackageVersions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace FixPackageVersionsTests
{
    [TestClass]
    public class FixRefs_Tests
    {
        [TestMethod]
        public void BlankInputThrows()
        {
            const string empty = "";

            Assert.ThrowsException<ArgumentNullException>(() => 
            {
                var _ = FixRefs.Fix(empty);
            });
        }

        [TestMethod]
        public void ParsesFragmentCorrectly()
        {

            var input = ReadFile("file1.xml");
            var expected = ReadFile("file2.xml");

            var actual = FixRefs.Fix(input);

            Console.WriteLine(input);
            Console.WriteLine(actual);

            Assert.AreEqual(expected, actual);
        }

        private string ReadFile(string filename)
        {
            string path = @$".\TestFiles\{filename}";
            if (!File.Exists(path))
                throw new FileNotFoundException(path);
            return File.ReadAllText(path);
        }
    }
}
