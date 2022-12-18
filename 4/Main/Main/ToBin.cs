using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Main
{
    internal class Kernel32
    {

        [StructLayout(LayoutKind.Sequential)]
        public struct SECURITY_ATTRIBUTES
        {
            public int nLength;
            public IntPtr lpSecurityDescriptor;
            public int bInheritHandle;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct PROCESS_INFORMATION
        {
            public IntPtr hProcess;
            public IntPtr hThread;
            public int dwProcessId;
            public int dwThreadId;
        }

        const uint ZERO_FLAG = 0x00000000;
        const uint CREATE_BREAKAWAY_FROM_JOB = 0x01000000;
        const uint CREATE_DEFAULT_ERROR_MODE = 0x04000000;
        const uint CREATE_NEW_CONSOLE = 0x00000010;
        const uint CREATE_NEW_PROCESS_GROUP = 0x00000200;
        const uint CREATE_NO_WINDOW = 0x08000000;
        const uint CREATE_PROTECTED_PROCESS = 0x00040000;
        const uint CREATE_PRESERVE_CODE_AUTHZ_LEVEL = 0x02000000;
        const uint CREATE_SEPARATE_WOW_VDM = 0x00001000;
        const uint CREATE_SHARED_WOW_VDM = 0x00001000;
        const uint CREATE_SUSPENDED = 0x00000004;
        const uint CREATE_UNICODE_ENVIRONMENT = 0x00000400;
        const uint DEBUG_ONLY_THIS_PROCESS = 0x00000002;
        const uint DEBUG_PROCESS = 0x00000001;
        const uint DETACHED_PROCESS = 0x00000008;
        const uint EXTENDED_STARTUPINFO_PRESENT = 0x00080000;
        const uint INHERIT_PARENT_AFFINITY = 0x00010000;


        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        struct STARTUPINFO
        {
            public Int32 cb;
            public string lpReserved;
            public string lpDesktop;
            public string lpTitle;
            public Int32 dwX;
            public Int32 dwY;
            public Int32 dwXSize;
            public Int32 dwYSize;
            public Int32 dwXCountChars;
            public Int32 dwYCountChars;
            public Int32 dwFillAttribute;
            public Int32 dwFlags;
            public Int16 wShowWindow;
            public Int16 cbReserved2;
            public IntPtr lpReserved2;
            public IntPtr hStdInput;
            public IntPtr hStdOutput;
            public IntPtr hStdError;
        }
        public static int si;
        public static int pi;
        public static void CreateProcess(string reference, int arg1, int arg2, int arg3, bool security, uint flag, int arg4, int arg5, StartupInfo startupinfo, ProcessInfo procinfo) {
            procinfo.flag = flag;
           
        }
    }

    class ProcessInfo {

        const uint ZERO_FLAG = 0x00000000;
        const uint CREATE_BREAKAWAY_FROM_JOB = 0x01000000;
        const uint CREATE_DEFAULT_ERROR_MODE = 0x04000000;
        const uint CREATE_NEW_CONSOLE = 0x00000010;
        const uint CREATE_NEW_PROCESS_GROUP = 0x00000200;
        const uint CREATE_NO_WINDOW = 0x08000000;
        const uint CREATE_PROTECTED_PROCESS = 0x00040000;
        const uint CREATE_PRESERVE_CODE_AUTHZ_LEVEL = 0x02000000;
        const uint CREATE_SEPARATE_WOW_VDM = 0x00001000;
        const uint CREATE_SHARED_WOW_VDM = 0x00001000;
        const uint CREATE_SUSPENDED = 0x00000004;
        const uint CREATE_UNICODE_ENVIRONMENT = 0x00000400;
        const uint DEBUG_ONLY_THIS_PROCESS = 0x00000002;
        const uint DEBUG_PROCESS = 0x00000001;
        const uint DETACHED_PROCESS = 0x00000008;
        const uint EXTENDED_STARTUPINFO_PRESENT = 0x00080000;
        const uint INHERIT_PARENT_AFFINITY = 0x00010000;
        public  bool ExitCode = true;
    public uint flag { get; set; }
    public void InvokeCallStream(ProcessStream stream) {

            if (flag == CREATE_NEW_CONSOLE)
            {
                Process p = new Process();
                p.StartInfo.FileName = "cmd.exe";
                p.StartInfo.Arguments = "/c start echo "+stream.t;
                p.StartInfo.CreateNoWindow = false;
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
                p.Start();
            }
            else {
                File.WriteAllText(System.AppContext.BaseDirectory + "\\result.txt", stream.t);

            }
        }
    }
    class StartupInfo
    {
       
       

    }
    class ProcessStream{
        public string t;
        public ProcessStream(string t) {

            this.t = t;
        }
    }
}
