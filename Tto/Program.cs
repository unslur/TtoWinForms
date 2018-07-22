using System;
using System.Windows.Forms;

namespace Tto
{
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Ttologin login = new Ttologin();

            login.ShowDialog();
            if (login.DialogResult == DialogResult.OK)
            {
                MainWin main = new MainWin();

                Application.Run(main);
            }
        }
    }
}