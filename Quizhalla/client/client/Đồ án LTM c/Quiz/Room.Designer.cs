namespace Quiz
{
    partial class Room
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Room));
            this.bttCreate = new System.Windows.Forms.Button();
            this.txtRooms = new System.Windows.Forms.ListView();
            this.txtID = new System.Windows.Forms.TextBox();
            this.bttJoin = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // bttCreate
            // 
            this.bttCreate.BackColor = System.Drawing.SystemColors.Info;
            this.bttCreate.FlatAppearance.BorderColor = System.Drawing.Color.Gold;
            this.bttCreate.FlatAppearance.BorderSize = 6;
            this.bttCreate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bttCreate.Font = new System.Drawing.Font("Oswald Stencil", 15F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bttCreate.ForeColor = System.Drawing.Color.SaddleBrown;
            this.bttCreate.Location = new System.Drawing.Point(516, 385);
            this.bttCreate.Name = "bttCreate";
            this.bttCreate.Size = new System.Drawing.Size(196, 64);
            this.bttCreate.TabIndex = 34;
            this.bttCreate.Text = "Create";
            this.bttCreate.UseVisualStyleBackColor = false;
            this.bttCreate.Click += new System.EventHandler(this.bttCreate_Click);
            // 
            // txtRooms
            // 
            this.txtRooms.BackColor = System.Drawing.Color.PeachPuff;
            this.txtRooms.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRooms.HideSelection = false;
            this.txtRooms.Location = new System.Drawing.Point(28, 40);
            this.txtRooms.Name = "txtRooms";
            this.txtRooms.Size = new System.Drawing.Size(684, 326);
            this.txtRooms.TabIndex = 35;
            this.txtRooms.UseCompatibleStateImageBehavior = false;
            this.txtRooms.View = System.Windows.Forms.View.List;
            // 
            // txtID
            // 
            this.txtID.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtID.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtID.Location = new System.Drawing.Point(28, 400);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(251, 36);
            this.txtID.TabIndex = 36;
            this.txtID.Text = "Nhập ID phòng";
            // 
            // bttJoin
            // 
            this.bttJoin.BackColor = System.Drawing.SystemColors.Info;
            this.bttJoin.FlatAppearance.BorderColor = System.Drawing.Color.Gold;
            this.bttJoin.FlatAppearance.BorderSize = 6;
            this.bttJoin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bttJoin.Font = new System.Drawing.Font("Oswald Stencil", 15F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bttJoin.ForeColor = System.Drawing.Color.SaddleBrown;
            this.bttJoin.Location = new System.Drawing.Point(285, 385);
            this.bttJoin.Name = "bttJoin";
            this.bttJoin.Size = new System.Drawing.Size(196, 64);
            this.bttJoin.TabIndex = 37;
            this.bttJoin.Text = "Join";
            this.bttJoin.UseVisualStyleBackColor = false;
            this.bttJoin.Click += new System.EventHandler(this.bttJoin_Click);
            // 
            // Room
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(742, 479);
            this.Controls.Add(this.bttJoin);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.txtRooms);
            this.Controls.Add(this.bttCreate);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "Room";
            this.Text = "Room";
            this.Load += new System.EventHandler(this.Room_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button bttCreate;
        private System.Windows.Forms.ListView txtRooms;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Button bttJoin;
    }
}