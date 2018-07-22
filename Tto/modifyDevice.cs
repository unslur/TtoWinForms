using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tto
{
    public partial class modifyDevice : Form
    {
        private string[] device;

        public modifyDevice(string[] device)
        {
            InitializeComponent();
            this.device = device;
        }

        private void modifyDevice_Load(object sender, EventArgs e)
        {
            if (device.Length < 3)
            {
                MessageBox.Show("打印机参数错误");
            }
            name.Text = device[0];
            ip.Text = device[2];
            port.Text = device[3];
            type.Text = device[1];
            if (name.Text == myUser.curPrinter[0])
            {
                checkBox1.Checked = true;
            }
        }

        private void modify_Click(object sender, EventArgs e)
        {
            bool blnTest = false;
            bool _Result = true;

            Regex regex = new Regex("^[0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3}$");
            blnTest = regex.IsMatch(ip.Text);
            if (blnTest == true)
            {
                string[] strTemp = this.ip.Text.Split(new char[] { '.' }); // textBox1.Text.Split(new char[] { '.' });
                for (int i = 0; i < strTemp.Length; i++)
                {
                    if (Convert.ToInt32(strTemp[i]) > 255)
                    { //大于255则提示，不符合IP格式
                        MessageBox.Show("不符合IP格式");
                        return;
                    }
                }
            }
            else
            {
                //输入非数字则提示，不符合IP格式
                MessageBox.Show("不符合IP格式");
                return;
            }
            if (checkBox1.Checked)
            {
                myUser.curPrinter[0] = name.Text;
                myUser.curPrinter[1] = ip.Text;
                myUser.curPrinter[2] = port.Text;
                myUser.curPrinter[3] = type.Text;
            }

            string sql = "update device set name='" + name.Text + "',ip='" + ip.Text + "',port='" + port.Text + "' where name='" + device[0] + "'";
            if (func.ExecuteSql(sql) > 0)
            {
                MessageBox.Show("修改成功");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void delete_Click(object sender, EventArgs e)
        {
            string sql = "delete from device where name='" + name.Text + "'";
            if (func.ExecuteSql(sql) > 0)
            {
                MessageBox.Show("删除成功");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            
        }
    }
}