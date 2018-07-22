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
    public partial class modify_yaocai_base : Form
    {
        private string[] info;

        public modify_yaocai_base(string[] info)
        {
            InitializeComponent();
            this.info = info;
            skinTextBox1.Text = info[1];
            skinTextBox2.Text = info[2];
            skinTextBox3.Text = info[3];
        }

        private void skinButton1_Click(object sender, EventArgs e)
        {
            if (skinTextBox3.Text.Trim() == "" || skinTextBox2.Text.Trim() == "")
            {
                MessageBox.Show("请完善信息");
                return;
            }
            if (skinTextBox2.Text.Length > 10)
            {
                MessageBox.Show("批次号长度应小于11");
                return;

            }
            if (!func.IsChineseAndNumChar(skinTextBox2.Text))
            {
                MessageBox.Show("不要输入特殊字符");
                return;
            }
            if (!func.IsINT(skinTextBox3.Text)) {
                MessageBox.Show("请输入正确数字");
                return;
            }
            Int32 weight = 0;
            try
            {
               weight= Convert.ToInt32(skinTextBox3.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("请输入正确数字");
                return;
            }
           
            string sql = "updateyaocaiinfo," + info[0] + "," + skinTextBox2.Text + "," +weight.ToString();
            string rtnStr = func.sendToServer(sql);
            if (rtnStr.Contains("ok"))
            {
                MessageBox.Show("操作成功");
                this.DialogResult = DialogResult.OK;
            }
            else {
                this.DialogResult = DialogResult.Cancel;
            }

            this.Close();
        }

        private void skinButton2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}