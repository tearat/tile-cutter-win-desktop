using System;
using System.IO;
using System.Windows.Forms;

namespace TileCutter
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            TileTools.TilesRootPath = Path.Combine(".", "tiles");
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
