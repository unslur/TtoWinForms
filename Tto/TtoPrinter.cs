using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Tto
{
    internal class TtoPrinter:IDisposable
    {
        private Socket Tto_soc = null;
        public bool workFlag = true;
        public bool taskFlag = true;
        public bool stop_signal = false;
        string[] taskarray;
        List<string> listCode;
        PrintStatus one;
       public void shutConnect(){
            closeSocket();
        }
        public TtoPrinter() { }
       public TtoPrinter(string[] taskarray,List<string > listCode,PrintStatus one) {
            this.taskarray = taskarray;
            this.listCode = listCode;
            this.one = one;
        }
        public void initPrintData(string[] taskarray, List<string> listCode, PrintStatus one) {
            this.taskarray = taskarray;
            this.listCode = listCode;
            this.one = one;
        }
        public int init()
        {
            try
            {
                Tto_soc = func.initTtosocket(myUser.curPrinter[1], Convert.ToInt32(myUser.curPrinter[2]));
            }
            catch (Exception e)
            {
                throw e;
            }
            if (Tto_soc == null)
            {
                return 1;
            }
            return 0;
        }

        [DllImport("TtoWdjAndMarkem.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern bool initSocket(string ip, int port);

        [DllImport("TtoWdjAndMarkem.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern bool closeSocket();

        // [DllImport("TtoWdjAndMarkem.dll", EntryPoint = "test"),CallingConvention=CallingConvention.Cdecl)]

        [DllImport("TtoWdjAndMarkem.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern bool test(int a, int b);

        [DllImport("TtoWdjAndMarkem.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern bool markemPrint(byte[] code2d, byte[] taskarray, bool firstFlag);

        [DllImport("TtoWdjAndMarkem.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern bool WDJPrint(byte[] code2d, byte[] taskarray, bool firstFlag);

        public string sendDUOInit(string[] infoArray)
        {
            string sendStr = "";
            string recvStr = "";
            //  return null;
            foreach (string info in infoArray)
            {
                sendStr += info + "|";
            }
            sendStr = sendStr.Remove(sendStr.LastIndexOf("|"), 1);
            byte[] sendBytes = Encoding.Unicode.GetBytes("[SOH]");
            //byte[] sendBytes = Encoding.UTF8.GetBytes("无");
            try
            {
                Tto_soc.Send(sendBytes);
                byte[] recBytes = new byte[30];
                int bytes = Tto_soc.Receive(recBytes, recBytes.Length, 0);
                recvStr = Encoding.UTF8.GetString(recBytes, 0, bytes);
                if (recvStr.IndexOf("Sent") <= -1)
                {
                    return recvStr;
                }
                bytes = Tto_soc.Receive(recBytes, recBytes.Length, 0);
                recvStr = Encoding.UTF8.GetString(recBytes, 0, bytes);
                if (recvStr.IndexOf("Printed") <= -1)
                {
                    return recvStr;
                }
            }
            catch (Exception e)
            {
                Tto_soc.Close();
                return e.Message;
            }
            return null;
        }

        public string sendMore(List<string> codeList, string code2_head)
        {
            string sendStr = "";
            string recvStr = "";
            //return null;
            try
            {
                foreach (string code in codeList)
                {
                    byte[] sendBytes = Encoding.UTF8.GetBytes(code);
                    Tto_soc.Send(sendBytes);
                    byte[] recBytes = new byte[30];
                    int bytes = Tto_soc.Receive(recBytes, recBytes.Length, 0);
                    recvStr = Encoding.UTF8.GetString(recBytes, 0, bytes);
                    if (recvStr.IndexOf("Sent") <= -1)
                    {
                        return recvStr;
                    }
                    bytes = Tto_soc.Receive(recBytes, recBytes.Length, 0);
                    recvStr = Encoding.UTF8.GetString(recBytes, 0, bytes);
                    if (recvStr.IndexOf("Printed") <= -1)
                    {
                        return recvStr;
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                Tto_soc.Close();
            }
            return null;
        }

        public string DuoMiNuo()
        {
            Socket client = func.initTtosocket(one.printTtoSo[1], Convert.ToInt32(one.printTtoSo[2]));
            if (client == null) {
                return one.printTtoSo[0] + "^设备连接访问失败";
            }
            string sql = "update device set status =  1 where name ='" + one.printTtoSo[0] + "'";
            if (func.ExecuteSql(sql) != 1) 
            {
                client.Close();
                return one.printTtoSo[0] + "^更新设备状态失败";
            }
#if true////////////////////////////
            int first_count = SendTTO_PrintCounter(client);
#else
            int first_count = 0;
#endif

            int task_start_num = first_count;

            SendTTO_by(client, "S0", taskarray[0]); //S1是变量名
            SendTTO_by(client, "S1_1", taskarray[1].Substring(0, 18)); //S1是变量名
            SendTTO_by(client, "S1_2", taskarray[1].Substring(19)); //S1是变量名
            SendTTO_by(client, "S2", taskarray[2]); //S1是变量名
            SendTTO_by(client, "S3", taskarray[3]); //S1是变量名
            SendTTO_by(client, "S4", taskarray[4]); //S1是变量名
            SendTTO_by(client, "S5", taskarray[5]); //S1是变量名
            SendTTO_by(client, "S6", taskarray[6]); //S1是变量名
            SendTTO_by(client, "S7", taskarray[7]); //S1是变量名
            SendTTO_by(client, "S8", taskarray[8]); //S1是变量名

            one.curNum = 0;
             Thread.Sleep(500);
            //Thread.Sleep(3000);
            //one.curNum++;
            /////////////
            one.curNum = SendTTO_PrintCounter(client) - first_count;
            /////////////////
            string sq = "delete from number where box_sourcecode='" + taskarray[1] + "'";
            while (func.ExecuteSql(sq) <1) {; };

            foreach (string code_tmp in listCode)
            {
                string[] codeArray = code_tmp.Split('^');
                string code = codeArray[0];
                ;
                one.workStatus = 1;
                while (true)
                {
                    if (workFlag)
                    {
                        one.workStatus = 1;
                        if (!taskFlag)
                        {
                            stop_signal = true;
                            Thread.Sleep(1100);
                            taskFlag = true;
                            closeSocket();
                            return one.printTtoSo[0] + "^";
                        }
                        if (duoStatus(client, code))
                        {
                            one.curNum++;
                            sql = "delete from number where box_sourcecode='" + code + "'";
                            while (func.ExecuteSql(sql) != 1) {; };
                            break;
                        }
                        else {
                            one.workStatus = 2;
                            Thread.Sleep(100);
                        }
                    }
                    else {
                        one.workStatus = 2;
                        Thread.Sleep(100);
                    }
                }
            }

            while (one.curNum < one.totalNum)
            {
                DataSet ds = func.getLocalCode(myUser.user_code, one.print_task);
                string localCode = func.Splice(ds.Tables[0]);
                listCode = func.GetStrlist(localCode, '|', false);
                if (listCode.Count == 0)
                {
                    one.curNum = one.totalNum;
                    break;
                }
                foreach (string code_tmp in listCode)
                {
                    string[] codeArray = code_tmp.Split('^');
                    string code = codeArray[0];
                    ;
                    one.workStatus = 1;
                    while (true)
                    {
                        if (workFlag)
                        {
                            one.workStatus = 1;
                            if (!taskFlag)
                            {
                                stop_signal = true;
                                Thread.Sleep(1100);
                                taskFlag = true;
                                closeSocket();
                                return one.printTtoSo[0] + "^";
                            }
                            if (duoStatus(client, code))
                            {
                                one.curNum++;
                                break;
                            }
                        }
                        else {
                            one.workStatus = 2;
                            Thread.Sleep(100);
                        }
                    }
                }
            }
            sql = "update device set status =  0 where name ='" + one.printTtoSo[0] + "'";
            func.ExecuteSql(sql);
            sql = "update task_info set task_flag=2 where task_code='" + one.print_task + "'";
            if (func.ExecuteSql(sql) != 1) {; };
            one.workStatus = 3;
            client.Close();
            return one.printTtoSo[0] + "^";
        }

        public string Markem()
        {
            // Socket client = func.initsocket(myUser.curPrinter[1], Convert.ToInt32(myUser.curPrinter[2]));
            
            string v = "";
            foreach (string s in taskarray)
            {
                v += s + "^";
            }
            v += "药典" + "^" + "正源中溯" + "^";
            string code_one = taskarray[1];
            byte[] ss = Encoding.Default.GetBytes(v);
            byte[] aaa = Encoding.Default.GetBytes(code_one);

            if (!initSocket(one.printTtoSo[1], Convert.ToInt32(one.printTtoSo[2])))
            {
                return one.printTtoSo[0] + "^连接失败失败" + myUser.curPrinter[1] + ":" + myUser.curPrinter[2];
            }
            string sql = "update device set status =  1 where name ='" + one.printTtoSo[0] + "'";
            func.ExecuteSql(sql);
            if (markemPrint(aaa, ss, true))
            {
                sql = "delete from number where box_sourcecode='" + code_one + "'";

                while (func.ExecuteSql(sql) != 1) {; };

                one.curNum = 1;
                foreach (string code_tmp in listCode)
                {
                    string[] codeArray = code_tmp.Split('^');
                    string code = codeArray[0];
                    if (codeArray[1] == "1") {
                        continue;
                    }
                    
                    while (true)
                    {
                        if (workFlag)
                        {
                            one.workStatus = 1;
                            aaa = Encoding.Default.GetBytes(code);
                            int i = 0;
                            for (; i < 10; i++)
                            {
                                if (!taskFlag)
                                {
                                    stop_signal = true;
                                    Thread.Sleep(1100);
                                    taskFlag = true;
                                    closeSocket();
                                    return one.printTtoSo[0] + "^";
                                }
                                if (markemPrint(aaa, null, false))
                                {
                                    one.curNum++;
                                    sql = "delete from number where box_sourcecode='" + code + "'";

                                    while (func.ExecuteSql(sql) != 1) {; };
                                    break;
                                }
                            }
                            if (i == 10)
                            {
                                closeSocket();
                                return one.printTtoSo[0] + "^打印失败";
                            }

                            break;
                        }
                        else {
                            one.workStatus = 2;
                            Thread.Sleep(100);
                        }
                    }
                }
            }
            else {
                closeSocket();
                sql = "update device set status =  0 where name ='" + one.printTtoSo[0] + "'";
                func.ExecuteSql(sql);
                return one.printTtoSo[0] + "^first print error";
            }
            while (one.curNum < one.totalNum)
            {
                DataSet ds = func.getLocalCode(myUser.user_code, one.print_task);
                string localCode = func.Splice(ds.Tables[0]);
                listCode = func.GetStrlist(localCode, '|', false);
                if (listCode.Count == 0)
                {
                    one.curNum = one.totalNum;
                    break;
                }
                foreach (string code_tmp in listCode)
                {
                    string[] codeArray = code_tmp.Split('^');
                    string code = codeArray[0];
                    while (true)
                    {
                        if (workFlag)
                        {
                            
                            one.workStatus = 1;
                            aaa = Encoding.Default.GetBytes(code);
                            int i = 0;
                            for (; i < 10; i++)
                            {
                                if (!taskFlag)
                                {
                                    stop_signal = true;
                                    Thread.Sleep(1100);
                                    taskFlag = true;
                                    closeSocket();
                                    return one.printTtoSo[0] + "^";
                                }
                                if (markemPrint(aaa, null, false))
                                {
                                    one.curNum++;
                                    sql = "delete from number where box_sourcecode='" + code + "'";

                                    while (func.ExecuteSql(sql) != 1) {; };
                                    break;
                                }
                            }
                            if (i == 10)
                            {
                                closeSocket();
                                return one.printTtoSo[0] + "^打印失败";
                            }

                            break;
                        }
                        else {
                            one.workStatus = 2;
                            Thread.Sleep(100);
                        }
                    }
                }
            }
            sql = "update device set status =  0 where name ='" + one.printTtoSo[0] + "'";
            func.ExecuteSql(sql);
            sql = "update task_info set task_flag=2 where task_code='" + one.print_task + "'";
            if (func.ExecuteSql(sql) != 1) {; };
            one.workStatus = 3;
            closeSocket();
            return one.printTtoSo[0] + "^";
        }

        private bool duoStatus(Socket client, string code)
        {
            SendTTO_by(client, "S1_1", code.Substring(0, 18)); //S1是变量名

            SendTTO_by(client, "S1_2", code.Substring(19));
            int later = 0, front = 0;

            later = front;
            Thread.Sleep(500);
            later = SendTTO_PrintCounter(client);
            //Thread.Sleep(3000);
            later++;
            if (later == -1)
            {
                return false;
            }
            if (later > front)
            {
                front = later;
                return true;
            }
            return false;
        }

        public string WDJ()
        {
            string sql = "update device set status =  1 where name ='" + one.printTtoSo[0] + "'";
            if (func.ExecuteSql(sql) != 1)
            {
                return one.printTtoSo[0]+"^更新设备状态失败";
            }
            string v = "";
            foreach (string s in taskarray)
            {
                v += s + "^";
            }
            v += "药典" + "^" + "正源中溯" + "^";
            string code_one = taskarray[1];
            byte[] ss = Encoding.Default.GetBytes(v);
            byte[] aaa = Encoding.Default.GetBytes(code_one);

            if (!initSocket(one.printTtoSo[1], Convert.ToInt32(one.printTtoSo[2])))
            {
                return one.printTtoSo[0]+"^连接失败失败" + one.printTtoSo[1] + ":" + one.printTtoSo[2];
            }
            if (WDJPrint(aaa, ss, true))
            {
                sql = "delete from number where box_sourcecode='" + code_one + "'";

                while (func.ExecuteSql(sql) != 1) {; };

                one.curNum = 1;
                foreach (string code_tmp in listCode)
                {
                    string[] codeArray = code_tmp.Split('^');
                    string code = codeArray[0];
                    
                    while (true)
                    {
                        if (workFlag)
                        {
                            one.workStatus = 1;
                            aaa = Encoding.Default.GetBytes(code);
                            int i = 0;
                            for (; i < 10; i++)
                            {
                                if (!taskFlag)
                                {
                                    stop_signal = true;
                                    Thread.Sleep(1100);
                                    taskFlag = true;
                                    closeSocket();
                                    return one.printTtoSo[0] + "^";
                                }
                                if (WDJPrint(aaa, null, false))
                                {
                                    one.curNum++;
                                    sql = "delete from number where box_sourcecode='" + code + "'";

                                    while (func.ExecuteSql(sql) != 1) {; };
                                    break;
                                }
                                
                            }
                            if (i == 10)
                            {
                                closeSocket();
                                return one.printTtoSo[0]+"^打印失败";
                            }

                            break;
                        }
                        else {
                            one.workStatus = 2;
                            Thread.Sleep(100);
                        }
                    }
                }
            }
            else {
                closeSocket();
                sql = "update device set status =  0 where name ='" + one.printTtoSo[0] + "'";
                func.ExecuteSql(sql);
                return one.printTtoSo[0]+"^first print error";
            }
            while (one.curNum < one.totalNum)
            {
                DataSet ds = func.getLocalCode(myUser.user_code, one.print_task);
                string localCode = func.Splice(ds.Tables[0]);
                listCode = func.GetStrlist(localCode, '|', false);
                if (listCode.Count == 0)
                {
                    one.curNum = one.totalNum;
                    break;
                }
                foreach (string code_tmp in listCode)
                {
                    string[] codeArray = code_tmp.Split('^');
                    string code = codeArray[0];
                    while (true)
                    {
                        if (workFlag)
                        {
                            
                            one.workStatus = 1;
                            aaa = Encoding.Default.GetBytes(code);
                            int i = 0;
                            for (; i < 10; i++)
                            {
                                if (!taskFlag)
                                {
                                    stop_signal = true;
                                    Thread.Sleep(1100);
                                    taskFlag = true;
                                    closeSocket();
                                    return one.printTtoSo[0] + "^";
                                }
                                if (WDJPrint(aaa, null, false))
                                {
                                    one.curNum++;
                                    sql = "delete from number where box_sourcecode='" + code + "'";

                                    while (func.ExecuteSql(sql) != 1) {; };
                                    break;
                                }
                            }
                            
                            if (i == 10)
                            {
                                closeSocket();
                                return one.printTtoSo[0]+"^打印失败";
                            }

                            break;
                        }
                        else {
                            one.workStatus = 2;
                            Thread.Sleep(100);
                        }
                    }
                }
            }
            sql = "update device set status =  0 where name ='" + one.printTtoSo[0] + "'";
            func.ExecuteSql(sql);
            sql = "update task_info set task_flag=2 where task_code='" + one.print_task + "'";
            if (func.ExecuteSql(sql) != 1) {; };
            one.workStatus = 3;
            closeSocket();
            return one.printTtoSo[0]+"^";
        }

        private void SendTTO_by(Socket client, string VarName, string Value)
        {
            Common cm = new Common();
            string strCommand = "[SOH]FillSerialVar=[STX]" + VarName + "[ETX],[STX]" + Value + "[ETX][ETB]";
            //SaveLog("发送指令:" + strCommand);
            strCommand = cm.StrTo16StrVserial(strCommand);
            byte[] data = cm.HexStringToByteArray(strCommand);
            client.Send(data);
        }

        //查询打印个数
        private int SendTTO_PrintCounter(Socket client)
        {
            Common cm = new Common();
            string strCommand = "[SOH]RequestPrintCounter[ETB]";
            strCommand = cm.StrTo16StrVserial(strCommand);
            byte[] data = cm.HexStringToByteArray(strCommand);
            client.Send(data);
            byte[] recBytes = new byte[0x100];
            string recvStr = "";
            int bytes = client.Receive(recBytes, recBytes.Length, 0);
            string sdata = cm.ByteToStr(recBytes);
            sdata = cm.Unicode_tto(sdata);
            byte[] bData = cm.HexStringToByteArray(sdata);
            sdata = System.Text.Encoding.Unicode.GetString(bData);
            // recvStr = Encoding.UTF8.GetString(recBytes, 0, bytes);
            if (sdata.Contains("AnswerPrintCounter") == false) { return -1; }

            int start_index = sdata.IndexOf('='); // 38
            int stop_index = sdata.IndexOf(','); ;   //40
            int printedcnt1 = Convert.ToInt32(sdata.Substring(start_index + 1, stop_index - start_index - 1));

            return printedcnt1;
        }

        public string StrTo16StrVserial(string str)
        {
            string result = "";

            //string str = cnt.ToString().PadLeft(4, '0');
            StringBuilder stringBuilder = new StringBuilder(str.Length * 2);

            for (int i = 0; i <= str.Length - 1; i++)
            {
                string temp = "";
                temp = temp + ((int)str[i]).ToString("X2");
                if (temp.Length == 4)
                {
                    temp = temp.Substring(2, 2) + " " + temp.Substring(0, 2) + " ";
                }
                else
                {
                    temp = temp + " 00 ";
                }
                result = result + temp;
            }

            stringBuilder.Append(result);

            // stringBuilder.Append(StrToUTF8str(str));
            stringBuilder = stringBuilder.Replace("5B 00 53 00 4F 00 48 00 5D 00", "01 00");  //[SOH] 5B 00 53 00 4F 00 48 00 5D 00
            stringBuilder = stringBuilder.Replace("5B 00 45 00 54 00 42 00 5D 00", "17 00");  //[ETB]
            stringBuilder = stringBuilder.Replace("5B 00 53 00 54 00 58 00 5D 00", "02 00");  //[STX]
            stringBuilder = stringBuilder.Replace("5B 00 45 00 54 00 58 00 5D 00", "03 00");  //[ETX]

            return stringBuilder.ToString();
        }

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)。
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。

                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~TtoPrinter() {
        //   // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

        // 添加此代码以正确实现可处置模式。
        public void Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}