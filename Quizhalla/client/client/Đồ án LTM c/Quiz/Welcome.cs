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
    public partial class Welcome : Form
    {
        public Welcome()
        {
            InitializeComponent();
        }

        public Welcome(string name)
        {
            InitializeComponent();
            Wname = name;
        }

        public delegate void delPassData(TcpClient socket, NetworkStream network);

        public void funData(TcpClient socket, NetworkStream network)
        {
            client = socket;
            stream = network;
        }

        TcpClient client = new TcpClient();
        NetworkStream stream;
        string Wname;

        //hàm chọn chế độ single
        private void bttSingle_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.ShowInTaskbar = false;
            Single single = new Single(Wname);
            delPassData del = new delPassData(single.funData);
            del(client, stream);
            single.ShowDialog();
            this.Close();
        }

        //hàm chọn chế độ multi
        private void bttMulti_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.ShowInTaskbar = false;
            Room room = new Room(Wname);
            delPassData del = new delPassData(room.funData);
            del(client, stream);
            room.ShowDialog();
            this.Close();
        }

        private void bttExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
