using System.Reflection;

namespace Main
{
    public partial class Form1 : Form
    {
        public List<MDI> formList;
        public Form1()
        {
            InitializeComponent();
            formList = new List<MDI>();
            this.IsMdiContainer = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button1.Hide();
        }

       

        private void button2_Click(object sender, EventArgs e)
        {
            MDI mdi = new MDI(this);
            formList.Add(mdi);
            mdi.Show();
            button1.Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

           
            MDI mdi = formList[formList.Count - 1];
            mdi.Hide();
            mdi.Dispose();
            formList.RemoveAt(formList.Count - 1);
            if (formList.Count == 0)
            {
                button1.Hide();
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("О программе1231231321");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            foreach (MDI mdi  in formList)
            {
                mdi.WindowState = FormWindowState.Minimized;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            foreach (MDI mdi in formList)
            {
                mdi.WindowState = FormWindowState.Normal;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int y = 100;
            int x = 100;
            foreach (MDI mdi in formList)
            {
                mdi.Location = new Point(x, y);
                x += 400;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            int y = 100;
            int x = 100;
            foreach (MDI mdi in formList)
            {
                mdi.Location = new Point(x, y);
                x += 50;
                y += 50;
            }
        }
    }
}