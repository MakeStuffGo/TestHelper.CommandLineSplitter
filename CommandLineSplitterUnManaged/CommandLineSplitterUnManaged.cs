using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace TestHelpers
{
    // created from the information found here:
   // https://stackoverflow.com/a/749653/572760
    public class CommandLineSplitterUnManaged
    {
        const string FakeExeArgument = "foo.exe";

        [DllImport("shell32.dll", SetLastError = true)]
        static extern IntPtr CommandLineToArgvW(
        [MarshalAs(UnmanagedType.LPWStr)] string lpCmdLine, out int pNumArgs);

        public static string[] SplitCommandLine(string commandLine)
        {
            int argc;

            // CommandLineToArgvW may behave a bit different if not exe is given in commanline
            var argv = CommandLineToArgvW(FakeExeArgument + " " + commandLine, out argc);
            
            if (argv == IntPtr.Zero)
                throw new System.ComponentModel.Win32Exception();
            try
            {
                var args = new string[argc];
                for (var i = 0; i < args.Length; i++)
                {
                    var p = Marshal.ReadIntPtr(argv, i * IntPtr.Size);
                    args[i] = Marshal.PtrToStringUni(p);
                }

                if (string.IsNullOrEmpty(FakeExeArgument))
                    return args;

                // skip exe name
                return args = args.Skip(1).ToArray();
            }
            finally
            {
                Marshal.FreeHGlobal(argv);
            }
        }
    }
}
