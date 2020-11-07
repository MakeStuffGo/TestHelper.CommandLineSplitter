using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestHelpers;

namespace CommandLineSplitter.Tests.Helpers
{
    public static class CompareArgsPasing
    {
        public static void AssertAllSplittersHasSameParseResult(this string argsString)
        {
            Assert.IsTrue(argsString.HasSameParseResult());
        }

        public static void AssertHasNArguments(this string argsString, int n)
        {
            Assert.IsTrue(CommandLineSplitterManaged.SplitCommandLine(argsString).Length == n);
            
        }

        public static bool HasSameParseResult(this string argsString)
        {
            var argsUn = CommandLineSplitterUnManaged.SplitCommandLine(argsString);
            var args = CommandLineSplitterManaged.SplitCommandLine(argsString);

            if (argsUn.Length != args.Length)
                return false;

            for (int i = 0; i < args.Length; i++)
            {
                if (argsUn[i] != args[i])
                    return false;
            }

            return true;
        }
    }
}
