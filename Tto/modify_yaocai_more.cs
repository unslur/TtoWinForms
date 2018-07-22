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
    public partial class modify_yaocai_more : Form
    {
        private string[] info;

        public modify_yaocai_more(string[] info)
        {
            InitializeComponent();
            this.info = info;
            skinTextBox1.Text = info[1];
            skinTextBox2.Text = info[2];
            skinTextBox3.Text = info[3];
           // skinTextBox4.Text = info[4];
        }

        private void skinButton1_Click(object sender, EventArgs e)
        {

            if (skinTextBox3.Text.Trim() == "" || skinTextBox1.Text.Trim() == ""||skinTextBox4.Text=="")
            {
                MessageBox.Show("请完善信息");
                return;
            }
            if (skinTextBox1.Text.Length > 30)
            {
                MessageBox.Show("供应商长度应小于31");
                return;

            }
            if (skinTextBox3.Text.Length > 10)
            {
                MessageBox.Show("执行标准长度应小于11");
                return;

            }
            if (skinTextBox4.Text.Length > 30)
            {
                MessageBox.Show("详细地址长度应小于11");
                return;

            }
            if (!func.IsChineseAndNumChar(skinTextBox1.Text))
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
            ComboboxItem ci = skinComboBox3.SelectedItem as ComboboxItem;
            string pcode = "";
            if (ci == null)
            {
                string sendStr, rtnStrs;
                sendStr = "getaddressinfoshortname," + skinComboBox3.Text;
                rtnStrs = func.sendToServer(sendStr);
                if (rtnStrs.Contains("error"))
                {
                    MessageBox.Show(rtnStrs);
                    return;
                }
                List<string> listinfo = func.GetStrlist(rtnStrs, ',', false);
                pcode = listinfo[0];
                // MessageBox.Show("请选择县区！");
                // return;
            }
            else {
                pcode = ci.Value.ToString();
            }
           
            string sql = "updateyaocaimore," + info[0] + "," + skinTextBox1.Text + "," + skinTextBox3.Text+
                ","+skinComboBox1.Text+","+skinComboBox2.Text+","+skinComboBox3.Text+","+skinTextBox4.Text.Trim()+","+pcode;
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

        private void modify_yaocai_more_Load(object sender, EventArgs e)
        {
           // skinComboBox1.Text = "ssss";
            string sql = "getaddress," + info[0]+",med";
            string rtnStr = func.sendToServer(sql);
            if (rtnStr.Contains("error"))
            {
                MessageBox.Show(rtnStr);
            }
            else
            {
                List<string> address = func.GetStrlist(rtnStr, ',', false);
                skinComboBox1.Text = address[0];
                skinComboBox2.Text = address[1];
                skinComboBox3.Text = address[2];
                skinTextBox4.Text = address[3];
            }
        }

        private void skinComboBox1_Click(object sender, EventArgs e)
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
            skinComboBox2.Text = "";
            skinComboBox3.Text = "";
        }

        private void skinComboBox2_Click(object sender, EventArgs e)
        {
            if (skinComboBox2.Text.Trim() == "")
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
                skinComboBox3.Text = "";
            }
            else {
                skinComboBox2.Items.Clear();
                string pcode = "";
                string sendStr, rtnStr;
                sendStr = "getaddressinfoname," + skinComboBox2.Text;
                rtnStr = func.sendToServer(sendStr);
                if (rtnStr.Contains("error"))
                {
                    MessageBox.Show(rtnStr);
                    return;
                }
                List<string> listinfo = func.GetStrlist(rtnStr, ',', false);
                pcode = listinfo[0];
                sendStr = "getaddressinfo," + pcode;
                rtnStr = func.sendToServer(sendStr);
                if (rtnStr.Contains("error"))
                {
                    MessageBox.Show(rtnStr);
                    return;
                }
                listinfo = func.GetStrlist(rtnStr, '|', false);
                foreach (string s in listinfo)
                {
                    if (s.Length < 5) { return; }
                    string[] info = s.Split(',');

                    ComboboxItem cTiem = new ComboboxItem();
                    cTiem.Text = info[1]; cTiem.Value = info[0];
                    skinComboBox2.Items.Add(cTiem);
                }
                skinComboBox3.Text = "";
            }
        }

        private void skinComboBox3_Click(object sender, EventArgs e)
        {
            if (skinComboBox3.Text.Trim() == "")
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
            else {
                skinComboBox3.Items.Clear();
                string pcode = "";
                string sendStr, rtnStr;
                sendStr = "getaddressinfoname," + skinComboBox3.Text;
                rtnStr = func.sendToServer(sendStr);
                if (rtnStr.Contains("error"))
                {
                    MessageBox.Show(rtnStr);
                    return;
                }
                List<string> listinfo = func.GetStrlist(rtnStr, ',', false);
                pcode = listinfo[0];
                sendStr = "getaddressinfo," + pcode;
                rtnStr = func.sendToServer(sendStr);
                if (rtnStr.Contains("error"))
                {
                    MessageBox.Show(rtnStr);
                    return;
                }
                listinfo = func.GetStrlist(rtnStr, '|', false);
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
    }
}