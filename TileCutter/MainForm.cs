using ImageMagick;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
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

            var erroneousInput = false;
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
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var image = new MagickImage(path);
            var maxExp = CalculateMaxExp(image.Width, image.Height, tileSize);
            var maxSize = maxExp * tileSize;
            var resizedMax = GetFullImageWithBackground(image, backgroundColor, maxSize);
            image.Dispose();

            // Calculate chunks number for every zoom level
            var images = GetImagesForChunks(resizedMax, maxExp, tileSize);
            resizedMax.Dispose();

            var progressBarMaximum = images.Sum(i => i.Chunks * i.Chunks);
            
            Log($"progressBar blocksCount: {progressBarMaximum}");
            progressBar.Maximum = progressBarMaximum;
            progressBar.Value = 0;

            // Cutting the tiles and save to folders
            var tilesDone = 0;
            
            TileTools.SetupRootPathForNewTiles();
            images.ForEach(imageSettings =>
            {
                Log($"Making tiles for zoom {imageSettings.Zoom}");
                var x = 0;
                while (x < imageSettings.Chunks)
                {
                    var y = 0;
                    while (y < imageSettings.Chunks)
                    {
                        using (var chunk = TileTools.MakeTile(imageSettings.Image, new Point(x, y), tileSize))
                        {
                            var fullPath = TileTools.CreateDirectoryStructure(new Point(x, y), imageSettings.Zoom);
                            chunk.Write(fullPath);
                        }
                        progressBar.Value++;
                        tilesDone++;
                        y++;
                    }
                    x++;
                }
            });
            Log("Work done");
            stopwatch.Stop();
            Log($"Time: {stopwatch.Elapsed}");
        }

        private MagickImage GetFullImageWithBackground(MagickImage image, string backgroundColor, int size)
        {
            var fillColor = new MagickColor(backgroundColor);
            var resizedMax = new MagickImage(fillColor, size, size);
            resizedMax.Composite(image, Gravity.Center, CompositeOperator.Atop);

            Log($"Max image size: {resizedMax.Width} : {resizedMax.Height}");

            return resizedMax;
        }

        private List<ImageSettings> GetImagesForChunks(MagickImage image, int maxExp, int tileSize)
        {
            var images = new List<ImageSettings>();
            var zoom = 0;
            var exp = 1;
            while (exp <= maxExp)
            {
                Log($"Resizing image to zoom {zoom}");
                var zoomSize = exp * tileSize;
                var newImage = (MagickImage)image.Clone();
                newImage.Resize(zoomSize, zoomSize);
                images.Add(new ImageSettings(zoom, exp, newImage));
                zoom++;
                exp *= 2;
            }

            return images;
        }

        private int CalculateMaxExp(int width, int height, int tileSize)
        {
            Log($"Size: {width} : {height}");
            
            var columns = (int)Math.Ceiling((decimal)width / tileSize);
            var rows = (int)Math.Ceiling((decimal)height / tileSize);
            
            Log($"Columns: {columns}, rows: {rows}");
            
            var maxChunksCount = Math.Max(columns, rows);
            
            Log($"Calculated chunks count: {maxChunksCount}");
            
            var maxExp = 1;
            while (maxExp < maxChunksCount)
                maxExp *= 2;
            
            Log($"Calculated zoom levels: {maxExp}");

            return maxExp;
        }

        private void Log(string text)
        {
            logListBox.Items.Add(text);
        }
    }
}
