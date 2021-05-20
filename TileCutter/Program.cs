using ImageMagick;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TileCutter
{
    class MyImage
    {
        public int Zoom { get; set; }
        public int Chunks { get; set; }
        public MagickImage Image { get; set; }
        public MyImage(int zoom, int chunks, MagickImage image)
        {
            Zoom = zoom;
            Chunks = chunks;
            Image = image;
        }
    }
    public enum CheckStatus { 
        Success, 
        WrongPath, 
        WrongSize, 
        WrongColor 
    };
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
