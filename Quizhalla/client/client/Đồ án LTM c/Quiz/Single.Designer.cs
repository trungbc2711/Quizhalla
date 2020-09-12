namespace Quiz
{
    partial class Single
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Single));
            this.bttExit = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Label();
            this.picLives = new System.Windows.Forms.PictureBox();
            this.picPic = new System.Windows.Forms.PictureBox();
            this.bttD = new System.Windows.Forms.Button();
            this.bttC = new System.Windows.Forms.Button();
            this.bttB = new System.Windows.Forms.Button();
            this.bttA = new System.Windows.Forms.Button();
            this.txtQuestion = new System.Windows.Forms.TextBox();
            this.txtAnswer = new System.Windows.Forms.TextBox();
            this.bttSubmit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picLives)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPic)).BeginInit();
            this.SuspendLayout();
            // 
            // bttExit
            // 
            this.bttExit.BackColor = System.Drawing.Color.MidnightBlue;
            this.bttExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bttExit.ForeColor = System.Drawing.Color.White;
            this.bttExit.Location = new System.Drawing.Point(1422, 12);
            this.bttExit.Name = "bttExit";
            this.bttExit.Size = new System.Drawing.Size(45, 40);
            this.bttExit.TabIndex = 30;
            this.bttExit.Text = "⨉";
            this.bttExit.UseVisualStyleBackColor = false;
            this.bttExit.Click += new System.EventHandler(this.bttExit_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.GreenYellow;
            this.label3.Location = new System.Drawing.Point(941, 341);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 25);
            this.label3.TabIndex = 24;
            this.label3.Text = "Trợ giúp";
            // 
            // timer
            // 
            this.timer.AutoSize = true;
            this.timer.Font = new System.Drawing.Font("Bauhaus 93", 50F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timer.ForeColor = System.Drawing.Color.GreenYellow;
            this.timer.Location = new System.Drawing.Point(107, 341);
            this.timer.Name = "timer";
            this.timer.Size = new System.Drawing.Size(138, 96);
            this.timer.TabIndex = 23;
            this.timer.Text = "60";
            // 
            // picLives
            // 
            this.picLives.Image = global::Quiz.Properties.Resources.hangman_1;
            this.picLives.Location = new System.Drawing.Point(1101, 41);
            this.picLives.Name = "picLives";
            this.picLives.Size = new System.Drawing.Size(291, 715);
            this.picLives.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picLives.TabIndex = 19;
            this.picLives.TabStop = false;
            // 
            // picPic
            // 
            this.picPic.Image = ((System.Drawing.Image)(resources.GetObject("picPic.Image")));
            this.picPic.Location = new System.Drawing.Point(251, 260);
            this.picPic.Name = "picPic";
            this.picPic.Size = new System.Drawing.Size(641, 278);
            this.picPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picPic.TabIndex = 18;
            this.picPic.TabStop = false;
            // 
            // bttD
            // 
            this.bttD.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.bttD.FlatAppearance.BorderColor = System.Drawing.Color.Lime;
            this.bttD.FlatAppearance.BorderSize = 5;
            this.bttD.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bttD.ForeColor = System.Drawing.Color.Lime;
            this.bttD.Location = new System.Drawing.Point(601, 657);
            this.bttD.Name = "bttD";
            this.bttD.Size = new System.Drawing.Size(466, 99);
            this.bttD.TabIndex = 16;
            this.bttD.UseVisualStyleBackColor = false;
            this.bttD.Click += new System.EventHandler(this.bttD_Click);
            // 
            // bttC
            // 
            this.bttC.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.bttC.FlatAppearance.BorderColor = System.Drawing.Color.Blue;
            this.bttC.FlatAppearance.BorderSize = 5;
            this.bttC.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bttC.ForeColor = System.Drawing.Color.Lime;
            this.bttC.Location = new System.Drawing.Point(75, 657);
            this.bttC.Name = "bttC";
            this.bttC.Size = new System.Drawing.Size(466, 99);
            this.bttC.TabIndex = 15;
            this.bttC.UseVisualStyleBackColor = false;
            this.bttC.Click += new System.EventHandler(this.bttC_Click);
            // 
            // bttB
            // 
            this.bttB.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.bttB.FlatAppearance.BorderColor = System.Drawing.Color.Yellow;
            this.bttB.FlatAppearance.BorderSize = 5;
            this.bttB.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bttB.ForeColor = System.Drawing.Color.Lime;
            this.bttB.Location = new System.Drawing.Point(601, 558);
            this.bttB.Name = "bttB";
            this.bttB.Size = new System.Drawing.Size(466, 93);
            this.bttB.TabIndex = 14;
            this.bttB.UseVisualStyleBackColor = false;
            this.bttB.Click += new System.EventHandler(this.bttB_Click);
            // 
            // bttA
            // 
            this.bttA.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.bttA.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.bttA.FlatAppearance.BorderSize = 5;
            this.bttA.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bttA.ForeColor = System.Drawing.Color.Lime;
            this.bttA.Location = new System.Drawing.Point(75, 558);
            this.bttA.Name = "bttA";
            this.bttA.Size = new System.Drawing.Size(466, 93);
            this.bttA.TabIndex = 17;
            this.bttA.UseVisualStyleBackColor = false;
            this.bttA.Click += new System.EventHandler(this.bttA_Click);
            // 
            // txtQuestion
            // 
            this.txtQuestion.BackColor = System.Drawing.Color.CornflowerBlue;
            this.txtQuestion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtQuestion.Enabled = false;
            this.txtQuestion.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQuestion.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtQuestion.Location = new System.Drawing.Point(75, 41);
            this.txtQuestion.Margin = new System.Windows.Forms.Padding(50);
            this.txtQuestion.Multiline = true;
            this.txtQuestion.Name = "txtQuestion";
            this.txtQuestion.Size = new System.Drawing.Size(992, 202);
            this.txtQuestion.TabIndex = 13;
            // 
            // txtAnswer
            // 
            this.txtAnswer.BackColor = System.Drawing.Color.LightCyan;
            this.txtAnswer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAnswer.Font = new System.Drawing.Font("Times New Roman", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAnswer.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtAnswer.Location = new System.Drawing.Point(75, 605);
            this.txtAnswer.Margin = new System.Windows.Forms.Padding(50);
            this.txtAnswer.Name = "txtAnswer";
            this.txtAnswer.Size = new System.Drawing.Size(992, 46);
            this.txtAnswer.TabIndex = 32;
            this.txtAnswer.Text = "Enter your answer here !!!";
            this.txtAnswer.Visible = false;
            this.txtAnswer.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Single_Click);
            // 
            // bttSubmit
            // 
            this.bttSubmit.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bttSubmit.Location = new System.Drawing.Point(281, 674);
            this.bttSubmit.Name = "bttSubmit";
            this.bttSubmit.Size = new System.Drawing.Size(580, 46);
            this.bttSubmit.TabIndex = 33;
            this.bttSubmit.Text = "Press Enter to submit or u can press this button ;))))";
            this.bttSubmit.UseVisualStyleBackColor = true;
            this.bttSubmit.Click += new System.EventHandler(this.bttSubmit_Click);
            // 
            // Single
            // 
            this.AcceptButton = this.bttSubmit;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MidnightBlue;
            this.ClientSize = new System.Drawing.Size(1479, 799);
            this.Controls.Add(this.bttSubmit);
            this.Controls.Add(this.txtAnswer);
            this.Controls.Add(this.bttExit);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.timer);
            this.Controls.Add(this.picLives);
            this.Controls.Add(this.picPic);
            this.Controls.Add(this.bttD);
            this.Controls.Add(this.bttC);
            this.Controls.Add(this.bttB);
            this.Controls.Add(this.bttA);
            this.Controls.Add(this.txtQuestion);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "Single";
            this.Text = "SinglePlayer";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.picLives)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPic)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bttExit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label timer;
        private System.Windows.Forms.PictureBox picLives;
        private System.Windows.Forms.PictureBox picPic;
        private System.Windows.Forms.Button bttD;
        private System.Windows.Forms.Button bttC;
        private System.Windows.Forms.Button bttB;
        private System.Windows.Forms.Button bttA;
        private System.Windows.Forms.TextBox txtQuestion;
        private System.Windows.Forms.TextBox txtAnswer;
        private System.Windows.Forms.Button bttSubmit;
    }
}