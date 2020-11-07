using CommandLineSplitter.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommandLineSplitter.Tests
{

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestSingleArg()
        {
            $"--singleArg".AssertAllSplittersHasSameParseResult();
        }

        [TestMethod]
        public void TestMultiArgs()
        {
            $"--arg1 --arg2 --arg3".AssertAllSplittersHasSameParseResult();
        }

        [TestMethod]
        public void TestMultiArgsWithValues()
        {
            $"--arg1=500 --arg2=myarg --arg3".AssertAllSplittersHasSameParseResult();
        }

        [TestMethod]
        public void TestQuotedArgs()
        {
            var quoted = @"--path=""c:\app data""";
            quoted.AssertAllSplittersHasSameParseResult();
            quoted.AssertHasNArguments(1);
        }

        [TestMethod]
        public void TestFromStackoverflow1() // https://stackoverflow.com/a/24829691/572760
        {
            var test = "\"C:\\Program Files\"";
            test.AssertAllSplittersHasSameParseResult();
            test.AssertHasNArguments(1);
        }

        [TestMethod]
        public void TestFromStackoverflow2() // https://stackoverflow.com/a/24829691/572760
        {
            var test = "\"He whispered to her \\\"I love you\\\".\"";
            test.AssertAllSplittersHasSameParseResult();
            test.AssertHasNArguments(1);
        }

        [TestMethod]
        public void TestFromStackoverflow3() // https://stackoverflow.com/a/24829691/572760
        {
            var test = "\"He whispered to her \\\"I love you\\\".\"";
            test.AssertAllSplittersHasSameParseResult();
        }

        [TestMethod]
        public void TestFromStackoverflow4() // https://stackoverflow.com/a/24829691/572760
        {
            var test = "a\" \\\"asdsd \\\"dsdsd AAA\"b";
            test.AssertAllSplittersHasSameParseResult();
        }

        [TestMethod]
        public void TestFromStackoverflow5() // https://stackoverflow.com/a/24829691/572760
        {
            var test = "\" \\\"asdsd \\\"dsdsd \"basds";
            test.AssertAllSplittersHasSameParseResult();
        }

        [TestMethod]
        public void TestFromStackoverflow6() // https://stackoverflow.com/a/24829691/572760
        {
            var test = "sdsd\" \\\"asdsd \\\"dsdsd \"";
            test.AssertAllSplittersHasSameParseResult();
        }

        [TestMethod]
        public void TestFromStackoverflow7() // https://stackoverflow.com/a/24829691/572760
        {
            var test = "\"A\\\"asdsd \\\"dsdsdA\"";
            test.AssertAllSplittersHasSameParseResult();
        }

    }
}
