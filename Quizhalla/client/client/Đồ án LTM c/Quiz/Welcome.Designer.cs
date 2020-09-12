namespace Quiz
{
    partial class Welcome
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Welcome));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.bttSingle = new System.Windows.Forms.Button();
            this.bttMulti = new System.Windows.Forms.Button();
            this.bttExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.label1.Font = new System.Drawing.Font("Edwardian Script ITC", 40.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.LavenderBlush;
            this.label1.Location = new System.Drawing.Point(299, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(268, 80);
            this.label1.TabIndex = 1;
            this.label1.Text = "Welcome to";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.label2.Font = new System.Drawing.Font("Pricedown Bl", 50F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.LavenderBlush;
            this.label2.Location = new System.Drawing.Point(242, 131);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(379, 101);
            this.label2.TabIndex = 1;
            this.label2.Text = "Quizhalla";
            // 
            // bttSingle
            // 
            this.bttSingle.FlatAppearance.BorderColor = System.Drawing.Color.Orange;
            this.bttSingle.FlatAppearance.BorderSize = 5;
            this.bttSingle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bttSingle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bttSingle.Location = new System.Drawing.Point(246, 302);
            this.bttSingle.Name = "bttSingle";
            this.bttSingle.Size = new System.Drawing.Size(375, 55);
            this.bttSingle.TabIndex = 2;
            this.bttSingle.Text = "Singleplayer";
            this.bttSingle.UseVisualStyleBackColor = true;
            this.bttSingle.Click += new System.EventHandler(this.bttSingle_Click);
            // 
            // bttMulti
            // 
            this.bttMulti.FlatAppearance.BorderColor = System.Drawing.Color.Orange;
            this.bttMulti.FlatAppearance.BorderSize = 5;
            this.bttMulti.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bttMulti.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bttMulti.Location = new System.Drawing.Point(246, 380);
            this.bttMulti.Name = "bttMulti";
            this.bttMulti.Size = new System.Drawing.Size(375, 55);
            this.bttMulti.TabIndex = 2;
            this.bttMulti.Text = "Multiplayer";
            this.bttMulti.UseVisualStyleBackColor = true;
            this.bttMulti.Click += new System.EventHandler(this.bttMulti_Click);
            // 
            // bttExit
            // 
            this.bttExit.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.bttExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bttExit.ForeColor = System.Drawing.Color.White;
            this.bttExit.Location = new System.Drawing.Point(808, 12);
            this.bttExit.Name = "bttExit";
            this.bttExit.Size = new System.Drawing.Size(45, 40);
            this.bttExit.TabIndex = 11;
            this.bttExit.Text = "⨉";
            this.bttExit.UseVisualStyleBackColor = false;
            this.bttExit.Click += new System.EventHandler(this.bttExit_Click);
            // 
            // Welcome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(865, 478);
            this.ControlBox = false;
            this.Controls.Add(this.bttExit);
            this.Controls.Add(this.bttMulti);
            this.Controls.Add(this.bttSingle);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Welcome";
            this.Text = "Welcome";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button bttSingle;
        private System.Windows.Forms.Button bttMulti;
        private System.Windows.Forms.Button bttExit;
    }
}