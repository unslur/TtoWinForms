using CCWin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tto
{
    public partial class AddTask : Form
    {
        public ListViewItem item;

        public AddTask(ListViewItem item)
        {
            this.item = item;
            InitializeComponent();
        }

        [System.Runtime.InteropServices.DllImport("user32")]
        private static extern bool AnimateWindow(IntPtr hwnd, int dwTime, int dwFlags);

        private const int AW_VER_NEGATIVE = 0x0008;
        private const int AW_SLIDE = 0x40000;
        private const int AW_BLEND = 0x80000;
        private const int AW_HIDE = 0x10000;
        private const int AW_VER_POSITIVE = 0x0004;

        private void AddTask_Load(object sender, EventArgs e)
        {
            name.Text = item.SubItems[3].Text;
            weight.Text = item.SubItems[4].Text;
            spec.Text = item.SubItems[5].Text;
            AnimateWindow(this.Handle, 500, AW_VER_NEGATIVE | AW_SLIDE);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AnimateWindow(this.Handle, 500, AW_VER_POSITIVE | AW_HIDE);
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Socket comm = func.initsocket();
            string printtask_code = "208";
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            printtask_code += Convert.ToInt64(ts.TotalMilliseconds).ToString() + new Random().Next(100, 999).ToString();

            string sendstr = "fenbao," + num.Text + "," + num.Text + ",0," + weight.Text + "," + myUser.company_cpc + "," + item.SubItems[1].Text + "," + myUser.loginname + "," + name.Text + "," + item.SubItems[2].Text + "," + myUser.company_areacode + "," + spec.Text + "," + printtask_code + "," + DateTime.Now.Year.ToString() + "," + DateTime.Now.Month.ToString() + "," + item.SubItems[7].Text + "," + myUser.company_code + "," + myUser.user_code + "," + item.SubItems[8].Text + "," + DateTime.Now.ToString();
            // MessageBox.Show(sendstr);
            byte[] sendbyte = Encoding.UTF8.GetBytes(sendstr);
            comm.Send(sendbyte);
            comm.Close();
            int rtn = func.downcode(printtask_code);
            if (rtn == -1)
            {
                return;
            }
            // MessageBox.Show( createdate.Value.ToString("yyyy-MM-dd hh:mm:ss"));
           // func.TaskInfoToLocaldb(printtask_code, name.Text, num.Text, weight.Text, createdate.Value.ToString("yyyy-MM-dd hh:mm:ss"), item.SubItems[5].Text, item.SubItems[7].Text);
            MessageBox.Show("成功");

            AnimateWindow(this.Handle, 500, AW_VER_POSITIVE | AW_HIDE);
            //this.Hide();

            LocalTask lt = new LocalTask();
            lt.ShowDialog();
            this.Close();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            func.downcode("2081461572490143435");
        }
    }
}