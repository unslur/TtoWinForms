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
    public partial class TaskList : Form
    {
        public TaskList()
        {
            InitializeComponent();
        }
        [System.Runtime.InteropServices.DllImport("user32")]
        private static extern bool AnimateWindow(IntPtr hwnd, int dwTime, int dwFlags);
        const int AW_VER_NEGATIVE = 0x0008;
        const int AW_SLIDE = 0x40000;
        const int AW_BLEND = 0x80000;
        const int AW_HIDE = 0x10000;
        const int AW_VER_POSITIVE = 0x0004;

        private void TaskList_Load(object sender, EventArgs e)
        {
            AnimateWindow(this.Handle, 500, AW_VER_NEGATIVE | AW_SLIDE);
            Task task = new Task();
            List<string> listinfo = func.TaskInfoOnline(myUser.company_code);
            if (listinfo.Count == 0)
            {
                MessageBox.Show("no task");
                return;
            }
            int i = 0;
            foreach (string s in listinfo)
            {
                string[] info = s.Split(',');
               
                ListViewItem tablet = new ListViewItem((i++).ToString());
                foreach (string ss in info)
                {
                    tablet.SubItems.Add(ss);
                }
                listView1.Items.Add(tablet);
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            int m = listView1.CheckedItems.Count;
            string[] a = new string[m];
            int rtn = 0;
            for (int i = 0; i < m; i++)
            {
                if (listView1.CheckedItems[i].Checked)
                {
                    ListViewItem oneInfo = this.listView1.CheckedItems[i];
                    MessageBox.Show(oneInfo.SubItems[1].Text);
                   // func.TaskInfoToLocaldb(oneInfo.SubItems[1].Text, oneInfo.SubItems[2].Text,oneInfo.SubItems[3].Text,oneInfo.SubItems[4].Text,oneInfo.SubItems[5].Text,oneInfo.SubItems[6].Text,oneInfo.SubItems[7].Text);
                    rtn = func.downcode(this.listView1.CheckedItems[i].SubItems[1].Text);
                    if (rtn < 0) {
                        
                    }
                }
            }
           // MessageBox.Show(this.listView1.CheckedItems[0].SubItems[1].Text);
             AnimateWindow(this.Handle, 500, AW_VER_POSITIVE | AW_HIDE);
            this.Close();
        }
       
    }
}
