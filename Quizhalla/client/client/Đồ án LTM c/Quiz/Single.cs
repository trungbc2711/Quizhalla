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
    public partial class Single : Form
    {
        public Single()
        {
            InitializeComponent();
        }

        public Single(string name)
        {
            InitializeComponent();
            Sname = name;
        }

        public delegate void delPassData(TcpClient socket, NetworkStream network);

        public void funData(TcpClient socket, NetworkStream network)
        {
            client = socket;
            stream = network;
            connect();
            ShowQuestion();
            Timer();
        }

        private bool tf;
        private int quest = 50;
        private int lives = 5;
        private string answer;
        private int right;
        private System.Windows.Forms.Timer aTimer;
        private int counter = 60;
        private string Sname;
        private string[] tmp = new string[10];
        private bool run = true;
        private string temp;
        TcpClient client = new TcpClient();
        NetworkStream stream;

        public void connect()
        {
           byte[] data = Encoding.UTF8.GetBytes("NEWROOM%" + Sname + "%single\n");
           stream.Write(data, 0, data.Length);          
           Thread listen = new Thread(receive);
           listen.Start();
        }

        //hàm nhận dữ liệu
        //khi nhận câu hỏi thì định dạng server gửi là QTN%TN/TL%mã câu hỏi%nội dung câu hỏi 
        //khi nhận đáp án thì định dạng server gửi là ASR%A%B%C%D%đáp án đúng ???
        public void receive()
        {
            try
            {
                while (run)
                {
                    string text = "";
                    byte[] data = new byte[1024 * 10000];
                    int recvByte = stream.Read(data, 0, data.Length);
                    text = Encoding.UTF8.GetString(data, 0, recvByte);
                    // MessageBox.Show(text);
                    if (text.StartsWith("IDR") == true || text.StartsWith("RDY") == true)
                    {
                        text = "";
                        continue;
                    }    
                    if (text.StartsWith("QTN") == true)
                    {
                        tmp = text.Split('%');
                        temp = tmp[10];
                    }
                    stream.Flush();
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
            counter --;
            timer.Text = counter.ToString();
            if (counter == 0)
            {
                aTimer.Stop();
                counter = 60;
                timer.Text = "0";
                lives--;
                Lives();
                Thread.Sleep(1000);
                if (lives != 0 || quest != 0)
                {
                    Answer();
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
            Answer answer = new Answer(tf, temp);
            answer.ShowDialog();
            this.Show();
        }

        private void GameOver()
        {
            this.Hide();
            this.ShowInTaskbar = false;
            Leaderboard leaderboard = new Leaderboard(Sname, right);
            run = false;
            leaderboard.ShowDialog();
            this.Close();
        }
                
        private void ShowQuestion()
        {
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
            //Gửi tín hiệu để nhận đáp án là ASR ừ
            // = Encoding.UTF8.GetBytes("ASR\n");
            //stream.Write(data, 0, data.Length);
            ////while (tmp == null) ;
            bttA.Text = tmp[4];
            bttB.Text = tmp[5];
            bttC.Text = tmp[6];
            bttD.Text = tmp[7];
        }

        private void Lives()
        {
            switch(lives)
            {
                case 4:
                    picLives.Image = Quiz.Properties.Resources.hangman_2;
                    break;
                case 3:
                    picLives.Image = Quiz.Properties.Resources.hangman_3;
                    break;
                case 2:
                    picLives.Image = Quiz.Properties.Resources.hangman_4;
                    break;
                case 1:
                    picLives.Image = Quiz.Properties.Resources.hangman_5;
                    break;
                case 0:
                    picLives.Image = Quiz.Properties.Resources.hangman_6;
                    break;
            }
            picLives.Refresh();
        }

        private void bttA_Click(object sender, EventArgs e)
        {
            answer = "A";
            aTimer.Stop();
            counter = 60;
            timer.Text = "0";
            quest--;
            if (answer == tmp[9])
            {
                right++;
                tf = true;
            }
            else
            {
                lives--;
                tf = false;
            }
            Lives();
            Thread.Sleep(1000);
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
            }
        }

        private void bttB_Click(object sender, EventArgs e)
        {
            answer = "B";
            aTimer.Stop();
            counter = 60;
            timer.Text = "0";
            quest--;
            if (answer == tmp[9])
            {
                right++;
                tf = true;
            }
            else
            {
                lives--;
                tf = false;
            }
            Lives();
            Thread.Sleep(1000);
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
            }
        }

        private void bttC_Click(object sender, EventArgs e)
        {
            answer = "C";
            aTimer.Stop();
            counter = 60;
            timer.Text = "0";
            quest--;
            if (answer == tmp[9])
            {
                right++;
                tf = true;
            }
            else
            {
                lives--;
                tf = false;
            }
            Lives();
            //Thread.Sleep(1000);
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
            }
        }

        private void bttD_Click(object sender, EventArgs e)
        {
            answer = "D";
            aTimer.Stop();
            counter = 60;
            timer.Text = "0";
            quest--;
            if (answer == tmp[9])
                right++;
            else
                lives--;
            if (answer == tmp[9])
            {
                right++;
                tf = true;
            }
            else
            {
                lives--;
                tf = false;
            }
            Lives();
            Thread.Sleep(1000);
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
            }
        }

        private void Single_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                bttSubmit.PerformClick();
        }

        private void bttExit_Click(object sender, EventArgs e) 
        {
            Application.Exit();
        }

        private void bttSubmit_Click(object sender, EventArgs e)
        {
            answer = txtAnswer.Text;
            aTimer.Stop();
            counter = 60;
            timer.Text = "0";
            quest--;
            if (String.Compare(answer, tmp[8], true) == 0)
            {
                right++;
                tf = true;
            }
            else
            {
                lives--;
                tf = false;
            }
            Lives();
            Thread.Sleep(1000);
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
            }
        }

        private void Single_Click(object sender, MouseEventArgs e)
        {
            txtAnswer.Clear();
        }
    }
}
