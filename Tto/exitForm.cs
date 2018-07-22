using CCWin;
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
    public partial class exitForm : CCSkinMain
    {
        public exitForm()
        {
            InitializeComponent();
        }

        [System.Runtime.InteropServices.DllImport("user32")]
        private static extern bool AnimateWindow(IntPtr hwnd, int dwTime, int dwFlags);

        private const int AW_VER_NEGATIVE = 0x0008;
        private const int AW_SLIDE = 0x40000;
        private const int AW_BLEND = 0x80000;
        private const int AW_HIDE = 0x10000;
        private const int AW_VER_POSITIVE = 0x0004;

        private void exitForm_Load(object sender, EventArgs e)
        {
            AnimateWindow(this.Handle, 200, AW_VER_POSITIVE | AW_SLIDE);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            func.WritePrivateProfileString("user", "name", "", myUser.profile);
            func.WritePrivateProfileString("user", "pass", "", myUser.profile);
            this.DialogResult = DialogResult.Abort;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }
    }
}