using CCWin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tto
{
    public partial class yaocai_name_base : Form
    {
        public yaocai_name_base()
        {
            InitializeComponent();
        }

        private int page_all = 0;
        public ListViewItem rtnlv;

        private void yaocai_name_base_Load(object sender, EventArgs e)
        {
            ImageList imgList = new ImageList();
            imgList.ImageSize = new Size(1, 30);//设置 ImageList 的宽和高
            skinListView1.SmallImageList = imgList;
            string sendStr = "getyaocainamenum,";
            string rtnStr = func.sendToServer(sendStr);
            if (rtnStr.Contains("error"))
            {
                MessageBox.Show(rtnStr);
                return;
            }
            page_all = Convert.ToInt32(rtnStr);
            sendStr = "getyaocainame,1,";
            rtnStr = func.sendToServer(sendStr);
            if (rtnStr.Contains("error"))
            {
                MessageBox.Show(rtnStr);
                return;
            }
            List<string> listinfo = func.GetStrlist(rtnStr, '|', false);
            foreach (string s in listinfo)
            {
                if (s.Length < 10) { return; }
                string[] info = s.Split(',');

                ListViewItem tablet = new ListViewItem(info[0]);

                tablet.SubItems.Add(info[1]);

                skinListView1.Items.Add(tablet);
            }
            label2.Text = page_all.ToString();
            if (page_all > 1)
            {
                if (page_all > 5)
                {
                    for (int i = 2; i < 7; i++)
                    {
                        CCWin.SkinControl.SkinButton bt = new CCWin.SkinControl.SkinButton();
                        // Button bt = new Button();
                        bt.Size = skinButton1.Size;
                        // Point head = skinButton1.PointToScreen(new Point(0, 0));
                        //  bt.Location = new Point(head.X + 40 * (i - 1), head.Y);
                        bt.BackColor = System.Drawing.Color.Transparent;
                        bt.BaseColor = System.Drawing.Color.White;
                        bt.BorderColor = System.Drawing.Color.Silver;
                        bt.ControlState = CCWin.SkinClass.ControlState.Normal;
                        bt.DownBack = null;
                        bt.FadeGlow = false;
                        bt.IsDrawGlass = false;
                        bt.MouseBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(142)))), ((int)(((byte)(183)))));
                        // bt.Location = new System.Drawing.Point(241, 250);
                        bt.MouseBack = null;

                        bt.NormlBack = null;
                        //// bt.Size = new System.Drawing.Size(30, 35);
                        //// bt.TabIndex = 1;
                        bt.Text = i.ToString();
                        bt.Click += new System.EventHandler(this.button_Click);
                        // bt.UseVisualStyleBackColor = false;
                        this.skinFlowLayoutPanel1.Controls.Add(bt);
                    }
                }
                if (page_all > 6)
                {
                    CCWin.SkinControl.SkinButton bt = new CCWin.SkinControl.SkinButton();
                    // Button bt = new Button();

                    // Point head = skinButton1.PointToScreen(new Point(0, 0));
                    //  bt.Location = new Point(head.X + 40 * (i - 1), head.Y);
                    bt.BackColor = System.Drawing.Color.Transparent;
                    bt.BaseColor = System.Drawing.Color.White;
                    bt.BorderColor = System.Drawing.Color.Silver;
                    bt.ControlState = CCWin.SkinClass.ControlState.Normal;
                    bt.DownBack = null;
                    bt.IsDrawGlass = false;
                    bt.FadeGlow = false;
                    // bt.Location = new System.Drawing.Point(241, 250);
                    bt.MouseBack = null;
                    bt.MouseBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(142)))), ((int)(((byte)(183)))));
                    bt.NormlBack = null;
                    bt.Size = new System.Drawing.Size(75, 23);
                    //// bt.TabIndex = 1;
                    bt.Text = "下一页";
                    bt.Click += new System.EventHandler(this.button_Click);
                    // bt.UseVisualStyleBackColor = false;
                    this.skinFlowLayoutPanel1.Controls.Add(bt);
                    CCWin.SkinControl.SkinButton btend = new CCWin.SkinControl.SkinButton();
                    // Button btend = new Button();

                    // Point head = skinButton1.PointToScreen(new Point(0, 0));
                    //  btend.Location = new Point(head.X + 40 * (i - 1), head.Y);
                    btend.BackColor = System.Drawing.Color.Transparent;
                    btend.BaseColor = System.Drawing.Color.White;
                    btend.BorderColor = System.Drawing.Color.Silver;
                    btend.ControlState = CCWin.SkinClass.ControlState.Normal;
                    btend.DownBack = null;
                    btend.IsDrawGlass = false;
                    btend.FadeGlow = false;
                    // btend.Location = new System.Drawing.Point(241, 250);
                    btend.MouseBack = null;
                    btend.MouseBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(142)))), ((int)(((byte)(183)))));
                    btend.NormlBack = null;
                    btend.Size = new System.Drawing.Size(75, 23);
                    //// btend.TabIndex = 1;
                    btend.Text = "末页";
                    btend.Click += new System.EventHandler(this.button_Click);
                    // btend.UseVisualStyleBackColor = false;
                    this.skinFlowLayoutPanel1.Controls.Add(btend);
                }
            }
        }

        private int curpage = 1;
        string page = "1";
        private void button_Click(object sender, EventArgs e)
        {
            Button bt = sender as Button;
            //MessageBox.Show(bt.Text);
            foreach (ListViewItem lt in skinListView1.Items)
            {
                lt.Remove();
            }
            string sendStr, rtnStr;
            if (bt.Text == "下一页")
            {
                page = (curpage + 1).ToString();
                curpage++;
            }
            else if (bt.Text == "末页")
            {
                page = (page_all / 10 + 1).ToString();
                curpage = page_all / 10;
            }
            else {
                curpage = Convert.ToInt32(bt.Text);
                page = bt.Text;
            }
            if (curpage > page_all / 10) {
                curpage = page_all / 10+1;
                page = curpage.ToString();
            }
            sendStr = "getyaocainame," + page+","+skinTextBox4.Text.Trim();

            rtnStr = func.sendToServer(sendStr);
            if (rtnStr.Contains("error"))
            {
                MessageBox.Show(rtnStr);
                curpage--;
                return;
            }
            List<string> listinfo = func.GetStrlist(rtnStr, '|', false);
            foreach (string s in listinfo)
            {
                if (s.Length < 10) { return; }
                string[] info = s.Split(',');

                ListViewItem tablet = new ListViewItem(info[0]);

                tablet.SubItems.Add(info[1]);

                skinListView1.Items.Add(tablet);
            }
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            rtnlv = this.skinListView1.SelectedItems[0];
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void skinButton3_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lt in skinListView1.Items)
            {
                lt.Remove();
            }
            string name = skinTextBox4.Text.Trim();
            string sendStr = "getyaocainamenum,"+name;
            string rtnStr = func.sendToServer(sendStr);
            if (rtnStr.Contains("error"))
            {
                MessageBox.Show(rtnStr);
                return;
            }
            page_all = Convert.ToInt32(rtnStr);
            label2.Text = page_all.ToString();
            if (page_all == 0) {
                skinFlowLayoutPanel1.Visible = false;
                return;
            }
            skinFlowLayoutPanel1.Visible = true; ;
            string page = "1";
           
            sendStr = "getyaocainame," + page+","+name;
            sendStr = Regex.Replace(sendStr, @"\s", "");
            rtnStr = func.sendToServer(sendStr);
            if (rtnStr.Contains("error"))
            {
                MessageBox.Show(rtnStr);
                return;
            }
            List<string> listinfo = func.GetStrlist(rtnStr, '|', false);
            foreach (string s in listinfo)
            {
                if (s.Length < 10) { return; }
                string[] info = s.Split(',');

                ListViewItem tablet = new ListViewItem(info[0]);

                tablet.SubItems.Add(info[1]);

                skinListView1.Items.Add(tablet);
            }
        }
    }
}