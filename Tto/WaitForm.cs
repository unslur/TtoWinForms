using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tto
{
    public partial class WaitForm : Form
    {
        string info = "耗时程序！！！";
        public WaitForm(string info)
        {
            InitializeComponent();
            this.info = info;
        }
        public WaitForm() {

            InitializeComponent();
        }
        private void WaitForm_Load(object sender, EventArgs e)
        {
            label2.Text = info;
        }
    }
}
