using ImageMagick;
using System.IO;
using System.Drawing;

namespace TileCutter
{
    public static class TileTools
    {
        public static MagickImage MakeTile(MagickImage image, Point point, int tileSize)
        {
            MagickGeometry geometry = new MagickGeometry
            {
                Width = tileSize,
                Height = tileSize,
                X = point.X * tileSize,
                Y = point.Y * tileSize
            };

            MagickImage chunk = (MagickImage)image.Clone();
            chunk.Crop(geometry);
            chunk.Format = MagickFormat.Png;
            return chunk;
        }

        public static void WriteTile(MagickImage chunk, Point point, int zoom)
        {
            string zoomPath = $"./tiles/{zoom}";
            if (!Directory.Exists(zoomPath))
                Directory.CreateDirectory(zoomPath);

            string xPath = $"./tiles/{zoom}/{point.X}";
            if (!Directory.Exists(xPath))
                Directory.CreateDirectory(xPath);

            string fullPath = $"./tiles/{zoom}/{point.X}/{point.Y}.png";
            chunk.Write(fullPath);
        }
    }
}
