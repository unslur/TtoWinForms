using CCWin;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using DevComponents.DotNetBar.Rendering;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Tto
{
    public partial class MainWin : CCSkinMain
    {
        public MainWin()
        {
            InitializeComponent();
        }

        [System.Runtime.InteropServices.DllImport("user32")]
        private static extern bool AnimateWindow(IntPtr hwnd, int dwTime, int dwFlags);

        private const int AW_VER_NEGATIVE = 0x0008;
        private const int AW_SLIDE = 0x40000;

        [DllImport("TtoWdjAndMarkem.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern bool initSocket(string ip, int port);

        [DllImport("TtoWdjAndMarkem.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern bool closeSocket();

        // [DllImport("TtoWdjAndMarkem.dll", EntryPoint = "test"),CallingConvention=CallingConvention.Cdecl)]

        [DllImport("TtoWdjAndMarkem.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern bool test(int a, int b);

        [DllImport("TtoWdjAndMarkem.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern bool markemPrint(byte[] code2d, byte[] taskarray, bool firstFlag);

        [DllImport("TtoWdjAndMarkem.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern bool WDJPrint(byte[] code2d, byte[] taskarray, bool firstFlag);

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            // pictureBox1.Image = Properties.Resources.a9d68cf1485336b2a861f3678e61bb38;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //AddTask task = new AddTask();
            //task.ShowDialog();
           // tabletlist tlist = new tabletlist();
           // tlist.ShowDialog();
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            // pictureBox1.Image = Properties.Resources.main1_1;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            //TaskList task = new TaskList();
            //task.ShowDialog();
        }

       // private LocalTask ts = new LocalTask();

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            //DeviceList device = new DeviceList();

            //device.ShowDialog();

           
        }

        private void MainWin_Load(object sender, EventArgs e)
        {
            
            func.FindAndCreateDB();
            skinButton1.Text = myUser.user_name;
            myUser.p_base = superTabControlPanel6.PointToScreen(new Point(0, 0));
            superTabItem2_Click(sender,e);
            labelX1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(165)))), ((int)(((byte)(242)))));
        }
        private void initWIN() {
           
        }
        private void moveOnpic(object sender, EventArgs e)
        {
            PictureBox pic = sender as PictureBox;
            Label s = new Label();
            s.Height = 70;
            s.Width = pic.Width;
            string device_info = pic.Tag.ToString();
            device_info = device_info.Replace('^', '\n');
            s.Font = new System.Drawing.Font("楷体", 13);//
            s.BackColor = Color.Transparent;//透明底色

            s.Text = device_info;

            pic.Controls.Add(s);
        }

        private void leaveOnpic(object sender, EventArgs e)
        {
            PictureBox pic = sender as PictureBox;
            try
            {
                pic.Controls.RemoveAt(0);
            }
            catch (Exception)
            {
                ;
            }
        }

        private void skinButton1_Click(object sender, EventArgs e)
        {
            exitForm ex = new exitForm();
            ex.Location = skinButton1.PointToScreen(new Point(0,0));
            ex.ShowDialog();
            if (ex.DialogResult == DialogResult.Cancel)
            {
                return;
            }
            if (ex.DialogResult == DialogResult.Abort)
            {
                try
                {
                    Application.Exit();
                }
                catch (Exception)
                {
                }
            }
            if (ex.DialogResult == DialogResult.No)
            {
                this.Close();
            }
        }

        private void superTabItem2_Click(object sender, EventArgs e)
        {
            curPage = 0;
            string sendtmp = "getnum," + myUser.user_code + ",0,";
            string totalitem = func.sendToServer(sendtmp);
            if (totalitem.Contains("error")) {
                MessageBox.Show(totalitem);
                return;
            }   
            
            label21.Text = totalitem;
            try
            {
                if (Convert.ToInt32(totalitem) == 0)
                {
                    return;
                }
            }
            catch (Exception)
            {

                MessageBox.Show("页码出错");
                return;
            }
            int page_all=0;
           
                page_all = Convert.ToInt32(totalitem) / 10;
           
            
            yaocaibaseLoadData(0,null);
            LoadPage(skinFlowLayoutPanel7,"0");
        }
        private void yaocaibaseLoadData(int page,string name) {
            int page_all = Convert.ToInt32(label21.Text);
            if (page >= page_all)
            {
                MessageBox.Show("最后一页了");
                curPage--;
                return;
            }
            if (page < 0)
            {
                MessageBox.Show("当前为第一页了");
                curPage++;
                return;
            }

            skinDataGridView1.Rows.Clear();
            // skinDataGridView1.DefaultCellStyle.SelectionBackColor = Color.Red;
            //string tabletinfo = func.getyaocaiInfo(myUser.user_code, page, "getyaocaiinfo");
            string tabletinfo = func.sendToServer("getyaocaiinfo,"+myUser.user_code+","+page+","+name);
            if (tabletinfo.Contains("error"))
            {
                MessageBox.Show(tabletinfo);
                return;
            }
            List<string> listinfo = func.GetStrlist(tabletinfo, '|', false);
            foreach (string s in listinfo)
            {
                if (s.Length < 40) { return; }
                string[] info = s.Split(',');

                int index = skinDataGridView1.Rows.Add();

                //  skinDataGridView1.Rows[index].Height = 30;
                int j = 0;
                foreach (string ss in info)
                {
                    skinDataGridView1.Rows[index].Cells[j].Value = info[j++];
                }

                skinDataGridView1.Rows[index].Cells["yaocai_base"].Value = "编辑";
            }
            DataGridViewButtonXColumn bcx = skinDataGridView1.Columns["yaocai_base"] as DataGridViewButtonXColumn;

            if (bcx != null)
            {
                bcx.BeforeCellPaint += Yaocai_base_BeforeCellPaint;
            }
        }
        private void Yaocai_base_BeforeCellPaint(object sender, BeforeCellPaintEventArgs e)
        {
            DataGridViewButtonXColumn bcx = sender as DataGridViewButtonXColumn;

            if (bcx != null)
            {
                bcx.Image = imageList2.Images[0];
            }
        }

        private void Yaocai_base_BeforeCellPaintPrint(object sender, BeforeCellPaintEventArgs e)
        {
            DataGridViewButtonXColumn bcx = sender as DataGridViewButtonXColumn;

            if (bcx != null)
            {
                bcx.Image = imageList2.Images[1];
            }
        }

        private void superTabItem6_Click(object sender, EventArgs e)
        {
            curPage = 0;
            string sendtmp = "getnum," + myUser.user_code + ",2,,";
            string totalitem = func.sendToServer(sendtmp);
            if (totalitem.Contains("error"))
            {
                MessageBox.Show(totalitem);
                return;
            }
            label27.Text = totalitem;
            try
            {
                if (Convert.ToInt32(totalitem) == 0)
                {
                    return;
                }
            }
            catch (Exception)
            {

                MessageBox.Show("页码出错");
                return;
            }
            int page_all = Convert.ToInt32(totalitem) / 10;
            
            yaocaicheckLoadData(0, null, null);
            LoadPage(skinFlowLayoutPanel9, "2");
        }
        private void yaocaicheckLoadData(int page, string condition1, string condition2) {
            int page_all = Convert.ToInt32(label27.Text);
            if (page >= page_all)
            {
                MessageBox.Show("最后一页了");
                curPage--;
                return;
            }
            if (page < 0)
            {
                MessageBox.Show("当前为第一页了");
                curPage++;
                return;
            }
            skinDataGridView3.Rows.Clear();

            //string tabletinfo = func.getyaocaiInfo(myUser.user_code, 0, "getyaocaidetect");
            string tabletinfo = func.sendToServer("getyaocaidetect,"+myUser.user_code+","+page+","+condition1+","+condition2);
            if (tabletinfo.Contains("error"))
            {
                MessageBox.Show(tabletinfo);
                return;
            }
            List<string> listinfo = func.GetStrlist(tabletinfo, '^', false);
            foreach (string s in listinfo)
            {
                if (s.Length < 40) { return; }
                string[] info = s.Split(',');

                int index = skinDataGridView3.Rows.Add();

                skinDataGridView3.Rows[index].Height = 30;
                int j = 0;
                foreach (string ss in info)
                {
                    skinDataGridView3.Rows[index].Cells[j].Value = info[j++];

                    skinDataGridView3.Rows[index].Cells["yaocai_check"].Value = "编辑";
                }
                DataGridViewButtonXColumn bcx = skinDataGridView3.Columns["yaocai_check"] as DataGridViewButtonXColumn;

                if (bcx != null)
                {
                    bcx.BeforeCellPaint += Yaocai_base_BeforeCellPaint;
                }
            }
        }
        private void superTabItem5_Click(object sender, EventArgs e)
        {
            curPage = 0;
            string sendtmp = "getnum," + myUser.user_code + ",1,,";
            string totalitem = func.sendToServer(sendtmp);
            if (totalitem.Contains("error"))
            {
                MessageBox.Show(totalitem);
                return;
            }
            label24.Text = totalitem;
            try
            {
                if (Convert.ToInt32(totalitem) == 0)
                {
                    return;
                }
            }
            catch (Exception)
            {

                MessageBox.Show("页码出错");
                return;
            }
            int page_all = Convert.ToInt32(totalitem) / 10;
            
            yaocaimoreLoadData(0, null,null);
            LoadPage(skinFlowLayoutPanel8, "1");
        }
        private void yaocaimoreLoadData(int page, string condition1,string condition2) {
            int page_all = Convert.ToInt32(label24.Text);
            if (page >= page_all)
            {
                MessageBox.Show("最后一页了");
                curPage--;
                return;
            }
            if (page < 0)
            {
                MessageBox.Show("当前为第一页了");
                curPage++;
                return;
            }
            skinDataGridView2.Rows.Clear();
           // string tabletinfo = func.getyaocaiInfo(myUser.user_code, 0, "getyaocaimore");
            string tabletinfo = func.sendToServer("getyaocaimore,"+myUser.user_code+","+page+","+condition1+","+condition2);
            if (tabletinfo.Contains("error"))
            {
                MessageBox.Show(tabletinfo);
                return;
            }
            List<string> listinfo = func.GetStrlist(tabletinfo, '|', false);
            foreach (string s in listinfo)
            {
                if (s.Length < 30) { return; }
                string[] info = s.Split(',');

                int index = skinDataGridView2.Rows.Add();

                skinDataGridView2.Rows[index].Height = 30;
                int j = 0;
                foreach (string ss in info)
                {
                    skinDataGridView2.Rows[index].Cells[j].Value = info[j++];
                }

                skinDataGridView2.Rows[index].Cells["yaocai_more"].Value = "编辑";
            }
            DataGridViewButtonXColumn bcx = skinDataGridView2.Columns["yaocai_more"] as DataGridViewButtonXColumn;
            bcx.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            bcx.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            if (bcx != null)
            {
                bcx.BeforeCellPaint += Yaocai_base_BeforeCellPaint;
            }
        }
        private void dataGridViewX2_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void skinDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewButtonXCell cell = skinDataGridView1.CurrentCell as DataGridViewButtonXCell;

            if (cell != null)
            {
                DataGridViewButtonXColumn bc =
                    skinDataGridView1.Columns[e.ColumnIndex] as DataGridViewButtonXColumn;

                if (bc != null)
                {
                    string[] info = new string[4];
                    info[0] = skinDataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                    for (int i = 0; i < 3; i++)
                    {
                        info[i] = skinDataGridView1.Rows[e.RowIndex].Cells[i].Value.ToString();
                    }
                    info[3] = skinDataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                    modify_yaocai_base mb = new modify_yaocai_base(info);
                    Point p = superTabControlPanel2.PointToScreen(new Point(0, 0));
                    mb.Location = p;
                    if (mb.ShowDialog() == DialogResult.OK)
                    {
                        superTabItem2_Click(null, null);
                    }
                }
            }
        }

        private void skinDataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewButtonXCell cell = skinDataGridView2.CurrentCell as DataGridViewButtonXCell;

            if (cell != null)
            {
                DataGridViewButtonXColumn bc =
                    skinDataGridView2.Columns[e.ColumnIndex] as DataGridViewButtonXColumn;

                if (bc != null)
                {
                    string[] info = new string[6];
                    info[0] = skinDataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();
                    info[1] = skinDataGridView2.Rows[e.RowIndex].Cells[4].Value.ToString(); ;
                    info[2] = skinDataGridView2.Rows[e.RowIndex].Cells[7].Value.ToString();
                    info[3] = skinDataGridView2.Rows[e.RowIndex].Cells[5].Value.ToString();
                    info[4] = skinDataGridView2.Rows[e.RowIndex].Cells[6].Value.ToString();
                    modify_yaocai_more mb = new modify_yaocai_more(info);
                    Point p = superTabControlPanel5.PointToScreen(new Point(0, 0));
                    mb.Location = p;
                    if (mb.ShowDialog() == DialogResult.OK)
                    {
                        superTabItem5_Click(null, null);
                    }
                }
            }
        }

        private void skinDataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewButtonXCell cell = skinDataGridView3.CurrentCell as DataGridViewButtonXCell;

            if (cell != null)
            {
                DataGridViewButtonXColumn bc =
                    skinDataGridView3.Columns[e.ColumnIndex] as DataGridViewButtonXColumn;

                if (bc != null)
                {
                    string[] info = new string[6];
                    info[0] = skinDataGridView3.Rows[e.RowIndex].Cells[0].Value.ToString();
                    info[1] = skinDataGridView3.Rows[e.RowIndex].Cells[4].Value.ToString(); ;
                    info[2] = skinDataGridView3.Rows[e.RowIndex].Cells[5].Value.ToString();
                    info[3] = skinDataGridView3.Rows[e.RowIndex].Cells[6].Value.ToString();
                    info[4] = skinDataGridView3.Rows[e.RowIndex].Cells[8].Value.ToString();
                    info[5] = skinDataGridView3.Rows[e.RowIndex].Cells[9].Value.ToString();
                    modify_yaocai_check mb = new modify_yaocai_check(info);
                    Point p = superTabControlPanel6.PointToScreen(new Point(0, 0));
                    mb.Location = p;
                    if (mb.ShowDialog() == DialogResult.OK)
                    {
                        superTabItem6_Click(null, null);
                    }
                }
            }
        }

        private void skinButton2_Click(object sender, EventArgs e)
        {
            add_yaocai_base ab = new add_yaocai_base();
            Point p = superTabControlPanel2.PointToScreen(new Point(0, 0));
            ab.Location = p;
            if (ab.ShowDialog() == DialogResult.OK)
            {
                superTabItem2_Click(null, null);
            }
        }

        private void labelX2_Click(object sender, EventArgs e)
        {
            superTabControl2.Visible = false;
            superTabControl1.Visible = true;
            labelX2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(165)))), ((int)(((byte)(242)))));
            labelX1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(99)))), ((int)(((byte)(123)))));
            superTabItem1_Click(sender,e);
        }

        private void labelX1_Click(object sender, EventArgs e)
        {
            superTabControl2.Visible = true;
            labelX1.BackColor= System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(165)))), ((int)(((byte)(242)))));
            labelX2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(99)))), ((int)(((byte)(123)))));
            superTabControl1.Visible = false;
        }

        private void superTabItem1_Click(object sender, EventArgs e)
        {
             curPage = 0;
            string sendtmp = "getnum," + myUser.user_code + ",3,";
            string totalitem = func.sendToServer(sendtmp);
            if (totalitem.Contains("error"))
            {
                MessageBox.Show(totalitem);
                return;
            }
            label3.Text = totalitem;
            try
            {
                if (Convert.ToInt32(totalitem) == 0)
                {
                    return;
                }
            }
            catch (Exception)
            {

                MessageBox.Show("页码出错");
                return;
            }
            int page_all = Convert.ToInt32(totalitem)/10;
            
            yinpianbaseLoadData(0,null);
            //LoadPage(page_all,this.skinFlowLayoutPanel1,"3");
            LoadPage(skinFlowLayoutPanel1,"3");
            
        }
        private void LoadPage(CCWin.SkinControl.SkinFlowLayoutPanel skl,string type) {
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
            bt.Size = new System.Drawing.Size(75, 23);
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
            bt2.Size = new System.Drawing.Size(75, 23);
            //// bt2.TabIndex = 1;
            bt2.Text = "下一页";
            bt2.Tag = type;

            bt2.Click += new System.EventHandler(this.button_Click);
            // bt2.UseVisualStyleBackColor = false;
            skl.Controls.Add(bt2);
        }
        private void selcetedColor(int index, CCWin.SkinControl.SkinFlowLayoutPanel skl) {
            CCWin.SkinControl.SkinButton bt= skl.Controls[index] as CCWin.SkinControl.SkinButton;
            bt.BackColor= System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(142)))), ((int)(((byte)(183)))));
        }
        private void LoadPage(int page_all, CCWin.SkinControl.SkinFlowLayoutPanel skl,string type) {

            if (page_all > 0)
            {
                if (page_all > 5)
                {
                    for (int i = 1; i < 7; i++)
                    {
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
                        bt.Size = new System.Drawing.Size(30, 30);
                        //// bt.TabIndex = 1;
                        bt.Text = i.ToString();
                        bt.Tag = type;
                        
                        bt.Click += new System.EventHandler(this.button_Click);
                        // bt.UseVisualStyleBackColor = false;
                        skl.Controls.Add(bt);
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
                    bt.Size = new System.Drawing.Size(60, 30);
                    //// bt.TabIndex = 1;
                    bt.Text = "下一页";
                    // bt.UseVisualStyleBackColor = false;
                    bt.Tag = type;
                    bt.Click += new System.EventHandler(this.button_Click);
                   skl.Controls.Add(bt);
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
                    btend.Size = new System.Drawing.Size(45, 30);
                    //// btend.TabIndex = 1;
                    btend.Text = "末页";
                    bt.Tag = type;
                    btend.Click += new System.EventHandler(this.button_Click);
                    // btend.UseVisualStyleBackColor = false;
                    skl.Controls.Add(btend);
                }
            }
        }
        int curPage=0;
        private void button_Click(object sender, EventArgs e)
        {
            Button bt = sender as Button;
            //MessageBox.Show(bt.Text);
            Int32 type = Convert.ToInt32( bt.Tag as string);
            string sendStr, rtnStr, page;
            page = bt.Text;
           
            int p=0;
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
            if (type == 0)
            {
                yaocaibaseLoadData(curPage*10,skinWaterTextBox1.Text);
            }
            else if (type == 1) {
                yaocaimoreLoadData(curPage*10,skinWaterTextBox3.Text,skinWaterTextBox4.Text);
            }
            else if (type == 2) {
                yaocaicheckLoadData(curPage*10,skinWaterTextBox5.Text,skinWaterTextBox6.Text);
            }
            else if (type == 3)
            {
                yinpianbaseLoadData(curPage*10,skinWaterTextBox8.Text);
            }
            else if (type == 4) {
                yinpianmoreLoadData(curPage*10,skinWaterTextBox9.Text,skinWaterTextBox10.Text);
            }
            else if (type == 5) {
                yinpianassistLoadData(curPage*10,skinWaterTextBox4.Text,skinWaterTextBox12.Text);
            }
            else if (type == 6) {
                yinpiancheckLoadDate(curPage*10,skinWaterTextBox13.Text,skinWaterTextBox14.Text);
            }
            else if (type == 7)
            {
                yinpianfenbao(curPage*10,skinWaterTextBox15.Text);
            }
            else {
                string name = skinWaterTextBox18.Text;
                string packtype = skinComboBox3.Text;
                string status = skinComboBox1.Text;
                //MessageBox.Show(skinComboBox3.SelectedIndex.ToString());
                if (packtype == "中包")
                {
                    packtype = "1";
                }
                else if (packtype == "小包")
                {
                    packtype = "2";
                }
                else
                {
                    packtype = "1 or 2";
                }
                if (status == "未打印")
                {
                    status = "1";
                }
                else if (status == "打印中")
                {
                    status = "2";
                }
                else if (status == "打印完成")
                {
                    status = "3";
                }
                else
                {
                    status = "1 or 2 or 3";
                }
                yinpianprinttask(curPage*10,packtype,status,skinWaterTextBox18.Text);
            }
        }
        private void yinpianbaseLoadData(int page,string condition) {
            int page_all = Convert.ToInt32(label3.Text);
            if (page >= page_all)
            {
                MessageBox.Show("最后一页了");
                curPage--;
                return;
            }
            if (page < 0)
            {
                MessageBox.Show("当前为第一页了");
                curPage++;
                return;
            }
            skinDataGridView4.Rows.Clear();
            string sendStr = "getyinpianinfo," + myUser.user_code +
                ","+page.ToString()+","+condition;
            string rtnStr = func.sendToServer(sendStr);
            if (rtnStr.Contains("error"))
            {
                MessageBox.Show(rtnStr);
                return;
            }
            List<string> listinfo = func.GetStrlist(rtnStr, '|', false);
            foreach (string s in listinfo)
            {
                if (s.Length < 30) { return; }
                string[] info = s.Split(',');

                int index = skinDataGridView4.Rows.Add();

                skinDataGridView4.Rows[index].Height = 30;
                int j = 0;
                foreach (string ss in info)
                {
                    skinDataGridView4.Rows[index].Cells[j].Value = info[j++];
                }

                skinDataGridView4.Rows[index].Cells[8].Value = "编辑";
            }
            
            DataGridViewButtonXColumn bcx = skinDataGridView4.Columns[8] as DataGridViewButtonXColumn;
            bcx.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            bcx.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            if (bcx != null)
            {
                bcx.BeforeCellPaint += Yaocai_base_BeforeCellPaint;
            }
        }
        private void skinDataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewButtonXCell cell = skinDataGridView4.CurrentCell as DataGridViewButtonXCell;

            if (cell != null)
            {
                DataGridViewButtonXColumn bc =
                    skinDataGridView4.Columns[e.ColumnIndex] as DataGridViewButtonXColumn;

                if (bc != null)
                {
                    string[] info = new string[5];
                    info[0] = skinDataGridView4.Rows[e.RowIndex].Cells[0].Value.ToString();
                    info[1] = skinDataGridView4.Rows[e.RowIndex].Cells[3].Value.ToString(); ;
                    info[2] = skinDataGridView4.Rows[e.RowIndex].Cells[6].Value.ToString();
                    info[3] = skinDataGridView4.Rows[e.RowIndex].Cells[4].Value.ToString();
                    info[4] = skinDataGridView4.Rows[e.RowIndex].Cells[5].Value.ToString();

                    modify_yinpian_base mb = new modify_yinpian_base(info);
                    Point p = superTabControlPanel1.PointToScreen(new Point(0, 0));
                    mb.Location = p;
                    if (mb.ShowDialog() == DialogResult.OK)
                    {
                        superTabItem1_Click(null, null);
                    }
                }
            }
        }

        private void skinButton4_Click(object sender, EventArgs e)
        {
            add_yinpian_base ab = new add_yinpian_base();
            Point p = superTabControlPanel2.PointToScreen(new Point(0, 0));
            ab.Location = p;
            if (ab.ShowDialog() == DialogResult.OK)
            {
                superTabItem1_Click(null, null);
            }
        }

        private void superTabItem3_Click(object sender, EventArgs e)
        {
            curPage = 0;
            string sendtmp = "getnum," + myUser.user_code + ",4,,";
            string totalitem = func.sendToServer(sendtmp);
            if (totalitem.Contains("error"))
            {
                MessageBox.Show(totalitem);
                return;
            }
            label6.Text = totalitem;
            try
            {
                if (Convert.ToInt32(totalitem) == 0)
                {
                    return;
                }
            }
            catch (Exception)
            {

                MessageBox.Show("页码出错");
                return;
            }
            int page_all = Convert.ToInt32(totalitem) / 10;
            
            yinpianmoreLoadData(0,null,null);
            LoadPage(skinFlowLayoutPanel2,"4");
        }
        private void yinpianmoreLoadData(int page,string condition1,string condition2) {
            int page_all = Convert.ToInt32(label6.Text);
            if (page >= page_all)
            {
                MessageBox.Show("最后一页了");
                curPage--;
                return;
            }
            if (page < 0)
            {
                MessageBox.Show("当前为第一页了");
                curPage++;
                return;
            }
            skinDataGridView5.Rows.Clear();
            string sendStr = "getyinpianmore," + myUser.user_code + ","+page.ToString()+","+condition1+","+condition2;
            string rtnStr = func.sendToServer(sendStr);
            if (rtnStr.Contains("error"))
            {
                MessageBox.Show(rtnStr);
                return;
            }
            List<string> listinfo = func.GetStrlist(rtnStr, '|', false);
            foreach (string s in listinfo)
            {
                if (s.Length < 30) { return; }
                string[] info = s.Split(',');

                int index = skinDataGridView5.Rows.Add();

                skinDataGridView5.Rows[index].Height = 30;
                int j = 0;
                foreach (string ss in info)
                {
                    skinDataGridView5.Rows[index].Cells[j].Value = info[j++];
                }

                skinDataGridView5.Rows[index].Cells[10].Value = "编辑";
            }
            DataGridViewButtonXColumn bcx = skinDataGridView5.Columns[10] as DataGridViewButtonXColumn;
            //  bcx.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            //bcx.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            if (bcx != null)
            {
                bcx.BeforeCellPaint += Yaocai_base_BeforeCellPaint;
            }
        }
        private void skinDataGridView5_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewButtonXCell cell = skinDataGridView5.CurrentCell as DataGridViewButtonXCell;

            if (cell != null)
            {
                DataGridViewButtonXColumn bc =
                    skinDataGridView5.Columns[e.ColumnIndex] as DataGridViewButtonXColumn;

                if (bc != null)
                {
                    string[] info = new string[6];
                    info[0] = skinDataGridView5.Rows[e.RowIndex].Cells[0].Value.ToString();
                    info[1] = skinDataGridView5.Rows[e.RowIndex].Cells[8].Value.ToString();
                    info[2] = skinDataGridView5.Rows[e.RowIndex].Cells[4].Value.ToString(); ;
                    info[3] = skinDataGridView5.Rows[e.RowIndex].Cells[5].Value.ToString();
                    info[4] = skinDataGridView5.Rows[e.RowIndex].Cells[6].Value.ToString();
                    info[5] = skinDataGridView5.Rows[e.RowIndex].Cells[7].Value.ToString();

                    modify_yinpian_more mm = new modify_yinpian_more(info);
                    Point p = superTabControlPanel1.PointToScreen(new Point(0, 0));
                    mm.Location = p;
                    if (mm.ShowDialog() == DialogResult.OK)
                    {
                        superTabItem3_Click(null, null);
                    }
                }
            }
        }

        private void skinButton3_Click(object sender, EventArgs e)
        {
            add_yaocai_more aym = new add_yaocai_more();
            Point p = superTabControlPanel2.PointToScreen(new Point(0, 0));
            aym.Location = p;
            if (aym.ShowDialog() == DialogResult.OK)
            {
                superTabItem5_Click(null, null);
            }
        }

        private void skinButton7_Click(object sender, EventArgs e)
        {
            add_yaocai_check aym = new add_yaocai_check();
            Point p = superTabControlPanel2.PointToScreen(new Point(0, 0));
            aym.StartPosition = FormStartPosition.Manual;
            aym.Location = p;
            if (aym.ShowDialog() == DialogResult.OK)
            {
                superTabItem6_Click(null, null);
            }
        }

        private void skinButton10_Click(object sender, EventArgs e)
        {
            add_yinpian_more aym = new add_yinpian_more();
            Point p = superTabControlPanel1.PointToScreen(new Point(0, 0));
            aym.StartPosition = FormStartPosition.Manual;
            aym.Location = p;
            if (aym.ShowDialog() == DialogResult.OK)
            {
                superTabItem3_Click(null, null);
            }
        }

        private void superTabItem4_Click(object sender, EventArgs e)
        {
            curPage = 0;
            string sendtmp = "getnum," + myUser.user_code + ",5,,";
            string totalitem = func.sendToServer(sendtmp);
            if (totalitem.Contains("error"))
            {
                MessageBox.Show(totalitem);
                return;
            }
            label9.Text = totalitem;
            try
            {
                if (Convert.ToInt32(totalitem) == 0)
                {
                    return;
                }
            }
            catch (Exception)
            {

                MessageBox.Show("页码出错");
                return;
            }
            int page_all = Convert.ToInt32(totalitem) / 10;
            yinpianassistLoadData(0,null,null);
            LoadPage( skinFlowLayoutPanel3, "5");
        }
        private void yinpianassistLoadData(int page,string condition1,string condition2) {
            int page_all = Convert.ToInt32(label9.Text);
            if (page >= page_all)
            {
                MessageBox.Show("最后一页了");
                curPage--;
                return;
            }
            if (page < 0)
            {
                MessageBox.Show("当前为第一页了");
                curPage++;
                return;
            }
            skinDataGridView6.Rows.Clear();
            string sendStr = "getyinpianassist," + myUser.user_code + ","+page.ToString()+","+condition1+","+condition2 ;
            string rtnStr = func.sendToServer(sendStr);
            if (rtnStr.Contains("error"))
            {
                MessageBox.Show(rtnStr);
                return;
            }
            List<string> listinfo = func.GetStrlist(rtnStr, '|', false);
            foreach (string s in listinfo)
            {
                if (s.Length < 20) { return; }
                string[] info = s.Split(',');

                int index = skinDataGridView6.Rows.Add();

                skinDataGridView6.Rows[index].Height = 30;
                int j = 0;
                foreach (string ss in info)
                {
                    skinDataGridView6.Rows[index].Cells[j].Value = info[j++];
                }

                skinDataGridView6.Rows[index].Cells[10].Value = "编辑";
            }
            DataGridViewButtonXColumn bcx = skinDataGridView6.Columns[10] as DataGridViewButtonXColumn;
            //  bcx.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            //bcx.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            if (bcx != null)
            {
                bcx.BeforeCellPaint += Yaocai_base_BeforeCellPaint;
            }
        }
        private void skinDataGridView6_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewButtonXCell cell = skinDataGridView6.CurrentCell as DataGridViewButtonXCell;

            if (cell != null)
            {
                DataGridViewButtonXColumn bc =
                    skinDataGridView6.Columns[e.ColumnIndex] as DataGridViewButtonXColumn;

                if (bc != null)
                {
                    string[] info = new string[7];
                    info[0] = skinDataGridView6.Rows[e.RowIndex].Cells[0].Value.ToString();
                    info[1] = skinDataGridView6.Rows[e.RowIndex].Cells[3].Value.ToString(); ;
                    info[2] = skinDataGridView6.Rows[e.RowIndex].Cells[4].Value.ToString();
                    info[3] = skinDataGridView6.Rows[e.RowIndex].Cells[5].Value.ToString();
                    info[4] = skinDataGridView6.Rows[e.RowIndex].Cells[6].Value.ToString();
                    info[5] = skinDataGridView6.Rows[e.RowIndex].Cells[7].Value.ToString();
                    info[6] = skinDataGridView6.Rows[e.RowIndex].Cells[8].Value.ToString();
                    modify_yinpian_assist mm = new modify_yinpian_assist(info);
                    Point p = superTabControlPanel1.PointToScreen(new Point(0, 0));
                    mm.Location = p;
                    if (mm.ShowDialog() == DialogResult.OK)
                    {
                        superTabItem4_Click(null, null);
                    }
                }
            }
        }

        private void skinButton12_Click(object sender, EventArgs e)
        {
            add_yinpian_assist aya = new add_yinpian_assist();
            Point p = superTabControlPanel1.PointToScreen(new Point(0, 0));
            aya.StartPosition = FormStartPosition.Manual;
            aya.Location = p;
            if (aya.ShowDialog() == DialogResult.OK)
            {
                superTabItem4_Click(null, null);
            }
        }

        private void superTabItem8_Click(object sender, EventArgs e)
        {
            curPage = 0;
            string condition0 = skinWaterTextBox13.Text.Trim();
            string condition1 = skinWaterTextBox14.Text.Trim();
            string sendtmp = "getnum," + myUser.user_code + ",6,,";
            string totalitem = func.sendToServer(sendtmp);
            if (totalitem.Contains("error"))
            {
                MessageBox.Show(totalitem);
                return;
            }
            label12.Text = totalitem;
            try
            {
                if (Convert.ToInt32(totalitem) == 0)
                {
                    return;
                }
            }
            catch (Exception)
            {

                MessageBox.Show("页码出错");
                return;
            }
            int page_all = Convert.ToInt32(totalitem) / 10;
            yinpiancheckLoadDate(0,null,null);
            LoadPage(skinFlowLayoutPanel4, "6");
        }
        private void yinpiancheckLoadDate(int page,string condition,string condition1) {
            int page_all = Convert.ToInt32(label12.Text);
            if (page >= page_all)
            {
                MessageBox.Show("最后一页了");
                curPage--;
                return;
            }
            if (page < 0)
            {
                MessageBox.Show("当前为第一页了");
                curPage++;
                return;
            }
           
            skinDataGridView7.Rows.Clear();
            string sendStr = "getyinpiandetect," + myUser.user_code + ","+page.ToString() + "," + condition+","+condition1;
            string rtnStr = func.sendToServer(sendStr);
            if (rtnStr.Contains("error"))
            {
                MessageBox.Show(rtnStr);
                return;
            }
            List<string> listinfo = func.GetStrlist(rtnStr, '|', false);
            foreach (string s in listinfo)
            {
                if (s.Length < 20) { return; }
                string[] info = s.Split(',');

                int index = skinDataGridView7.Rows.Add();

                skinDataGridView7.Rows[index].Height = 30;
                int j = 0;
                foreach (string ss in info)
                {
                    skinDataGridView7.Rows[index].Cells[j].Value = info[j++];
                }

                skinDataGridView7.Rows[index].Cells[10].Value = "编辑";
            }
            DataGridViewButtonXColumn bcx = skinDataGridView7.Columns[10] as DataGridViewButtonXColumn;
            //  bcx.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            //bcx.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            if (bcx != null)
            {
                bcx.BeforeCellPaint += Yaocai_base_BeforeCellPaint;
            }
        }
        private void skinDataGridView7_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewButtonXCell cell = skinDataGridView7.CurrentCell as DataGridViewButtonXCell;

            if (cell != null)
            {
                DataGridViewButtonXColumn bc =
                    skinDataGridView7.Columns[e.ColumnIndex] as DataGridViewButtonXColumn;

                if (bc != null)
                {
                    string[] info = new string[6];
                    info[0] = skinDataGridView7.Rows[e.RowIndex].Cells[0].Value.ToString();
                    info[1] = skinDataGridView7.Rows[e.RowIndex].Cells[7].Value.ToString(); ;
                    info[2] = skinDataGridView7.Rows[e.RowIndex].Cells[5].Value.ToString();
                    info[3] = skinDataGridView7.Rows[e.RowIndex].Cells[6].Value.ToString();
                    info[4] = skinDataGridView7.Rows[e.RowIndex].Cells[4].Value.ToString();
                    info[5] = skinDataGridView7.Rows[e.RowIndex].Cells[8].Value.ToString();

                    modify_yinpian_check mm = new modify_yinpian_check(info);
                    Point p = superTabControlPanel1.PointToScreen(new Point(0, 0));
                    mm.Location = p;
                    if (mm.ShowDialog() == DialogResult.OK)
                    {
                        superTabItem8_Click(null, null);
                    }
                }
            }
        }

        private void skinButton14_Click(object sender, EventArgs e)
        {
            add_yinpian_check aya = new add_yinpian_check();
            Point p = superTabControlPanel1.PointToScreen(new Point(0, 0));
            aya.StartPosition = FormStartPosition.Manual;
            aya.Location = p;
            if (aya.ShowDialog() == DialogResult.OK)
            {
                superTabItem8_Click(null, null);
            }
        }

        private void superTabItem9_Click(object sender, EventArgs e)
        {
            curPage = 0;
            string sendtmp = "getnum," + myUser.user_code + ",7,";
            string totalitem = func.sendToServer(sendtmp);
            if (totalitem.Contains("error"))
            {
                MessageBox.Show(totalitem);
                return;
            }
            label15.Text = totalitem;
            try
            {
                if (Convert.ToInt32(totalitem) == 0)
                {
                    return;
                }
            }
            catch (Exception)
            {

                MessageBox.Show("页码出错");
                return;
            }
            int page_all = Convert.ToInt32(totalitem) / 10;
            yinpianfenbao(0,null);
            LoadPage(skinFlowLayoutPanel5, "7");
        }
        private void yinpianfenbao(int page,string condition) {
            int page_all = Convert.ToInt32(label15.Text);
            if (page >= page_all)
            {
                MessageBox.Show("最后一页了");
                curPage--;
                return;
            }
            if (page < 0)
            {
                MessageBox.Show("当前为第一页了");
                curPage++;
                return;
            }
            skinDataGridView8.Rows.Clear();
            string sendStr = "getyinpianfenbao," + myUser.user_code + ","+page.ToString()+","+condition;
            string rtnStr = func.sendToServer(sendStr);
            if (rtnStr.Contains("error"))
            {
                MessageBox.Show(rtnStr);
                return;
            }
            List<string> listinfo = func.GetStrlist(rtnStr, '|', false);
            foreach (string s in listinfo)
            {
                if (s.Length < 20) { return; }
                string[] info = s.Split(',');

                int index = skinDataGridView8.Rows.Add();

                skinDataGridView8.Rows[index].Height = 30;
                int j = 0;
                foreach (string ss in info)
                {
                    skinDataGridView8.Rows[index].Cells[j].Value = info[j++];
                }

                skinDataGridView8.Rows[index].Cells[9].Value = "饮片分包";
            }
            DataGridViewButtonXColumn bcx = skinDataGridView8.Columns[9] as DataGridViewButtonXColumn;
            //  bcx.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            //bcx.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            if (bcx != null)
            {
                bcx.BeforeCellPaint += Yaocai_base_BeforeCellPaintPrint;
            }
        }
        private void skinDataGridView8_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewButtonXCell cell = skinDataGridView8.CurrentCell as DataGridViewButtonXCell;

            if (cell != null)
            {
                DataGridViewButtonXColumn bc =
                    skinDataGridView8.Columns[e.ColumnIndex] as DataGridViewButtonXColumn;

                if (bc != null)
                {
                    string[] info = new string[9];
                    info[0] = skinDataGridView8.Rows[e.RowIndex].Cells[0].Value.ToString();
                    info[1] = skinDataGridView8.Rows[e.RowIndex].Cells[1].Value.ToString(); ;
                    info[2] = skinDataGridView8.Rows[e.RowIndex].Cells[2].Value.ToString();
                    info[3] = skinDataGridView8.Rows[e.RowIndex].Cells[3].Value.ToString();
                    info[4] = skinDataGridView8.Rows[e.RowIndex].Cells[4].Value.ToString();
                    info[5] = skinDataGridView8.Rows[e.RowIndex].Cells[5].Value.ToString();
                    info[6] = skinDataGridView8.Rows[e.RowIndex].Cells[6].Value.ToString();
                    info[7] = skinDataGridView8.Rows[e.RowIndex].Cells[7].Value.ToString();
                    info[8] = skinDataGridView8.Rows[e.RowIndex].Cells[8].Value.ToString();

                    yinpian_fenbao mm = new yinpian_fenbao(info);
                    Point p = superTabControlPanel1.PointToScreen(new Point(-121,-29));
                    mm.Location = p;
                    if (mm.ShowDialog() == DialogResult.OK)
                    {
                        superTabItem9_Click(null, null);
                    }
                }
            }
        }

        private void superTabItem10_Click(object sender, EventArgs e)
        {
            curPage = 0;
            string sendtmp = "getnum," + myUser.user_code + ",8,1 or 2,1 or 2 or 3,";
            string totalitem = func.sendToServer(sendtmp);
            if (totalitem.Contains("error"))
            {
                MessageBox.Show(totalitem);
                return;
            }
            skinComboBox1.Text = "请选择";
            skinComboBox3.Text = "请选择";
            label18.Text = totalitem;
            try
            {
                if (Convert.ToInt32(totalitem) == 0)
                {
                    return;
                }
            }
            catch (Exception)
            {

                MessageBox.Show("页码出错");
                return;
            }
            int page_all = Convert.ToInt32(totalitem) / 10;
            yinpianprinttask(0,"1 or 2","1 or 2 or 3",null);
           
            LoadPage(skinFlowLayoutPanel6,"8");
        }
        private void yinpianprinttask(int page,string condition1,string condition2,string condition3) {
            int page_all =Convert.ToInt32( label18.Text);
            if (page >= page_all) {
                MessageBox.Show("最后一页了");
                curPage--;
                return;
            }
            if (page < 0) {
                MessageBox.Show("当前为第一页了");
                curPage++;
                return;
            }
            skinDataGridView9.Rows.Clear();
            string sendStr = "gettaskinfo," + myUser.user_code + ","+page.ToString()+","+
                condition1+","+condition2+","+condition3;
            string rtnStr = func.sendToServer(sendStr);
            if (rtnStr.Contains("error"))
            {
                MessageBox.Show(rtnStr);
                return;
            }
            List<string> listinfo = func.GetStrlist(rtnStr, '|', false);
            foreach (string s in listinfo)
            {
                if (s.Length < 20) { return; }
                string[] info = s.Split(',');

                int index = skinDataGridView9.Rows.Add();

                skinDataGridView9.Rows[index].Height = 30;
                int j = 0;
                foreach (string ss in info)
                {
                    skinDataGridView9.Rows[index].Cells[j].Value = info[j++];
                }
                if (skinDataGridView9.Rows[index].Cells[8].Value.ToString() == "完成")
                {
                    skinDataGridView9.Rows[index].Cells[10].Value = "打印完成";
                }
                else
                {
                    skinDataGridView9.Rows[index].Cells[10].Value = "在线打印";
                }
            }
            DataGridViewButtonXColumn bcx = skinDataGridView9.Columns[10] as DataGridViewButtonXColumn;
            //DataGridViewLabelXColumn bcx = skinDataGridView9.Columns[9] as DataGridViewLabelXColumn;
            //  bcx.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            //bcx.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            if (bcx != null)
            {
                bcx.BeforeCellPaint += task_base_BeforeCellPaint;
            }
        }
        private void task_base_BeforeCellPaint(object sender, BeforeCellPaintEventArgs e)
        {
            DataGridViewButtonXColumn bcx = sender as DataGridViewButtonXColumn;

            if (bcx != null)
            {
                bcx.Image = imageList2.Images[0];
            }
        }

        private void skinDataGridView9_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewButtonXCell cell = skinDataGridView9.CurrentCell as DataGridViewButtonXCell;

            if (cell != null)
            {
                DataGridViewButtonXColumn bc =
                    skinDataGridView9.Columns[e.ColumnIndex] as DataGridViewButtonXColumn;

                if (bc != null)
                {
                    if (skinDataGridView9.Rows[e.RowIndex].Cells[10].Value.ToString() == "--")
                    {
                        return;
                    }
                    string[] info = new string[9];
                    info[0] = skinDataGridView9.Rows[e.RowIndex].Cells[0].Value.ToString();
                    info[1] = skinDataGridView9.Rows[e.RowIndex].Cells[1].Value.ToString(); ;
                    info[2] = skinDataGridView9.Rows[e.RowIndex].Cells[5].Value.ToString();
                    info[3] = skinDataGridView9.Rows[e.RowIndex].Cells[3].Value.ToString();
                    info[4] = skinDataGridView9.Rows[e.RowIndex].Cells[7].Value.ToString();
                    info[5] = skinDataGridView9.Rows[e.RowIndex].Cells[9].Value.ToString();
                    info[6] = skinDataGridView9.Rows[e.RowIndex].Cells[2].Value.ToString();
                    info[7] = skinDataGridView9.Rows[e.RowIndex].Cells[8].Value.ToString();
                   // info[8] = skinDataGridView9.Rows[e.RowIndex].Cells[10].Value.ToString();
                    PrintForm pf = new PrintForm(info);
                    Point p = superTabControlPanel1.PointToScreen(new Point(0, 0));
                    pf.Location = p;
                    pf.Show();
                    superTabItem10_Click(null,null);
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            printerSetting pf = new printerSetting();
            Point p = labelX1.PointToScreen(new Point(0, 0));
            pf.Location = p;
            
            pf.ShowDialog();
        }

        private void skinButton6_Click(object sender, EventArgs e)
        {
            curPage = 0;
            string name = skinWaterTextBox8.Text.Trim();
            if (!func.IsChineseAndNumChar(name)) {
                MessageBox.Show("请不要输入特殊符号");
                return;
            }
            string sendtmp = "getnum," + myUser.user_code + ",3,"+name;
            string totalitem = func.sendToServer(sendtmp);
            if (totalitem.Contains("error"))
            {
                MessageBox.Show(totalitem);
                return;
            }
            label3.Text = totalitem;
            if (totalitem == "0")
            {
                skinDataGridView4.Rows.Clear();
                return;
            }
            
            int page_all = Convert.ToInt32(totalitem) / 10;
           
            yinpianbaseLoadData(0,name);
        }

        private void skinButton17_Click(object sender, EventArgs e)
        {
            curPage = 0;
            string name = skinWaterTextBox15.Text;
            if (!func.IsChineseAndNumChar(name))
            {
                MessageBox.Show("请不要输入特殊符号");
                return;
            }
            string sendtmp = "getnum," + myUser.user_code + ",7," + name;
            string totalitem = func.sendToServer(sendtmp);
            if (totalitem.Contains("error"))
            {
                MessageBox.Show(totalitem);
                return;
            }
            label15.Text = totalitem;
            if (totalitem == "0")
            {
                skinDataGridView8.Rows.Clear();
                return;
            }
           
            int page_all = Convert.ToInt32(totalitem) / 10;

            yinpianfenbao(0,name);
        }

        private void skinButton15_Click(object sender, EventArgs e)
        {
            curPage = 0;
            string name = skinWaterTextBox13.Text.Trim();
            string user = skinWaterTextBox14.Text.Trim();
            string sendtmp = "getnum," + myUser.user_code + ",6," + name+","+user;
            string totalitem = func.sendToServer(sendtmp);
            if (totalitem.Contains("error"))
            {
                MessageBox.Show(totalitem);
                return;
            }
            label12.Text = totalitem;
            if (totalitem == "0")
            {
                skinDataGridView7.Rows.Clear();
                return;
            }
           
            int page_all = Convert.ToInt32(totalitem) / 10;

            yinpiancheckLoadDate(0,name,user);
        }

        private void skinButton11_Click(object sender, EventArgs e)
        {
            curPage = 0;
            string name = skinWaterTextBox9.Text.Trim();
            string standard = skinWaterTextBox10.Text.Trim();
            if (!func.IsChineseAndNumChar(name))
            {
                MessageBox.Show("请不要输入特殊符号");
                return;
            }
            if (!func.IsChineseAndNumChar(standard))
            {
                MessageBox.Show("请不要输入特殊符号");
                return;
            }
            string sendtmp = "getnum," + myUser.user_code + ",4," + name + "," + standard;
            string totalitem = func.sendToServer(sendtmp);
            if (totalitem.Contains("error"))
            {
                MessageBox.Show(totalitem);
                return;
            }
            label6.Text = totalitem;
            if (totalitem == "0")
            {
                skinDataGridView5.Rows.Clear();
                return;
            }
            
            int page_all = Convert.ToInt32(totalitem) / 10;
            yinpianmoreLoadData(0,name,standard);
        }

        private void skinButton13_Click(object sender, EventArgs e)
        {
            curPage = 0;
            string name = skinWaterTextBox11.Text.Trim();
            string assistname = skinWaterTextBox12.Text.Trim();
            if (!func.IsChineseAndNumChar(name))
            {
                MessageBox.Show("请不要输入特殊符号");
                return;
            }
            if (!func.IsChineseAndNumChar(assistname))
            {
                MessageBox.Show("请不要输入特殊符号");
                return;
            }
            string sendtmp = "getnum," + myUser.user_code + ",5," + name + "," + assistname;
            string totalitem = func.sendToServer(sendtmp);
            if (totalitem.Contains("error"))
            {
                MessageBox.Show(totalitem);
                return;
            }
            label9.Text = totalitem;
            if (totalitem == "0")
            {
                skinDataGridView6.Rows.Clear();
                return;
            }
           
            int page_all = Convert.ToInt32(totalitem) / 10;
            yinpianassistLoadData(0, name, assistname);
        }

        private void skinButton19_Click(object sender, EventArgs e)
        {
            curPage = 0;
            string name = skinWaterTextBox18.Text;
            if (!func.IsChineseAndNumChar(name))
            {
                MessageBox.Show("请不要输入特殊符号");
                return;
            }
            string type = skinComboBox3.Text;
            string status = skinComboBox1.Text;
            //MessageBox.Show(skinComboBox3.SelectedIndex.ToString());
            if (type == "中包")
            {
                type = "1";
            }
            else if (type == "小包")
            {
                type = "2";
            }
            else {
                type = "1 or 2";
            }
            if (status == "未打印")
            {
                status = "1";
            }
            else if (status == "打印中")
            {
                status = "2";
            }
            else if (status == "打印完成")
            {
                status = "3";
            }
            else {
                status = "1 or 2 or 3";
            }
            string sendtmp = "getnum," + myUser.user_code + ",8," + type + "," +status+","+name;
            string totalitem = func.sendToServer(sendtmp);
            if (totalitem.Contains("error"))
            {
                MessageBox.Show(totalitem);
                return;
            }
            label18.Text = totalitem;
            if (totalitem == "0")
            {
                skinDataGridView1.Rows.Clear();
                return;
            }
            int page_all = Convert.ToInt32(totalitem) / 10;
            yinpianprinttask(0,type,status,name);
        }

        private void skinButton9_Click(object sender, EventArgs e)
        {
            curPage = 0;
            string name = skinWaterTextBox1.Text.Trim();
            string sendtmp = "getnum," + myUser.user_code + ",0," + name;
            string totalitem = func.sendToServer(sendtmp);
            if (totalitem.Contains("error"))
            {
                MessageBox.Show(totalitem);
                return;
            }
            label21.Text = totalitem;
            if (totalitem == "0") {
                skinDataGridView1.Rows.Clear();
                return;
            }
            
            int page_all = Convert.ToInt32(totalitem) / 10;
            yaocaibaseLoadData(0,name);

        }

        private void skinButton5_Click(object sender, EventArgs e)
        {
            curPage = 0;
            string name = skinWaterTextBox3.Text.Trim();
            string privider = skinWaterTextBox4.Text.Trim();
            string sendtmp = "getnum," + myUser.user_code + ",1," + name+","+privider;
            string totalitem = func.sendToServer(sendtmp);
            if (totalitem.Contains("error"))
            {
                MessageBox.Show(totalitem);
                return;
            }
            label24.Text = totalitem;
            if (totalitem == "0")
            {
                skinDataGridView2.Rows.Clear();
                return;
            }
           
            int page_all = Convert.ToInt32(totalitem) / 10;
            yaocaimoreLoadData(0,name,privider);
        }

        private void skinButton8_Click(object sender, EventArgs e)
        {
            curPage = 0;
            string name = skinWaterTextBox5.Text.Trim();
            string username = skinWaterTextBox6.Text.Trim();
            string sendtmp = "getnum," + myUser.user_code + ",2," + name + "," +username;
            string totalitem = func.sendToServer(sendtmp);
            if (totalitem.Contains("error"))
            {
                MessageBox.Show(totalitem);
                return;
            }
            label27.Text = totalitem;
            if (totalitem == "0")
            {
                skinDataGridView3.Rows.Clear();
                return;
            }
           
            int page_all = Convert.ToInt32(totalitem) / 10;
            yaocaicheckLoadData(0, name, username);
        }
    }
}