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
    public partial class Lobby : Form
    {
        public Lobby()
        {
            InitializeComponent();
        }

        public Lobby(string name, string ID, int host)
        {
            InitializeComponent();
            txtID.Text = ID;
            LID = ID;
            ad = host;
            Lname = name;
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
        Thread listen;
        private string Lname;
        private string text;
        private string LID;
        private int ad;
        public int status = 0;
        private static string[] tmp = new string[10];
        private bool run = true;

        public void connect()
        {
            listen = new Thread(receive);
            listen.Priority = ThreadPriority.AboveNormal;
            listen.IsBackground = true;
            listen.Start();
            
        }
        //
        public void receive()
        {
            //try
            //{
                while (run)
                {
                    byte[] data = new byte[1024 * 5000];
                    int recvByte = stream.Read(data, 0, data.Length);
                    text = Encoding.UTF8.GetString(data);
                    if (text.StartsWith("NPR") == true)
                    {
                        tmp = text.Split('%');
                        newplayer();
                    }
                    // RDY%vị trí người chơi ready%vị trí người chơi cancel
                    if (text.StartsWith("RDY") == true)
                    {
                        tmp = text.Split('%');
                        ready();
                        //unready();
                        byte[] data1 = Encoding.UTF8.GetBytes("SS\n");
                        stream.Write(data1, 0, data1.Length);
                        
                    }
                    if (text.StartsWith("GO") == true)
                    {
                        tmp = text.Split('%');
                        status = Int32.Parse(tmp[1]);
                        if (status == 1)
                        {
                            start();
                        }
                        else
                        {
                            MessageBox.Show("Có người chơi chưa sẵn sàng");
                        }
                    }
                    if (text.StartsWith("PLR") == true)
                    {
                        tmp = text.Split('%');
                        player();
                    }
                    stream.Flush();
                }
            //}
            //catch
            //{
            //    //close();
            //}
        }
     
        public void close()
        {
            stream.Close();
            client.Close();
        }
        
        private void newplayer()
        {
                while (tmp[1] == null) ;
                switch (tmp[1])
                {
                    case "2":
                        infotextbox(txtP2, tmp[2]);
                        break;
                    case "3":
                        infotextbox(txtP3, tmp[3]);
                        break;
                    case "4":
                        infotextbox(txtP4, tmp[4]);
                        break;

                } // khỏi in
                Thread.Sleep(1000);
        }
        private void player()
        {
            infotextbox(txtP1, tmp[1]);
            infotextbox(txtP2, tmp[2]);
            infotextbox(txtP3, tmp[3]);
            infotextbox(txtP4, tmp[4]);
        }

        private void ready()
        {
            //Khi có người chơi nào sẵn sàng thì server gửi tín hiệu với định dạng là RDY%vị trí người chơi sẵn sàng
            
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate ()
                {
                    if (tmp[1] == "2")
                        ReadyP2.Enabled = true;
                    else if (tmp[1] == "3")
                        ReadyP3.Enabled = true;
                    else if (tmp[1] == "4")
                        ReadyP4.Enabled = true;
                    this.Refresh();
                }));
            }
            else
            {
                if (tmp[1] == "2")
                    ReadyP2.Enabled = true;
                else if (tmp[1] == "3")
                    ReadyP3.Enabled = true;
                else if (tmp[1] == "4")
                    ReadyP4.Enabled = true;
                this.Refresh();
            }
        }

        private void unready()
        {
            
                //Khi có người chơi nào hủy sẵn sàng thì server gửi tín hiệu với định dạng là RDY%vị trí người chơi hủy sẵn sàng
            if (tmp[2] == "2")
                ReadyP2.Enabled = false;
            else if (tmp[2] == "3")
                ReadyP3.Enabled = false;
            else if (tmp[2] == "4")
                ReadyP4.Enabled = false;
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate ()
                {
                    this.Refresh();
                }));
            }
            else
            {
                this.Refresh();
            }
        }

        public void infotextbox(TextBox textBox, string data)
        {
            if (textBox.InvokeRequired)
            {
                textBox.Invoke(new MethodInvoker(delegate ()
                {
                    textBox.Text = data;
                    this.Refresh();
                }));
            }
            else
            {
                textBox.Text = data;
                this.Refresh();
            }
        }
        // do stream bị dính lệnh vs tn
        private void start()
        {
            if (bttStart.InvokeRequired)
            {
                bttStart.Invoke(new MethodInvoker(delegate ()
                {
                    bttStart.Enabled = false;
                    this.Hide();
                    this.ShowInTaskbar = false;
                }));
            }
            else
            {
                bttStart.Enabled = false;
                this.Hide();
                this.ShowInTaskbar = false;
            } // listen liên quan gì đến form :D
            Multi multi = new Multi(Lname, LID);
            multi.Owner = this;
            delPassData del = new delPassData(multi.funData);
            del(client, stream);
            run = false;
            multi.ShowDialog();
            //close();
            if (this.InvokeRequired)
            {
                bttStart.Invoke(new MethodInvoker(delegate ()
                {
                    this.Close();
                }));
            }
            else
            {
                this.Close();
            }
        }

        private void bttReady_Click(object sender, EventArgs e)
        {
            bttReady.Visible = false;
            bttCancel.Visible = true;
            //Gửi tín hiệu người chơi đã sẵn sàng là STS%tên%1
            //Server đánh dấu người chơi đó là sẵn sàng và khi có yêu cầu RDY thì gửi người chơi vừa sẵn sàng về
            Byte[] data = Encoding.UTF8.GetBytes("STS%" + Lname + "%1\n");
            stream.Write(data, 0, data.Length);
        }

        private void bttCancel_Click(object sender, EventArgs e)
        {
            bttCancel.Visible = false;
            bttReady.Visible = true;
            //Gửi tín hiệu người chơi hủy sẵn sàng là STS%tên%0
            //Server đánh dấu người chơi đó là chưa sẵn sàng
            Byte[] data = Encoding.UTF8.GetBytes("STS%" + Lname + "%0\n");
            stream.Write(data, 0, data.Length);
        }

        private void bttStart_Click(object sender, EventArgs e)
        {
            //Gửi tín hiệu bắt đầu là GO
            //Nếu tất cả người chơi đã sẵn sàng thì server trả về số 1, còn chưa thì trả về số 0
            Byte[] data = Encoding.UTF8.GetBytes("GO\n");
            stream.Write(data, 0, data.Length);
        } // test thử

        private void Lobby_Load(object sender, EventArgs e)
        {
            this.Owner.Enabled = false;
            if (ad == 1)
            {
                //chủ phòng gửi tín hiệu báo là đã sẵn sàng STS%tên%1
                Byte[] data = Encoding.UTF8.GetBytes("STS%" + Lname + "%1\n");
                stream.Write(data, 0, data.Length);
                txtP1.Text = Lname;
                ReadyP1.Enabled = true;
                bttReady.Visible = false;
                bttCancel.Visible = false;
                bttStart.Visible = true;
            }
            else
            {
                //người chơi kh là chủ phòng yêu cầu danh sách người chơi để hiện ra với tín hiệu PLR
                //server trả về danh sách người chơi với định dạng PLR%người 1%người 2 ...
                Byte[] data = Encoding.UTF8.GetBytes("PLR\n");
                stream.Write(data, 0, data.Length);
                ReadyP1.Enabled = true;
                bttReady.Visible = true;
                bttCancel.Visible = false;
                bttStart.Visible = false;
                //Thread go = new Thread(() =>
                //{
                //    while (status == 0) ;
                //    bttReady.Enabled = false;
                //    bttCancel.Enabled = false;
                //    start();
                //});
                //go.Priority = ThreadPriority.Normal;
            }
        }

        private void bttExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Lobby_Closed(object sender, FormClosedEventArgs e)
        {
            this.Owner.Enabled = true;
        }
    }
}
