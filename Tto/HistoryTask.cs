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
    public partial class HistoryTask : Form
    {
        public HistoryTask()
        {
            InitializeComponent();
        }

        private void HistoryTask_Load(object sender, EventArgs e)
        {
            ImageList imgList = new ImageList();
            imgList.ImageSize = new Size(1, 25);//设置 ImageList 的宽和高
            listView1.SmallImageList = imgList;

            string sql = "select id,task_code, task_name,task_weight,task_itemnum,task_createdate,task_lotnumber from task_info where user_code='" + myUser.user_code + "'and task_flag=2 order by task_createdate desc";
            DataSet ds = null;
            try
            {
                ds = func.sqlQuery(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ListViewItem lv = null;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    lv = new ListViewItem(ds.Tables[0].Rows[i][0].ToString());
                    lv.Tag = ds.Tables[0].Rows[i][0].ToString();
                    for (int j = 1; j < ds.Tables[0].Columns.Count; j++)
                    {
                        lv.SubItems.Add(ds.Tables[0].Rows[i][j].ToString());
                    }

                    this.listView1.Items.Add(lv);
                }
            }
        }

        private void skinButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void skinButton2_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lt in listView1.Items)
            {
                lt.Remove();
            }
            HistoryTask_Load(sender, e);
        }
    }
}