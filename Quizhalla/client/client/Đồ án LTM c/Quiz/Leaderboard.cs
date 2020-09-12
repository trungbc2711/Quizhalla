using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quiz
{
    public partial class Leaderboard : Form
    {
        public Leaderboard()
        {
            InitializeComponent();
        }

        public Leaderboard(string name, int right)
        {
            InitializeComponent();
            no = right;
            Lname = name;
            mode = 0;
        }

        public Leaderboard(string ID, string data)
        {
            InitializeComponent();
            LID = ID;
            ldr = data;
            mode = 1;
        }

        public delegate void delPassData(TcpClient socket, NetworkStream network);

        public void funData(TcpClient socket, NetworkStream network)
        {
            client = socket;
            stream = network;
        }

        private int no;
        private string Lname;
        private string LID;
        private string[] tmp = new string[10];
        private int mode;
        private string ldr;
        
        TcpClient client = new TcpClient();
        NetworkStream stream;

        public void close()
        {
            stream.Close();
            client.Close();
        }

        private void Single()
        {
            int point = no*10;
            name1.Text = Lname;
            point1.Text = point.ToString();
            right1.Text = no.ToString();
            rank1.Text = "1";
        }

        private void Multi()
        {
            //Gửi tín hiệu để nhận danh sách xếp hạng từ thấp đến cao do server sắp xếp là LDR
            //Server gửi về với định dạng tên người 1%điểm 1 (do server tính)%số câu đúng 1%tên người 2%điểm 2 (do server tính)%số câu đúng 2% ...
            tmp = ldr.Split('%');
            name1.Text = tmp[1];
            point1.Text = tmp[2];
            right1.Text = tmp[3];
            rank1.Text = "1";
            name2.Text = tmp[4];
            point2.Text = tmp[5];
            right2.Text = tmp[6];
            rank2.Text = "2";
            name3.Text = tmp[7];
            point3.Text = tmp[8];
            right3.Text = tmp[9];
            rank3.Text = "3";
            name4.Text = tmp[10];
            point4.Text = tmp[11];
            right4.Text = tmp[12];
            rank4.Text = "4";
        }

        private void bttExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bttNew_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.ShowInTaskbar = false;
            Login login = new Login();
            login.ShowDialog();
            this.Close();
        }

        private void Leaderboard_Load(object sender, EventArgs e)
        {
            if (mode == 0)
                Single();
            else
                Multi();
        }
    }
}
