using ImageMagick;
using System;

namespace TileCutter
{
    public class ImageSettings
    {
        public int Zoom { get; set; }
        public int Chunks { get; set; }
        public MagickImage Image { get; set; }

        public ImageSettings(int zoom, int chunks, MagickImage image)
        {
            Zoom = zoom;
            Chunks = chunks > 0 ? chunks : throw new ArgumentOutOfRangeException($"{nameof(chunks)} can't be negative", nameof(chunks));
            Image = image ?? throw new ArgumentNullException($"{nameof(image)} can't be null", nameof(image));
        }
    }
}
