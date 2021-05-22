using ImageMagick;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace TileCutter
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void selectButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
                selectStatusLabel.Text = openFileDialog.FileName;
        }

        private void cutButton_Click(object sender, EventArgs e)
        {
            var path = openFileDialog.FileName;
            var tileSize = tileSizeValue.Text;
            var backgrouncColor = backgroundColorValue.Text;

            bool erroneousInput = false;
            selectErrorProvider.SetError(selectButton, string.Empty);
            tileSizeErrorProvider.SetError(tileSizeValue, string.Empty);
            backgroundColorErrorProvider.SetError(backgroundColorValue, string.Empty);

            if (path.Length == 0 || !File.Exists(path))
            {
                selectErrorProvider.SetError(selectButton, "Image is not selected.");
                erroneousInput = true;
            }

            if (tileSize == string.Empty)
            {
                tileSizeErrorProvider.SetError(tileSizeValue, "Tile size is missing.");
                erroneousInput = true;
            }

            if (!Regex.Match(backgrouncColor, "^#[0-9a-fA-F]{6}$", RegexOptions.IgnoreCase).Success)
            {
                backgroundColorErrorProvider.SetError(backgroundColorValue, "Background color must be specified in hex format.");
                erroneousInput = true;
            }

            if (erroneousInput)
                return;

            CutTiles(path, int.Parse(tileSize), backgrouncColor);
        }

        private void CutTiles(string path, int tileSize, string backgroundColor)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var image = new MagickImage(path);

            var width = image.Width;
            var height = image.Height;
            Log($"Size: {width} : {height}");

            var columns = (int)Math.Ceiling((decimal)width / tileSize);
            var rows = (int)Math.Ceiling((decimal)height / tileSize);
            Log($"Columns: {columns}, rows: {rows}");

            // Calculate max zoom level
            int[] anArray = { columns, rows };
            var maxChunksCount = anArray.Max();
            Log($"Calculated chunks count: {maxChunksCount}");
            int maxExp = 1;
            while (maxExp < maxChunksCount)
                maxExp *= 2;
            Log($"Calculated zoom levels: {maxExp}");

            // Create a blank image and composite our image into
            var newSize = maxExp * tileSize;
            MagickColor fillColor = new MagickColor(backgroundColor);
            var resizedMax = new MagickImage(fillColor, newSize, newSize);
            resizedMax.Composite(image, Gravity.Center, CompositeOperator.Atop);
            Log($"Max image size: {resizedMax.Width} : {resizedMax.Height}");

            // Calculate chunks number for every zoom level
            var images = new List<ImageSettings>();
            var zoom = 0;
            var exp = 1;
            while (exp <= maxExp)
            {
                Log($"Resizing image to zoom {zoom}");
                var zoomSize = exp * tileSize;
                MagickImage newImage = (MagickImage)resizedMax.Clone();
                newImage.Resize(zoomSize, zoomSize);
                images.Add(new ImageSettings(zoom, exp, newImage));
                zoom++;
                exp *= 2;
            }

            // Create a folder if not exist and empty old tiles
            string tilesPath = "./tiles";
            if (!Directory.Exists(tilesPath))
                Directory.CreateDirectory(tilesPath);
            DirectoryInfo tilesDirectory = new DirectoryInfo(tilesPath);
            foreach (FileInfo file in tilesDirectory.GetFiles()) file.Delete();

            int x = 0;
            int y = 0;

            // Calculate progressbar blocks
            int progressBarblocksCount = 0;
            images.ForEach((ImageSettings img) =>
            {
                int chunks_count = img.Chunks;
                for (x = 0; x < chunks_count; x++)
                    for (y = 0; y < chunks_count; y++)
                        progressBarblocksCount++;
            });
            Log($"progressBar blocksCount: {progressBarblocksCount}");
            progressBar.Maximum = progressBarblocksCount;
            progressBar.Value = 0;

            // Cutting the tiles and save to folders
            int tiles_done = 0;
            images.ForEach((ImageSettings img) =>
            {
                int chunks_count = img.Chunks;
                Log($"Making tiles for zoom {img.Zoom}");
                x = 0;
                while (x < chunks_count)
                {
                    y = 0;
                    while (y < chunks_count)
                    {
                        var chunk = MakeTile(img.Image, x, y, tileSize);
                        WriteTile(chunk, x, y, img.Zoom);
                        progressBar.Value++;
                        tiles_done++;
                        y++;
                    }
                    x++;
                }
            });
            Log("Work done");
            stopwatch.Stop();
            Log($"Time: {stopwatch.Elapsed}");
        }

        private MagickImage MakeTile(MagickImage image, int x, int y, int tileSize)
        {
            MagickGeometry geometry = new MagickGeometry
            {
                Width = tileSize,
                Height = tileSize,
                X = x * tileSize,
                Y = y * tileSize
            };

            MagickImage chunk = (MagickImage)image.Clone();
            chunk.Crop(geometry);
            chunk.Format = MagickFormat.Png;
            return chunk;
        }

        private void WriteTile(MagickImage chunk, int x, int y, int zoom)
        {
            string zPath = "./tiles/" + zoom;
            if (!Directory.Exists(zPath))
                Directory.CreateDirectory(zPath);

            string xPath = $"./tiles/{zoom}/{x}";
            if (!Directory.Exists(xPath))
                Directory.CreateDirectory(xPath);
            
            string fullPath = $"./tiles/{zoom}/{x}/{y}.png";
            chunk.Write(fullPath);
        }

        private void Log(string text)
        {
            logListBox.Items.Add(text);
        }
    }
}
