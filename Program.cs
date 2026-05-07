using System;
using System.Windows.Forms;
using MemoryCard_GameLatHinh_.GUI;

namespace MemoryCard_GameLatHinh_
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new TrangChu());
        }
    }
}