using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quiz
{
    public partial class Answer : Form
    {
        public Answer()
        {
            InitializeComponent();
        }

        public Answer(bool answer, string ghichu)
        {
            InitializeComponent();
            ans = answer;
            truth = ghichu;
        }

        private bool ans;
        private string truth;
        
        private void ShowAnswer()
        {
            if (ans == true)
                RoW.Text = "Ñuùng roài !!! UwU";
            else
                RoW.Text = "Sai roài !!! Leâu leâu";
            if (truth != "")
                Truth.Text = truth;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Visible = true;
            this.ShowInTaskbar = true;
            this.WindowState = FormWindowState.Normal;
            this.BringToFront();
            this.TopMost = true;
            ShowAnswer();
            this.Refresh();
            Thread.Sleep(3000);
            this.Close();
        }
    }
}
