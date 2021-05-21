using ImageMagick;

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
            Chunks = chunks;
            Image = image;
        }
    }
}
