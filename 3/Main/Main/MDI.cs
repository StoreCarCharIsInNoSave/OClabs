using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Main
{
    public partial class MDI : Form
    {
        public MDI( Form parrent)
        {
            InitializeComponent();
            this.MdiParent = parrent; 
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
