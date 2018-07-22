using System;
using System.Data;

namespace Tto
{
    internal class Task
    {
        private int stateFlag;
        private String printFlag;

        public int GetstateFlag()
        {
            return stateFlag;
        }

        public void Setstateflag(int v)
        {
            stateFlag = v;
        }

        public String PrintFlag { get { return printFlag; } set { this.printFlag = value; } }
        public uint printtask_code { get; set; }

        public DataTable getNumber()
        {
            string sql = "select * from number where printtask_code =" + printtask_code + " limit 1000";
            DataTable dt = func.sqlQuery(sql).Tables[0];
            return dt;
        }

        public DataTable getLocalInfo(string printtask_code)
        {
            string sql = "select task_code,id,task_name,task_spec task_weight,task_creatdate,task_lotnumber from task_info where task_code='" + printtask_code + "'";
            //sql += printtask_code.ToString();
            DataTable dt = func.sqlQuery(sql).Tables[0];
            return dt;
        }

        public int Ttoprint()
        {
            return 0;
        }
    }
}