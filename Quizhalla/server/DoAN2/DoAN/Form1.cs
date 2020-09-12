using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Timers;
using System.Data.SqlClient;

namespace DoAN
{
    public partial class Form1 : Form
    {
        SqlConnection sqlconnect;
        public Form1()
        {
            InitializeComponent();
            sqlconnect = new SqlConnection(@"Data Source=DESKTOP-2ML2HR6;Initial Catalog=GAMEQUIZ;Integrated Security=True");
            sqlconnect.Open();
        }

        IPEndPoint ipepServer;
        Socket listenerSocket;
        List<Socket> clientList = new List<Socket>();
        List<string> clientname = new List<string>();
        
        public class sList
        {
            public List<Socket> listsockets = new List<Socket>();
            public List<string> DSCauhoi= new List<string>();
            public List<string> listname = new List<string>();
            public int[] live = new int[5];
            public int RDY = 0;
            public int ID_ROOM = 0;
            public int Type = 4;
            //public bool status = false;

        }
        sList[] lists= new sList[20];

        private void listen_Click(object sender, EventArgs e)
        {
            listen.Enabled = false;
            connect();
        }

        public void connect()
        {
            listenerSocket = new Socket(
                AddressFamily.InterNetwork,
                SocketType.Stream,
                ProtocolType.Tcp);
            ipepServer = new IPEndPoint(IPAddress.Any, 8080);
            listenerSocket.Bind(ipepServer);
            infoMessage("Server running on 127.0.0.1:8080");
            Thread Listen = new Thread(() =>
            {
                try
                {
                    while (true)
                    {
                        listenerSocket.Listen(10);
                        Socket clientSocket = listenerSocket.Accept();
                        clientList.Add(clientSocket);
                        
                        byte[] rec = new byte[1];
                        int recvByte = -1;
                        string text = "";
                        do
                        {
                            recvByte = clientSocket.Receive(rec);
                            text += Encoding.UTF8.GetString(rec);
                        } while (text[text.Length - 1] != '\n');
                        string[] stext = text.Split('%');

                        //stext[1] :là tên client
                        //lưu client vào database
                        // Random ID_USER
                        clientname.Add(stext[1]);

                        Random r = new Random();
                        int ID_user = r.Next(1, 1000);
                        AddUSer(ID_user, stext[1], clientSocket.RemoteEndPoint.ToString());

                        Thread recv = new Thread(receive);
                        infoMessage("New client connected form: " + clientSocket.RemoteEndPoint);
                        recv.IsBackground = true;
                        recv.Start(clientSocket);
                    }
                }
                catch
                {
                    listenerSocket = new Socket(
                        AddressFamily.InterNetwork,
                        SocketType.Stream,
                        ProtocolType.Tcp);
                    ipepServer = new IPEndPoint(IPAddress.Any, 8080);
                }
            });
            Listen.IsBackground = true;
            Listen.Start();
        }

        // Tìm vị trí socket của client
        private int FindPosSocket(Socket client)
        {
            int pos = 0;
            foreach(Socket socket in clientList)
            {
                if (socket != null && socket == client)
                    return pos;
                pos++;
            }
            
            return -1;
        }

        // Lưu thông tin người vào table USER
        // Kiểm tra ID_USER có tồn tại hay không.
        public void AddUSer(int ID_user, string name, string IP)
        {

            //if (CheckID_User(ID_user) == false)
            {
                string insert = "INSERT INTO [dbo].[USERS] ([ID_USER], [TEN], [SOMANG], [IP_USER]) VALUES (";
                insert += "'" + ID_user + "',N'" + name + "',5,'" + IP + "')";
                SqlCommand cmd = new SqlCommand(insert, sqlconnect);
                //SqlDataReader dr;
                //dr = cmd.ExecuteReader();
                cmd.ExecuteNonQuery();
            }

        }

