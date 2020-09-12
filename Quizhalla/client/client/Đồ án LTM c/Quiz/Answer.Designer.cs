namespace Quiz
{
    partial class Answer
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
            this.Truth = new System.Windows.Forms.Label();
            this.RoW = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Truth
            // 
            this.Truth.BackColor = System.Drawing.Color.CornflowerBlue;
            this.Truth.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Truth.Font = new System.Drawing.Font("Trebuchet MS", 14F, System.Drawing.FontStyle.Italic);
            this.Truth.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Truth.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Truth.Location = new System.Drawing.Point(23, 137);
            this.Truth.Name = "Truth";
            this.Truth.Padding = new System.Windows.Forms.Padding(10, 5, 5, 5);
            this.Truth.Size = new System.Drawing.Size(755, 248);
            this.Truth.TabIndex = 3;
            this.Truth.Text = "* Hiển nhiên thế rồi còn đòi giải thích gì nữa :D *";
            this.Truth.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // RoW
            // 
            this.RoW.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.RoW.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.RoW.Font = new System.Drawing.Font("VNI-Hobo", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RoW.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.RoW.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.RoW.Location = new System.Drawing.Point(182, 43);
            this.RoW.Name = "RoW";
            this.RoW.Size = new System.Drawing.Size(440, 62);
            this.RoW.TabIndex = 4;
            this.RoW.Text = "Sai roài !!! Leâu leâu";
            this.RoW.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Answer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MidnightBlue;
            this.ClientSize = new System.Drawing.Size(800, 411);
            this.ControlBox = false;
            this.Controls.Add(this.Truth);
            this.Controls.Add(this.RoW);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Answer";
            this.Text = "Answer";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label Truth;
        private System.Windows.Forms.Label RoW;
    }
}