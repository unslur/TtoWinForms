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
    public partial class yinpian_fenbao : Form
    {
        private string[] info;

        public yinpian_fenbao(string[] info)
        {
            InitializeComponent();
            this.info = info;
            label5.Text = info[3];
            label7.Text = info[4];
            label8.Text = info[6];
            label9.Text = info[7];
            label10.Text = info[8];
            skinComboBox1.Text = "中包";
        }

        private void skinComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (skinComboBox1.SelectedIndex == 1)
            {
                label14.Visible = false;
                skinTextBox1.Visible = false;
            }
            else {
                label14.Visible = true;
                skinTextBox1.Visible = true;
            }
        }

        private void skinButton1_Click(object sender, EventArgs e)
        {
            int Remaining_weight = 0;
            try
            {
                Remaining_weight = (int)Convert.ToDouble(label9.Text);
            }
            catch (Exception)
            {

                MessageBox.Show("剩余重量异常");
                return;
            }

            string printtask_code = "208";
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            printtask_code += Convert.ToInt64(ts.TotalMilliseconds).ToString() + new Random().Next(100, 999).ToString();
            string sendStr, rtnStr;
            int Preweight = 0;
            if (skinComboBox1.SelectedIndex == 1) {
                Int16 num = 0;
                Int16 weight = 0;
                try
                {
                    num = Convert.ToInt16(skinTextBox4.Text.Trim());
                    weight = Convert.ToInt16(skinTextBox2.Text.Trim());
                    Preweight = num * weight;
                }
                catch (Exception)
                {

                    MessageBox.Show("请输入正确数字");
                    return;
                }
                if (Preweight>Remaining_weight) {
                    MessageBox.Show("分包重量大于当前剩余重量，请重新分包！");
                    return;
                }
                sendStr = "fenbao," + num.ToString() + "," + num.ToString() + ",0," +weight.ToString() + "," +
                    myUser.company_cpc + "," + info[1] + "," + myUser.loginname + "," + info[3] + "," +
                    info[2] + "," + myUser.company_areacode + "," + info[5] + "," +
                    printtask_code + "," + DateTime.Now.Year.ToString() + "," +
                    DateTime.Now.Month.ToString() + "," + info[4] + "," +
                    myUser.company_code + "," + myUser.user_code + "," + info[0];
            }
            else {
                int boxitemnum = 0, totalnum = 0, boxnum = 0,boxweight=0;
                double boxweightall =0;
                try
                {
                    boxitemnum = Convert.ToInt32(skinTextBox4.Text.Trim());//small package num per middle package
                    boxnum = Convert.ToInt32(skinTextBox1.Text.Trim());//middle package num
                    boxweight = Convert.ToInt32(skinTextBox2.Text.Trim());
                    boxweightall = boxweight * boxitemnum / 1000;//total weight

                }
                catch (Exception)
                {

                    MessageBox.Show("请输入正确数字");
                    return;
                }
                totalnum = boxitemnum * boxnum;
                Preweight = (int)boxweightall;
                if (Preweight > Remaining_weight)
                {
                    MessageBox.Show("分包重量大于当前剩余重量，请重新分包！");
                    return;
                }
                sendStr = "fenbaomiddle," + totalnum.ToString() + "," + 
                    boxitemnum.ToString() + ","+boxnum.ToString()+","
                    +boxweightall.ToString()+"," +
                   boxweight.ToString() + "," +
                myUser.company_cpc + "," + info[1] + "," + myUser.loginname + "," + 
                info[3] + "," +
                info[2] + "," + myUser.company_areacode + "," + info[5] + "," +
                printtask_code + "," + DateTime.Now.Year.ToString() + "," +
                DateTime.Now.Month.ToString() + "," + info[4] + "," +
                myUser.company_code + "," + myUser.user_code + "," + info[0];
            }
            WaitForm wf = new WaitForm();
            wf.Show();
            rtnStr = func.sendToServer(sendStr);
            if (rtnStr == "OK")
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
                wf.Close();
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