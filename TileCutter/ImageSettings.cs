using ImageMagick;
using System;

namespace TileCutter
{
    public class ImageSettings
    {
        public int Zoom { get; }
        public int Chunks { get; }
        public MagickImage Image { get; }

        public ImageSettings(int zoom, int chunks, MagickImage image)
        {
            Zoom = zoom;
            Chunks = chunks > 0 ? chunks : throw new ArgumentOutOfRangeException(nameof(chunks), $"{nameof(chunks)} can't be negative");
            Image = image ?? throw new ArgumentNullException(nameof(image), $"{nameof(image)} can't be null");
        }
    }
}
