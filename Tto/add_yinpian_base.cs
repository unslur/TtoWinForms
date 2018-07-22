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
    public partial class add_yinpian_base : Form
    {
        public add_yinpian_base()
        {
            InitializeComponent();
        }

        private string sourceid;

        private void skinButton3_Click(object sender, EventArgs e)
        {
            yinpian_name_base ynb = new yinpian_name_base();
            Point p = this.PointToScreen(new Point(-121, -29));
            ynb.Location = p;
            ynb.ShowDialog();
            if (ynb.rtnlv == null) {
                this.Close();
            }
            skinTextBox1.Text = ynb.rtnlv.SubItems[1].Text;
            sourceid = ynb.rtnlv.SubItems[0].Text;
        }

        private string med_code;

        private void skinButton4_Click(object sender, EventArgs e)
        {
            yinpian_yuanyaocai_base yb = new yinpian_yuanyaocai_base(3);
            Point p = this.PointToScreen(new Point(-121, -29));
            yb.Location = p;
            yb.ShowDialog();

            try
            {
                skinTextBox5.Text = yb.rtnlv.SubItems[2].Text;
                med_code = yb.rtnlv.SubItems[0].Text;
            }
            catch (Exception)
            {

                return;
            }
           
        }

        private void skinButton1_Click(object sender, EventArgs e)
        {
            if (skinTextBox2.Text.Trim() == "" || skinTextBox4.Text.Trim() == "" || skinTextBox3.Text.Trim() == "")
            {
                MessageBox.Show("请完善信息");
                return;
            }
            if (skinTextBox1.Text == "" || skinTextBox5.Text == "")
            {
                MessageBox.Show("请选择药材或饮品");
                return;
            }
            if (skinTextBox3.Text.Length > 10)
            {
                MessageBox.Show("饮片批次号长度应小于11");
                return;

            }
            if (skinTextBox4.Text.Length > 30)
            {
                MessageBox.Show("饮片规格长度应小于31");
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
            
            if (!func.IsDouble(skinTextBox2.Text)) {
                MessageBox.Show("请输入正确数字");
                return;
            }
            double v = 0;
            try
            {
                 v= Convert.ToDouble(skinTextBox2.Text.Trim());
                if (v == 0) {
                    MessageBox.Show("请输入正确数字");
                    return;
                }
                
            }
            catch (Exception)
            {
                MessageBox.Show("请输入正确数字");
                return;
            }
           
            string sendStr, rtnStr;
            string tablet_code = "207";

            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            tablet_code += Convert.ToInt64(ts.TotalMilliseconds).ToString() + new Random().Next(100, 999).ToString();
            sendStr = "addyinpianbase," + tablet_code + "," + skinTextBox1.Text + "," +v.ToString() + "," +
                skinTextBox3.Text + "," + skinTextBox4.Text + "," + tablet_code +
                "," + myUser.company_code + "," + myUser.user_code + "," + sourceid + "," + myUser.company_cpc;
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

        private void skinButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}