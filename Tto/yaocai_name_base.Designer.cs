﻿namespace Tto
{
    partial class yaocai_name_base
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
            this.skinButton1 = new CCWin.SkinControl.SkinButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.skinFlowLayoutPanel1 = new CCWin.SkinControl.SkinFlowLayoutPanel();
            this.skinListView1 = new CCWin.SkinControl.SkinListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.skinTextBox4 = new CCWin.SkinControl.SkinTextBox();
            this.skinButton3 = new CCWin.SkinControl.SkinButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.skinLine1 = new CCWin.SkinControl.SkinLine();
            this.skinLabel17 = new CCWin.SkinControl.SkinLabel();
            this.skinFlowLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // skinButton1
            // 
            this.skinButton1.BackColor = System.Drawing.Color.Transparent;
            this.skinButton1.BaseColor = System.Drawing.Color.White;
            this.skinButton1.BorderColor = System.Drawing.Color.Silver;
            this.skinButton1.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skinButton1.DownBack = null;
            this.skinButton1.FadeGlow = false;
            this.skinButton1.GlowColor = System.Drawing.Color.Transparent;
            this.skinButton1.IsDrawGlass = false;
            this.skinButton1.Location = new System.Drawing.Point(3, 3);
            this.skinButton1.MouseBack = null;
            this.skinButton1.MouseBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(142)))), ((int)(((byte)(183)))));
            this.skinButton1.Name = "skinButton1";
            this.skinButton1.NormlBack = null;
            this.skinButton1.Size = new System.Drawing.Size(20, 23);
            this.skinButton1.TabIndex = 1;
            this.skinButton1.Text = "1";
            this.skinButton1.UseVisualStyleBackColor = false;
            this.skinButton1.Click += new System.EventHandler(this.button_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(117, 395);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "总共";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(153, 395);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "label2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(199, 395);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "条记录";
            // 
            // skinFlowLayoutPanel1
            // 
            this.skinFlowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.skinFlowLayoutPanel1.Controls.Add(this.skinButton1);
            this.skinFlowLayoutPanel1.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skinFlowLayoutPanel1.DownBack = null;
            this.skinFlowLayoutPanel1.Location = new System.Drawing.Point(246, 387);
            this.skinFlowLayoutPanel1.MouseBack = null;
            this.skinFlowLayoutPanel1.Name = "skinFlowLayoutPanel1";
            this.skinFlowLayoutPanel1.NormlBack = null;
            this.skinFlowLayoutPanel1.Size = new System.Drawing.Size(372, 31);
            this.skinFlowLayoutPanel1.TabIndex = 4;
            // 
            // skinListView1
            // 
            this.skinListView1.BorderColor = System.Drawing.Color.Transparent;
            this.skinListView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.skinListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader3});
            this.skinListView1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinListView1.FullRowSelect = true;
            this.skinListView1.HeadColor = System.Drawing.Color.White;
            this.skinListView1.Location = new System.Drawing.Point(123, 43);
            this.skinListView1.Name = "skinListView1";
            this.skinListView1.OwnerDraw = true;
            this.skinListView1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.skinListView1.RowBackColor2 = System.Drawing.Color.WhiteSmoke;
            this.skinListView1.SelectedColor = System.Drawing.Color.Gray;
            this.skinListView1.Size = new System.Drawing.Size(332, 338);
            this.skinListView1.TabIndex = 5;
            this.skinListView1.UseCompatibleStateImageBehavior = false;
            this.skinListView1.View = System.Windows.Forms.View.Details;
            this.skinListView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 0;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "品名";
            this.columnHeader3.Width = 317;
            // 
            // skinTextBox4
            // 
            this.skinTextBox4.BackColor = System.Drawing.Color.Transparent;
            this.skinTextBox4.DownBack = null;
            this.skinTextBox4.Icon = null;
            this.skinTextBox4.IconIsButton = false;
            this.skinTextBox4.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.skinTextBox4.IsPasswordChat = '\0';
            this.skinTextBox4.IsSystemPasswordChar = false;
            this.skinTextBox4.Lines = new string[0];
            this.skinTextBox4.Location = new System.Drawing.Point(123, 10);
            this.skinTextBox4.Margin = new System.Windows.Forms.Padding(0);
            this.skinTextBox4.MaxLength = 32767;
            this.skinTextBox4.MinimumSize = new System.Drawing.Size(28, 28);
            this.skinTextBox4.MouseBack = null;
            this.skinTextBox4.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.skinTextBox4.Multiline = false;
            this.skinTextBox4.Name = "skinTextBox4";
            this.skinTextBox4.NormlBack = null;
            this.skinTextBox4.Padding = new System.Windows.Forms.Padding(5);
            this.skinTextBox4.ReadOnly = false;
            this.skinTextBox4.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.skinTextBox4.Size = new System.Drawing.Size(276, 28);
            // 
            // 
            // 
            this.skinTextBox4.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.skinTextBox4.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skinTextBox4.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.skinTextBox4.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.skinTextBox4.SkinTxt.Name = "BaseText";
            this.skinTextBox4.SkinTxt.Size = new System.Drawing.Size(266, 18);
            this.skinTextBox4.SkinTxt.TabIndex = 0;
            this.skinTextBox4.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.skinTextBox4.SkinTxt.WaterText = "品名";
            this.skinTextBox4.TabIndex = 7;
            this.skinTextBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.skinTextBox4.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.skinTextBox4.WaterText = "品名";
            this.skinTextBox4.WordWrap = true;
            // 
            // skinButton3
            // 
            this.skinButton3.BackColor = System.Drawing.Color.Silver;
            this.skinButton3.BaseColor = System.Drawing.Color.Silver;
            this.skinButton3.BorderColor = System.Drawing.Color.Silver;
            this.skinButton3.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skinButton3.DownBack = null;
            this.skinButton3.DrawType = CCWin.SkinControl.DrawStyle.None;
            this.skinButton3.IsDrawGlass = false;
            this.skinButton3.Location = new System.Drawing.Point(383, 11);
            this.skinButton3.MouseBack = null;
            this.skinButton3.Name = "skinButton3";
            this.skinButton3.NormlBack = null;
            this.skinButton3.Size = new System.Drawing.Size(56, 26);
            this.skinButton3.TabIndex = 4;
            this.skinButton3.Text = "查询";
            this.skinButton3.UseVisualStyleBackColor = false;
            this.skinButton3.Click += new System.EventHandler(this.skinButton3_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.skinButton3);
            this.panel1.Controls.Add(this.skinTextBox4);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.skinListView1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.skinFlowLayoutPanel1);
            this.panel1.Location = new System.Drawing.Point(127, 69);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(657, 418);
            this.panel1.TabIndex = 8;
            // 
            // skinLine1
            // 
            this.skinLine1.BackColor = System.Drawing.Color.Transparent;
            this.skinLine1.LineColor = System.Drawing.Color.Black;
            this.skinLine1.LineHeight = 1;
            this.skinLine1.Location = new System.Drawing.Point(246, 49);
            this.skinLine1.Name = "skinLine1";
            this.skinLine1.Size = new System.Drawing.Size(320, 10);
            this.skinLine1.TabIndex = 19;
            this.skinLine1.Text = "skinLine1";
            // 
            // skinLabel17
            // 
            this.skinLabel17.AutoSize = true;
            this.skinLabel17.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel17.BorderColor = System.Drawing.Color.White;
            this.skinLabel17.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel17.ForeColor = System.Drawing.SystemColors.Highlight;
            this.skinLabel17.Location = new System.Drawing.Point(246, 25);
            this.skinLabel17.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.skinLabel17.Name = "skinLabel17";
            this.skinLabel17.Size = new System.Drawing.Size(74, 21);
            this.skinLabel17.TabIndex = 18;
            this.skinLabel17.Text = "药材查询";
            // 
            // yaocai_name_base
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(890, 503);
            this.Controls.Add(this.skinLine1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.skinLabel17);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "yaocai_name_base";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "yaocai_name_base";
            this.Load += new System.EventHandler(this.yaocai_name_base_Load);
            this.skinFlowLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private CCWin.SkinControl.SkinButton skinButton1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private CCWin.SkinControl.SkinFlowLayoutPanel skinFlowLayoutPanel1;
        private CCWin.SkinControl.SkinListView skinListView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private CCWin.SkinControl.SkinTextBox skinTextBox4;
        private CCWin.SkinControl.SkinButton skinButton3;
        private System.Windows.Forms.Panel panel1;
        private CCWin.SkinControl.SkinLine skinLine1;
        private CCWin.SkinControl.SkinLabel skinLabel17;
    }
}