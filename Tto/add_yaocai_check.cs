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
    public partial class add_yaocai_check : Form
    {
        public add_yaocai_check()
        {
            InitializeComponent();
        }

        private string med_code = "";

        private void skinButton3_Click(object sender, EventArgs e)
        {
            yinpian_yuanyaocai_base yyb = new yinpian_yuanyaocai_base(2);
            yyb.StartPosition = FormStartPosition.Manual;
            yyb.Location = this.PointToScreen(new Point(-121,-29)); ;
            yyb.ShowDialog();
            if (yyb.rtnlv == null) {
                this.Close();
            }
            try
            {
                med_code = yyb.rtnlv.SubItems[0].Text;
                skinTextBox1.Text = yyb.rtnlv.SubItems[2].Text;
            }
            catch (Exception)
            {

                return;
            }
        }

        private void skinButton1_Click(object sender, EventArgs e)
        {
            string sendStr, rtnStr;
            string code = "204";
            if (skinTextBox1.Text.Trim() == "") {
                MessageBox.Show("请选择药材");
                return;
            }
            if (skinTextBox4.Text.Trim() == "" || skinTextBox3.Text.Trim() == "")
            {
                MessageBox.Show("请完善信息");
                return;
            }
            if (skinComboBox1.SelectedIndex == -1 || skinComboBox2.SelectedIndex == -1)
            {
                MessageBox.Show("请完善信息");
                return;
            }
            if (skinTextBox3.Text.Length > 10)
            {
                MessageBox.Show("检验人员长度应小于11");
                return;

            }
            if (skinTextBox4.Text.Length > 10)
            {
                MessageBox.Show("贮藏条件长度应小于11");
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
            
            
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            code += Convert.ToInt64(ts.TotalMilliseconds).ToString() + new Random().Next(100, 999).ToString();
            sendStr = "addyaocaidetect," + med_code + "," + code + "," + skinComboBox1.Text + "," +
                skinComboBox2.Text + "," + skinTextBox4.Text + "," + skinTextBox3.Text + "," +
               dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss") + "," + myUser.company_code + "," + myUser.user_code;
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