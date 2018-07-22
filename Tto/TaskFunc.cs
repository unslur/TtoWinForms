using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace Tto
{
    internal class TaskFunc
    {
        private string host;
        private string user;
        private string key = "zyczs";

        // private string recStr = "";
        private IPEndPoint ipe;

        private Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        public TaskFunc(string Shost, int Sport, string user_name)
        {
            ipe = new IPEndPoint(IPAddress.Parse(Shost), Sport);
            user = user_name;
        }

        public Boolean syncTask()
        {
            List<string> command = new List<string>();
            command.Add("synctask");
            string taskinfo = commToserver(command);

            MySqlConnection conn = new MySqlConnection("server=127.0.0.1;user=root;password=123123;Database=mydb");
            conn.Open();
            //  DataTable dt = conn.ExecuteDataTable("select * from info");
            MySqlCommand cmd = new MySqlCommand("insert into ss values(5,'fff')", conn);
            //cmd.BeginExecuteNonQuery();
            try
            {
                MySqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    MessageBox.Show(rd.GetString(0) + rd.GetString(1));
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                //MySql.Data.MySqlClient.MySqlException ex = new MySqlException();
                MessageBox.Show(ex.Message);

                // throw;
            }

            return true;
        }

        public string commToserver(List<string> method)
        {
            string recStr = "";
            try
            {
                clientSocket.Connect(ipe);
                string sendStr = this.user;

                foreach (string tmp in method)
                {
                    sendStr += "," + tmp;
                }

                byte[] sendBytes = Encoding.ASCII.GetBytes(sendStr);
                clientSocket.Send(sendBytes);

                byte[] recBytes = new byte[4096];

                int bytes = clientSocket.Receive(recBytes, recBytes.Length, 0);
               // recBytes = func.deCompressBytes(recBytes);
                recStr += Encoding.ASCII.GetString(recBytes, 0, bytes);
                clientSocket.Close();
                return recStr;
            }
            catch (Exception)
            {
                MessageBox.Show("服务器连接失败");

                clientSocket.Close();
                return null;
            }
        }

        public int add(string name, string number, string type)
        {
            List<string> command = new List<string>();
            command.Add("addtask");
            command.Add(name);
            command.Add(number);
            command.Add(type);
            string rtn = commToserver(command);

            return 1;
        }
    }
}