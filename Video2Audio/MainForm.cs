using CrispyTool.Video2Audio.src;

namespace CrispyTool.Video2Audio
{
    /// <summary>
    /// メインフォームクラス
    /// author: CrispyCrack
    /// date: 2024/10/19
    /// </summary>
    public class MainForm : Form
    {
        private readonly Label label;
        private readonly TextBox outputPathTextBox;
        private readonly Button extractButton;
        private readonly Button browseButton;
        private readonly Label fileNameLabel;
        private readonly Panel mainPanel;
        private readonly TableLayoutPanel tableLayoutPanel;

        public MainForm()
        {
            Text = "CrispyAudioExtractor";
            Width = 400;
            Height = 250;

            mainPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(10)
            };

            label = new Label
            {
                Text = "Drag and drop an MP4 file here",
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleCenter,
                Height = 50
            };

            fileNameLabel = new Label
            {
                Text = "No file selected",
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleCenter,
                Height = 30
            };

            outputPathTextBox = new TextBox
            {
                PlaceholderText = "Enter output file path here",
                Dock = DockStyle.Fill,
                Margin = new Padding(10)
            };

            browseButton = new Button
            {
                Text = "Browse...",
                Dock = DockStyle.Fill,
                Height = 20,
                Width = 75
            };
            browseButton.Click += BrowseButton_Click;

            tableLayoutPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                ColumnCount = 2,
                RowCount = 1,
                Height = 50,
                Margin = new Padding(0, 0, 0, 20)
            };
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tableLayoutPanel.Controls.Add(outputPathTextBox, 0, 0);
            tableLayoutPanel.Controls.Add(browseButton, 1, 0);

            extractButton = new Button
            {
                Text = "Extract Audio",
                Dock = DockStyle.Top,
                Height = 30
            };
            extractButton.Click += ExtractButton_Click;

            mainPanel.Controls.Add(extractButton);
            mainPanel.Controls.Add(tableLayoutPanel);
            mainPanel.Controls.Add(fileNameLabel);
            mainPanel.Controls.Add(label);

            Controls.Add(mainPanel);
            AllowDrop = true;
            DragEnter += new DragEventHandler(Form_DragEnter);
            DragDrop += new DragEventHandler(Form_DragDrop);
        }

        private void Form_DragEnter(object? sender, DragEventArgs e)
        {
            if (e.Data != null && e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void Form_DragDrop(object? sender, DragEventArgs e)
        {
            if (e.Data != null)
            {
                if (e.Data.GetData(DataFormats.FileDrop) is string[] files && files.Length > 0)
                {
                    var inputFilePath = files[0];
                    if (Path.GetExtension(inputFilePath).Equals(".mp4", StringComparison.OrdinalIgnoreCase) && FileUtils.IsMp4File(inputFilePath))
                    {
                        outputPathTextBox.Tag = inputFilePath;
                        fileNameLabel.Text = $"Selected file: {Path.GetFileName(inputFilePath)}";
                    }
                    else
                    {
                        MessageBox.Show("Please drag and drop a valid MP4 file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void BrowseButton_Click(object? sender, EventArgs e)
        {
            using var folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                outputPathTextBox.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void ExtractButton_Click(object? sender, EventArgs e)
        {
            var inputFilePath = outputPathTextBox.Tag as string;
            var outputDirectory = outputPathTextBox.Text;

            if (string.IsNullOrEmpty(inputFilePath))
            {
                MessageBox.Show("Please drag and drop an MP4 file first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(outputDirectory))
            {
                MessageBox.Show("Please select a valid output directory.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var outputFileName = Path.GetFileNameWithoutExtension(inputFilePath) + ".mp3";
            var outputFilePath = Path.Combine(outputDirectory, outputFileName);
            AudioExtractor.ExtractAudio(inputFilePath, outputFilePath);
        }
    }
}
