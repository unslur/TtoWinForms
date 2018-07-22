using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Web;

namespace Tto
{
    ///// <summary>
    ///// 日志类型
    ///// </summary>
    //public enum LogFile
    //{
    //    Trace,
    //    Warning,
    //    Error,
    //    SQL
    //}
    public class Common
    {
        private bool CharInArray(char aChar, char[] charArray)
        {
            return (Array.Exists<char>(charArray, delegate (char a) { return a == aChar; }));
        }

        //16进制字符
        private char[] HexDigits = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'a', 'b', 'c', 'd', 'e', 'f' };

        private char[] bases = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <param name="mode"></param> 2：2进制；34：34进制
        /// <returns></returns>
        public string MyConvert10To34(int value, int mode)
        {
            if (value / mode == 0)
                return Convert.ToString(bases[value % mode]);
            else
                return MyConvert10To34(value / mode, mode) + Convert.ToString(bases[value % mode]);
        }

        public string MyConvert34To10(string value, int mode)
        {
            double temp = 0;
            for (int i = 0; i <= value.Length - 1; i++)
            {
                for (int j = 0; j < bases.Length; j++)
                {
                    if (value[i].ToString() == bases[j].ToString())
                    {
                        if (value[i].ToString() != "0")
                        {
                            if (i < value.Length - 1)
                            {
                                temp = temp + Math.Pow(34, value.Length - 1 - i) * j;  //pow(a,m) a的m次幂
                            }
                            else
                            {
                                temp = temp + j;
                            }
                        }
                        else
                        {
                            temp = temp + 0;
                        }
                        break;
                    }
                }
            }

            return temp.ToString();
        }

        /// <summary>
        /// 进制转换
        /// </summary>
        /// <param name="value">待转换字符串</param>
        /// <param name="fromBase">原始进制</param>
        /// <param name="toBase">目标进制</param>
        /// <returns>以目标进制表示的字符串</returns>
        public string ConvertStr(string value, int fromBase, int toBase)
        {
            int intValue = Convert.ToInt32(value, fromBase);
            return Convert.ToString(intValue, toBase);
        }

        /// <summary>
        /// 将AscII码转换为字符串
        /// </summary>
        /// <param name="buf"></param>
        /// <returns></returns>
        public string Ascii2str(byte[] buf)
        {
            return System.Text.Encoding.ASCII.GetString(buf);//这里的buf就是存放ASCII的byte数组
        }

        public string Unicode_tto(string strdata)
        {
            strdata = strdata.Replace("01 00", "5B 00 53 00 4F 00 48 00 5D 00");  //[SOH]
            strdata = strdata.Replace("17 00", "5B 00 53 00 4F 00 48 00 5D 00");  //[SOH]
            strdata = strdata.Replace("02 00", "5B 00 53 00 54 00 58 00 5D 00");  //[STX]
            strdata = strdata.Replace("03 00", "5B 00 45 00 54 00 58 00 5D 00");  //[ETX]
            //  strdata = GetChsFromHex(strdata);
            return strdata;
        }

