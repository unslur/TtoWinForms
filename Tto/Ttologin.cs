using CCWin;
using CCWin.SkinControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tto
{
    public partial class Ttologin : Form
    {
        public Ttologin()
        {
            InitializeComponent();
        }

        //private string verifyCode = "";//生成的验证码

        private void button1_Click(object sender, EventArgs e)
        {
            // MessageBox.Show(skinCode1.CodeStr);
            if (user.Text.Length==0||pass.Text.Length==0) {
                MessageBox.Show("请先输入账号和密码");
                return;
            }
            if (skinCode1.CodeStr.ToLower() != verify.Text.ToLower())
            {
                MessageBox.Show("验证码错误");
                // this.refresh(sender, e);
                verify.SelectAll();

                return;
            };
            SkinProgressIndicator sk = new SkinProgressIndicator();

            string recStr = "";
            Socket clientSocket;

            try
            {
                // clientSocket = func.initsocket();

                string sendStr = "login," + user.Text + "," + pass.Text;

                //byte[] sendBytes = Encoding.ASCII.GetBytes(sendStr);
                //clientSocket.Send(sendBytes);

                //byte[] recBytes = new byte[4096];

                //int bytes = clientSocket.Receive(recBytes, recBytes.Length, 0);
                //clientSocket.Close();

                //recStr += Encoding.UTF8.GetString(recBytes, 0, bytes);
                recStr = func.sendToServer(sendStr);
            }
            catch (Exception)
            {
                //MessageBox.Show("服务器连接失败");
                Ttologin_Load(sender, e);

                return;
            }
            //Console.WriteLine(recStr);
            if (recStr.IndexOf(user.Text) != -1)
            {
                string[] info = recStr.Split(',');
                myUser.company_code = info[0];
                myUser.company_cpc = info[1];
                myUser.company_areacode = info[2];
                myUser.user_code = info[3];
                myUser.company_name = info[4];
                myUser.user_name = info[5];
                myUser.user_pass = pass.Text;
                myUser.loginname = user.Text;
                if (checkBox1.CheckState == CheckState.Checked)
                {
                    func.WritePrivateProfileString("user", "name", user.Text, "./Profile.ini");
                    func.WritePrivateProfileString("user", "pass", func.EncryptString(pass.Text), "./Profile.ini");
                }
                else {
                    func.WritePrivateProfileString("user", "name", "", "./Profile.ini");
                    func.WritePrivateProfileString("user", "pass", "", "./Profile.ini");
                }
                this.DialogResult = DialogResult.OK;
                //  MessageBox.Show(recStr);
                this.Close();
            }
            else if (recStr.Contains("303")) {
                MessageBox.Show("远程服务器连接数据库失败，联系服务器管理人员");
            }
            else if (recStr.Contains("304"))
            {
               
            }
            else {
                MessageBox.Show("用户名或者密码错误");
                //  Ttologin_Load(sender, e);
            }
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }
        string ip = "";
        string port = "";
        private void Ttologin_Load(object sender, EventArgs e)
        {
           
            StringBuilder temp = new StringBuilder(255);
            func.GetPrivateProfileString("server", "ip", "noValue", temp, 255, "./Profile.ini");
            if (temp.ToString() == "noValue"|| temp.ToString()=="")
            {
                MessageBox.Show("请对Profile.ini文件进行网络配置");
                this.Close();
                return;
            }
            ip = temp.ToString();
            func.GetPrivateProfileString("server", "port", "noValue", temp, 255, "./Profile.ini");
            if (temp.ToString() == "noValue" || temp.ToString() == "")
            {
                MessageBox.Show("请对Profile.ini文件进行网络配置");
                this.Close();
                return;
            }
            port = temp.ToString();
            int iport = 0;
            try
            {
                iport = Convert.ToInt32(port);
            }
            catch (Exception)
            {
                MessageBox.Show("请对Profile.ini文件进行正确端口配置，");
                this.Close();
                return;

            }
            func.initserver(ip,iport);
            func.GetPrivateProfileString("user", "pass", "noValue", temp, 255, "./Profile.ini");
            if (func.DecryptString(temp.ToString()) == "noValue"|| func.DecryptString(temp.ToString()) == "")
            {
                return;
            }
            string passtmp = func.DecryptString(temp.ToString());
            func.GetPrivateProfileString("user", "name", "noValue", temp, 255, "./Profile.ini");
            if (temp.ToString()== "noValue" || temp.ToString() == "")
            {
                return;
            }
            pass.Text = passtmp;
            user.Text = temp.ToString();
            verify.Text = skinCode1.CodeStr;
            checkBox1.Checked = true;
        }
        private void update() {
            FtpWebRequest reqFtp;
            string ftpservice = "ftp://192.168.9/198/Tto.exe";
            string tmpname = Guid.NewGuid().ToString();
            string localfile = tmpname;
            FileStream outputstream = new FileStream("Tto.exe", FileMode.Create);
            reqFtp = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpservice));
            reqFtp.Method = WebRequestMethods.Ftp.DownloadFile;
            reqFtp.UseBinary = true;
            reqFtp.KeepAlive = false;
            reqFtp.Credentials = new NetworkCredential("joy","1");
            FtpWebResponse response = (FtpWebResponse)reqFtp.GetResponse();
            Stream ftpstream = response.GetResponseStream();
            long cl = response.ContentLength;
            int buffersize = 2048;
            int readcount;
            byte[] buffer = new byte[buffersize];
            readcount = ftpstream.Read(buffer, 0, buffersize);
            while (readcount > 0) {
                outputstream.Write(buffer,0,readcount);
                readcount = ftpstream.Read(buffer,0,buffersize);

            }
        }
        private void verify_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.button1_Click(sender, e);
            }
        }

        private void refresh(object sender, EventArgs e)
        {
            skinCode1.NewCode();
            user.Text = "";
            pass.Text = "";
            verify.Text = "";
        }
    }
}