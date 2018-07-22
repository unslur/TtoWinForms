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
    public partial class add_yinpian_more : Form
    {
        public add_yinpian_more()
        {
            InitializeComponent();
        }

        private string tablet_code = "";

        private void skinButton3_Click(object sender, EventArgs e)
        {
            // yinpian_yuanyaocai_base yyb = new yinpian_yuanyaocai_base(4);
            yinpian_yaocai yy = new yinpian_yaocai(0);
            yy.StartPosition = FormStartPosition.Manual;
            yy.Location = this.PointToScreen(new Point(-121, -29)); ;
            yy.ShowDialog();
            if (yy.rtnlv == null) {
                this.Close();
            }
            try
            {
                tablet_code = yy.rtnlv.SubItems[0].Text;
                skinTextBox1.Text = yy.rtnlv.SubItems[2].Text;
            }
            catch (Exception)
            {

                return;
            }
        }

        private void labelX1_Click(object sender, EventArgs e)
        {
        }

        private void skinButton1_Click(object sender, EventArgs e)
        {
            if (skinTextBox2.Text.Trim() == "" || skinTextBox5.Text.Trim() == "" || skinTextBox1.Text.Trim() == "")
            {
                MessageBox.Show("请完善信息");
                return;
            }
            if (skinComboBox1.SelectedIndex == -1 || skinComboBox2.SelectedIndex == -1 || skinComboBox3.SelectedIndex == -1)
            {
                MessageBox.Show("请完善信息");
                return;
            }
            if (skinTextBox2.Text.Length > 10)
            {
                MessageBox.Show("执行标准长度应小于11");
                return;

            }
           
            if (skinTextBox3.Text.Length > 30)
            {
                MessageBox.Show("批准文号长度应小于31");
                return;

            }
            if (skinTextBox4.Text.Length > 5)
            {
                MessageBox.Show("饮片有效期长度应小于6");
                return;
            }
            if (!func.IsChineseAndNumChar(skinTextBox2.Text))
            {
                MessageBox.Show("不要输入符号");
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
            if (!func.IsChineseAndNumChar(skinTextBox5.Text))
            {
                MessageBox.Show("不要输入符号");
                return;
            }
            
            
            string sendStr, rtnStr;
            string code = "206";

            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            code += Convert.ToInt64(ts.TotalMilliseconds).ToString() + new Random().Next(100, 999).ToString();
            sendStr = "addyinpianmore," + code + "," + tablet_code + "," +
                dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss") + "," +
                skinTextBox2.Text + "," + skinTextBox3.Text + "," + skinTextBox4.Text + "," +
                skinComboBox1.Text + "," + skinComboBox2.Text + "," + skinComboBox3.Text + "," +
                skinTextBox5.Text + "," + myUser.company_code + "," + myUser.user_code;
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

        private void add_yinpian_more_Load(object sender, EventArgs e)
        {
            string sendStr, rtnStr;
            sendStr = "getaddressinfo,0";
            rtnStr = func.sendToServer(sendStr);
            if (rtnStr.Contains("error"))
            {
                MessageBox.Show(rtnStr);
                return;
            }
            List<string> listinfo = func.GetStrlist(rtnStr, '|', false);
            foreach (string s in listinfo)
            {
                if (s.Length < 10) { return; }
                string[] info = s.Split(',');

                myComboboxItem ci = new myComboboxItem();
                ci.Text = info[1]; ci.Value = info[0];
                skinComboBox1.Items.Add(ci);
            }
        }

        private void skinComboBox2_Click(object sender, EventArgs e)
        {
            myComboboxItem ci = skinComboBox1.SelectedItem as myComboboxItem;
            if (ci == null)
            {
                MessageBox.Show("请选择省份！");
                return;
            }
            skinComboBox2.Items.Clear();
            string pcode = ci.Value.ToString();
            string sendStr, rtnStr;
            sendStr = "getaddressinfo," + pcode;
            rtnStr = func.sendToServer(sendStr);
            if (rtnStr.Contains("error"))
            {
                MessageBox.Show(rtnStr);
                return;
            }
            List<string> listinfo = func.GetStrlist(rtnStr, '|', false);
            foreach (string s in listinfo)
            {
                if (s.Length < 5) { return; }
                string[] info = s.Split(',');

                myComboboxItem cTiem = new myComboboxItem();
                cTiem.Text = info[1]; cTiem.Value = info[0];
                skinComboBox2.Items.Add(cTiem);
            }
        }

        private void skinComboBox3_Click(object sender, EventArgs e)
        {
            myComboboxItem ci = skinComboBox2.SelectedItem as myComboboxItem;
            if (ci == null)
            {
                MessageBox.Show("请选择市！");
                return;
            }
            skinComboBox3.Items.Clear();
            string pcode = ci.Value.ToString();
            string sendStr, rtnStr;
            sendStr = "getaddressinfo," + pcode;
            rtnStr = func.sendToServer(sendStr);
            if (rtnStr.Contains("error"))
            {
                MessageBox.Show(rtnStr);
                return;
            }
            List<string> listinfo = func.GetStrlist(rtnStr, '|', false);
            foreach (string s in listinfo)
            {
                if (s.Length < 5) { return; }
                string[] info = s.Split(',');

                ComboboxItem cTiem = new ComboboxItem();
                cTiem.Text = info[1]; cTiem.Value = info[0];
                skinComboBox3.Items.Add(cTiem);
            }
        }

        private void skinButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

    public class myComboboxItem
    {
        public string Text { get; set; }
        public object Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}