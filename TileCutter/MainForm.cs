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
            var dialog = openFileDialog;

            using (dialog)
            {
                dialog.InitialDirectory = "c:\\";
                dialog.Filter = "Image Files|*.jpg;*.jpeg;*.png|All files (*.*)|*.*";
                dialog.FilterIndex = 1;
                dialog.RestoreDirectory = true;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    selectStatusLabel.Text = dialog.FileName;
                }
            }
        }

        private void cutButton_Click(object sender, EventArgs e)
        {
            var path = openFileDialog.FileName;
            var tile_size = tabSizeValue.Text.Length > 0 ? (int)tabSizeValue.Value : 0;
            var bg_color = backgroundColorValue.Text;
            var extension = extensionValue.Text;

            clearErrorLabels();
            var checkResult = checkParams(path, tile_size, bg_color);
            if( checkResult == CheckStatus.Success ) {
                cutTiles(path, tile_size, bg_color, extension);
            } else {
                showErrorMessage(checkResult);
            }
        }

        private void clearErrorLabels()
        {
            label_path_error.Text = "";
            label_size_error.Text = "";
            label_color_error.Text = "";
        }

        private void showErrorMessage(CheckStatus error)
        {
            if (error == CheckStatus.WrongPath) label_path_error.Text = "File path is wrong";
            if (error == CheckStatus.WrongSize) label_size_error.Text = "Tile size is wrong";
            if (error == CheckStatus.WrongColor) label_color_error.Text = "Color format is wrong";
        }

        private CheckStatus checkParams(string path, int tile_size, string bg_color)
        {
            if ( path.Length == 0 || !File.Exists(path) )
            {
                return CheckStatus.WrongPath;
            }
            if( tile_size == 0 )
            {
                return CheckStatus.WrongSize;
            }
            if( !Regex.Match(bg_color, @"^#[0-9a-fA-F]{6}$", RegexOptions.IgnoreCase).Success )
            {
                return CheckStatus.WrongColor;
            }
            return CheckStatus.Success;
        }

        private void cutTiles(string path, int tile_size, string bg_color, string extension)
        {
            // Bench starts
            Stopwatch sw = new Stopwatch();
            sw.Start();

            // Get image from the path
            var image = new MagickImage(path);

            var width = image.Width;
            var height = image.Height;
            logToList("Size: " + width + " : " + height);

            var columns = (int) Math.Ceiling((decimal)width / tile_size);
            var rows = (int) Math.Ceiling((decimal)height / tile_size);
            logToList("Columns: " + columns + ", rows: " + rows);

            // Calculate max zoom level
            int[] anArray = { columns, rows };
            var max_chunks_count = anArray.Max();
            logToList("Calculated chunks count: " + max_chunks_count);
            int max_exp = 1;
            while (max_exp < max_chunks_count)
            {
                max_exp *= 2;
            }
            logToList("Calculated zoom levels: " + max_exp);

            // Create a blank image and composite our image into
            var new_size = max_exp * tile_size;
            MagickColor fillColor = new MagickColor(bg_color);
            var resized_max = new MagickImage(fillColor, new_size, new_size);
            resized_max.Composite(image, Gravity.Center, CompositeOperator.Atop);
            logToList("Max image size: " + resized_max.Width + " : " + resized_max.Height);

            // Calculate chunks number for every zoom level
            var images = new List<ImageSettings>();
            var zoom = 0;
            var exp = 1;
            while (exp <= max_exp)
            {
                logToList("Resizing image to zoom " + zoom);
                var zoom_size = exp * tile_size;
                MagickImage new_image = (MagickImage)resized_max.Clone();
                new_image.Resize(zoom_size, zoom_size);
                images.Add(new ImageSettings(zoom, exp, new_image));
                zoom += 1;
                exp *= 2;
            }

            // Create a folder if not exist and empty old tiles
            string tilesPath = @"./tiles";
            if (!Directory.Exists(tilesPath)) Directory.CreateDirectory(tilesPath);
            System.IO.DirectoryInfo tilesDirectory = new System.IO.DirectoryInfo(tilesPath);
            foreach (System.IO.FileInfo file in tilesDirectory.GetFiles()) file.Delete();

            int x = 0;
            int y = 0;

            // Calculate progressbar blocks
            int progressBarblocksCount = 0;
            images.ForEach(delegate (ImageSettings img)
            {
                int chunks_count = img.Chunks;
                for (x = 0; x < chunks_count; x++)
                {
                    for (y = 0; y < chunks_count; y++)
                    {
                        progressBarblocksCount++;
                    }
                }
            });
            logToList("progressBar blocksCount: " + progressBarblocksCount);
            progressBar.Maximum = progressBarblocksCount;
            progressBar.Value = 0;

            // Cutting the tiles and save to folders
            int tiles_done = 0;
            images.ForEach(delegate (ImageSettings img)
            {
                int chunks_count = img.Chunks;
                logToList("Making tiles for zoom " + img.Zoom);
                x = 0;
                while(x < chunks_count)
                {
                    y = 0;
                    while(y < chunks_count)
                    {
                        makeTile(x, y, img, tile_size);
                        progressBar.Value++;
                        tiles_done++;
                        y++;
                    }
                    x++;
                }
            });
            logToList("Work done");
            sw.Stop();
            logToList("Time: " + sw.Elapsed.ToString());
        }

        private void makeTile(int x, int y, ImageSettings img, int tile_size)
        {
            int x_offset = x * tile_size;
            int y_offset = y * tile_size;
            int z = img.Zoom;

            MagickGeometry geometry = new MagickGeometry();
            geometry.Width = tile_size;
            geometry.Height = tile_size;
            geometry.X = x_offset;
            geometry.Y = y_offset;

            MagickImage chunk = (MagickImage)img.Image.Clone();
            chunk.Crop(geometry);
            chunk.Format = MagickFormat.Png;
            string zPath = "./tiles/" + z;
            if (!Directory.Exists(zPath)) Directory.CreateDirectory(zPath);
            string xPath = "./tiles/" + z + "/" + x;
            if (!Directory.Exists(xPath)) Directory.CreateDirectory(xPath);
            string fullPath = "./tiles/" + z + "/" + x + "/" + y + ".png";
            chunk.Write(fullPath);
        }

        private void logToList(string text)
        {
            logListBox.Items.Add(text);
        }
    }
}
