namespace Tto
{
    partial class AddTask
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
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.name = new CCWin.SkinControl.SkinTextBox();
            this.weight = new CCWin.SkinControl.SkinTextBox();
            this.spec = new CCWin.SkinControl.SkinTextBox();
            this.num = new CCWin.SkinControl.SkinTextBox();
            this.address = new CCWin.SkinControl.SkinTextBox();
            this.skinLabel1 = new CCWin.SkinControl.SkinLabel();
            this.lotnumber = new CCWin.SkinControl.SkinTextBox();
            this.skinLabel2 = new CCWin.SkinControl.SkinLabel();
            this.skinLabel3 = new CCWin.SkinControl.SkinLabel();
            this.createdate = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackgroundImage = global::Tto.Properties.Resources.greenMain;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Image = global::Tto.Properties.Resources.back;
            this.button1.Location = new System.Drawing.Point(-3, -4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(125, 40);
            this.button1.TabIndex = 0;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(81, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "名称";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(81, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "每包重量";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(81, 116);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "单位";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(83, 147);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "数量";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(518, 227);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "确认分包";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // name
            // 
            this.name.BackColor = System.Drawing.Color.Transparent;
            this.name.DownBack = null;
            this.name.Icon = null;
            this.name.IconIsButton = false;
            this.name.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.name.IsPasswordChat = '\0';
            this.name.IsSystemPasswordChar = false;
            this.name.Lines = new string[] {
        "上午"};
            this.name.Location = new System.Drawing.Point(152, 41);
            this.name.Margin = new System.Windows.Forms.Padding(0);
            this.name.MaxLength = 32767;
            this.name.MinimumSize = new System.Drawing.Size(28, 28);
            this.name.MouseBack = null;
            this.name.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.name.Multiline = false;
            this.name.Name = "name";
            this.name.NormlBack = null;
            this.name.Padding = new System.Windows.Forms.Padding(5);
            this.name.ReadOnly = false;
            this.name.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.name.Size = new System.Drawing.Size(185, 28);
            // 
            // 
            // 
            this.name.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.name.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.name.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.name.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.name.SkinTxt.Name = "BaseText";
            this.name.SkinTxt.Size = new System.Drawing.Size(175, 18);
            this.name.SkinTxt.TabIndex = 0;
            this.name.SkinTxt.Text = "上午";
            this.name.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.name.SkinTxt.WaterText = "";
            this.name.TabIndex = 8;
            this.name.Text = "上午";
            this.name.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.name.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.name.WaterText = "";
            this.name.WordWrap = true;
            // 
            // weight
            // 
            this.weight.BackColor = System.Drawing.Color.Transparent;
            this.weight.DownBack = null;
            this.weight.Icon = null;
            this.weight.IconIsButton = false;
            this.weight.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.weight.IsPasswordChat = '\0';
            this.weight.IsSystemPasswordChar = false;
            this.weight.Lines = new string[] {
        "23"};
            this.weight.Location = new System.Drawing.Point(152, 69);
            this.weight.Margin = new System.Windows.Forms.Padding(0);
            this.weight.MaxLength = 32767;
            this.weight.MinimumSize = new System.Drawing.Size(28, 28);
            this.weight.MouseBack = null;
            this.weight.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.weight.Multiline = false;
            this.weight.Name = "weight";
            this.weight.NormlBack = null;
            this.weight.Padding = new System.Windows.Forms.Padding(5);
            this.weight.ReadOnly = false;
            this.weight.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.weight.Size = new System.Drawing.Size(185, 28);
            // 
            // 
            // 
            this.weight.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.weight.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.weight.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.weight.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.weight.SkinTxt.Name = "BaseText";
            this.weight.SkinTxt.Size = new System.Drawing.Size(175, 18);
            this.weight.SkinTxt.TabIndex = 0;
            this.weight.SkinTxt.Text = "23";
            this.weight.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.weight.SkinTxt.WaterText = "";
            this.weight.TabIndex = 8;
            this.weight.Text = "23";
            this.weight.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.weight.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.weight.WaterText = "";
            this.weight.WordWrap = true;
            // 
            // spec
            // 
            this.spec.BackColor = System.Drawing.Color.Transparent;
            this.spec.DownBack = null;
            this.spec.Icon = null;
            this.spec.IconIsButton = false;
            this.spec.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.spec.IsPasswordChat = '\0';
            this.spec.IsSystemPasswordChar = false;
            this.spec.Lines = new string[] {
        "kg"};
            this.spec.Location = new System.Drawing.Point(152, 100);
            this.spec.Margin = new System.Windows.Forms.Padding(0);
            this.spec.MaxLength = 32767;
            this.spec.MinimumSize = new System.Drawing.Size(28, 28);
            this.spec.MouseBack = null;
            this.spec.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.spec.Multiline = false;
            this.spec.Name = "spec";
            this.spec.NormlBack = null;
            this.spec.Padding = new System.Windows.Forms.Padding(5);
            this.spec.ReadOnly = false;
            this.spec.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.spec.Size = new System.Drawing.Size(185, 28);
            // 
            // 
            // 
            this.spec.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.spec.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spec.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.spec.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.spec.SkinTxt.Name = "BaseText";
            this.spec.SkinTxt.Size = new System.Drawing.Size(175, 18);
            this.spec.SkinTxt.TabIndex = 0;
            this.spec.SkinTxt.Text = "kg";
            this.spec.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.spec.SkinTxt.WaterText = "";
            this.spec.TabIndex = 8;
            this.spec.Text = "kg";
            this.spec.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.spec.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.spec.WaterText = "";
            this.spec.WordWrap = true;
            // 
            // num
            // 
            this.num.BackColor = System.Drawing.Color.Transparent;
            this.num.DownBack = null;
            this.num.Icon = null;
            this.num.IconIsButton = false;
            this.num.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.num.IsPasswordChat = '\0';
            this.num.IsSystemPasswordChar = false;
            this.num.Lines = new string[] {
        "2"};
            this.num.Location = new System.Drawing.Point(152, 131);
            this.num.Margin = new System.Windows.Forms.Padding(0);
            this.num.MaxLength = 32767;
            this.num.MinimumSize = new System.Drawing.Size(28, 28);
            this.num.MouseBack = null;
            this.num.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.num.Multiline = false;
            this.num.Name = "num";
            this.num.NormlBack = null;
            this.num.Padding = new System.Windows.Forms.Padding(5);
            this.num.ReadOnly = false;
            this.num.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.num.Size = new System.Drawing.Size(185, 28);
            // 
            // 
            // 
            this.num.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.num.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.num.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.num.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.num.SkinTxt.Name = "BaseText";
            this.num.SkinTxt.Size = new System.Drawing.Size(175, 18);
            this.num.SkinTxt.TabIndex = 0;
            this.num.SkinTxt.Text = "2";
            this.num.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.num.SkinTxt.WaterText = "";
            this.num.TabIndex = 8;
            this.num.Text = "2";
            this.num.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.num.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.num.WaterText = "";
            this.num.WordWrap = true;
            // 
            // address
            // 
            this.address.BackColor = System.Drawing.Color.Transparent;
            this.address.DownBack = null;
            this.address.Icon = null;
            this.address.IconIsButton = false;
            this.address.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.address.IsPasswordChat = '\0';
            this.address.IsSystemPasswordChar = false;
            this.address.Lines = new string[] {
        "单位的"};
            this.address.Location = new System.Drawing.Point(152, 187);
            this.address.Margin = new System.Windows.Forms.Padding(0);
            this.address.MaxLength = 32767;
            this.address.MinimumSize = new System.Drawing.Size(28, 28);
            this.address.MouseBack = null;
            this.address.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.address.Multiline = false;
            this.address.Name = "address";
            this.address.NormlBack = null;
            this.address.Padding = new System.Windows.Forms.Padding(5);
            this.address.ReadOnly = false;
            this.address.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.address.Size = new System.Drawing.Size(185, 28);
            // 
            // 
            // 
            this.address.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.address.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.address.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.address.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.address.SkinTxt.Name = "BaseText";
            this.address.SkinTxt.Size = new System.Drawing.Size(175, 18);
            this.address.SkinTxt.TabIndex = 0;
            this.address.SkinTxt.Text = "单位的";
            this.address.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.address.SkinTxt.WaterText = "";
            this.address.TabIndex = 8;
            this.address.Text = "单位的";
            this.address.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.address.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.address.WaterText = "";
            this.address.WordWrap = true;
            // 
            // skinLabel1
            // 
            this.skinLabel1.AutoSize = true;
            this.skinLabel1.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel1.BorderColor = System.Drawing.Color.White;
            this.skinLabel1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel1.Location = new System.Drawing.Point(80, 198);
            this.skinLabel1.Name = "skinLabel1";
            this.skinLabel1.Size = new System.Drawing.Size(32, 17);
            this.skinLabel1.TabIndex = 11;
            this.skinLabel1.Text = "地址";
            // 
            // lotnumber
            // 
            this.lotnumber.BackColor = System.Drawing.Color.Transparent;
            this.lotnumber.DownBack = null;
            this.lotnumber.Icon = null;
            this.lotnumber.IconIsButton = false;
            this.lotnumber.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.lotnumber.IsPasswordChat = '\0';
            this.lotnumber.IsSystemPasswordChar = false;
            this.lotnumber.Lines = new string[] {
        "2344"};
            this.lotnumber.Location = new System.Drawing.Point(152, 227);
            this.lotnumber.Margin = new System.Windows.Forms.Padding(0);
            this.lotnumber.MaxLength = 32767;
            this.lotnumber.MinimumSize = new System.Drawing.Size(28, 28);
            this.lotnumber.MouseBack = null;
            this.lotnumber.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.lotnumber.Multiline = false;
            this.lotnumber.Name = "lotnumber";
            this.lotnumber.NormlBack = null;
            this.lotnumber.Padding = new System.Windows.Forms.Padding(5);
            this.lotnumber.ReadOnly = false;
            this.lotnumber.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.lotnumber.Size = new System.Drawing.Size(185, 28);
            // 
            // 
            // 
            this.lotnumber.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lotnumber.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lotnumber.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.lotnumber.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.lotnumber.SkinTxt.Name = "BaseText";
            this.lotnumber.SkinTxt.Size = new System.Drawing.Size(175, 18);
            this.lotnumber.SkinTxt.TabIndex = 0;
            this.lotnumber.SkinTxt.Text = "2344";
            this.lotnumber.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.lotnumber.SkinTxt.WaterText = "";
            this.lotnumber.TabIndex = 12;
            this.lotnumber.Text = "2344";
            this.lotnumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.lotnumber.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.lotnumber.WaterText = "";
            this.lotnumber.WordWrap = true;
            // 
            // skinLabel2
            // 
            this.skinLabel2.AutoSize = true;
            this.skinLabel2.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel2.BorderColor = System.Drawing.Color.White;
            this.skinLabel2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel2.Location = new System.Drawing.Point(89, 237);
            this.skinLabel2.Name = "skinLabel2";
            this.skinLabel2.Size = new System.Drawing.Size(32, 17);
            this.skinLabel2.TabIndex = 13;
            this.skinLabel2.Text = "批号";
            // 
            // skinLabel3
            // 
            this.skinLabel3.AutoSize = true;
            this.skinLabel3.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel3.BorderColor = System.Drawing.Color.White;
            this.skinLabel3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel3.Location = new System.Drawing.Point(92, 172);
            this.skinLabel3.Name = "skinLabel3";
            this.skinLabel3.Size = new System.Drawing.Size(32, 17);
            this.skinLabel3.TabIndex = 14;
            this.skinLabel3.Text = "时间";
            // 
            // createdate
            // 
            this.createdate.Location = new System.Drawing.Point(152, 162);
            this.createdate.Name = "createdate";
            this.createdate.Size = new System.Drawing.Size(185, 21);
            this.createdate.TabIndex = 15;
            // 
            // AddTask
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.BackgroundImage = global::Tto.Properties.Resources.greenMain;
            this.ClientSize = new System.Drawing.Size(690, 262);
            this.Controls.Add(this.createdate);
            this.Controls.Add(this.skinLabel3);
            this.Controls.Add(this.skinLabel2);
            this.Controls.Add(this.lotnumber);
            this.Controls.Add(this.skinLabel1);
            this.Controls.Add(this.address);
            this.Controls.Add(this.num);
            this.Controls.Add(this.spec);
            this.Controls.Add(this.weight);
            this.Controls.Add(this.name);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(340, 350);
            this.Name = "AddTask";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "AddTask";
            this.Load += new System.EventHandler(this.AddTask_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button2;
        private CCWin.SkinControl.SkinTextBox name;
        private CCWin.SkinControl.SkinTextBox weight;
        private CCWin.SkinControl.SkinTextBox spec;
        private CCWin.SkinControl.SkinTextBox num;
        private CCWin.SkinControl.SkinTextBox address;
        private CCWin.SkinControl.SkinLabel skinLabel1;
        private CCWin.SkinControl.SkinTextBox lotnumber;
        private CCWin.SkinControl.SkinLabel skinLabel2;
        private CCWin.SkinControl.SkinLabel skinLabel3;
        private System.Windows.Forms.DateTimePicker createdate;
    }
}