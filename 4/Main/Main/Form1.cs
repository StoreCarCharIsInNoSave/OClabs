using System.Diagnostics;
using System.Reflection;

namespace Main
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

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

                if (checkBox1.Checked)
                {
                    Process p = new Process();
                    p.StartInfo.FileName = "cmd.exe";
                    p.StartInfo.Arguments = "/c start echo sdffsdf";
                    p.StartInfo.CreateNoWindow = false;
                    p.StartInfo.UseShellExecute = false;
                    p.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
                    p.Start();
                  
                   
                }
                else {
                    File.WriteAllText(System.AppContext.BaseDirectory + "\\result.txt", result);
                }
               
            }
            catch (Exception ec)
            {
                MessageBox.Show(ec.Message);
          
            }
            
        }
       
    }
}