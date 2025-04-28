using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pdv
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }


        private void btnLogin_Click(object sender, EventArgs e)
        {

                if (MainClass.IsValidUser(txtUser.Text, txtPass.Text) == false)
                {
                    guna2MessageDialog1.Show("Usuário ou senha inválido.");
                    return;
                }
                else
                {
                    this.Hide();
                    FrmMain frm = new FrmMain();
                    frm.Show();
                }
            }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }
    }
    }
    
