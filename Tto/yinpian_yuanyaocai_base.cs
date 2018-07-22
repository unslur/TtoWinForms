using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tto
{
    public partial class yinpian_yuanyaocai_base : Form
    {
        private int type = 0;

        public yinpian_yuanyaocai_base()
        {
            InitializeComponent();
        }

        public yinpian_yuanyaocai_base(int type)
        {
            InitializeComponent();
            this.type = type;
        }

        public ListViewItem rtnlv;

        private void yinpian_yuanyaocai_base_Load(object sender, EventArgs e)
        {
            ImageList imgList = new ImageList();
            imgList.ImageSize = new Size(1, 30);//设置 ImageList 的宽和高
            skinListView1.SmallImageList = imgList;
            string sendStr, rtnStr, page_Req, page_num;
            
            if (type == 3)
            {
                page_Req = "getnum," + myUser.user_code + ",11,";
                sendStr = "getyaocaiinfo_base," + myUser.user_code + ",0";
                LoadPage(skinFlowLayoutPanel1, null);
            }
            else if (type == 1)
            {
                page_Req = "getnum," + myUser.user_code + ",10,tcm_medinfo";
                sendStr = "getyaocaiinfo_more," + myUser.user_code + ",0";
                LoadPage(skinFlowLayoutPanel1, null);
            }
            else if (type == 2)
            {
                page_Req = "getnum," + myUser.user_code + ",10,tcm_check";
                sendStr = "getyaocaiinfo_check," + myUser.user_code + ",0";
                LoadPage(skinFlowLayoutPanel1, null);
            }
           
            else
            {
                return;
            }
            page_num = func.sendToServer(page_Req);
            if (page_num.Contains("error"))
            {
                page_num = "0";
                return;
            }
            if (page_num=="0")
            {
                page_num = "0";
                MessageBox.Show("没有可处理的项目了，请重新添加基础信息");
                this.Close();
                return;
            }
            label3.Text = page_num;
            rtnStr = func.sendToServer(sendStr);
            if (rtnStr.Contains("error"))
            {
                MessageBox.Show(rtnStr);
                this.Close();
                //return;
            }
            List<string> listinfo = func.GetStrlist(rtnStr, '|', false);
            foreach (string s in listinfo)
            {
                if (s.Length < 10) { return; }
                string[] info = s.Split(',');

                ListViewItem tablet = new ListViewItem(info[0]);
                foreach (string ss in info)
                {
                    tablet.SubItems.Add(ss);
                }
                skinListView1.Items.Add(tablet);
            }
        }
        private void LoadPage(CCWin.SkinControl.SkinFlowLayoutPanel skl, string type)
        {
            skl.Controls.Clear();

            CCWin.SkinControl.SkinButton bt = new CCWin.SkinControl.SkinButton();
            // Button bt = new Button();
            //bt.Size = skinButton1.Size;
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
            bt.Size = new System.Drawing.Size(75,23);
            //// bt.TabIndex = 1;
            bt.Text = "上一页";
            bt.Tag = type;

            bt.Click += new System.EventHandler(this.button_Click);
            // bt.UseVisualStyleBackColor = false;
            skl.Controls.Add(bt);
            CCWin.SkinControl.SkinButton bt2 = new CCWin.SkinControl.SkinButton();
            // Button bt2 = new Button();
            //bt2.Size = skinButton1.Size;
            // Point head = skinButton1.PointToScreen(new Point(0, 0));
            //  bt2.Location = new Point(head.X + 40 * (i - 1), head.Y);
            bt2.BackColor = System.Drawing.Color.Transparent;
            bt2.BaseColor = System.Drawing.Color.White;
            bt2.BorderColor = System.Drawing.Color.Silver;
            bt2.ControlState = CCWin.SkinClass.ControlState.Normal;
            bt2.DownBack = null;
            bt2.FadeGlow = false;
            bt2.IsDrawGlass = false;
            bt2.MouseBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(142)))), ((int)(((byte)(183)))));
            // bt2.Location = new System.Drawing.Point(241, 250);
            bt2.MouseBack = null;

            bt2.NormlBack = null;
            bt2.Size = new System.Drawing.Size(75,23);
            //// bt2.TabIndex = 1;
            bt2.Text = "下一页";
            bt2.Tag = type;

            bt2.Click += new System.EventHandler(this.button_Click);
            // bt2.UseVisualStyleBackColor = false;
            skl.Controls.Add(bt2);
        }
        int curPage = 0;
        private void button_Click(object sender, EventArgs e)
        {
            Button bt = sender as Button;
            //MessageBox.Show(bt.Text);
            string type = bt.Tag as string;
            string sendStr, rtnStr, page;
            page = bt.Text;
            int p = 0;

            if (bt.Text == "下一页")
            {
                curPage++;
            }
            else if (bt.Text == "上一页")
            {
                curPage--;

            }
            else
            {
                curPage = 0;

            }
            loadDate(curPage * 10);

        }
        private void loadDate(int page)
        {
            string sendStr, rtnStr;
            int page_all = 0;
            try
            {
                page_all = Convert.ToInt32(label3.Text);
            }
            catch (Exception)
            {

                return;
            }
            if (page > page_all)
            {
                MessageBox.Show("当前为最后一页了");
                curPage--;
                return;
            }
            else if (page < 0)
            {
                MessageBox.Show("当前为第一页了");
                curPage = 0;
                return;
            }
            if (type == 0)
            {

                sendStr = "getyaocaiinfo_base," + myUser.user_code + "," + page;

            }
            else if (type == 1)
            {

                sendStr = "getyaocaiinfo_more," + myUser.user_code + "," + page;

            }
            else if (type == 2)
            {

                sendStr = "getyaocaiinfo_check," + myUser.user_code + "," + page;

            }
            else 
            {

                sendStr = "getyaocaiinfo_base," + myUser.user_code + "," + page;

            }

            foreach (ListViewItem item in this.skinListView1.Items)
            {

                item.Remove();

            }
            rtnStr = func.sendToServer(sendStr);
            if (rtnStr.Contains("error"))
            {
                MessageBox.Show(rtnStr + "或者没有需要进行操作的饮片");
                return;
            }
            List<string> listinfo = func.GetStrlist(rtnStr, '|', false);
            foreach (string s in listinfo)
            {
                if (s.Length < 10) { return; }
                string[] info = s.Split(',');

                ListViewItem tablet = new ListViewItem(info[0]);
                foreach (string ss in info)
                {
                    tablet.SubItems.Add(ss);
                }
                skinListView1.Items.Add(tablet);
            }
        }
        private void skinListView1_MouseClick(object sender, MouseEventArgs e)
        {
            rtnlv = this.skinListView1.SelectedItems[0];
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}