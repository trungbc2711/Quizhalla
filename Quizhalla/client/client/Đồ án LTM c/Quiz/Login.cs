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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            connect();
        }

        public delegate void delPassData(TcpClient socket, NetworkStream network);

        TcpClient client = new TcpClient();
        NetworkStream stream;
        private string name;

        //hàm dùng để kết nối tới server lần đầu
        public void connect()
        {
            try
            {
                client.Connect("192.168.43.24", 8080);
                stream = client.GetStream();
            }
            catch
            {
                MessageBox.Show("Server is closed !!!");
            }
        }

        //hàm đóng
        public void close()
        {
            stream.Close();
            client.Close();
        }

        private void Login_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                bttGo.PerformClick();
        }

        //hàm gửi tên người chơi sau đó vào game
        private void bttGo_Click(object sender, EventArgs e)
        {
            try
            {
                name = txtName.Text;
                this.Hide();
                this.ShowInTaskbar = false;
                Byte[] data = Encoding.UTF8.GetBytes("NAME%" + name + "\n");
                stream.Write(data, 0, data.Length);
                Welcome welcome = new Welcome(name);
                delPassData del = new delPassData(welcome.funData);
                del(client, stream);
                welcome.ShowDialog(this);
            }
            catch
            { }
        }
    }
}
