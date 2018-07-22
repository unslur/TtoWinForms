using System;
using System.Windows.Forms;

namespace Tto
{
    public partial class addDevice : Form
    {
        public addDevice()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql = "insert into device(name,ip,port,user_code,deviceType) values('" + name.Text + "','" + ip.Text + "','" + port.Text + "','" + myUser.user_code + "','" + comboBox1.SelectedItem.ToString() + "')";
            try
            {
                if (1 == func.ExecuteSql(sql))
                {
                    MessageBox.Show("添加成功");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else {
                    MessageBox.Show("添加失败");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("添加失败,确保名称唯一");
            }
        }

        private void skinButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}