        public string ByteToStr(byte[] data)
        {
            string hexContent = "";
            foreach (byte b in data)
            {
                hexContent += string.Format("{0:X2}", b) + " ";
            }

            return hexContent;
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

        public string StrTo16Str(string str)
        {
            //string str = cnt.ToString().PadLeft(4, '0');
            StringBuilder stringBuilder = new StringBuilder(str.Length * 2);

            for (int i = 0; i <= str.Length - 1; i++)
            {
                stringBuilder.Append(((int)str[i]).ToString("X2") + " 00 ");//x2是小写.
                // LogManager.WriteLog("debug", "str is :" + str + "i is:" + i.ToString() + "str[i]:" + str[i]);
            }
            return stringBuilder.ToString();
        }

        //C# 怎么把汉字转换成16进制
        public string StrTo16Str2(string str)
        {
            //string str = "你好";
            byte[] bytes = Encoding.Unicode.GetBytes(str);
            string[] strArr = new string[bytes.Length];
            string temp = "";

            //for (int i = 0; i < bytes.Length; i++)
            //{
            //    strArr[i] = bytes[i].ToString("x");
            //    temp = temp + strArr[i];
            //}

            var bin = System.Text.Encoding.GetEncoding("gb2312").GetBytes(str);
            foreach (byte b in bin)
            {
                temp = temp + b.ToString();
            }

            return temp;
        }

        /// <summary>
        /// 将字符串转换为AscII码
        /// </summary>
        /// <param name="str1"></param>
        /// <returns></returns>
        public string strToAscii(string str1)
        {
            byte[] array = System.Text.Encoding.ASCII.GetBytes(str1.Trim());
            string str = null;
            for (int i = 0; i < array.Length; i++)
            {
                int asciicode = (int)(array[i]);
                str += Convert.ToString(asciicode);
            }
            return str;
        }

        /// <summary>
        /// 从汉字转换到16进制
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public string GetHexFromChs(string s)
        {
            if ((s.Length % 2) != 0)
            {
                s += " ";//空格
                //throw new ArgumentException("s is not valid chinese string!");
            }

            System.Text.Encoding chs = System.Text.Encoding.GetEncoding("gb2312");

            byte[] bytes = chs.GetBytes(s);

            string str = "";

            for (int i = 0; i < bytes.Length; i++)
            {
                str += string.Format("{0:X}", bytes[i]);
            }

            return str;
        }

        /// <summary>
        /// 从16进制转换成汉字
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public string GetChsFromHex(string hex)
        {
            if (hex == null)
                throw new ArgumentNullException("hex");
            if (hex.Length % 2 != 0)
            {
                hex += "20";//空格
                //throw new ArgumentException("hex is not a valid number!", "hex");
            }
            // 需要将 hex 转换成 byte 数组。
            byte[] bytes = new byte[hex.Length / 2];

            for (int i = 0; i < bytes.Length; i++)
            {
                try
                {
                    // 每两个字符是一个 byte。
                    bytes[i] = byte.Parse(hex.Substring(i * 2, 2),
                    System.Globalization.NumberStyles.HexNumber);
                }
                catch
                {
                    // Rethrow an exception with custom message.
                    throw new ArgumentException("hex is not a valid hex number!", "hex");
                }
            }

            // 获得 GB2312，Chinese Simplified。
            System.Text.Encoding chs = System.Text.Encoding.GetEncoding("gb2312");

            return chs.GetString(bytes);
        }

        /// 十六进制字符串转换字节数组
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public byte[] HexStringToByteArray(string s)
        {
            //            s = s.Replace(" ", "");
            StringBuilder sb = new StringBuilder(s.Length);
            foreach (char aChar in s)
            {
                if (CharInArray(aChar, HexDigits))
                    sb.Append(aChar);
            }
            s = sb.ToString();
            int bufferlength;
            if ((s.Length % 2) == 1)
                bufferlength = s.Length / 2 + 1;
            else bufferlength = s.Length / 2;
            byte[] buffer = new byte[bufferlength];
            for (int i = 0; i < bufferlength - 1; i++)
                buffer[i] = (byte)Convert.ToByte(s.Substring(2 * i, 2), 16);
            if (bufferlength > 0)
                buffer[bufferlength - 1] = (byte)Convert.ToByte(s.Substring(2 * (bufferlength - 1), (s.Length % 2 == 1 ? 1 : 2)), 16);
            return buffer;
        }

        public string GetMonth(string month)
        {
            string temp;

            switch (Convert.ToInt32(month))
            {
                case 1: temp = "A"; break;
                case 2: temp = "B"; break;
                case 3: temp = "C"; break;
                case 4: temp = "D"; break;
                case 5: temp = "E"; break;
                case 6: temp = "F"; break;
                case 7: temp = "G"; break;
                case 8: temp = "H"; break;
                case 9: temp = "I"; break;
                case 10: temp = "J"; break;
                case 11: temp = "K"; break;
                case 12: temp = "L"; break;
                default: temp = "A"; break;
            }
            return temp;
        }

        //public string GetStartNumber(string LineNumber, out string lN )
        public string GetStartNumber(string LineNumber)
        {
            string temp;
            switch (LineNumber)
            {
                case "1线":
                    temp = "0001"; break;
                case "2线":
                    temp = "3TRS"; break;
                case "3线":
                    temp = "7MHH"; break;
                case "4线":
                    temp = "BF98"; break;
                case "5线":
                    temp = "F90Z"; break;
                case "6线":
                    temp = "K2SQ"; break;
                case "7线":
                    temp = "NWJF"; break;
                default:
                    temp = "K2SQ"; break;
            }
            return temp;
        }

        //private int WeekOfYear(string date)
        public int WeekOfYear(DateTime curDay)
        {
            //DateTime curDay = Convert.ToDateTime(date);
            int firstdayofweek = Convert.ToInt32(Convert.ToDateTime(curDay.Year.ToString() + "- " + "1-1 ").DayOfWeek);

            int days = curDay.DayOfYear;
            int daysOutOneWeek = days - (7 - firstdayofweek);

            if (daysOutOneWeek <= 0)
            {
                return 1;
            }
            else
            {
                int weeks = daysOutOneWeek / 7;
                if (daysOutOneWeek % 7 != 0)
                    weeks++;

                return weeks + 1;
            }
        }
    }

    public class LabelFont
    {
        [NonSerialized()]
        private Font font;

        public LabelFont()
            : this("Arial", 25)
        {
        }

        public string Name
        {
            set { font = new Font(value, font.Size); }
            get { return font.FontFamily.Name; }
        }

        public int Height { get { return font.Height; } }

        public float EmSize
        {
            set { font = new Font(font.FontFamily.Name, value); }
            get { return font.Size; }
        }

        public LabelFont(string famliyName, int emSize)
        {
            font = new Font(famliyName, emSize);
        }

