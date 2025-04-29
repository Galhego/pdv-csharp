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
            this.Padding = new Padding(1);

        }
        // Meio para movimentar e maximizar a tela pelo header
        private void Form_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (e.Clicks == 2)
                {
                    // Clique duplo detectado
                    AlternarMaximizacao();
                }
                else
                {
                    ReleaseCapture();
                    SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
                }
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

        private Rectangle previousBounds;
        private bool isMaximized = false;

        private void AlternarMaximizacao()
        {
            if (!isMaximized)
            {
                previousBounds = this.Bounds;
                MaximizarSemCobrirBarraDeTarefas();
                isMaximized = true;
            }
            else
            {
                this.Bounds = previousBounds;
                isMaximized = false;
            }
        }

        //Botão de maximizar
        private void btnMax_Click(object sender, EventArgs e)
        {
            AlternarMaximizacao();
        }
        
        private void MaximizarSemCobrirBarraDeTarefas()
        {
            this.WindowState = FormWindowState.Normal; // Garante que não esteja em modo Maximized
            this.FormBorderStyle = FormBorderStyle.None;

            Rectangle areaTrabalho = Screen.FromHandle(this.Handle).WorkingArea;
            this.Location = areaTrabalho.Location;
            this.Size = areaTrabalho.Size;
        }

        //Botão de Mimizar
        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        protected override void WndProc(ref Message m)
        {
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

                int RESIZE_HANDLE_SIZE = 12; // Deixei maior para ser confortável
                var cursorPoint = PointToClient(Cursor.Position);

                bool isTop = cursorPoint.Y >= 0 && cursorPoint.Y <= RESIZE_HANDLE_SIZE;
                bool isBottom = cursorPoint.Y >= (ClientSize.Height - RESIZE_HANDLE_SIZE) && cursorPoint.Y <= ClientSize.Height;
                bool isLeft = cursorPoint.X >= 0 && cursorPoint.X <= RESIZE_HANDLE_SIZE;
                bool isRight = cursorPoint.X >= (ClientSize.Width - RESIZE_HANDLE_SIZE) && cursorPoint.X <= ClientSize.Width;

                if (isTop && isLeft)
                {
                    m.Result = (IntPtr)HTTOPLEFT;
                    return;
                }
                else if (isTop && isRight)
                {
                    m.Result = (IntPtr)HTTOPRIGHT;
                    return;
                }
                else if (isBottom && isLeft)
                {
                    m.Result = (IntPtr)HTBOTTOMLEFT;
                    return;
                }
                else if (isBottom && isRight)
                {
                    m.Result = (IntPtr)HTBOTTOMRIGHT;
                    return;
                }
                else if (isLeft)
                {
                    m.Result = (IntPtr)HTLEFT;
                    return;
                }
                else if (isRight)
                {
                    m.Result = (IntPtr)HTRIGHT;
                    return;
                }
                else if (isTop)
                {
                    m.Result = (IntPtr)HTTOP;
                    return;
                }
                else if (isBottom)
                {
                    m.Result = (IntPtr)HTBOTTOM;
                    return;
                }
                else
                {
                    m.Result = (IntPtr)HTCLIENT;
                    return;
                }
            }

            base.WndProc(ref m);
        }

        private void header_Paint(object sender, PaintEventArgs e)
        {

        }


    }

}
