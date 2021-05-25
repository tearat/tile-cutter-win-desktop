using ImageMagick;
using System;

namespace TileCutter
{
    public class ImageSettings
    {
        public int Zoom { get; }
        public int Chunks { get; }
        public string ImagePath { get; }

        public ImageSettings(int zoom, int chunks, string imagePath)
        {
            Zoom = zoom;
            Chunks = chunks > 0 ? chunks : throw new ArgumentOutOfRangeException($"{nameof(chunks)} can't be negative", nameof(chunks));
            ImagePath = imagePath ?? throw new ArgumentNullException($"{nameof(imagePath)} can't be null", nameof(imagePath));
        }
    }
}
