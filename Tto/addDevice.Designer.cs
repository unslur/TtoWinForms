namespace Tto
{
    partial class addDevice
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
            this.components = new System.ComponentModel.Container();
            this.name = new System.Windows.Forms.TextBox();
            this.ip = new System.Windows.Forms.TextBox();
            this.port = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.skinButton16 = new CCWin.SkinControl.SkinButton();
            this.skinButton1 = new CCWin.SkinControl.SkinButton();
            this.SuspendLayout();
            // 
            // name
            // 
            this.name.Location = new System.Drawing.Point(141, 55);
            this.name.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(132, 25);
            this.name.TabIndex = 0;
            this.name.Text = "name";
            // 
            // ip
            // 
            this.ip.Location = new System.Drawing.Point(141, 108);
            this.ip.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ip.Name = "ip";
            this.ip.Size = new System.Drawing.Size(132, 25);
            this.ip.TabIndex = 1;
            this.ip.Text = "ip";
            // 
            // port
            // 
            this.port.Location = new System.Drawing.Point(141, 159);
            this.port.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.port.Name = "port";
            this.port.Size = new System.Drawing.Size(132, 25);
            this.port.TabIndex = 2;
            this.port.Text = "port";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "wdj",
            "markem",
            "duominuo"});
            this.comboBox1.Location = new System.Drawing.Point(141, 192);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(160, 23);
            this.comboBox1.TabIndex = 4;
            // 
            // skinButton16
            // 
            this.skinButton16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(144)))), ((int)(((byte)(210)))));
            this.skinButton16.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skinButton16.DownBack = null;
            this.skinButton16.DrawType = CCWin.SkinControl.DrawStyle.Img;
            this.skinButton16.Location = new System.Drawing.Point(80, 287);
            this.skinButton16.MouseBack = null;
            this.skinButton16.Name = "skinButton16";
            this.skinButton16.NormlBack = null;
            this.skinButton16.Size = new System.Drawing.Size(79, 29);
            this.skinButton16.TabIndex = 16;
            this.skinButton16.Text = "添加";
            this.skinButton16.UseVisualStyleBackColor = false;
            this.skinButton16.Click += new System.EventHandler(this.button1_Click);
            // 
            // skinButton1
            // 
            this.skinButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(144)))), ((int)(((byte)(210)))));
            this.skinButton1.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skinButton1.DownBack = null;
            this.skinButton1.DrawType = CCWin.SkinControl.DrawStyle.Img;
            this.skinButton1.Location = new System.Drawing.Point(194, 287);
            this.skinButton1.MouseBack = null;
            this.skinButton1.Name = "skinButton1";
            this.skinButton1.NormlBack = null;
            this.skinButton1.Size = new System.Drawing.Size(79, 29);
            this.skinButton1.TabIndex = 16;
            this.skinButton1.Text = "返回";
            this.skinButton1.UseVisualStyleBackColor = false;
            this.skinButton1.Click += new System.EventHandler(this.skinButton1_Click);
            // 
            // addDevice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 328);
            this.Controls.Add(this.skinButton1);
            this.Controls.Add(this.skinButton16);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.port);
            this.Controls.Add(this.ip);
            this.Controls.Add(this.name);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "addDevice";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "addDevice";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox name;
        private System.Windows.Forms.TextBox ip;
        private System.Windows.Forms.TextBox port;
        private System.Windows.Forms.ComboBox comboBox1;
        private CCWin.SkinControl.SkinButton skinButton16;
        private CCWin.SkinControl.SkinButton skinButton1;
    }
}