using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using zlib;

namespace Tto
{
    internal static class func
    {
        private static string dbpath = "tcm.db";
        public static string connectionString = "Data Source=" + dbpath + ";";

        public static Byte[] refreshWin(int length, string verifyCode)
        {
            verify code = new verify();
            verifyCode = code.CreateValidateCode(6);

            return code.CreateValidateGraphic(verifyCode);
        }

        /// <summary>
        /// 复制流
        /// </summary>
        /// <param name="input">原始流</param>
        /// <param name="output">目标流</param>
        public static void CopyStream(System.IO.Stream input, System.IO.Stream output)
        {
            byte[] buffer = new byte[2000];
            int len;
            while ((len = input.Read(buffer, 0, 2000)) > 0)
            {
                output.Write(buffer, 0, len);
            }
            output.Flush();
        }

        ///// <summary>
        ///// 压缩字节数组
        ///// </summary>
        ///// <param name="sourceByte">需要被压缩的字节数组</param>
        ///// <returns>压缩后的字节数组</returns>
        //public static byte[] compressBytes(byte[] sourceByte)
        //{
        //    MemoryStream inputStream = new MemoryStream(sourceByte);
        //    Stream outStream = compressStream(inputStream);
        //    byte[] outPutByteArray = new byte[outStream.Length];
        //    outStream.Position = 0;
        //    outStream.Read(outPutByteArray, 0, outPutByteArray.Length);
        //    outStream.Close();
        //    inputStream.Close();
        //    return outPutByteArray;
        //}

        ///// <summary>
        ///// 解压缩字节数组
        ///// </summary>
        ///// <param name="sourceByte">需要被解压缩的字节数组</param>
        ///// <returns>解压后的字节数组</returns>
        //public static byte[] deCompressBytes(byte[] sourceByte)
        //{
        //    MemoryStream inputStream = new MemoryStream(sourceByte);
        //    Stream outputStream = deCompressStream(inputStream);
        //    byte[] outputBytes = new byte[outputStream.Length];
        //    outputStream.Position = 0;
        //    outputStream.Read(outputBytes, 0, outputBytes.Length);
        //    outputStream.Close();
        //    inputStream.Close();
        //    return outputBytes;
        //}

        ///// <summary>
        ///// 压缩流
        ///// </summary>
        ///// <param name="sourceStream">需要被压缩的流</param>
        ///// <returns>压缩后的流</returns>
        //private static Stream compressStream(Stream sourceStream)
        //{
        //    MemoryStream streamOut = new MemoryStream();
        //    ZOutputStream streamZOut = new ZOutputStream(streamOut, zlibConst.Z_DEFAULT_COMPRESSION);
        //    CopyStream(sourceStream, streamZOut);
        //    streamZOut.finish();
        //    return streamOut;
        //}

        ///// <summary>
        ///// 解压缩流
        ///// </summary>
        ///// <param name="sourceStream">需要被解压缩的流</param>
        ///// <returns>解压后的流</returns>
        //private static Stream deCompressStream(Stream sourceStream)
        //{
        //    MemoryStream outStream = new MemoryStream();
        //    ZOutputStream outZStream = new ZOutputStream(outStream);
        //    CopyStream(sourceStream, outZStream);
        //    outZStream.finish();
        //    return outStream;
        //}

        public static List<string> GetStrlist(string str, char speater, bool toLower)
        {
            List<string> list = new List<string>();
            string[] ss = str.Split(speater);
            foreach (string s in ss)
            {
                if (!string.IsNullOrEmpty(s) && s != speater.ToString())
                {
                    string strVal = s;
                    if (toLower)
                    {
                        strVal = s.ToLower();
                    }
                    list.Add(strVal);
                }
            }
            return list;
        }

