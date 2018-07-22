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
    public partial class modify_yinpian_assist : Form
    {
        private string[] info;

        public modify_yinpian_assist(string[] info)
        {
            InitializeComponent();
            this.info = info;
            skinTextBox1.Text = info[1];
            skinTextBox2.Text = info[2];
            skinTextBox3.Text = info[3];
            skinTextBox4.Text = info[4];
            skinTextBox5.Text = info[5];
            skinTextBox6.Text = info[6];
        }

        private void modify_yinpian_assist_Load(object sender, EventArgs e)
        {
        }

        private void skinButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void skinButton1_Click(object sender, EventArgs e)
        {
            if (skinTextBox1.Text.Trim() == "" || skinTextBox2.Text.Trim() == "" || skinTextBox3.Text.Trim() == "" || skinTextBox4.Text.Trim() == "" || skinTextBox5.Text.Trim() == "" || skinTextBox6.Text.Trim() == "")
            {
                MessageBox.Show("请完善信息");
                return;
            }
            if (skinTextBox1.Text.Length > 10)
            {
                MessageBox.Show("辅料名称长度应小于11");
                return;

            }
            if (skinTextBox2.Text.Length > 10)
            {
                MessageBox.Show("辅料编号长度应小于11");
                return;

            }
            Double weight = 0;
            try
            {
                weight= Convert.ToDouble(skinTextBox3.Text.Trim());
            }
            catch (Exception)
            {
                MessageBox.Show("请输入正确数字");
                return;
            }
            if (skinTextBox4.Text.Length > 10)
            {
                MessageBox.Show("辅料产地长度应小于11");
                return;

            }
            if (skinTextBox5.Text.Length > 10)
            {
                MessageBox.Show("使用比例长度应小于11");
                return;

            }
            if (skinTextBox6.Text.Length > 10)
            {
                MessageBox.Show("辅料净药材长度应小于11");
                return;

            }
            if (!func.IsChineseAndNumChar(skinTextBox1.Text))
            {
                MessageBox.Show("不要输入符号");
                return;
            }
            if (!func.IsChineseAndNumChar(skinTextBox2.Text))
            {
                MessageBox.Show("不要输入符号");
                return;
            }
            if (!func.IsChineseAndNumChar(skinTextBox4.Text))
            {
                MessageBox.Show("不要输入符号");
                return;
            }
            if (!func.IsChineseAndNumCharpercentage(skinTextBox5.Text))
            {
                MessageBox.Show("不要输入符号");
                return;
            }
            if (!func.IsChineseAndNumChar(skinTextBox6.Text))
            {
                MessageBox.Show("不要输入符号");
                return;
            }
            if (!func.IsDouble(skinTextBox3.Text))
            {
                MessageBox.Show("请输入正确数字");
                return;
            }
          
            string sendStr, rtnStr;
            sendStr = "updateyinpianassist," + info[0] + "," + skinTextBox1.Text + "," + skinTextBox2.Text + "," + weight.ToString() + "," +
                skinTextBox4.Text + "," + skinTextBox5.Text + "," + skinTextBox6.Text;
            rtnStr = func.sendToServer(sendStr);
            if (rtnStr == "OK")
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else {
                MessageBox.Show(rtnStr);
            }
        }
    }
}