        public bool CheckID_User(int ID_User)
        {
            string data = "SELECT * FROM USERS WHERE ID_USER = '" + ID_User + "'";
            SqlCommand cmd = new SqlCommand(data, sqlconnect);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd); // Lưu dữ liệu lấy được vào đây
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows[0][0] == null ) return false;
            return true;
        }

        // Lấy câu hỏi từ database
        public string Getquestion(int i)
        {
            string macauhoi = LAYMCH();
            //DSCauhoi.Add(macauhoi);

            if (IsInList(macauhoi, i) == true)
            {
                while (IsInList(macauhoi, i) == true)
                {
                    macauhoi = LAYMCH();
                }
            }

            string getques = "SELECT LOAICAUHOI, MACAUHOI, CAUHOI, A, B, C, D, DATL, DAPAN, GHICHU " +
                "FROM BOCAUHOI " +
                "WHERE MACAUHOI = " + '\'' + macauhoi + '\'';
            SqlCommand cmd = new SqlCommand(getques, sqlconnect);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd); // Lưu dữ liệu lấy được vào đây //
            DataTable dt = new DataTable();
            da.Fill(dt);

            // Trả về câu hỏi lấy từ ROOMQUESTION tương ứng.
            string question = "";
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                {
                    question += "QTN%";
                    question += dt.Rows[j][0].ToString() + "%";
                    question += dt.Rows[j][1].ToString() + "%";
                    question += dt.Rows[j][2].ToString() + "%";
                    question += dt.Rows[j][3].ToString() + "%";
                    question += dt.Rows[j][4].ToString() + "%";
                    question += dt.Rows[j][5].ToString() + "%";
                    question += dt.Rows[j][6].ToString() + "%";
                    question += dt.Rows[j][7].ToString() + "%";
                    question += dt.Rows[j][8].ToString() + "%";
                    question += dt.Rows[j][9].ToString();
                }
            }

            return question;
            string data = "INSERT INTO ROOMQUESTION VALUES (STT, MACAUHOI, ID_ROOM, ID)";
        }
        private string LAYMCH()
        {
            List<string> ques_type = new List<string>();
            ques_type.Add("KHXH");
            ques_type.Add("KHTN");
            ques_type.Add("VH");
            ques_type.Add("DS");
            ques_type.Add("TA");

            Random type = new Random();
            int theloai = type.Next(0, 4);
            int stt = 0;

            Random num = new Random();
            string macauhoi = "";
            switch (theloai)
            {
                case 0:
                    stt = num.Next(1, 50);
                    macauhoi = ques_type[0];
                    break;
                case 1:
                    stt = num.Next(1, 82);
                    macauhoi = "TN";
                    break;
                case 2:
                    stt = num.Next(1, 25);
                    macauhoi = ques_type[2];
                    break;
                case 3:
                    stt = num.Next(1, 20);
                    macauhoi = ques_type[3];
                    break;
                case 4:
                    stt = num.Next(1, 5);
                    macauhoi = ques_type[4];
                    break;
            }

            if (stt < 10) macauhoi = macauhoi + '0' + stt.ToString();
            else macauhoi = macauhoi + stt.ToString();

            return macauhoi;
        }

        private bool IsInList(string check, int i)
        {
            foreach (string item in lists[i].DSCauhoi)
            {
                if (check == item) return true;
            }
            return false;
        }

        //Code số câu hỏi

        public void receive(object obj)
        {
            Socket client = obj as Socket;
            byte[] recv = new byte[1];
            int recvByte = -1;
            try
            {
                while (true)
                {
                    string text = "";
                    do
                    {
                        recvByte = client.Receive(recv);
                        text += Encoding.UTF8.GetString(recv);
                    } while (text[text.Length - 1] != '\n');

                    if (text.StartsWith("NEWROOM%")) // Create ID Room and add client to room
                    {
                        // định dạng nhận từ client NEWROOM%tên người tạo
                        string[] temp = text.Split('%');
                        Random random = new Random();
                        int idroom = random.Next(1000, 9999);

                        if (text.Contains("single") == false)
                        {
                            //check idroom in database                
                            Byte[] data = Encoding.UTF8.GetBytes("IDR%" + idroom.ToString()); //send newly created ID_ROOM to the Client
                            client.Send(data);
                        }

                        for (int i = 0; i < 20; i++)
                        {

                            if (lists[i] == null)
                            {
                                lists[i] = new sList();
                                //int j = FindPosSocket(client);
                                lists[i].listname.Add(temp[1]);

                                clientList.Remove(client);
                                clientname.Remove(temp[1]);
                                lists[i].listsockets.Add(client);
                                lists[i].ID_ROOM = idroom;
                                i = 20;
                            }

                        }
                    }
                    else if (text.StartsWith("QTN"))
                    {

                        int i = FindROOM(client);

                        string question = Getquestion(i);

                        infoMessage(question);
                        Byte[] data = Encoding.UTF8.GetBytes(question);
                        client.Send(data);

                        //định dạng server khi gửi là QTN%TN/TL%mã câu hỏi%nội dung câu hỏi
                    }
                    
                    else if (text.StartsWith("sing"))
                    {
                        Byte[] data = Encoding.UTF8.GetBytes("Hello");
                        client.Send(data);
                    }

                    else if (text.StartsWith("ROOMS")) //get ID ROOMS
                    {
                        string sdata = "";

                        for (int i = 0; i < 20; i++)//send all ID_ROOM to the Client
                        {

                            if (lists[i] != null && lists[i].ID_ROOM != 0)
                            {
                                //cấu trúc id%số người chơi hiện tại
                                sdata = lists[i].ID_ROOM.ToString() + "              " + lists[i].listsockets.Count.ToString() + "/4";
                                infoMessage(sdata);
                                Byte[] data = Encoding.UTF8.GetBytes(sdata);
                                client.Send(data);
                            }

                        }

                    }
                    else if (text.StartsWith("PLR"))//gửi tên người chơi trong Room
                    {
                        string data = "PLR";

                        int i = FindROOM(client);
                        //lấy tên client

                        int numberclient = lists[i].listname.Count;
                        foreach(string item in lists[i].listname)
                        {
                            data += "%" + item;
                        }

                        if (numberclient == 2) data += "%%";
                        if (numberclient == 3) data += "%";

                        Byte[] sdata = Encoding.UTF8.GetBytes(data);
                        
                        foreach (Socket socket in lists[i].listsockets)
                        {
                            socket.Send(sdata);
                            infoMessage(data + socket.RemoteEndPoint.ToString());
                        }
                        client.Send(sdata);
/*
                        for (int i = 0; i < 20; i++)
                        {
                            if (lists[i] != null)
                            {
                                foreach (Socket socket in lists[i].listsockets)
                                {
                                    if (socket != null && socket == client)
                                    {
                                        foreach (string sname in lists[i].listname)
                                        {
                                            data += "%" + sname;
                                        }
                                        //cấu trúc PLR%name1%name2%name3%name4
                                        Byte[] sdata = Encoding.UTF8.GetBytes(data);
                                        client.Send(sdata);
                                        break;
                                    }
                                }
                            }
                        }*/
                    }
                    else if (text.StartsWith("GO")) //trưởng phòng sẽ gửi nhãn này
                    {
                        infoMessage(client.RemoteEndPoint.ToString() + "   " + text);
                        for (int i = 0; i < 20; i++)
                        {
                            if (lists[i] != null)
                            {
                                foreach (Socket socket in lists[i].listsockets)
                                {
                                    if (socket != null && socket == client)
                                    {
                                        if (lists[i].listsockets.Count == lists[i].RDY)
                                        {
                                            Byte[] data = Encoding.UTF8.GetBytes("GO%1");
                                            client.Send(data);
                                            foreach(Socket item in lists[i].listsockets)
                                            {
                                                if(item!=null)
                                                {
                                                    item.Send(data);
                                                }
                                            }
                                            //Getquestion(i);

                                        }
                                        else
                                        {
                                            Byte[] data = Encoding.UTF8.GetBytes("GO%0");
                                            client.Send(data);
                                        }
                                        break;
                                    }
                                }
                            }
                        }
                    }

                    else if (text.StartsWith("STS%"))//xác định trạng thái các client đã sẵn sàng hay chưa
                    {
                        //STS%name%0/1
                        infoMessage(text);
                        string[] sname = text.Split('%');
                        int z = Int32.Parse(sname[2]);
                        if (z == 1)
                        {
                            for (int i = 0; i < 20; i++)
                            {
                                if (lists[i] != null)
                                    foreach (Socket item in lists[i].listsockets)
                                    {
                                        if (item != null)
                                        {
                                            if (client == item)
                                            {
                                                lists[i].RDY += 1;
                                                int x = 1;
                                                foreach (Socket items in lists[i].listsockets)
                                                {
                                                    if (items != null)
                                                    {
                                                        Byte[] data = Encoding.UTF8.GetBytes("RDY%" + x.ToString());
                                                        item.Send(data);

                                                    }
                                                    x++;
                                                }
                                                break;
                                            }

                                        }
                                    }
                            }
                        }
                        else
                        {
                            int x = -1;
                            for (int i = 0; i < 20; i++)
                            {
                                if (lists[i] != null)
                                    foreach (Socket item in lists[i].listsockets)
                                    {
                                        if (item != null)
                                        {
                                            if (client == item)
                                            {
                                                lists[i].RDY -= 1;
                                                foreach (Socket items in lists[i].listsockets)
                                                {
                                                    if (items != null)
                                                    {
                                                        Byte[] data = Encoding.UTF8.GetBytes("STS%" + sname[1] + "%0");
                                                        item.Send(data);

                                                    }
                                                }
                                                x = i;
                                            }


                                        }
                                    }
                            }
                            if (x > -1)
                            {
                                lists[x].listsockets.Remove(client);
                                clientList.Add(client);
                            }
                        }


                    }
                    else if (text.StartsWith("JOI%"))  //add client to existing ID_ROOM
                    {
                        string[] sLobby = text.Split('%');
                        int id = int.Parse(sLobby[1]);
                        int i;
                        
                            for (i = 0; i < 20; i++)
                            {
                                if (lists[i] != null && id == lists[i].ID_ROOM)
                                {
                                    if (lists[i].listsockets.Count < lists[i].Type)  //Allow client to add ROOM
                                    {
                                        try
                                        {
                                        int j = FindPosSocket(client);
                                        lists[i].listsockets.Add(client);
                                        clientList.Remove(client);
                                        lists[i].listname.Add(clientname[j]);
                                        clientname.Remove(clientname[j]);
                                        }
                                           catch
                                        {
                                        MessageBox.Show("Lỗi");
                                        }
                                        //lấy tên của client từ database lưu vào list[i].listname
                                        //Byte[] data = Encoding.UTF8.GetBytes("OK");
                                        //client.Send(data);
                                    }


                                    break;
                                }

                            }
                            if (i == 20)
                            {
                            Byte[] data = Encoding.UTF8.GetBytes("ERR");
                            client.Send(data);
                        }
                    }
                    else if (text.StartsWith("TRT%"))
                    {
                        //gửi giải thích câu hỏi
                    }
                    else if (text.StartsWith("LIV"))
                    {
                        string[] sdata = text.Split('%');
                        int liv = Int32.Parse(sdata[1]);

                        for (int i = 0; i < 20; i++)
                        {
                            if (lists[i] != null)
                            {
                                int count = 0;
                                foreach (Socket socket in lists[i].listsockets)
                                {
                                    if (socket != null && socket == client)
                                    {
                                        int die = 0; //tính số client có mạng =0
                                        lists[i].live[count] = liv;

                                        if (liv == 0) die++;

                                        //Nếu tất cả người chơi đều có số mạng là 0 thì gửi nhãn EGE để kết thúc
                                        if ((die == lists[i].listsockets.Count - 1) && lists[i].listsockets.Count > 1)
                                            foreach (Socket items in clientList)
                                            {
                                                if (items != null)
                                                {
                                                    Byte[] data = Encoding.UTF8.GetBytes("EGE");
                                                    items.Send(data);

                                                }
                                            }
                                        else if (die == lists[i].listsockets.Count)
                                        {
                                            Byte[] data = Encoding.UTF8.GetBytes("EGE");
                                            client.Send(data);
                                            infoMessage("EGE");
                                        }

                                    }
                                    count++;
                                }
                            }
                        }
                    }
                    else if (text.StartsWith("LIF"))
                    {
                        //trả về số mạng các người chơi trong phòng
                        int i = FindROOM(client);
                        string data = "LIF";
                        for (int j = 0; j < lists[i].listsockets.Count; j++)
                        {
                            data += "%" + lists[i].live[j].ToString();
                        }
                        
                        Byte[] sdata = Encoding.UTF8.GetBytes(data);
                        
                        foreach (Socket socket in lists[i].listsockets)
                        {
                            if(socket!=null)
                            {
                                socket.Send(sdata);
                            }
                        }
                    }
                    else
                    chat(client, text);
                    infoMessage(client.RemoteEndPoint + ": " + text);
                }
            }
            catch
            {
                clientList.Remove(client);
                client.Close();
            }
        }
        public int FindROOM(Socket socket)
        {
            //xác định client đang ở Room nào
            for(int i=0;i<20;i++)
            {
                if(lists[i]!=null)
                    foreach(Socket item in lists[i].listsockets)
                    {
                        if (item != null && item == socket)
                            return i;
                    }
            }
            return -1;
        }

        public void chat(Socket socket,string text)
        {
            int flag = 0;
            foreach (Socket item in clientList)
            {
                if(item!=null&& socket==item)  //Find ROOM containing this client
                {
                    foreach(Socket items in clientList)
                    {
                        if(items!=null&&items!=socket)
                        {
                            Byte[] data = Encoding.UTF8.GetBytes(text);
                            item.Send(data);
                        }
                    }
                    flag = 1;
                }
            }
            if(flag==0)
            for(int i=0;i<20;i++)
            {
                if(lists[i]!=null)
                {
                    foreach(Socket item in lists[i].listsockets)
                    {
                        if(socket==item&&item!=null)
                        {
                            foreach(Socket items in lists[i].listsockets)
                            {
                                if (items != null&&items != socket)
                                {
                                    Byte[] data = Encoding.UTF8.GetBytes(text);
                                    items.Send(data);
                                }
                            }
                                i = 20;
                        }
                    }
                }
            }
        }

        public void close()
        {
            listenerSocket.Close();
            infoMessage("Client disconnected");
        }




        public void infoMessage(string message)
        {
            ListViewItem item = new ListViewItem();
            item.Text = message;

            if (listMessage.InvokeRequired)
            {
                listMessage.Invoke(new MethodInvoker(delegate ()
                {
                    listMessage.Items.Add(item);
                }));
            }
            else
            {
                listMessage.Items.Add(item);
            }
        }
    }
}
