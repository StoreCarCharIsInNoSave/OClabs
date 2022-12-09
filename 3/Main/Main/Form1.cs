using System.Drawing;
using System.Reflection;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;

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
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
            this.TopMost = true;
            IntPtr p1 = CreateEllipticRgn(0, 0, Width, Height);
            IntPtr p2 = CreateEllipticRgn(140, 140, 250, 250);
            IntPtr p3 = CreateEllipticRgn(160, 160, 230, 230);
            CombineRgn(p2, p2, p3, 3);
            CombineRgn(p1, p1, p2, 3);

            SetWindowLong(p1, -20, 0x80000 | 0x20);
            SetWindowRgn(Handle, p1, true);
            this.TopMost = true;
          
        }

        [DllImport("gdi32.dll")]
        static extern int CombineRgn(IntPtr hrgnDest, IntPtr hrgnSrc1, IntPtr hrgnSrc2, int fnCombineMode);


        [DllImport("gdi32.dll")]
        static extern IntPtr CreateEllipticRgn(int nLeftRect, int nTopRect,
         int nRightRect, int nBottomRect);


        [DllImport("user32.dll")]
        static extern int SetWindowRgn(IntPtr hWnd, IntPtr hRgn, bool bRedraw);

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
       (
           int nLeftRect,
           int nTopRect,
           int nRightRect,
           int nBottomRect,
           int nWidthEllipse,
           int nHeightEllipse
       );

        [DllImport("user32.dll", EntryPoint = "GetDC")]
        static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("gdi32.dll")]
        static extern bool FillRgn(IntPtr hdc, IntPtr hrgn, IntPtr hbr);

        [DllImport("gdi32.dll")]
        static extern IntPtr CreateRectRgn(int nLeftRect, int nTopRect, int nRightRect,
            int nBottomRect);

        [DllImport("gdi32.dll")]
        static extern IntPtr CreateSolidBrush(uint crColor);

        [DllImport("gdi32.dll")]
        static extern bool DeleteObject(IntPtr hObject);

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
            foreach (MDI mdi in formList)
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

        [DllImport("user32.dll")]
        static extern bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);

        public const int GWL_EXSTYLE = -20;
        public const int WS_EX_LAYERED = 0x80000;
        public const int LWA_ALPHA = 0x2;
        public const int LWA_COLORKEY = 0x1;

        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);
        [DllImport("user32.dll", EntryPoint = "GetWindowLong")]
        private static extern uint GetWindowLong(IntPtr hWnd, int nIndex);
        private void Form1_Deactivate(object sender, EventArgs e)
        {

            SetWindowLong(Handle, GWL_EXSTYLE, GetWindowLong(Handle, GWL_EXSTYLE) ^ WS_EX_LAYERED);
            SetLayeredWindowAttributes(Handle, 0, 204, LWA_ALPHA);

        }

        private void Form1_Activated(object sender, EventArgs e)
        {

            SetLayeredWindowAttributes(Handle, 0, 255, LWA_ALPHA);

        }
    }
}