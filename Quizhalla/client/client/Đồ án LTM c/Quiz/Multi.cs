using System;
using System.CodeDom.Compiler;
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
    public partial class Multi : Form
    {
        public Multi()
        {
            InitializeComponent();
        }

        public Multi(string name, string ID)
        {
            InitializeComponent();
            Mname = name;
            MID = ID;
        }
        // vd: có 2 thằng cùng vô cùng lúc thì server gửi 2 lần liên tục
        public delegate void delPassData(TcpClient socket, NetworkStream network);

        public void funData(TcpClient socket, NetworkStream network)
        {
            client = socket;
            stream = network;
            //stream = client.GetStream();
        }

        private bool tf;
        private bool endgame = false;
        private int lives = 5;
        private string answer;
        private int right;
        private System.Windows.Forms.Timer aTimer;
        private int counter = 10;
        private string Mname;
        private string MID;
        private string[] tmp = new string[10];
        private string temp;
        private bool run = true;
        private string ldr;
        private string ans;
        TcpClient client = new TcpClient();
        NetworkStream stream;
        Thread listen;

        public void connect()
        {
            byte[] data = Encoding.UTF8.GetBytes("SS\n");
            stream.Write(data, 0, data.Length);
            listen = new Thread(receive);
            //listen.Priority = ThreadPriority.Lowest;
            listen.IsBackground = true;
            listen.Start();

        }

        //hàm nhận dữ liệu
        //khi nhận câu hỏi thì định dạng server gửi là QTN%TN/TL%mã câu hỏi%nội dung câu hỏi
        //khi nhận đáp án thì định dạng server gửi là ASR%A%B%C%D%đáp án đúng 
        //khi mạng tất cả người chơi bằng 0, server gửi một tín hiệu EGE
        public void receive()
        {
            try
            {
                while (run)
                { 
                    string text;
                    byte[]data = new byte[1024 * 10000];
                    int recvByte = stream.Read(data, 0, data.Length);
                    text = Encoding.UTF8.GetString(data, 0, recvByte);
                    //MessageBox.Show(text);
                    if (text.StartsWith("SS") == true)
                    {
                        stream.Flush();
                    }    
                    if (text.Contains("QTN") == true)
                    {                        
                        tmp = text.Split('%');
                        temp = tmp[10];
                        if (tmp[1] == "TN")
                            ans = tmp[9];
                        else
                            ans = tmp[8];
                        stream.Flush();
                    } 
                    if (text.Contains("LIF") == true)
                    {
                        tmp = text.Split('%');
                        stream.Flush();
                    }    
                    if (text.StartsWith("EGE") == true)
                    {
                        endgame = true;
                        stream.Flush();
                    }
                    if (text.StartsWith("TXT") == true)
                    {
                        tmp = text.Split('%');
                        InfoMessage(tmp[1]);
                        stream.Flush();
                    }
                    if (text.StartsWith("LDR") == true)
                    {
                        ldr = text;
                        stream.Flush();
                    }
                    // kêu server thêm cái txt
                    
                //continue;                    
                }
            }
            catch
            {
                close();
            }
        }

        public void close()
        {
            stream.Close();
            client.Close();
        }

        private void Timer()
        {
            aTimer = new System.Windows.Forms.Timer();
            aTimer.Tick += new EventHandler(aTimer_Tick);
            aTimer.Interval = 1000;
            aTimer.Start();
            timer.Text = counter.ToString();
        }

        private void aTimer_Tick(object sender, EventArgs e)
        {
            counter--;
            timer.Text = counter.ToString();
            if (counter == 0)
            {
                aTimer.Stop();
                counter = 10;
                timer.Text = "0";
                if (answer == null)
                {
                    lives--;
                    byte[] da = Encoding.UTF8.GetBytes("LIV%" + lives + '\n');
                    stream.Write(da, 0, da.Length);
                }
                //byte[] data = Encoding.UTF8.GetBytes("SS\n");
                //stream.Write(data, 0, data.Length);
                Lives();
                Thread.Sleep(1000);
                if (lives == 0)
                {
                    Answer();
                    MessageBox.Show("YOU DIED !!!");
                    while (endgame == false) ;
                    GameOver();
                }
                else
                {
                    Answer();
                    ShowQuestion();
                    Timer();
                }
            }
        }

        private void Answer()
        {
            this.Hide();
            Answer ans = new Answer(tf, temp);
            ans.ShowDialog();
            this.Show();
        }

        private void GameOver()
        {
            this.Hide();
            this.ShowInTaskbar = false;
            //Gửi tín hiệu để gửi số câu đúng là RGT%số câu đúng
            //Server nhận vào tính toán điểm (mỗi câu đúng 10đ)
            byte[] data = Encoding.UTF8.GetBytes("RGT%" + right + '\n');
            stream.Write(data, 0, data.Length);
            data = Encoding.UTF8.GetBytes("LDR\n");
            stream.Write(data, 0, data.Length);
            while (ldr == null) ;
            Leaderboard leaderboard = new Leaderboard(MID, ldr);
            leaderboard.Owner = this;
            delPassData del = new delPassData(leaderboard.funData);
            del(client, stream);
            run = false;
            leaderboard.ShowDialog();
            this.Close();
        }

        private void ShowQuestion()
        {
            try
            {
                answer = null;
                tf = false;
                //Gửi tín hiệu để nhận câu hỏi là QTN
                byte[] data = Encoding.UTF8.GetBytes("QTN\n");
                stream.Write(data, 0, data.Length);
                Thread.Sleep(1000);
                while (tmp[1] == null) ;
                txtQuestion.Text = tmp[3];

                if (tmp[1] == "TN")
                {
                    bttA.Visible = true;
                    bttB.Visible = true;
                    bttC.Visible = true;
                    bttD.Visible = true;
                    txtAnswer.Visible = false;
                    bttSubmit.Visible = false;
                }
                else
                {
                    bttA.Visible = false;
                    bttB.Visible = false;
                    bttC.Visible = false;
                    bttD.Visible = false;
                    txtAnswer.Visible = true;
                    bttSubmit.Visible = true;
                }
                //Gửi tín hiệu để nhận đáp án là ASR
                bttA.Text = tmp[4];
                bttB.Text = tmp[5];
                bttC.Text = tmp[6];
                bttD.Text = tmp[7];
            }
            catch
            { }
        }
        // lỗi gì show ra để biết sửa
        private void Lives()
        {
            //Gửi tín hiệu để nhận số mạng của người chơi là LIF
            //Server trả về danh sách mạng còn lại của người chơi LIF%mạng 1&mạng 2%mạng 3%mạng 4
            byte[] data = Encoding.UTF8.GetBytes("LIF\n");
            stream.Write(data, 0, data.Length);
            Thread.Sleep(1000);
            while (tmp.Length != 5) ;
            int i = 1;
            Int32 life;
            life = Int32.Parse(tmp[i]);
            switch (life)
            {
                case 4:
                    picLives1.Image = Quiz.Properties.Resources.hangman_2;
                    break;
                case 3:
                    picLives1.Image = Quiz.Properties.Resources.hangman_3;
                    break;
                case 2:
                    picLives1.Image = Quiz.Properties.Resources.hangman_4;
                    break;
                case 1:
                    picLives1.Image = Quiz.Properties.Resources.hangman_5;
                    break;
                case 0:
                    picLives1.Image = Quiz.Properties.Resources.hangman_6;
                    break;
            }
            picLives1.Refresh();
            i++;
            life = Int32.Parse(tmp[i]);
            switch (life)
            {
                case 4:
                    picLives2.Image = Quiz.Properties.Resources.hangman_2;
                    break;
                case 3:
                    picLives2.Image = Quiz.Properties.Resources.hangman_3;
                    break;
                case 2:
                    picLives2.Image = Quiz.Properties.Resources.hangman_4;
                    break;
                case 1:
                    picLives2.Image = Quiz.Properties.Resources.hangman_5;
                    break;
                case 0:
                    picLives2.Image = Quiz.Properties.Resources.hangman_6;
                    break;
            }
            picLives2.Refresh();
            i++;
            life = Int32.Parse(tmp[i]);
            switch (life)
            {
                case 4:
                    picLives3.Image = Quiz.Properties.Resources.hangman_2;
                    break;
                case 3:
                    picLives3.Image = Quiz.Properties.Resources.hangman_3;
                    break;
                case 2:
                    picLives3.Image = Quiz.Properties.Resources.hangman_4;
                    break;
                case 1:
                    picLives3.Image = Quiz.Properties.Resources.hangman_5;
                    break;
                case 0:
                    picLives3.Image = Quiz.Properties.Resources.hangman_6;
                    break;
            }
            picLives3.Refresh();
            i++;
            life = Int32.Parse(tmp[i]);
            switch (life)
            {
                case 4:
                    picLives4.Image = Quiz.Properties.Resources.hangman_2;
                    break;
                case 3:
                    picLives4.Image = Quiz.Properties.Resources.hangman_3;
                    break;
                case 2:
                    picLives4.Image = Quiz.Properties.Resources.hangman_4;
                    break;
                case 1:
                    picLives4.Image = Quiz.Properties.Resources.hangman_5;
                    break;
                case 0:
                    picLives4.Image = Quiz.Properties.Resources.hangman_6;
                    break;
            }
            picLives4.Refresh();
        }

        public void InfoMessage(string message)
        {
            ListViewItem item = new ListViewItem();
            item.Text = message;

            if (txtData.InvokeRequired)
            {
                txtData.Invoke(new MethodInvoker(delegate ()
                {
                    txtData.Items.Add(item);
                }));
            }
            else
                txtData.Items.Add(item);
        }

        private void bttA_Click(object sender, EventArgs e)
        {
            answer = "A";
            if (answer == ans)
            {
                right++;
                tf = true;
            }
            else
            {
                lives--;
                tf = false;
            }
            //Gửi tín hiệu để báo số mạng còn lại của người chơi LIV%mạng
            //Server nhận và khi nào tất cả mạng người chơi là 0 thì gửi tín hiệu EGE
            byte[] data = Encoding.UTF8.GetBytes("LIV%" + lives + '\n');
            stream.Write(data, 0, data.Length);
            /*Thread.Sleep(1000);
            if (lives != 0 && quest != 0)
            {
                Answer();
                ShowQuestion();
                Timer();
            }
            else
            {
                Answer();
                GameOver();
            }*/
        }

        private void bttB_Click(object sender, EventArgs e)
        {
            answer = "B";
            if (answer == ans)
            {
                right++;
                tf = true;
            }
            else
            {
                lives--;
                tf = false;
            }
            byte[] data = Encoding.UTF8.GetBytes("LIV%" + lives + '\n');
            stream.Write(data, 0, data.Length);
        }

        private void bttC_Click(object sender, EventArgs e)
        {
            answer = "C";
            if (answer == ans)
            {
                right++;
                tf = true;
            }
            else
            {
                lives--;
                tf = false;
            }
            byte[] data = Encoding.UTF8.GetBytes("LIV%" + lives + '\n');
            stream.Write(data, 0, data.Length);
        }

        private void bttD_Click(object sender, EventArgs e)
        {
            answer = "D";
            if (answer == ans)
            {
                right++;
                tf = true;
            }
            else
            {
                lives--;
                tf = false;
            }
            byte[] data = Encoding.UTF8.GetBytes("LIV%" + lives + '\n');
            stream.Write(data, 0, data.Length);
        }

        private void bttSubmit_Click(object sender, EventArgs e)
        {
            answer = txtAnswer.Text;
            if (String.Compare(answer, ans, true) == 0)
            {
                right++;
                tf = true;
            }
            else
            {
                lives--;
                tf = false;
            }
            txtAnswer.Clear();
            byte[] data = Encoding.UTF8.GetBytes("LIV%" + lives + '\n');
            stream.Write(data, 0, data.Length);
            /*Thread.Sleep(1000);
            if (lives != 0 && quest != 0)
            {
                Answer();
                ShowQuestion();
                Timer();
            }
            else
            {
                Answer();
                GameOver();
            }*/
        }

        private void bttSend_Click(object sender, EventArgs e)
        {
            //chat
            Byte[] data = Encoding.UTF8.GetBytes("TXT%" + Mname + ": " + txtChat.Text + '\n');
            stream.Write(data, 0, data.Length);
            txtChat.Clear();
        }

        private void Multi_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                bttSend.PerformClick();
        }

        private void bttExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Multi_Load(object sender, EventArgs e)
        {
            this.Owner.Enabled = false;
            connect();
            ShowQuestion();
            Timer();
        }

        private void Multi_Closed(object sender, FormClosedEventArgs e)
        {
            this.Owner.Enabled = true;
        }

        private void Multi_Click(object sender, MouseEventArgs e)
        {
            txtAnswer.Clear();
        }
    }
}
