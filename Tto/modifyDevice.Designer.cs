namespace Tto
{
    partial class modifyDevice
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
            this.modify = new CCWin.SkinControl.SkinButton();
            this.delete = new CCWin.SkinControl.SkinButton();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.type = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // name
            // 
            this.name.Location = new System.Drawing.Point(153, 58);
            this.name.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(132, 25);
            this.name.TabIndex = 0;
            // 
            // ip
            // 
            this.ip.Location = new System.Drawing.Point(153, 109);
            this.ip.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ip.Name = "ip";
            this.ip.Size = new System.Drawing.Size(132, 25);
            this.ip.TabIndex = 1;
            // 
            // port
            // 
            this.port.Location = new System.Drawing.Point(153, 160);
            this.port.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.port.Name = "port";
            this.port.Size = new System.Drawing.Size(132, 25);
            this.port.TabIndex = 2;
            // 
            // modify
            // 
            this.modify.BackColor = System.Drawing.Color.Transparent;
            this.modify.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.modify.DownBack = null;
            this.modify.Location = new System.Drawing.Point(60, 286);
            this.modify.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.modify.MouseBack = null;
            this.modify.Name = "modify";
            this.modify.NormlBack = null;
            this.modify.Size = new System.Drawing.Size(100, 29);
            this.modify.TabIndex = 3;
            this.modify.Text = "修改";
            this.modify.UseVisualStyleBackColor = false;
            this.modify.Click += new System.EventHandler(this.modify_Click);
            // 
            // delete
            // 
            this.delete.BackColor = System.Drawing.Color.Transparent;
            this.delete.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.delete.DownBack = null;
            this.delete.Location = new System.Drawing.Point(251, 286);
            this.delete.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.delete.MouseBack = null;
            this.delete.Name = "delete";
            this.delete.NormlBack = null;
            this.delete.Size = new System.Drawing.Size(100, 29);
            this.delete.TabIndex = 4;
            this.delete.Text = "删除";
            this.delete.UseVisualStyleBackColor = false;
            this.delete.Click += new System.EventHandler(this.delete_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(153, 259);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(149, 19);
            this.checkBox1.TabIndex = 5;
            this.checkBox1.Text = "设置为默认打印机";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // type
            // 
            this.type.Location = new System.Drawing.Point(153, 213);
            this.type.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.type.Name = "type";
            this.type.Size = new System.Drawing.Size(132, 25);
            this.type.TabIndex = 6;
            // 
            // modifyDevice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 328);
            this.Controls.Add(this.type);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.delete);
            this.Controls.Add(this.modify);
            this.Controls.Add(this.port);
            this.Controls.Add(this.ip);
            this.Controls.Add(this.name);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "modifyDevice";
            this.Text = "modifyDevice";
            this.Load += new System.EventHandler(this.modifyDevice_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox name;
        private System.Windows.Forms.TextBox ip;
        private System.Windows.Forms.TextBox port;
        private CCWin.SkinControl.SkinButton modify;
        private CCWin.SkinControl.SkinButton delete;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TextBox type;
    }
}