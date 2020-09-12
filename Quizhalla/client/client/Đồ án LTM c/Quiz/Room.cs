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
    public partial class Room : Form
    {
        public Room()
        {
            InitializeComponent();
        }

        public Room(string name)
        {
            InitializeComponent();
            Rname = name;
        }

        public delegate void delPassData(TcpClient socket, NetworkStream network);

        public void funData(TcpClient socket, NetworkStream network)
        {
            client = socket;
            stream = network;
            connect();
        }


        TcpClient client = new TcpClient();
        NetworkStream stream;
        private string Rname;
        private string ID;
        private string[] tmp;
        private bool join = true;
        private bool run = true;
        Thread listen;

        public void connect()
        {
            listen = new Thread(receive);
            //listen.IsBackground = true;
            listen.Priority = ThreadPriority.Lowest;
            listen.Start();
            //Gửi tín hiệu nhận danh sách phòng là ROOMS. Cứ 30s là refresh 1 lần
            //Server chỉ cần trả về danh sách phòng không cần mã đầu
            Byte[] data = Encoding.UTF8.GetBytes("ROOMS\n");
            stream.Write(data, 0, data.Length);
        }

        public void receive()
        {
            //try
            //{
                while (run)
                {
                    
                    string text;
                    byte[] data = new byte[1024 * 5000];
                    int recvByte = stream.Read(data, 0, data.Length);
                    text = Encoding.UTF8.GetString(data);
                    if (text.StartsWith("IDR") == true)
                    {
                        tmp = text.Split('%');
                        ID = tmp[1];
                        continue;
                    }
                    if (text.StartsWith("ERR") == true)
                    {
                        join = false;
                        MessageBox.Show("Mình code sai rồi, bạn xin lỗi mình đi >v< !!!");
                        join = true;
                        continue;
                    }
                    InfoMessage(text);
                    stream.Flush();
                }
            //}
            //catch
            //{
            //    close();
               
            //}
}

        public void close()
        {
            stream.Close();
            client.Close();
        }

        public void InfoMessage(string message)
        {
            ListViewItem item = new ListViewItem();
            item.Text = message;

            if (txtRooms.InvokeRequired)
            {
                txtRooms.Invoke(new MethodInvoker(delegate ()
                {
                    txtRooms.Items.Add(item);
                }));
            }
            else
                txtRooms.Items.Add(item);
        }

        private void Room_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                bttJoin.PerformClick();
        }

        private void bttCreate_Click(object sender, EventArgs e)
        {
            //Gửi tín hiệu để tạo phòng là NEWROOM%tên người chơi
            //Server tạo 1 phòng mới rồi gửi về mã phòng với định dạng IDR%ID của phòng
            byte[] data = Encoding.UTF8.GetBytes("NEWROOM%" + Rname + "\n");
            stream.Write(data, 0, data.Length);
            this.Hide();
            this.ShowInTaskbar = false;
            while (ID == null) ;
            Lobby lobby = new Lobby(Rname, ID, 1);
            lobby.Owner = this;
            delPassData del = new delPassData(lobby.funData);
            del(client, stream);
            run = false;
            lobby.ShowDialog();
            this.Close();
        }

        private void bttJoin_Click(object sender, EventArgs e)
        {
            //Gửi tín hiệu để vào phòng là JOI%ID phòng muốn vào
            byte[] data = Encoding.UTF8.GetBytes("JOI%" + txtID.Text + "\n");
            stream.Write(data, 0, data.Length);
            Thread.Sleep(1000);
            if (join == true)
            {
                this.Hide();
                this.ShowInTaskbar = false;
                ID = txtID.Text;
                Lobby lobby = new Lobby(Rname, ID, 0);
                lobby.Owner = this;
                delPassData del = new delPassData(lobby.funData);
                del(client, stream);
                run = false;
                lobby.ShowDialog();
                this.Close();
            }
        }

        private void Room_Load(object sender, EventArgs e)
        {

        }
    }
}
