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
    public partial class modify_yinpian_base : Form
    {
        private string[] info;

        public modify_yinpian_base(string[] info)
        {
            InitializeComponent();
            this.info = info;
        }

        private void modify_yinpian_base_Load(object sender, EventArgs e)
        {
            skinTextBox1.Text = info[1];
            skinTextBox2.Text = info[2];
            skinTextBox3.Text = info[3];
            skinTextBox4.Text = info[4];
        }

        private void skinButton1_Click(object sender, EventArgs e)
        {
            if (skinTextBox3.Text.Trim() == "" || skinTextBox4.Text.Trim() == "")
            {
                MessageBox.Show("请完善信息");
                return;
            }
            if (skinTextBox3.Text.Length > 10)
            {
                MessageBox.Show("批次号长度应小于11");
                return;

            }
            if (skinTextBox4.Text.Length > 30)
            {
                MessageBox.Show("规格长度应小于31");
                return;

            }
            if (!func.IsChineseAndNumChar(skinTextBox3.Text))
            {
                MessageBox.Show("不要输入符号");
                return;
            }
            if (!func.IsChineseAndNumChar(skinTextBox4.Text))
            {
                MessageBox.Show("不要输入符号");
                return;
            }
            
            string sendStr, rtnStr;
            sendStr = "updateyinpianbase," + info[0] + "," + skinTextBox3.Text + "," + skinTextBox4.Text;
            rtnStr = func.sendToServer(sendStr);
            // MessageBox.Show(rtnStr);
            if (rtnStr == "update ok")
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else {
                MessageBox.Show(rtnStr);
            }
        }

        private void skinButton2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}