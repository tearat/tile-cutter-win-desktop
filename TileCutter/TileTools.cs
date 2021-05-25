using ImageMagick;
using System.IO;
using System.Drawing;
using System;

namespace TileCutter
{
    public static class TileTools
    {
        public static string TilesRootPath;
        
        public static MagickImage MakeTile(MagickImage image, Point point, int tileSize)
        {
            if (image is null)
                throw new ArgumentNullException($"{nameof(image)} can't be null", nameof(image));
            if (tileSize < 0)
                throw new ArgumentOutOfRangeException($"{nameof(tileSize)} can't be negative", nameof(tileSize));

            using (image)
            {
                var geometry = new MagickGeometry
                {
                    Width = tileSize,
                    Height = tileSize,
                    X = point.X * tileSize,
                    Y = point.Y * tileSize
                };
                var chunk = (MagickImage)image.Clone();
                chunk.Crop(geometry);
                chunk.Format = MagickFormat.Png;
                return chunk;
            }
        }

        public static void SetupRootPathForNewTiles()
        {
            if (string.IsNullOrEmpty(TilesRootPath))
                throw new ArgumentNullException($"{nameof(TilesRootPath)} can't be empty");

            if (!Directory.Exists(TilesRootPath)) Directory.CreateDirectory(TilesRootPath);
            else
            {
                var di = new DirectoryInfo(TilesRootPath);
                foreach (var directory in di.GetDirectories()) directory.Delete(true);
            }
        }

        public static string CreateDirectoryStructure(Point point, int zoom)
        {
            var zoomPath = Path.Combine(TilesRootPath, zoom.ToString());
            if (!Directory.Exists(zoomPath))
                Directory.CreateDirectory(zoomPath);

            var xPath = Path.Combine(zoomPath, point.X.ToString());
            if (!Directory.Exists(xPath))
                Directory.CreateDirectory(xPath);

            var path = Path.Combine(xPath, $"{point.Y}.png");
            return path;
        }
    }
}