        //public class ZebraCmdFactory
        // {
        //   //  protected static log4net.ILog LOG = LogManager.GetLogger(typeof(ZebraCmdFactory));
        //     private static string _HexZebraString(CFG_LABELDETAIL labeldetail, int n, bool isVertical)
        //     {
        //         StringBuilder sb = new StringBuilder();
        //         string labelName = "label" + n.ToString();
        //         int orientation = isVertical ? 90 : 0;
        //         unsafe
        //         {
        //             char[] buf = new char[5000];
        //             fixed (char* pBuf = buf)
        //             {
        //              // int len = ZebraApi.GETFONTHEX(labeldetail.RealText, labeldetail.LableFont.Name, labelName, (short)orientation, GetHeigh(labeldetail.LableFont), GetSingleCharWeigth(labeldetail.LableFont), 1, 0, pBuf);
        //                 int len = ZebraApi.GETFONTHEX(labeldetail.RealText, "Arial", labelName, (short)orientation, GetHeigh(labeldetail.LableFont), GetSingleCharWeigth(labeldetail.LableFont), 1, 0, pBuf);

        //                 foreach (char c in buf)
        //                 {
        //                     if ('\0' == c)
        //                         break;
        //                     char l = (char)(c & 0x00ff);
        //                     char h = (char)(c >> 8);
        //                     sb.Append(l);
        //                     sb.Append(h);
        //                 }
        //             }
        //         }
        //         sb.Append("^XG");
        //         sb.Append(labelName);
        //         return sb.ToString();
        //     }

        //     private static short GetSingleCharWeigth(LabelFont font)
        //     {
        //         return (short)(font.Height / 2);
        //     }

        //     private static short GetHeigh(LabelFont font)
        //     {
        //         return (short)font.Height;
        //     }

        //     //public static string CreateBarcodeCmd(CFG_LABEL label)
        //     //{
        //     //    StringBuilder sb = new StringBuilder();
        //     //    int i = 1;

        //     //    // 初始化设置
        //     //    // ^XA
        //     //    // ^LHx,y^PW{width}
        //     //    sb.Append("^XA");
        //     //    sb.Append("^LH");
        //     //    sb.Append(label.LEFT);
        //     //    sb.Append(",");
        //     //    sb.Append(label.TOP);
        //     //    sb.Append("^PW");
        //     //    sb.AppendLine(label.WIDTH.ToString());

        //     //    // 循环处理数据
        //     //    foreach (CFG_LABELDETAIL labeldetail in label.DetailList)
        //     //    {
        //     //        // 标签
        //     //        // ^FOx,y^AD^FD{data}^FS
        //     //        // 汉字标签
        //     //        // ^FOx,y^XG{labelName}^FS
        //     //        if (labeldetail.TYPE == "0")
        //     //        {
        //     //            sb.Append("^FO");
        //     //            sb.Append(labeldetail.LEFT);
        //     //            sb.Append(",");
        //     //            sb.Append(labeldetail.TOP);
        //     //            string str = _HexZebraString(labeldetail, i++, labeldetail.IsVertical);
        //     //            sb.Append(str);

        //     //            //LOG.Debug(label.Value + ":" + testbarcode + ":" + str);

        //     //            sb.AppendLine("^FS");
        //     //        }
        //     //        // 条码
        //     //        // ^FOx,y^BY{比例}^BCN,height,N,N,N,N^FD{data}^FS
        //     //        else if (labeldetail.TYPE == "1")
        //     //        {
        //     //            sb.Append("^FO");
        //     //            sb.Append(labeldetail.LEFT);
        //     //            sb.Append(",");
        //     //            sb.Append(labeldetail.TOP);
        //     //            //if (label.IsVertical)
        //     //            //{
        //     //            //    sb.Append("^A0R");
        //     //            //    sb.Append(zbd.Bounds.Width);
        //     //            //    sb.Append(",");
        //     //            //    sb.Append(zbd.Bounds.Height);
        //     //            //}
        //     //            sb.Append("^BY");
        //     //            sb.Append(labeldetail.SCALE);
        //     //            if (labeldetail.IsVertical)
        //     //                sb.Append("^BCR,");
        //     //            else
        //     //                sb.Append("^BCN,");
        //     //            sb.Append(labeldetail.HEIGHT);
        //     //            sb.Append(",Y,N^FD>;");
        //     //            sb.Append(labeldetail.RealText);
        //     //            sb.AppendLine("^FS");
        //     //        }
        //     //    }

        //     //    // 设置结束打印
        //     //    // ^PQ{份数}
        //     //    sb.Append("^PQ");
        //     //    sb.Append(label.Quantity);
        //     //    sb.AppendLine("^FS^XZ");

        //     //    if (LOG.IsDebugEnabled)
        //     //    {
        //     //        LOG.Debug("Label: " + sb.ToString());
        //     //    }

        //     //    return sb.ToString();
        //     //}
        // }
    }
}