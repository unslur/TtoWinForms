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
    public partial class add_yinpian_assist : Form
    {
        public add_yinpian_assist()
        {
            InitializeComponent();
        }

        private string tablet_code = "";

        private void skinButton3_Click(object sender, EventArgs e)
        {
            yinpian_yaocai yy = new yinpian_yaocai(1);
            yy.StartPosition = FormStartPosition.Manual;
            yy.Location = this.PointToScreen(new Point(-121,-29)); ;
            yy.ShowDialog();
            if (yy.rtnlv == null) {
                this.Close();
            }
            try
            {
                tablet_code = yy.rtnlv.SubItems[0].Text;
                skinTextBox7.Text = yy.rtnlv.SubItems[2].Text;
            }
            catch (Exception)
            {

                return;
            }
        }

        private void skinButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void skinButton1_Click(object sender, EventArgs e)
        {
            if (skinTextBox1.Text.Trim() == "" || skinTextBox2.Text.Trim() == ""|| skinTextBox3.Text.Trim() == "" || skinTextBox4.Text.Trim() == "" || skinTextBox5.Text.Trim() == "" || skinTextBox6.Text.Trim() == "" )
            {
                MessageBox.Show("请完善信息");
                return;
            }
            if (skinTextBox7.Text.Trim() == "")
            {
                MessageBox.Show("请选择饮片");
                return;
            }
            if (skinTextBox1.Text.Length > 10)
            {
                MessageBox.Show("辅料品名长度应小于11");
                return;

            }
            if (skinTextBox2.Text.Length > 10)
            {
                MessageBox.Show("辅料编号长度应小于11");
                return;

            }
            if (skinTextBox4.Text.Length > 10)
            {
                MessageBox.Show("辅料产地长度应小于11");
                return;

            }
            if (skinTextBox5.Text.Length > 10)
            {
                MessageBox.Show("辅料使用比列长度应小于11");
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
            if (!func.IsDouble(skinTextBox3.Text)) {
                MessageBox.Show("请输入正确数字");
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
            
            string sendStr, rtnStr;
            string code = "205";

            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            code += Convert.ToInt64(ts.TotalMilliseconds).ToString() + new Random().Next(100, 999).ToString();
            sendStr = "addyinpianassist," + code + "," + tablet_code + "," +
                skinTextBox1.Text + "," + skinTextBox2.Text + "," +
                weight.ToString() + "," + skinTextBox4.Text + "," +
                skinTextBox5.Text + "," + skinTextBox6.Text + "," +
                myUser.company_code + "," + myUser.user_code;
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