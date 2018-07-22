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
    public partial class add_yaocai_more : Form
    {
        public add_yaocai_more()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void label4_Click(object sender, EventArgs e)
        {
        }

        private void labelX7_Click(object sender, EventArgs e)
        {
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private string med_code = "";

        private void skinButton3_Click(object sender, EventArgs e)
        {
            yinpian_yuanyaocai_base yyb = new yinpian_yuanyaocai_base(1);
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
            if (skinTextBox2.Text.Trim() == "" || skinTextBox4.Text.Trim() == ""||skinTextBox5.Text.Trim()=="")
            {
                MessageBox.Show("请完善信息");
                return;
            }
            if (skinTextBox1.Text == "")
            {
                MessageBox.Show("请选择药材");
                return;
            }
            if (skinTextBox2.Text.Length > 30)
            {
                MessageBox.Show("供应商长度应小于31");
                return;

            }
            if (skinTextBox4.Text.Length > 10)
            {
                MessageBox.Show("执行标准长度应小于11");
                return;

            }
            if (skinComboBox1.SelectedIndex == -1 || skinComboBox2.SelectedIndex == -1 || skinComboBox3.SelectedIndex == -1)
            {
                MessageBox.Show("请选择地址！");
                return;
            }
            ComboboxItem ci = skinComboBox3.SelectedItem as ComboboxItem;
            if (ci == null)
            {
                MessageBox.Show("请选择县区！");
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
            if (!func.IsChineseAndNumChar(skinTextBox5.Text))
            {
                MessageBox.Show("不要输入符号");
                return;
            }
           

            string pcode = ci.Value.ToString();
            string sendStr, rtnStr;
            string code = "201";

            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            code += Convert.ToInt64(ts.TotalMilliseconds).ToString() + new Random().Next(100, 999).ToString();
            sendStr = "addyaocaimore," + med_code + "," + skinTextBox2.Text + "," +
                dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss") + "," +
                skinTextBox4.Text + "," + skinComboBox1.Text + "," +
               skinComboBox2.Text + "," + skinComboBox3.Text + "," +
                skinTextBox5.Text + "," + myUser.company_code + "," +
                code + "," + myUser.user_code + "," + pcode;
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

        private void add_yaocai_more_Load(object sender, EventArgs e)
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

                ComboboxItem ci = new ComboboxItem();
                ci.Text = info[1]; ci.Value = info[0];
                skinComboBox1.Items.Add(ci);
            }
        }

        private void skinComboBox2_Click(object sender, EventArgs e)
        {
            ComboboxItem ci = skinComboBox1.SelectedItem as ComboboxItem;
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

                ComboboxItem cTiem = new ComboboxItem();
                cTiem.Text = info[1]; cTiem.Value = info[0];
                skinComboBox2.Items.Add(cTiem);
            }
        }

        private void skinButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void skinComboBox3_Click(object sender, EventArgs e)
        {
            ComboboxItem ci = skinComboBox2.SelectedItem as ComboboxItem;
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
    }

    public class ComboboxItem
    {
        public string Text { get; set; }
        public object Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}