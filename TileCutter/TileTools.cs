﻿using ImageMagick;
using System.IO;
using System.Drawing;
using System;

namespace TileCutter
{
    public static class TileTools
    {
        public static MagickImage MakeTile(MagickImage image, Point point, int tileSize)
        {
            if (image is null)
                throw new ArgumentNullException($"{nameof(image)} can't be null", nameof(image));
            if (tileSize < 0)
                throw new ArgumentOutOfRangeException($"{nameof(tileSize)} can't be negative", nameof(tileSize));

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
            if (chunk is null)
                throw new ArgumentNullException($"{nameof(chunk)} can't be null", nameof(chunk));

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
