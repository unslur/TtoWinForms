using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tto
{
    public class PrintStatus
    {
        public int index;
        public int curNum;
        public int totalNum;
        public string []printTtoSo=new string[3];
        public int workStatus;//0 无 1 打印中 2暂停中 3结束 4异常
        public string print_task;

    }
}