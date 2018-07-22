using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace Tto
{
    internal class normalPrinter
    {
       [DllImport("normalPrint.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern bool Print3(string name, UInt32 type);
        [DllImport("normalPrint.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern bool Print2(string name, UInt32 type);
        private  string defualtPrinter = new PrintDocument().PrinterSettings.PrinterName;

        [DllImport("kernel32.dll")]
        private extern static IntPtr LoadLibrary(string path);

        [DllImport("kernel32.dll")]
        private extern static IntPtr GetProcAddress(IntPtr lib, string funcName);

        [DllImport("kernel32.dll")]
        private extern static bool FreeLibrary(IntPtr lib);

        [UnmanagedFunctionPointerAttribute(CallingConvention.Cdecl)]
        public delegate int fPrint_2( string msg, UInt32 place);
        [UnmanagedFunctionPointerAttribute(CallingConvention.Cdecl)]
        public delegate int fPrint_3(string msg, UInt32 place);

        private Delegate GetAddress(IntPtr dllModule, string functionname, Type t)
        {

            int addr =(int) GetProcAddress(dllModule, functionname);
            if (addr == 0)
                return null;
            else
                return Marshal.GetDelegateForFunctionPointer(new IntPtr(addr), t);
        }
        private string[] info = null;
        private List<string> code = null;
     
        public bool workFlag = true;
        PrintStatus one = null;
        private IntPtr hLib;
        fPrint_2 print_2;
        fPrint_3 print_3;
        public int init2() {
            hLib = LoadLibrary("normalPrint.dll");
            if (hLib.ToInt32() == 0)
            {
                
                MessageBox.Show("缺少normalPrint.dll");
                return 0;
            }
          
            print_2 = (fPrint_2)GetAddress(hLib, "Print2", typeof(fPrint_2));
            return 1;

        }
        private int init3()
        {
            hLib = LoadLibrary("normalPrint.dll");
            if (hLib.ToInt32() == 0)
            {
                MessageBox.Show("缺少normalPrint.dll");
                return 0;
            }

            print_3 = (fPrint_3)GetAddress(hLib, "Print3", typeof(fPrint_3));
            return 1;

        }
        private void uinit() {
            FreeLibrary(hLib);
        }
        public int PrintThree(string[] info, List<string> code,PrintStatus _one)
        {
            one = _one;
            this.info = info;
            this.code = code;
            // MessageBox.Show("555");
            string code_3 = "";
            int i = 0;
            foreach (string oneCode in this.code)
            {
                while (true)
                {
                    if (workFlag)
                    {

                        string[] codeArray = oneCode.Split('^');
                        if (codeArray[1] == 2.ToString())//小包
                        {
                            if (i == 2 || oneCode.Equals(this.code[this.code.Count - 1]))
                            {
                                one.curNum++;
                                code_3 += codeArray[0];
                               
                                    PrintThree(code_3);
                                    
                               

                                code_3 = "";
                                i = 0;
                            }
                            else
                            {
                                code_3 += codeArray[0] + " ";
                                i++;
                                one.curNum++;
                            }
                        }
                        else
                        {
                            if (code_3.Length > 5)
                            {
                              
                                    PrintThree(code_3);

                            }
                            
                                PrintThree(codeArray[0]);

                            i = 0;
                        }
                        break;
                    }
                    else
                    {
                        
                        Thread.Sleep(100);
                    }
                }
            }
          
            return 0;
        }
        public int PrintTwo(string[] info, List<string> code, PrintStatus _one)
        {
            one = _one;
            this.info = info;
            this.code = code;
            // MessageBox.Show("555");
            string code_2 = "";
            int i = 0;
            foreach (string oneCode in this.code)
            {
                while (true)
                {
                    if (workFlag)
                    {

                        string[] codeArray = oneCode.Split('^');
                        if (codeArray[1] == 2.ToString())//小包
                        {
                            if (i == 1 || oneCode.Equals(this.code[this.code.Count - 1]))
                            {
                                one.curNum++;
                                code_2 += codeArray[0];

                                PrintTwo(code_2);



                                code_2 = "";
                                i = 0;
                            }
                            else
                            {
                                code_2 += codeArray[0] + " ";
                                i++;
                                one.curNum++;
                            }
                        }
                        else
                        {
                            if (code_2.Length > 5)
                            {

                                PrintTwo(code_2);

                            }

                            PrintTwo(codeArray[0]);

                            i = 0;
                        }
                        break;
                    }
                    else
                    {

                        Thread.Sleep(100);
                    }
                }
            }

            return 0;
        }



        private void PrintThree(string code_3)
        {
           
            string[] code = code_3.Split(' ');
            string allcode = "";
            foreach(string one_Code in code) {
                //前缀 溯源码 名称重量 产地 生产日期（日） 公司

                allcode += string.Format("{0}{1}^{1}^{2}g^{3}^{4}^{5}^{6}",info[0],one_Code,info[1]+info[2],info[3],info[4],info[5],info[6]);
                allcode += "|";
            }
            allcode = allcode.Substring(0,allcode.Length - 1);
            //MessageBox.Show(allcode);
            try
            {
                if (Print3(allcode, 4)) {

                }
            }
            catch (Exception)
            {

                MessageBox.Show("打印错误");
            }
            uinit();
        }
        private void PrintTwo(string code_2)
        {
            //init2();
            string[] code = code_2.Split(' ');
            string allcode = "";
            foreach (string one_Code in code)
            {
                //前缀 溯源码 名称 重量 产地 生产日期（日） 公司

                allcode += string.Format("{0}{1}^{1}^品 名:{2}^重 量:{3}g^产地:{4}^生产日期:{5}^批号:{6}^{7}", info[0], one_Code, info[1], info[2], info[3], info[4], info[5],info[6]);
                allcode += "|";
            }
            allcode = allcode.Substring(0, allcode.Length - 1);
            //MessageBox.Show(allcode);
          //  MessageBox.Show("3");
            try
            {
                if (Print2(allcode, 3)) {

                }
            }
            catch (Exception)
            {

                MessageBox.Show("打印错误");
            }
           // uinit();
        }


    }
}