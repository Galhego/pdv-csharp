using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;


namespace pdv
{
    public partial class FrmMain : Form
    {
        // === ARRASTAR ===
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;

        public FrmMain()
        {
            InitializeComponent();
            header.MouseDown += Form_MouseDown;
        }

        private void Form_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }


        //Botão de fechar
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }


        //Botão de maximizar
        private void btnMax_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
                this.WindowState = FormWindowState.Maximized;
            else
                this.WindowState = FormWindowState.Normal;
        }

        //Botão de Mimizar
        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        protected override void WndProc(ref Message m)
        {
            const int RESIZE_HANDLE_SIZE = 10;
            const int WM_NCHITTEST = 0x84;
            const int HTCLIENT = 1;
            const int HTLEFT = 10;
            const int HTRIGHT = 11;
            const int HTTOP = 12;
            const int HTTOPLEFT = 13;
            const int HTTOPRIGHT = 14;
            const int HTBOTTOM = 15;
            const int HTBOTTOMLEFT = 16;
            const int HTBOTTOMRIGHT = 17;

            if (m.Msg == WM_NCHITTEST)
            {
                base.WndProc(ref m);

                var screenPoint = new Point(m.LParam.ToInt32());
                var clientPoint = this.PointToClient(screenPoint);

                if (clientPoint.Y <= RESIZE_HANDLE_SIZE)
                {
                    if (clientPoint.X <= RESIZE_HANDLE_SIZE)
                        m.Result = (IntPtr)HTTOPLEFT;
                    else if (clientPoint.X < (Size.Width - RESIZE_HANDLE_SIZE))
                        m.Result = (IntPtr)HTTOP;
                    else
                        m.Result = (IntPtr)HTTOPRIGHT;
                }
                else if (clientPoint.Y <= (Size.Height - RESIZE_HANDLE_SIZE))
                {
                    if (clientPoint.X <= RESIZE_HANDLE_SIZE)
                        m.Result = (IntPtr)HTLEFT;
                    else if (clientPoint.X < (Size.Width - RESIZE_HANDLE_SIZE))
                        m.Result = (IntPtr)HTCLIENT;
                    else
                        m.Result = (IntPtr)HTRIGHT;
                }
                else
                {
                    if (clientPoint.X <= RESIZE_HANDLE_SIZE)
                        m.Result = (IntPtr)HTBOTTOMLEFT;
                    else if (clientPoint.X < (Size.Width - RESIZE_HANDLE_SIZE))
                        m.Result = (IntPtr)HTBOTTOM;
                    else
                        m.Result = (IntPtr)HTBOTTOMRIGHT;
                }
                return;
            }

            base.WndProc(ref m);
        }

        private void header_Paint(object sender, PaintEventArgs e)
        {

        }
    }

}
