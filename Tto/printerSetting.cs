using DevComponents.DotNetBar.Controls;
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
    public partial class printerSetting : Form
    {
        public printerSetting()
        {
            InitializeComponent();
        }

        private void printerSetting_Load(object sender, EventArgs e)
        {
            skinDataGridView9.Rows.Clear();
            string printer = func.getDevice();
            List<string> listinfo = func.GetStrlist(printer, '|', false);
            foreach (string s in listinfo) {
                if (s.Length < 20) { return; }
                string[] info = s.Split('^');

                int index = skinDataGridView9.Rows.Add();

                skinDataGridView9.Rows[index].Height = 30;
                int j = 0;
                foreach (string ss in info)
                {
                    skinDataGridView9.Rows[index].Cells[j].Value = info[j++];
                }

                //skinDataGridView9.Rows[index].Cells[5].Value = "编辑";
            }
        }

        private void skinDataGridView9_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void skinButton16_Click(object sender, EventArgs e)
        {
            string sentStr, rtnStr;
            sentStr = "getprinter,"+myUser.user_code;
            rtnStr = func.sendToServer(sentStr);
            if (rtnStr.Contains("error")) {
                MessageBox.Show(rtnStr);
                return;
            }
            List<string> listinfo = func.GetStrlist(rtnStr, '|', false);
            List<string> deviceSql = new List<string>();
            foreach (string s in listinfo)
            {
                if (s.Length < 3) { return; }
                string[] info = s.Split(',');
                if (info.Count() != 4) {
                    MessageBox.Show("获得数据错误");
                    return;
                }
                string sql = string.Format("insert into device values('{0}','{1}','{2}','{3}',0,'{4}')",info[0],info[1],info[2],myUser.user_code,info[3]);
                deviceSql.Add(sql);
               
            }
            func.ExecuteSql("delete from device where user_code='"+myUser.user_code+"'");
            func.ExecuteSqlTran(deviceSql);
            printerSetting_Load(null,null);
            //addDevice ad = new addDevice();
            //Point p = skinDataGridView9.PointToScreen(new Point(0, 0));
            //ad.Location = p;

            //if (ad.ShowDialog() == DialogResult.OK)
            //{
            //    printerSetting_Load(sender, e);
            //}
        }

        private void skinButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
