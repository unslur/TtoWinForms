using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tto
{
    public partial class PrintForm : Form
    {
        private bool workFlag = true;
        public delegate string asyncresult();
        public PrintForm(string[] info)
        {
            this.info = info;
            InitializeComponent();
        }
        public delegate string AysnPrintDelegate();
        private string[] info;
        /*
         0,task_code
         1,name
         2,数量
        3，重量
        4，时间
        5，规格
        6，批次号
             */
        private void PrintForm_Load(object sender, EventArgs e)
        {
           // normalPrinter ss = new normalPrinter();
            //ss.init2();
            if (!info[7].Contains("中"))
            {
                int rtn = func.downcode(info[0]);
                if (rtn == -1)
                {
                    Close();
                    return;
                }
                string rtnStr = func.sendToServer("address," + info[0]);
                info[8] = rtnStr;
                func.TaskInfoToLocaldb(info[0], info[1], info[2], info[3], info[4], info[5], info[6], info[8]);
               
            }
            
            
            DataSet ds = func.getLocalCode(myUser.user_code, info[0]);
            string localCode = func.Splice(ds.Tables[0]);
            if (!localCode.Contains("^")) {
                MessageBox.Show("本地任务已删除");
                func.sendToServer("finishtask," + info[0] + ",3");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            ds = func.getaddr(info[0]);
            string addr = func.Splice(ds.Tables[0]);
            try
            {
                info[8] = addr.Substring(0, addr.IndexOf('^') );
            }
            catch (Exception)
            {

                info[8] = null;
            }
            string localprinter = func.getDevice();
            List<string> listinfo = func.GetStrlist(localprinter, '|', false);
            foreach (string s in listinfo)
            {
                if (s.Length < 1) { return; }
                string[] infop = s.Split('^');
                ComboboxItem cbi = new ComboboxItem();
                cbi.Text = infop[0];
                cbi.Value = infop;
                //myComboboxItem ci = new myComboboxItem();
                //ci.Text = info[1]; ci.Value = info[0];
                skinComboBox2.Items.Add(cbi);
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
        normalPrinter np = new normalPrinter();
        int Printflag = 0;
        private void skinButton4_Click(object sender, EventArgs e)
        {
            string printtask_code = info[0];
            DataSet ds = func.getLocalCode(myUser.user_code, printtask_code);
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
                sql = "update task_info set task_flag=2 where task_code='" + printtask_code + "'";
                func.sendToServer("finishtask,"+printtask_code+",3");
                func.ExecuteSql(sql);
                return;
            }
            if (skinComboBox2.SelectedIndex == -1 || skinComboBox2.SelectedIndex == 0)
            {//打印机
                int type = skinComboBox1.SelectedIndex;
                Printflag = 1;
                PrintStatus one = new PrintStatus();
                
                one.totalNum = Convert.ToInt32(this.info[2]);
                one.print_task = info[0];
                
                thread_set_progress = new Thread(new ParameterizedThreadStart(PrintNum));
                thread_set_progress.Start(one);
                string[] normaltaskarray = new string[7];
                /*
                 0,二维码前缀如http：//  cpc
                 
                 1,名称  重量
                 2,产地
                 3,生产日期
                 4,批号
                 5,厂家名称 company_name
                             
                 */

                normaltaskarray[0] = skinTextBox2.Text;//前缀

                normaltaskarray[1] = info[1];
                normaltaskarray[2]= info[2];//名称重量
                if (info[8] == null) {
                    info[8] = "四川省成都市";
                }
                if (info[8].Contains("广西壮族自治区")) {
                    info[8] = "广西" + info[8].Substring(7);
                }
                else if (info[8].Contains("新疆维吾尔自治区"))
                {
                    info[8] = "新疆" + info[8].Substring(8);
                }
                else if (info[8].Contains("宁夏回族自治区"))
                {
                    info[8] = "宁夏" + info[8].Substring(7);
                }
                else if (info[8].Contains("西藏自治区"))
                {
                    info[8] = "西藏" + info[8].Substring(5);
                }
                else if (info[8].Contains("内蒙古自治区"))
                {
                    info[8] = "内蒙" + info[8].Substring(6);
                }
                normaltaskarray[3] = info[8];//产地
                normaltaskarray[5] = info[6];//批号
                
                normaltaskarray[4] = info[4].Substring(0,10);//日期
                normaltaskarray[6] = "厂家:" + myUser.company_name;
                List<string> normallistCode = func.GetStrlist(localCode, '|', false);
                if (type == 1)
                {//三列
                    //前缀0 溯源码 名称重量1 产地2 生产日期（日）3 批号4 公司5
                    np.PrintThree(normaltaskarray, normallistCode,one);
                    //Dispose(true);
                }
                else
                {//两列
                    MessageBox.Show("2");
                    np.PrintTwo(normaltaskarray, normallistCode, one);
                }
            }
            else
            {
                Printflag = 2;
                int j = temp.IndexOf('^');//判读是否为中包，不打印中包
                temp = localCode.Substring(0, j);
                string type = localCode.Substring(j+1,1);
                if (type == "1") {
                   localCode= localCode.Substring(i + 1);

                }
                localCode = localCode.Substring(i + 1);

                string[] taskarray = new string[9];
                /*
                 0,二维码前缀如http：//  cpc
                 1,第一个溯源码
                 2,名称 
                 3,重量
                 4,规格
                 5,厂家名称 company_name
                 6,时间
                 7,批次
                 8,一维码数据             
                 */

                taskarray[0] = skinTextBox2.Text;
                taskarray[1] = temp;
                // MessageBox.Show(temp);
                // MessageBox.Show(localCode);
                taskarray[2] = info[1];
                taskarray[3] = info[2];
                taskarray[4] = info[5];
                taskarray[5] = "厂家:" + myUser.company_name;
                taskarray[7] = info[4];
                taskarray[6] = info[6];
                taskarray[8] = "15161";
                List<string> listCode = func.GetStrlist(localCode, '|', false);

                printProgress(taskarray, listCode);
            }
        }
        private void printProgress(string[] taskarray, List<string> listCode)
        {
            ComboboxItem ci = skinComboBox2.SelectedItem as ComboboxItem;
            if (ci == null) {
                MessageBox.Show("选择打码机");
            }
            string[] printer =(string []) ci.Value;//打印机名称，ip以及端口信息；
            if ( printer[2].Length< 8)
            {
                MessageBox.Show(string.Format("ip error {0},{1}",printer[2],printer[1]));
                //normalPrinter print_N = new normalPrinter();
                //print_N.init();
                //print_N.PrintThree(taskarray, listCode);
                //listView1.Items.Remove(this.listView1.SelectedItems[0]);
            }
            else
            {


                //string printer_type = ci.Value.ToString();

               // circularProgressBar1.Visible = true;
                string printer_name = ci.Text;
                
                PrintStatus one = new PrintStatus();
                one.printTtoSo[0] = printer[0];
                one.printTtoSo[1] = printer[2];
                one.printTtoSo[2] = printer[3];
                one.totalNum = Convert.ToInt32( this.info[2]);
                one.print_task = info[0];
                TtoPrinter tp = new TtoPrinter(taskarray, listCode, one);
                if (printer[1].IndexOf("duo") > -1) {//打码机类型

                    BeginPrinting(taskarray, listCode, one, 0);
                    skinLabel1.Text = "打印中";
                }
                else if (printer[1].IndexOf("wdj") > -1)
                {
                    BeginPrinting(taskarray, listCode, one, 1);
                    skinLabel1.Text = "打印中";
                }
                else if (printer[1].IndexOf("markem") > -1)
                {
                    BeginPrinting(taskarray, listCode, one, 2);
                    skinLabel1.Text = "打印中";
                }
                Control.CheckForIllegalCrossThreadCalls = false;
                thread_set_progress = new Thread(new ParameterizedThreadStart(PrintNum));
                thread_set_progress.IsBackground = true;
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
                    if (printer.stop_signal) {
                        return;
                    }
                    skinLabel4.Text = one.curNum + "/" + one.totalNum;
                    //circularProgressBar1.Value = Convert.ToInt32(one.curNum) / Convert.ToInt32(one.totalNum);
                    if (one.workStatus > 2)
                    {

                        skinLabel1.Text = "打印结束";
                        //circularProgressBar1.Visible = false;
                        Printflag = 0;
                        func.sendToServer("finishtask," + one.print_task + ",3");//3为打印完成

                        break;
                    }
                    else if (one.workStatus == 2) {

                        skinLabel1.Text = "暂停中";
                    }
                }
                catch (Exception e)
                {
                  //  MessageBox.Show(e.Message);
                    return;
                }
                Thread.Sleep(1000);
            }
        }
        TtoPrinter printer =null;
        public IAsyncResult BeginPrinting(string [] taskarray ,List<string> listCode,PrintStatus one,int printer_type) {
            AysnPrintDelegate PrintDelegate = null;
            printer = new TtoPrinter();
             printer.initPrintData(taskarray,listCode,one);
            if (printer_type == 0)
            {
                PrintDelegate = new AysnPrintDelegate(printer.DuoMiNuo);
            }
            else if (printer_type == 1)
            {
                PrintDelegate = new AysnPrintDelegate(printer.WDJ);
            }
            else
            {
                PrintDelegate = new AysnPrintDelegate(printer.Markem);
            }
            return PrintDelegate.BeginInvoke(Printed, PrintDelegate);
        }
        int OverOrErr = 0;
        private void Printed(IAsyncResult result)
        {
            AysnPrintDelegate aysnDelegate = result.AsyncState as AysnPrintDelegate;
            if (aysnDelegate != null)
            {
                string rtn = "";
                try
                {
                    rtn = aysnDelegate.EndInvoke(result);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    return;
                }
                int index = 0;
                string machine_name = rtn.Substring(0, rtn.IndexOf('^') );
                rtn = rtn.Substring(rtn.IndexOf('^') + 1);
                string sql = "";
                sql = "update device set status=0 where name='" + machine_name + "'";
                if (rtn.Length >= 4)
                {
                    MessageBox.Show(rtn);

                    OverOrErr = 2;
                    //this.listView1.Items[one.index].SubItems[7].Text = "打印结束";
                    thread_set_progress.Abort();
                }
                else
                {
                    OverOrErr = 1;
                    // index = Convert.ToInt32(rtn);
                    //this.label3.Text = ":空闲";
                    // sql = "update device set status=0 where name='" + this.listView1.Items[index].SubItems[8].Text + "'";
                    // thread_set_progress.Abort();
                }
               
                func.ExecuteSql(sql);

                
            }
            else
            {
                MessageBox.Show("打印失败");
            }
        }

        private void skinComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (skinComboBox2.SelectedIndex == 0 || skinComboBox2.SelectedIndex == -1)
            {
                skinComboBox1.Visible = true;
            }
            else {
                //string sql = string.Format("select status from device where name ='{1}'",skinComboBox2.Text);
               string status= func.getDeviceStatus(skinComboBox2.Text);
                if (status.Contains("1")) {
                    MessageBox.Show("该打码机正在打印中，请选择其他打码机");
                }
                skinComboBox1.Visible = false;
            }
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int type = skinComboBox1.SelectedIndex;
            normalPrinter np = new normalPrinter();
           
            string[] normaltaskarray = new string[6];
            /*
             0,二维码前缀如http：//  cpc

             1,名称  重量
             2,产地
             3,生产日期
             4,批号
             5,厂家名称 company_name

             */

            normaltaskarray[0] = skinTextBox2.Text;

            normaltaskarray[1] = info[1] + info[2];
            normaltaskarray[2] = info[2];
            normaltaskarray[3] = info[5];
            normaltaskarray[5] = "厂家:" + myUser.company_name;
            normaltaskarray[4] = info[4];
            PrintStatus one = new PrintStatus();

            one.totalNum = Convert.ToInt32(this.info[2]);
            one.print_task = info[0];
            List<string> normallistCode = new List<string>();
            for (int i=0;i<5;i++) {
                string s = "1234567890123456789012345678^2";
                normallistCode.Add(s);
            }
            if (type == 1)
            {//三列
                
                np.PrintThree(normaltaskarray, normallistCode,one);
            }
            else
            {//两列
               
            }
        }

        private void skinButton1_Click(object sender, EventArgs e)
        {
            if (Printflag ==2)
            {
                printer.workFlag = !printer.workFlag;
            } else if (Printflag==1) {
                np.workFlag = !np.workFlag;
            }
        }

        private void PrintForm_FormClosing(object sender, EventArgs e)
        {
            if (Printflag != 0)
            {
                DialogResult dr = MessageBox.Show("任务进行中，确认关闭吗？", "提示", MessageBoxButtons.OKCancel);
                if (dr == DialogResult.OK)
                {
                    //用户选择确认的操作
                    if (Printflag == 1)
                    {//打印机


                    }
                    else
                    {//打码机
                        try
                        {
                            //this.Close();
                            //return;
                            printer.taskFlag = false;
                            Thread.Sleep(1500);
                            while (!printer.taskFlag && OverOrErr == 0)
                            {
                                ;
                            }
                            //thread_set_progress.Abort();
                            // Thread.Sleep(1000);
                            // printer.workFlag = false;
                            // thread_set_progress.Abort();
                            // printer.shutConnect();
                            //// printer = null;
                            // ComboboxItem ci = skinComboBox2.SelectedItem as ComboboxItem;
                            // if (ci == null)
                            // {
                            //     MessageBox.Show("选择打码机");
                            // }
                            // string[] printerarray = (string[])ci.Value;//打印机名称，ip以及端口信息；
                            //  string   sql = "update device set status =  0 where name ='" + printerarray[0] + "'";
                            // func.ExecuteSql(sql);
                            // IDisposable disposable = printer as IDisposable;
                            //  if (disposable != null)
                            // disposable.Dispose();
                            //this.Close();
                            this.Dispose();
                            this.Close();
                        }
                        catch (Exception err)
                        {
                            // MessageBox.Show(err.Message);
                            return;
                        }
                        finally
                        {


                        }
                    }
                }
                else if (dr == DialogResult.Cancel)
                {
                    //用户选择取消的操作
                    // return;
                }

            }
            else {
                this.Close();
            }
        }

        private void PrintForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //printer.taskFlag = false;
        }
    }
}