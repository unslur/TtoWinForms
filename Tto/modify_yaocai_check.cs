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
    public partial class modify_yaocai_check : Form
    {
        private string[] info;

        public modify_yaocai_check(string[] info)
        {
            InitializeComponent();
            this.info = info;
            skinComboBox1.Text = info[4];
            skinComboBox2.Text = info[2];
            skinTextBox1.Text = info[3];
            skinTextBox2.Text = info[1];
            skinTextBox3.Text = info[5];
        }

        private void skinButton1_Click(object sender, EventArgs e)
        {
            if (skinTextBox1.Text.Trim() == "" || skinTextBox2.Text.Trim() == "")
            {
                MessageBox.Show("请完善信息");
                return;
            }
            if (skinComboBox1.SelectedIndex == -1 || skinComboBox2.SelectedIndex == -1)
            {
                MessageBox.Show("请完善信息");
                return;
            }
            if (skinTextBox1.Text.Length > 10)
            {
                MessageBox.Show("储藏条件长度应小于11");
                return;

            }
            if (skinTextBox2.Text.Length > 10)
            {
                MessageBox.Show("检验人员长度应小于11");
                return;

            }
            if (!func.IsChineseAndNumChar(skinTextBox1.Text)) {
                MessageBox.Show("不要输入符号");
                return;
            }
            if (!func.IsChineseAndNumChar(skinTextBox2.Text))
            {
                MessageBox.Show("不要输入符号");
                return;
            }
            
            string sql = "updateyaocaidetect," + info[0] + "," + skinComboBox1.Text + ","
                + skinComboBox2.Text + "," + skinTextBox1.Text + ","
                + skinTextBox2.Text;
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