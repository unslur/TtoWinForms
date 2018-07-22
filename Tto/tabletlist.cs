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
    public partial class tabletlist : Form
    {
        public tabletlist()
        {
            InitializeComponent();
        }

        private void tabletlist_Load(object sender, EventArgs e)
        {
            ImageList imgList = new ImageList();
            imgList.ImageSize = new Size(1, 25);//设置 ImageList 的宽和高
            listView1.SmallImageList = imgList;
            string tabletinfo = func.getTabletInfo(myUser.company_code);
            int i = 0;
            List<string> listinfo = func.GetStrlist(tabletinfo, '|', false);
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
            //  AnimateWindow(this.Handle, 500, AW_VER_NEGATIVE | AW_SLIDE);
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
        }

        [System.Runtime.InteropServices.DllImport("user32")]
        private static extern bool AnimateWindow(IntPtr hwnd, int dwTime, int dwFlags);

        private const int AW_VER_NEGATIVE = 0x0008;
        private const int AW_SLIDE = 0x40000;
        private const int AW_BLEND = 0x80000;
        private const int AW_HIDE = 0x10000;
        private const int AW_VER_POSITIVE = 0x0004;

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            //MessageBox.Show("ss");
            ListViewItem tablet = this.listView1.SelectedItems[0];
            // string name = tablet.SubItems[3].Text;

            AddTask task = new AddTask(tablet);
            task.ShowDialog();
            AnimateWindow(this.Handle, 500, AW_VER_POSITIVE | AW_HIDE);
            this.Close();
        }

        private void skinButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}