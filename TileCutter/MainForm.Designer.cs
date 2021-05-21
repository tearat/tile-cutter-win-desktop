namespace TileCutter
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.selectLabel = new System.Windows.Forms.Label();
            this.selectStatusLabel = new System.Windows.Forms.Label();
            this.selectButton = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.tabSizeLabel = new System.Windows.Forms.Label();
            this.backgroundColorLabel = new System.Windows.Forms.Label();
            this.extensionLabel = new System.Windows.Forms.Label();
            this.backgroundColorValue = new System.Windows.Forms.TextBox();
            this.extensionValue = new System.Windows.Forms.ComboBox();
            this.cutButton = new System.Windows.Forms.Button();
            this.label0 = new System.Windows.Forms.Label();
            this.label_path_error = new System.Windows.Forms.Label();
            this.label_size_error = new System.Windows.Forms.Label();
            this.label_color_error = new System.Windows.Forms.Label();
            this.tabSizeValue = new System.Windows.Forms.NumericUpDown();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.logListBox = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.tabSizeValue)).BeginInit();
            this.SuspendLayout();
            // 
            // selectLabel
            // 
            this.selectLabel.AutoSize = true;
            this.selectLabel.Location = new System.Drawing.Point(12, 53);
            this.selectLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.selectLabel.Name = "selectLabel";
            this.selectLabel.Size = new System.Drawing.Size(93, 17);
            this.selectLabel.TabIndex = 0;
            this.selectLabel.Text = "Select image:";
            // 
            // selectStatusLabel
            // 
            this.selectStatusLabel.AutoSize = true;
            this.selectStatusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.selectStatusLabel.Location = new System.Drawing.Point(12, 80);
            this.selectStatusLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.selectStatusLabel.Name = "selectStatusLabel";
            this.selectStatusLabel.Size = new System.Drawing.Size(99, 17);
            this.selectStatusLabel.TabIndex = 0;
            this.selectStatusLabel.Text = "Not selected";
            // 
            // selectButton
            // 
            this.selectButton.BackColor = System.Drawing.SystemColors.Highlight;
            this.selectButton.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.selectButton.Location = new System.Drawing.Point(16, 105);
            this.selectButton.Margin = new System.Windows.Forms.Padding(4);
            this.selectButton.Name = "selectButton";
            this.selectButton.Size = new System.Drawing.Size(263, 41);
            this.selectButton.TabIndex = 1;
            this.selectButton.Text = "Select";
            this.selectButton.UseVisualStyleBackColor = false;
            this.selectButton.Click += new System.EventHandler(this.selectButton_Click);
            // 
            // tabSizeLabel
            // 
            this.tabSizeLabel.AutoSize = true;
            this.tabSizeLabel.Location = new System.Drawing.Point(12, 169);
            this.tabSizeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.tabSizeLabel.Name = "tabSizeLabel";
            this.tabSizeLabel.Size = new System.Drawing.Size(64, 17);
            this.tabSizeLabel.TabIndex = 3;
            this.tabSizeLabel.Text = "Tile size:";
            // 
            // backgroundColorLabel
            // 
            this.backgroundColorLabel.AutoSize = true;
            this.backgroundColorLabel.Location = new System.Drawing.Point(12, 197);
            this.backgroundColorLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.backgroundColorLabel.Name = "backgroundColorLabel";
            this.backgroundColorLabel.Size = new System.Drawing.Size(123, 17);
            this.backgroundColorLabel.TabIndex = 5;
            this.backgroundColorLabel.Text = "Background color:";
            // 
            // extensionLabel
            // 
            this.extensionLabel.AutoSize = true;
            this.extensionLabel.Location = new System.Drawing.Point(12, 224);
            this.extensionLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.extensionLabel.Name = "extensionLabel";
            this.extensionLabel.Size = new System.Drawing.Size(73, 17);
            this.extensionLabel.TabIndex = 6;
            this.extensionLabel.Text = "Extension:";
            this.extensionLabel.Visible = false;
            // 
            // backgroundColorValue
            // 
            this.backgroundColorValue.Location = new System.Drawing.Point(145, 193);
            this.backgroundColorValue.Margin = new System.Windows.Forms.Padding(4);
            this.backgroundColorValue.Name = "backgroundColorValue";
            this.backgroundColorValue.Size = new System.Drawing.Size(132, 22);
            this.backgroundColorValue.TabIndex = 3;
            this.backgroundColorValue.Text = "#111111";
            // 
            // extensionValue
            // 
            this.extensionValue.AllowDrop = true;
            this.extensionValue.FormattingEnabled = true;
            this.extensionValue.Items.AddRange(new object[] {
            ".png",
            ".jpg"});
            this.extensionValue.Location = new System.Drawing.Point(145, 220);
            this.extensionValue.Margin = new System.Windows.Forms.Padding(4);
            this.extensionValue.Name = "extensionValue";
            this.extensionValue.Size = new System.Drawing.Size(132, 24);
            this.extensionValue.TabIndex = 4;
            this.extensionValue.Tag = "";
            this.extensionValue.Text = ".png";
            this.extensionValue.Visible = false;
            // 
            // cutButton
            // 
            this.cutButton.BackColor = System.Drawing.SystemColors.Highlight;
            this.cutButton.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.cutButton.Location = new System.Drawing.Point(16, 265);
            this.cutButton.Margin = new System.Windows.Forms.Padding(4);
            this.cutButton.Name = "cutButton";
            this.cutButton.Size = new System.Drawing.Size(263, 41);
            this.cutButton.TabIndex = 5;
            this.cutButton.Text = "Cut!";
            this.cutButton.UseVisualStyleBackColor = false;
            this.cutButton.Click += new System.EventHandler(this.cutButton_Click);
            // 
            // label0
            // 
            this.label0.AutoSize = true;
            this.label0.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label0.Location = new System.Drawing.Point(319, 11);
            this.label0.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label0.Name = "label0";
            this.label0.Size = new System.Drawing.Size(241, 31);
            this.label0.TabIndex = 0;
            this.label0.Text = "TileCutter Desktop";
            // 
            // label_path_error
            // 
            this.label_path_error.AutoSize = true;
            this.label_path_error.ForeColor = System.Drawing.Color.DarkRed;
            this.label_path_error.Location = new System.Drawing.Point(283, 80);
            this.label_path_error.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_path_error.Name = "label_path_error";
            this.label_path_error.Size = new System.Drawing.Size(0, 17);
            this.label_path_error.TabIndex = 7;
            // 
            // label_size_error
            // 
            this.label_size_error.AutoSize = true;
            this.label_size_error.ForeColor = System.Drawing.Color.DarkRed;
            this.label_size_error.Location = new System.Drawing.Point(287, 169);
            this.label_size_error.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_size_error.Name = "label_size_error";
            this.label_size_error.Size = new System.Drawing.Size(0, 17);
            this.label_size_error.TabIndex = 8;
            // 
            // label_color_error
            // 
            this.label_color_error.AutoSize = true;
            this.label_color_error.ForeColor = System.Drawing.Color.DarkRed;
            this.label_color_error.Location = new System.Drawing.Point(287, 197);
            this.label_color_error.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_color_error.Name = "label_color_error";
            this.label_color_error.Size = new System.Drawing.Size(0, 17);
            this.label_color_error.TabIndex = 9;
            // 
            // tabSizeValue
            // 
            this.tabSizeValue.Location = new System.Drawing.Point(145, 166);
            this.tabSizeValue.Margin = new System.Windows.Forms.Padding(4);
            this.tabSizeValue.Maximum = new decimal(new int[] {
            4096,
            0,
            0,
            0});
            this.tabSizeValue.Name = "tabSizeValue";
            this.tabSizeValue.Size = new System.Drawing.Size(133, 22);
            this.tabSizeValue.TabIndex = 10;
            this.tabSizeValue.Value = new decimal(new int[] {
            256,
            0,
            0,
            0});
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(16, 321);
            this.progressBar.Margin = new System.Windows.Forms.Padding(4);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(263, 28);
            this.progressBar.Step = 1;
            this.progressBar.TabIndex = 11;
            // 
            // logListBox
            // 
            this.logListBox.FormattingEnabled = true;
            this.logListBox.ItemHeight = 16;
            this.logListBox.Location = new System.Drawing.Point(484, 105);
            this.logListBox.Margin = new System.Windows.Forms.Padding(4);
            this.logListBox.Name = "logListBox";
            this.logListBox.Size = new System.Drawing.Size(432, 244);
            this.logListBox.TabIndex = 12;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(929, 379);
            this.Controls.Add(this.logListBox);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.tabSizeValue);
            this.Controls.Add(this.label_color_error);
            this.Controls.Add(this.label_size_error);
            this.Controls.Add(this.label_path_error);
            this.Controls.Add(this.label0);
            this.Controls.Add(this.cutButton);
            this.Controls.Add(this.extensionValue);
            this.Controls.Add(this.backgroundColorValue);
            this.Controls.Add(this.extensionLabel);
            this.Controls.Add(this.backgroundColorLabel);
            this.Controls.Add(this.tabSizeLabel);
            this.Controls.Add(this.selectButton);
            this.Controls.Add(this.selectStatusLabel);
            this.Controls.Add(this.selectLabel);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.Text = "TileCutter Desktop";
            ((System.ComponentModel.ISupportInitialize)(this.tabSizeValue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label selectLabel;
        private System.Windows.Forms.Label selectStatusLabel;
        private System.Windows.Forms.Button selectButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Label tabSizeLabel;
        private System.Windows.Forms.Label backgroundColorLabel;
        private System.Windows.Forms.Label extensionLabel;
        private System.Windows.Forms.TextBox backgroundColorValue;
        private System.Windows.Forms.ComboBox extensionValue;
        private System.Windows.Forms.Button cutButton;
        private System.Windows.Forms.Label label0;
        private System.Windows.Forms.Label label_path_error;
        private System.Windows.Forms.Label label_size_error;
        private System.Windows.Forms.Label label_color_error;
        private System.Windows.Forms.NumericUpDown tabSizeValue;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.ListBox logListBox;
    }
}

