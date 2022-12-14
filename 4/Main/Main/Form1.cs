using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Main
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

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

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern bool CreatePro?ess(
        string lpApplicationName,
           string lpCommandLine,
           ref SECURITY_ATTRIBUTES lpProcessAttributes,
           ref SECURITY_ATTRIBUTES lpThreadAttributes,
           bool bInheritHandles,
           uint dwCreationFlags,
           IntPtr lpEnvironment,
           string lpCurrentDirectory,
           [In] ref STARTUPINFO lpStartupInfo,
           out PROCESS_INFORMATION lpProcessInformation);


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

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                int num = Convert.ToInt32(textBox1.Text);
                int system = Convert.ToInt32(comboBox1.Text);
                string result = "";
                if (system == 3)
                {
                    var dll = Assembly.LoadFile(System.AppContext.BaseDirectory + "\\toTrenary.dll");
                    foreach (Type type in dll.GetExportedTypes())
                    {
                        result = type.InvokeMember("toTrenary", BindingFlags.InvokeMethod, null, Activator.CreateInstance(type), new object[] { num }).ToString();
                    }

                }
                else
                {
                    result = Convert.ToString(num, system);
                }

                ProcessInfo pi = new ProcessInfo();
                StartupInfo si = new StartupInfo();
                ProcessStream stream = new ProcessStream(result);
                if (checkBox1.Checked)
                {
                    Kernel32.CreateProcess("Main.exe", 0,0,0, false, CREATE_NEW_CONSOLE, 0, 0, si,pi );
                  
                   
                }
                else {
                    Kernel32.CreateProcess("Main.exe", 0, 0, 0, false, DETACHED_PROCESS, 0, 0,si, pi);
                }
                if (pi.ExitCode)
                {
                    pi.InvokeCallStream(stream);
                }

            }
            catch (Exception ec)
            {
                MessageBox.Show(ec.Message);
          
            }
            
        }
       
    }
}