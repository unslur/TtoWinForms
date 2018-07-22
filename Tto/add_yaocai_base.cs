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
    public partial class add_yaocai_base : Form
    {
        public add_yaocai_base()
        {
            InitializeComponent();
        }

        private string sourceid;

        private void buttonX1_Click(object sender, EventArgs e)
        {
            yaocai_name_base ynb = new yaocai_name_base();
            Point p = PointToScreen(new Point(-121, -29));
            ynb.Location = p;
            ynb.ShowDialog();
            skinTextBox1.Text = ynb.rtnlv.SubItems[1].Text;
            sourceid = ynb.rtnlv.SubItems[0].Text;
        }

        private void skinButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void skinButton1_Click(object sender, EventArgs e)
        {
            string med_code = "200";
            if (skinTextBox2.Text.Trim() == ""||skinTextBox3.Text.Trim()=="")
            {
                MessageBox.Show("请完善信息");
                return;
            }
            if (skinTextBox2.Text.Length > 10)
            {
                MessageBox.Show("批号长度应小于11");
                return;

            }
            if (skinTextBox1.Text == "")
            {
                MessageBox.Show("请选择药材");
                return;
            }
            if (!func.IsChineseAndNumChar(skinTextBox2.Text))
            {
                MessageBox.Show("不要输入符号");
                return;
            }
            Int32 iweight = 0;
            try
            {
                if ((iweight= Convert.ToInt32(skinTextBox3.Text)) == 0) {
                    MessageBox.Show("请输入正确数字");
                    return;
                }
            }
            catch (Exception)
            {

                MessageBox.Show("请输入正确数字");
                return;
            }
          
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            med_code += Convert.ToInt64(ts.TotalMilliseconds).ToString() + new Random().Next(100, 999).ToString();
            string med_name = skinTextBox1.Text;
            string batchnumber = skinTextBox2.Text;
            
            string weight = skinTextBox3.Text;
            string sendStr = "addyaocaibase" + "," + med_code + "," + med_name + "," + batchnumber.Trim() + "," + iweight.ToString() + "," + myUser.company_code + "," + myUser.user_code + "," + sourceid + "," + myUser.company_cpc;
            string rtn = func.sendToServer(sendStr);
            if (rtn == "OK")
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            // MessageBox.Show(rtn);
        }

        private void labelX6_Click(object sender, EventArgs e)
        {
        }

        private void Addyaocai_base_Load(object sender, EventArgs e)
        {
        }

        private void labelX1_Click(object sender, EventArgs e)
        {
        }

        private void skinLine1_Click(object sender, EventArgs e)
        {
        }

        private void labelX2_Click(object sender, EventArgs e)
        {
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }
    }
}