        public static DataSet sqlQuery(string SQLString)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();
                    SQLiteDataAdapter command = new SQLiteDataAdapter(SQLString, connection);
                    command.Fill(ds, "ds");
                }
                catch (System.Data.SQLite.SQLiteException ex)
                {
                    throw new Exception(ex.Message);
                }
                catch (Exception e) {
                    MessageBox.Show(e.Message);
                }
                return ds;
            }
        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">多条SQL语句</param>
        public static void ExecuteSqlTran(List<string> SQLStringList)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                SQLiteCommand cmd = new SQLiteCommand();
                cmd.Connection = conn;
                SQLiteTransaction tx = conn.BeginTransaction();
                cmd.Transaction = tx;
                try
                {
                    foreach (string strsql in SQLStringList)
                    {
                        if (strsql.Trim().Length > 1)
                        {
                            cmd.CommandText = strsql;
                            cmd.ExecuteNonQuery();
                        }
                    }
                    tx.Commit();
                }
                catch (System.Data.SQLite.SQLiteException E)
                {
                    tx.Rollback();
                    throw new Exception(E.Message);
                }
            }
        }

        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string SQLString)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        int rows = cmd.ExecuteNonQuery();
                        return rows;
                    }
                    catch (System.Data.SQLite.SQLiteException E)
                    {
                        connection.Close();
                        throw new Exception(E.Message);
                    }
                }
            }
        }

        /// <summary>
        /// 将dataset分隔为指定格式字符串
        /// </summary>
        /// <param name="dt"></param>
        /// <returns>____^____^|____^____^|</returns>
        public static string Splice(DataTable dt)
        {
            string result = "";
            foreach (DataRow s in dt.Rows)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    result += s[i].ToString().Length == 0 ? null : s[i].ToString() + "^";
                }
                result += "|";
            }
            return result;
        }

        public static string addFormat(string text)
        {
            return "\"" + text + "\",";
        }

        /// <summary>
        ///  下载的溯源码下载到本地数据库
        /// </summary>
        /// <param name="code"></param>
        /// <param name="printtask_code"></param>
        /// <returns></returns>
        public static int writeCodeToLocaldb(List<string> code, string printtask_code)
        {
            foreach (string s in code)
            {
                string middllecode;
                string smallcode;

                middllecode = s.Substring(0, s.IndexOf(","));
                smallcode = s.Substring(s.IndexOf(",") + 1);
                if (middllecode.Length > 15)
                {
                    string sql = "insert into number values('" + middllecode + "','" + middllecode + "','" + printtask_code + "'," + "1)";
                    try
                    {
                        ExecuteSql(sql);
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
                List<string> values = GetStrlist(smallcode, ',', false);
                List<string> exesql = new List<string>();
                foreach (string value in values)
                {
                    string sqltmp = "insert into number values('" + middllecode + "','" + value + "','" + printtask_code + "'," + "2)";
                    exesql.Add(sqltmp);
                }
                try
                {
                    ExecuteSqlTran(exesql);
                }
                catch (Exception e)
                {
                    throw e;
                }
            }

            return 0;
        }

        public static int TaskInfoToLocaldb(string task_code, string task_name, string num, string weight, string date, string spec, string lotnumber,string addr)
        {
            string sql = "delete from task_info where task_code='" + task_code + "'";
            ExecuteSql(sql);
            sql = "insert into task_info(task_code,task_name,task_spec,task_flag,task_weight,task_itemnum,task_createdate,task_lotnumber,user_code,task_address) values('" + task_code + "','" + task_name + "','" + spec + "',1," + float.Parse(weight) + "," + int.Parse(num) + ",'" + date + "','" + lotnumber + "','" + myUser.user_code + "','"+addr+ "')";
            ExecuteSql(sql);
            return 0;
        }

        public static List<string> TaskInfoOnline(string company_code)
        {
            Socket s;
            string info;
            List<string> Listcode = new List<string>();

            try
            {
                s = initsocket();
                string sendStr = "taskinfo," + company_code;
                byte[] sendBytes = Encoding.ASCII.GetBytes(sendStr);
                s.Send(sendBytes);

                byte[] recBytes = new byte[0x400 * 0x400];
                int bytes = s.Receive(recBytes, recBytes.Length, 0);
                s.Close();
                info = Encoding.UTF8.GetString(recBytes, 0, bytes);
                // MessageBox.Show(info);
                if (info.Length < 10)
                {
                    return null;
                }
                Listcode = GetStrlist(info, '|', false);
            }
            catch (Exception e)
            {
                MessageBox.Show("Get task info erro:" + e.Message);
            }

            return Listcode;
        }
        private static string ipserver = "";
        private static int portserver = 0;
        public static void initserver(string _ip,int _port) {
            ipserver = _ip;
            portserver = _port;
        }
        public static Socket initsocket(string host = "192.168.9.205", int port = 14445)
        {
            Socket clientSocket = null;
            if (ipserver.Length > 1) {
                host = ipserver;
            }
            if (portserver > 0) {
                port = portserver;
            }
            try
            {
                IPAddress ip = IPAddress.Parse(host);
                  IPEndPoint ipe = new IPEndPoint(ip, port);

                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                clientSocket.Connect(ipe);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return null;
            }

            return clientSocket;
        }
        public static Socket initTtosocket(string host = "192.168.9.205", int port = 14445)
        {
            Socket clientSocket = null;
           
            try
            {
                IPAddress ip = IPAddress.Parse(host);
                IPEndPoint ipe = new IPEndPoint(ip, port);

                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                clientSocket.Connect(ipe);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return null;
            }

            return clientSocket;
        }

        //public static Socket initsocket(string host, int port)
        //{
        //    IPAddress ip = IPAddress.Parse(host);
        //    IPEndPoint ipe = new IPEndPoint(ip, port);

        //    Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //    try
        //    {
        //        clientSocket.Connect(ipe);
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //    return clientSocket;
        //}

        /// <summary>
        /// 根据company_code 获得现有tablet的信息
        /// </summary>
        /// <param name="company_code"></param>
        /// <returns> info</returns>
        public static string getTabletInfo(string company_code)
        {
            Socket s = initsocket();
            string sendStr = "tabletinfo," + company_code;
            byte[] sendBytes = Encoding.ASCII.GetBytes(sendStr);
            s.Send(sendBytes);

            byte[] recBytes = new byte[4096];

            int bytes = s.Receive(recBytes, recBytes.Length, 0);
            s.Close();
            return Encoding.UTF8.GetString(recBytes, 0, bytes);//存在中文所有使用utf8编码格式
        }

        /// <summary>
        /// 根据company_code 获得现有yaocai 基础的信息
        /// </summary>
        /// <param name="user_code"></param>
        /// <returns> info</returns>
        public static string getyaocaiInfo(string user_code, int page, string function = "getyaocaiinfo")
        {
            Socket s = initsocket();
            string sendStr = function + "," + user_code + "," + page.ToString();
            byte[] sendBytes = Encoding.ASCII.GetBytes(sendStr);
            s.Send(sendBytes);

            byte[] recBytes = new byte[4096];

            int bytes = s.Receive(recBytes, recBytes.Length, 0);
            s.Close();
            return Encoding.UTF8.GetString(recBytes, 0, bytes);//存在中文所有使用utf8编码格式
        }

        public static string updateyaocaiInfo(string user_code, int page, string function = "getyaocaiinfo")
        {
            Socket s = initsocket();
            string sendStr = function + "," + user_code + "," + page.ToString();
            byte[] sendBytes = Encoding.ASCII.GetBytes(sendStr);
            s.Send(sendBytes);

            byte[] recBytes = new byte[4096];

            int bytes = s.Receive(recBytes, recBytes.Length, 0);
            s.Close();
            return Encoding.UTF8.GetString(recBytes, 0, bytes);//存在中文所有使用utf8编码格式
        }

        public static string sendToServer(string sendStr, string ip = "192.168.9.205", int port = 14444)
        {
            Socket s = initsocket(ip, port);
            if (s == null) {
                return "error:304连接错误";
            }
            byte[] sendBytes = Encoding.UTF8.GetBytes(sendStr);
            s.Send(sendBytes);

            byte[] recBytes = new byte[4096];

            int bytes = 0;
            try
            {
                bytes = s.Receive(recBytes, recBytes.Length, 0);
            }
            catch (Exception)
            {

                s.Close();
                return "未收到数据，服务器可能发生错误！";
            }
            s.Close();
            return Encoding.UTF8.GetString(recBytes, 0, bytes);//存在中文所有使用utf8编码格式
        }

        /// <summary>
        /// 从服务器下载二维码到本地数据库
        /// </summary>
        /// <param name="printtask_code"></param>
        /// <returns></returns>
        public static int downcode(string printtask_code)
        {
            string code = "";
            List<string> Listcode = new List<string>();
            try
            {
                Socket s = initsocket();
                string sendStr = "downcode," + Md5Encrypt(myUser.loginname + myUser.user_pass + DateTime.Now.ToShortDateString()).ToLower() + "," + DateTime.Now.ToShortDateString() + "," + printtask_code;
                byte[] sendBytes = Encoding.ASCII.GetBytes(sendStr);
                s.Send(sendBytes);

                byte[] recBytes = new byte[0x400 * 0x400];
                int bytes = s.Receive(recBytes, recBytes.Length, 0);
                s.Close();
                //  byte[] byte_zip = new byte[0x400 * 0x400];
                //    byte_zip=deCompressBytes(recBytes);
                //MessageBox.Show(bytes.ToString());
                //MessageBox.Show(byte_zip.ToString());
                code += Encoding.ASCII.GetString(recBytes, 0, bytes);
                // MessageBox.Show(code);
                if (-1 != code.IndexOf("error"))
                {
                    MessageBox.Show(code);
                    return -1;
                }
                //code += Encoding.ASCII.GetString(byte_zip, 0, byte_zip.Length);
                // MessageBox.Show(code);
                Listcode = GetStrlist(code, '|', false);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return writeCodeToLocaldb(Listcode, printtask_code);
        }

        #region MD5加密

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="input">输入</param>
        /// <returns></returns>
        public static string Md5Encrypt(string input)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            var data = Encoding.UTF8.GetBytes(input);
            var encs = md5.ComputeHash(data);
            return BitConverter.ToString(encs).Replace("-", "");
        }

        #endregion MD5加密

        /// <summary>
        /// 判断存在tcm.db否，没有就新建并添加表
        /// </summary>
        /// <returns></returns>
        public static int FindAndCreateDB()
        {
            try
            {
                if (File.Exists(dbpath))
                {
                    return 0;
                }
                else
                {
                    try
                    {
                        // File.Create(dbpath);
                        ExecuteSql(@"CREATE TABLE 'task_info' ('id'  INTEGER NOT NULL,'task_code'  TEXT,'task_name'  TEXT,'task_spec'  TEXT,'task_flag'  INTEGER,'task_weight'  REAL,'task_boxnum'  INTEGER,'task_itemnum'  INTEGER,'task_createdate'  TEXT,'task_dateto'  TEXT,'task_address'  TEXT,'task_lotnumber'  TEXT,'user_code'  TEXT,'task_standard'  TEXT,'task_define1'  TEXT,'task_define2'  TEXT,'task_define3'  TEXT,PRIMARY KEY('id' ASC));");
                        //MessageBox.Show("创建数据库成功！");
                        ExecuteSql(@"CREATE TABLE 'device' ('name'  TEXT NOT NULL,'ip'  TEXT,'port'  TEXT,'user_code'  TEXT,'status'  INTEGER DEFAULT 0,'deviceType'  TEXT,PRIMARY KEY('name' ASC)); ");
                        ExecuteSql("CREATE TABLE number(box_code varchar(500),box_sourcecode varchar(500),printtask_code varchar(500),flag int);");
                        return 0;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("创建数据库失败1" + ex.ToString().Substring(1, 60));
                        return -1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("创建数据库失败2" + ex.ToString().Substring(1, 60));
                return -1;
            }
        }

        public static DataSet getLocalCode(string user, string printtask_code)
        {
            string sql = "select box_sourcecode,flag from number where printtask_code='" + printtask_code + "' limit 300";
            return sqlQuery(sql);
        }
        public static DataSet getaddr(string printtask_code)
        {
            string sql = "select task_address from task_info where task_code='" + printtask_code + "' ";
            return sqlQuery(sql);
        }
        public static string getDevice()
        {
            string sql = "select name ,deviceType,ip,port,status from device where user_code='" + myUser.user_code + "'";
            DataSet ds = sqlQuery(sql);
            return Splice(ds.Tables[0]);
        }
        public static string getDeviceStatus(string name)
        {
            string sql = "select status from device where user_code='" + myUser.user_code + "' and name='"+name+"'";
            DataSet ds = sqlQuery(sql);
            return Splice(ds.Tables[0]);
        }
        public static DataSet getDeviceDataSet()
        {
            string sql = "select name ,ip,port,deviceType from device where user_code='" + myUser.user_code + "'";
            DataSet ds = sqlQuery(sql);
            return ds;
        }
        /// <summary>
        /// 按字符串长度切分成数组
        /// </summary>
        /// <param name="str">原字符串</param>
        /// <param name="separatorCharNum">切分长度</param>
        /// <returns>字符串数组</returns>
        public static string[] SplitByLen(this string str, int separatorCharNum)
        {
            if (string.IsNullOrEmpty(str) || str.Length <= separatorCharNum)
            {
                return new string[] { str };
            }
            string tempStr = str;
            List<string> strList = new List<string>();
            int iMax = Convert.ToInt32(Math.Ceiling(str.Length / (separatorCharNum * 1.0)));//获取循环次数
            for (int i = 1; i <= iMax; i++)
            {
                string currMsg = tempStr.Substring(0, tempStr.Length > separatorCharNum ? separatorCharNum : tempStr.Length);
                strList.Add(currMsg);
                if (tempStr.Length > separatorCharNum)
                {
                    tempStr = tempStr.Substring(separatorCharNum, tempStr.Length - separatorCharNum);
                }
            }
            return strList.ToArray();
        }

        public static void delete_task(string printask_code)
        {
            string sql = "update  task_info set task_flag=2 where task_code='" + printask_code + "'";
            ExecuteSql(sql);
        }

        [DllImport("kernel32")]
        public static extern int GetPrivateProfileString(string section, string key, string defVal, StringBuilder retVal, int size, string filePath);

        [DllImport("kernel32")]
        public static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        #region "定义加密字串变量"

        private static SymmetricAlgorithm mCSP = new DESCryptoServiceProvider();   //声明对称算法变量
        private const string CIV = "Mi9l/+7Zujhy12se6Yjy111A";  //初始化向量
        private const string CKEY = "jkHuIy9D/9i="; //密钥（常量）

        #endregion "定义加密字串变量"

        /// <summary>
        /// 加密字符串
        /// </summary>
        /// <param name="Value">需加密的字符串</param>
        /// <returns></returns>
        public static string EncryptString(string Value)
        {
            ICryptoTransform ct; //定义基本的加密转换运算
            MemoryStream ms; //定义内存流
            CryptoStream cs; //定义将内存流链接到加密转换的流
            byte[] byt;
            //CreateEncryptor创建(对称数据)加密对象
            ct = mCSP.CreateEncryptor(Convert.FromBase64String(CKEY), Convert.FromBase64String(CIV)); //用指定的密钥和初始化向量创建对称数据加密标准
            byt = Encoding.UTF8.GetBytes(Value); //将Value字符转换为UTF-8编码的字节序列
            ms = new MemoryStream(); //创建内存流
            cs = new CryptoStream(ms, ct, CryptoStreamMode.Write); //将内存流链接到加密转换的流
            cs.Write(byt, 0, byt.Length); //写入内存流
            cs.FlushFinalBlock(); //将缓冲区中的数据写入内存流，并清除缓冲区
            cs.Close(); //释放内存流
            return Convert.ToBase64String(ms.ToArray()); //将内存流转写入字节数组并转换为string字符
        }

        /// <summary>
        /// 解密字符串
        /// </summary>
        /// <param name="Value">要解密的字符串</param>
        /// <returns>string</returns>
        public static string DecryptString(string Value)
        {
            try
            {
                ICryptoTransform ct; //定义基本的加密转换运算
                MemoryStream ms; //定义内存流
                CryptoStream cs; //定义将数据流链接到加密转换的流
                byte[] byt;
                ct = mCSP.CreateDecryptor(Convert.FromBase64String(CKEY), Convert.FromBase64String(CIV)); //用指定的密钥和初始化向量创建对称数据解密标准
                byt = Convert.FromBase64String(Value); //将Value(Base 64)字符转换成字节数组
                if (byt.Length == 0)
                {
                    return "";
                }
                ms = new MemoryStream();
                cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
                cs.Write(byt, 0, byt.Length);
                cs.FlushFinalBlock();
                cs.Close();
                return Encoding.UTF8.GetString(ms.ToArray()); //将字节数组中的所有字符解码为一个字符串
            }
            catch (Exception)
            {

                return "";
            }
        }
        public static bool IsSpeChar(string str) {

           
           
            return !Regex.IsMatch(str, @"^[a-zA-Z0-9]+$");

        }
        public static bool IsChinese(string str)
        {


            return Regex.IsMatch(str, @"^[\u4e00-\u9fa5]+$");
        }
        public static bool IsChineseAndNumChar(string str)
        {
            if (str.Trim() == "") {
                return true;
            }

           // return Regex.IsMatch(str, @"^[^~!$%@#*^&()_=]+$");
            return Regex.IsMatch(str, @"^[^~!$%@#^&*()_\+\-=\[\]\{\};:,.<>'""|\\?/]+$");
        }
        public static bool IsChineseAndNumCharpercentage(string str)
        {
            return Regex.IsMatch(str, @"^[^~!$@#^&*()_\+\-=\[\]\{\};,.<>'""|\\?/]+$");
        }
        public static bool IsINT(string str)
        {


            Regex reg = new Regex(@"^[0-9]\d*$");
            Match m = reg.Match(str);
            if (m.Success)
            {
                try
                {
                    if (Convert.ToInt16(str) == 0)
                    {
                        return false;
                    }
                }
                catch (Exception)
                {

                    return false;
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool IsDouble(string str)
        {


            Regex reg = new Regex(@"(^\d{1,4}\.(([0]{1}[1-9]{1}$)|([1-9]{1}[0-9]{0,1}$)))|(^[1-9]{1}\d{0,5}$)");
            Match m = reg.Match(str);
            if (m.Success)
            {
                if (Convert.ToDouble(str) == 0)
                {
                    return false;
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        [System.Runtime.InteropServices.DllImport("user32")]
        public static extern bool AnimateWindow(IntPtr hwnd, int dwTime, int dwFlags);

        private const int AW_VER_NEGATIVE = 0x0008;
        private const int AW_SLIDE = 0x40000;
        private const int AW_BLEND = 0x80000;
        private const int AW_HIDE = 0x10000;
        private const int AW_VER_POSITIVE = 0x0004;
    }
}