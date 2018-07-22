using CCWin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tto
{
    public delegate string AysnPrintDelegate(string[] taskarray, List<string> listCode, PrintStatus one);

    public partial class LocalTask : CCSkinMain
    {
        private bool openflag = false;

        public LocalTask()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        [System.Runtime.InteropServices.DllImport("user32")]
        private static extern bool AnimateWindow(IntPtr hwnd, int dwTime, int dwFlags);

        private const int AW_VER_NEGATIVE = 0x0008;
        private const int AW_SLIDE = 0x40000;
        private const int AW_BLEND = 0x80000;
        private const int AW_HIDE = 0x10000;
        private const int AW_VER_POSITIVE = 0x0004;

        private void LocalTask_Load(object sender, EventArgs e)
        {
            //AnimateWindow(this.Handle, 500, AW_VER_NEGATIVE | AW_SLIDE);
            if (myUser.curPrinter[0] == null)
            {
                string sql1 = "select name,ip,port,deviceType from device ;";
                DataSet ds1 = func.sqlQuery(sql1);
                if (ds1.Tables[0].Rows[0][0].ToString() != null)
                {
                    myUser.curPrinter[0] = ds1.Tables[0].Rows[0][0].ToString();
                    myUser.curPrinter[1] = ds1.Tables[0].Rows[0][1].ToString();
                    myUser.curPrinter[2] = ds1.Tables[0].Rows[0][2].ToString();
                    myUser.curPrinter[3] = ds1.Tables[0].Rows[0][3].ToString();
                    label1.Text = myUser.curPrinter[0];
                }
                else {
                    label1.Text = "";
                }
            }
            label1.Text = myUser.curPrinter[0];
            if (openflag)
            {
                return;
            }
            refresh();
        }

        private void printThread(string[] taskarray, List<string> listCode)
        {
            if (myUser.curPrinter[1].Length < 8)
            {
                normalPrinter print_N = new normalPrinter();
               // print_N.init();
               // print_N.PrintThree(taskarray, listCode);
                listView1.Items.Remove(this.listView1.SelectedItems[0]);
            }
            else {
                this.listView1.SelectedItems[0].SubItems[7].Text = myUser.curPrinter[0] + "打印中";
                int index = this.listView1.SelectedItems[0].Index;
                this.label3.Text = ":打印中";
                this.listView1.SelectedItems[0].SubItems[8].Text = myUser.curPrinter[0];
                PrintStatus one = new PrintStatus();
                one.index = index;
                //one.printName = myUser.curPrinter[0];
                one.totalNum = Convert.ToInt32(listView1.SelectedItems[0].SubItems[4].Text);
                one.print_task = listView1.SelectedItems[0].SubItems[1].Text;

                if (myUser.curPrinter[3].IndexOf("duo") > -1)
                {
                    BeginPrinting(taskarray, listCode, one, 0);
                }
                else if (myUser.curPrinter[3].IndexOf("wdj") > -1)
                {
                    BeginPrinting(taskarray, listCode, one, 1);
                }
                else if (myUser.curPrinter[3].IndexOf("markem") > -1)
                {
                    BeginPrinting(taskarray, listCode, one, 2);
                }
                thread_set_progress = new Thread(new ParameterizedThreadStart(PrintNum));
                thread_set_progress.Start(one);
            }
        }

        private Thread thread_set_progress = null;

        private void PrintNum(object obj)
        {
            PrintStatus one = (PrintStatus)obj;
            while (true)
            {
                try
                {
                    //this.listView1.Items[one.index].SubItems[7].Text = one.printName + ":" + one.curNum + "/" + one.totalNum;
                    if (one.workStatus > 2)
                    {
                        this.listView1.Items[one.index].SubItems[7].Text = "打印结束";
                        break;
                    }
                }
                catch (Exception)
                {
                    return;
                }
                Thread.Sleep(1000);
            }
        }

       public IAsyncResult BeginPrinting(string[] taskarray, List<string> listCode, PrintStatus one, int printer_type)
        {
            //TtoPrinter printer = new TtoPrinter();
            //AysnPrintDelegate PrintDelegate = null;
            //if (printer_type == 0)
            //{
            //    PrintDelegate = new AysnPrintDelegate(printer.DuoMiNuo);
            //}
            //else if (printer_type == 1)
            //{
            //    PrintDelegate = new AysnPrintDelegate(printer.WDJ);
            //}
            //else {
            //    PrintDelegate = new AysnPrintDelegate(printer.Markem);
            //}

            return null;///PrintDelegate.BeginInvoke(taskarray, listCode, one, Printed, PrintDelegate);
        }

        //private void Printed(IAsyncResult result)
        //{
        //    AysnPrintDelegate aysnDelegate = result.AsyncState as AysnPrintDelegate;
        //    if (aysnDelegate != null)
        //    {
        //        string rtn = aysnDelegate.EndInvoke(result);
        //        int index = 0;
        //        string sql = "";
        //        if (rtn.Length > 4)
        //        {
        //            MessageBox.Show(rtn);
        //            sql = "update device set status=0 ";
        //            //this.listView1.Items[one.index].SubItems[7].Text = "打印结束";
        //            thread_set_progress.Abort();
        //        }
        //        else {
        //            index = Convert.ToInt32(rtn);
        //            this.label3.Text = ":空闲";
        //            sql = "update device set status=0 where name='" + this.listView1.Items[index].SubItems[8].Text + "'";
        //        }

        //        func.ExecuteSql(sql);

        //        skinButton3.Visible = false;
        //    }
        //    else
        //    {
        //        MessageBox.Show("打印失败");
        //    }
        //}

        private void skinButton1_Click(object sender, EventArgs e)
        {
            string printask_code;
            try
            {
                printask_code = this.listView1.SelectedItems[0].SubItems[1].Text;
            }
            catch (Exception)
            {
                MessageBox.Show("选择任务");
                return;
            }
            // MessageBox.Show(printask_code);
            DataSet ds = func.getLocalCode(myUser.user_code, printask_code);
            string localCode = func.Splice(ds.Tables[0]);

            int i = 0;
            i = localCode.IndexOf('|');
            string temp = "";
            try
            {
                temp = localCode.Substring(0, i);
            }
            catch (Exception)
            {
                MessageBox.Show("本地任务已删除");
                string sql = "";
                sql = "update task_info set task_flag=2 where task_code='" + printask_code + "'";
                func.ExecuteSql(sql);
                return;
            }
            int j = temp.IndexOf('^');
            temp = localCode.Substring(0, j);
            localCode = localCode.Substring(i + 1);

            string[] taskarray = new string[9];
            taskarray[0] = "http::";
            taskarray[1] = temp;
            // MessageBox.Show(temp);
            // MessageBox.Show(localCode);
            taskarray[2] = this.listView1.Columns[2].Text + ":" + this.listView1.SelectedItems[0].SubItems[2].Text;
            taskarray[3] = this.listView1.Columns[3].Text + ":" + this.listView1.SelectedItems[0].SubItems[3].Text;
            taskarray[4] = "片";
            taskarray[5] = "厂家:" + myUser.company_name;
            taskarray[7] = this.listView1.Columns[5].Text + ":" + this.listView1.SelectedItems[0].SubItems[5].Text.Substring(0, 10);
            taskarray[6] = this.listView1.Columns[6].Text + ":" + this.listView1.SelectedItems[0].SubItems[6].Text;
            taskarray[8] = "15161";
            //  MessageBox.Show(localCode);
            //foreach (string sss in taskarray)
            //{
            //     MessageBox.Show(sss);
            //}
            List<string> listCode = func.GetStrlist(localCode, '|', false);
            skinButton3.Visible = true;
            skinButton1.Visible = false;
            printThread(taskarray, listCode);
        }

        private void nextCode()
        {
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            skinButton1_Click(sender, e);
        }

        private void skinButton2_Click(object sender, EventArgs e)
        {
            //AnimateWindow(this.Handle, 500, AW_VER_POSITIVE | AW_HIDE);

            try
            {
                this.Hide();
            }
            catch (Exception)
            {
                ;
            }
        }

        //private void SendTTO_by(Socket client, string VarName, string Value)
        //{
        //    Common cm = new Common();
        //    string strCommand = "[SOH]FillSerialVar=[STX]" + VarName + "[ETX],[STX]" + Value + "[ETX][ETB]";
        //    //SaveLog("发送指令:" + strCommand);
        //    strCommand = cm.StrTo16StrVserial(strCommand);
        //    byte[] data = cm.HexStringToByteArray(strCommand);
        //    client.Send(data);
        //}

        ////查询打印个数
        //private int SendTTO_PrintCounter(Socket client)
        //{
        //    Common cm = new Common();
        //    string strCommand = "[SOH]RequestPrintCounter[ETB]";
        //    strCommand = cm.StrTo16StrVserial(strCommand);
        //    byte[] data = cm.HexStringToByteArray(strCommand);
        //    client.Send(data);
        //    byte[] recBytes = new byte[0x100];
        //    string recvStr = "";
        //    int bytes = client.Receive(recBytes, recBytes.Length, 0);
        //    string sdata = cm.ByteToStr(recBytes);
        //    sdata = cm.Unicode_tto(sdata);
        //    byte[] bData = cm.HexStringToByteArray(sdata);
        //    sdata = System.Text.Encoding.Unicode.GetString(bData);
        //    // recvStr = Encoding.UTF8.GetString(recBytes, 0, bytes);
        //    if (sdata.Contains("AnswerPrintCounter") == false) { return -1; }

        //    int start_index = sdata.IndexOf('='); // 38
        //    int stop_index = sdata.IndexOf(','); ;   //40
        //    int printedcnt1 = Convert.ToInt32(sdata.Substring(start_index + 1, stop_index - start_index - 1));

        //    return printedcnt1;
        //}

        //public string StrTo16StrVserial(string str)
        //{
        //    string result = "";

        //    //string str = cnt.ToString().PadLeft(4, '0');
        //    StringBuilder stringBuilder = new StringBuilder(str.Length * 2);

        //    for (int i = 0; i <= str.Length - 1; i++)
        //    {
        //        string temp = "";
        //        temp = temp + ((int)str[i]).ToString("X2");
        //        if (temp.Length == 4)
        //        {
        //            temp = temp.Substring(2, 2) + " " + temp.Substring(0, 2) + " ";
        //        }
        //        else
        //        {
        //            temp = temp + " 00 ";
        //        }
        //        result = result + temp;
        //    }

        //    stringBuilder.Append(result);

        //    // stringBuilder.Append(StrToUTF8str(str));
        //    stringBuilder = stringBuilder.Replace("5B 00 53 00 4F 00 48 00 5D 00", "01 00");  //[SOH] 5B 00 53 00 4F 00 48 00 5D 00
        //    stringBuilder = stringBuilder.Replace("5B 00 45 00 54 00 42 00 5D 00", "17 00");  //[ETB]
        //    stringBuilder = stringBuilder.Replace("5B 00 53 00 54 00 58 00 5D 00", "02 00");  //[STX]
        //    stringBuilder = stringBuilder.Replace("5B 00 45 00 54 00 58 00 5D 00", "03 00");  //[ETX]

        //    return stringBuilder.ToString();
        //}

        private void setStatus()
        {
            while (true)
            {
                try
                {
                    string sql = "select status from device where name='" + myUser.curPrinter[0] + "'";
                    DataSet ds = func.sqlQuery(sql);
                    if (ds.Tables[0].Rows[0][0].ToString() == 0.ToString())//空闲
                    {
                        label3.Text = ":空闲";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == 1.ToString())//打印中
                    {
                        label3.Text = ":打印中";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == 2.ToString())
                    {
                        label3.Text = ":保留";
                    }
                    else                //故障
                    {
                        label3.Text = ":故障:" + ds.Tables[0].Rows[0][0].ToString();
                        //  MessageBox.Show(0.ToString());
                    }
                    Thread.Sleep(2000);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }

        private Thread t;

        private void refresh()
        {
            ImageList imgList = new ImageList();
            imgList.ImageSize = new Size(1, 25);//设置 ImageList 的宽和高
            listView1.SmallImageList = imgList;

            t = new Thread(setStatus);
            t.IsBackground = true;
            t.Start();

            openflag = true;
            string sql = "select id,task_code, task_name,task_weight,task_itemnum,task_createdate,task_lotnumber from task_info where user_code='" + myUser.user_code + "'and task_flag=1 order by task_createdate desc";
            DataSet ds = null;
            try
            {
                ds = func.sqlQuery(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            myUser.workFlag = new bool[ds.Tables[0].Rows.Count + 1];
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ListViewItem lv = null;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    lv = new ListViewItem(ds.Tables[0].Rows[i][0].ToString());
                    lv.Tag = ds.Tables[0].Rows[i][0].ToString();
                    for (int j = 1; j < ds.Tables[0].Columns.Count; j++)
                    {
                        lv.SubItems.Add(ds.Tables[0].Rows[i][j].ToString());
                    }
                    lv.SubItems.Add("");
                    lv.SubItems.Add("");
                    myUser.workFlag[i] = true;
                    this.listView1.Items.Add(lv);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Socket client = func.initsocket(myUser.curPrinter[1], Convert.ToInt32(myUser.curPrinter[2]));
            //SendTTO_by(client, "S0", "打印机");
        }

        private void skinButton3_Click(object sender, EventArgs e)
        {
            if (skinButton3.Text == "暂停任务")
            {
                myUser.workFlag[listView1.SelectedItems[0].Index] = false;
                skinButton3.Text = "继续任务";
            }
            else {
                myUser.workFlag[listView1.SelectedItems[0].Index] = true;
                skinButton3.Text = "暂停任务";
            }
        }

        private void listView1_Click(object sender, EventArgs e)
        {
            string IsWork = listView1.SelectedItems[0].SubItems[7].Text;
            if (IsWork.IndexOf("/") <= -1)
            {
                skinButton3.Visible = false;
                skinButton1.Visible = true;
            }
            else {
                skinButton3.Visible = true;
                skinButton1.Visible = false;
            }
        }

        private void skinButton4_Click(object sender, EventArgs e)
        {
            openflag = false;

            foreach (ListViewItem lt in listView1.Items)
            {
                lt.Remove();
            }
            this.LocalTask_Load(sender, e);
        }
    }
}