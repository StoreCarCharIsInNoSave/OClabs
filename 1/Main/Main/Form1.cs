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

                MessageBox.Show(result);
            }
            catch (Exception ec)
            {
                MessageBox.Show(ec.Message);
          
            }
            
        }
       
    }
}