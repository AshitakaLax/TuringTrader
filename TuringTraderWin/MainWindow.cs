namespace TuringTraderWin
{
  public partial class Form1 : Form
  {
    public Form1()
    {
      InitializeComponent();
    }

        private SplitContainer splitContainer1;

        private void InitializeComponent()
        {
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.richTextBox1 = new System.Windows.Forms.RichTextBox();
      this.ParametersLabel = new System.Windows.Forms.Label();
      this.dataGridView1 = new System.Windows.Forms.DataGridView();
      this.RunButton = new System.Windows.Forms.Button();
      this.menuStrip1 = new System.Windows.Forms.MenuStrip();
      this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.ReportButton = new System.Windows.Forms.Button();
      this.OptimizeButton = new System.Windows.Forms.Button();
      this.OptimizeResultsButton = new System.Windows.Forms.Button();
      this.comboBox1 = new System.Windows.Forms.ComboBox();
      this.AlgorithmLabel = new System.Windows.Forms.Label();
      this.DescriptionLabel = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
      this.menuStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // splitContainer1
      // 
      this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.splitContainer1.Location = new System.Drawing.Point(0, 206);
      this.splitContainer1.Name = "splitContainer1";
      this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.dataGridView1);
      this.splitContainer1.Panel1.Controls.Add(this.ParametersLabel);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.richTextBox1);
      this.splitContainer1.Size = new System.Drawing.Size(1736, 816);
      this.splitContainer1.SplitterDistance = 429;
      this.splitContainer1.TabIndex = 0;
      // 
      // richTextBox1
      // 
      this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.richTextBox1.Location = new System.Drawing.Point(0, 0);
      this.richTextBox1.Name = "richTextBox1";
      this.richTextBox1.Size = new System.Drawing.Size(1736, 383);
      this.richTextBox1.TabIndex = 0;
      this.richTextBox1.Text = "";
      // 
      // ParametersLabel
      // 
      this.ParametersLabel.AutoSize = true;
      this.ParametersLabel.Location = new System.Drawing.Point(12, 0);
      this.ParametersLabel.Name = "ParametersLabel";
      this.ParametersLabel.Size = new System.Drawing.Size(172, 41);
      this.ParametersLabel.TabIndex = 0;
      this.ParametersLabel.Text = "Parameters:";
      // 
      // dataGridView1
      // 
      this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dataGridView1.Location = new System.Drawing.Point(0, 44);
      this.dataGridView1.Name = "dataGridView1";
      this.dataGridView1.RowHeadersWidth = 102;
      this.dataGridView1.RowTemplate.Height = 49;
      this.dataGridView1.Size = new System.Drawing.Size(1736, 385);
      this.dataGridView1.TabIndex = 1;
      // 
      // RunButton
      // 
      this.RunButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.RunButton.Location = new System.Drawing.Point(937, 63);
      this.RunButton.Name = "RunButton";
      this.RunButton.Size = new System.Drawing.Size(188, 58);
      this.RunButton.TabIndex = 1;
      this.RunButton.Text = "Run";
      this.RunButton.UseVisualStyleBackColor = true;
      // 
      // menuStrip1
      // 
      this.menuStrip1.ImageScalingSize = new System.Drawing.Size(40, 40);
      this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.helpToolStripMenuItem});
      this.menuStrip1.Location = new System.Drawing.Point(0, 0);
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.Size = new System.Drawing.Size(1736, 49);
      this.menuStrip1.TabIndex = 2;
      this.menuStrip1.Text = "menuStrip1";
      // 
      // fileToolStripMenuItem
      // 
      this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
      this.fileToolStripMenuItem.Size = new System.Drawing.Size(87, 45);
      this.fileToolStripMenuItem.Text = "File";
      // 
      // editToolStripMenuItem
      // 
      this.editToolStripMenuItem.Name = "editToolStripMenuItem";
      this.editToolStripMenuItem.Size = new System.Drawing.Size(92, 45);
      this.editToolStripMenuItem.Text = "Edit";
      // 
      // helpToolStripMenuItem
      // 
      this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
      this.helpToolStripMenuItem.Size = new System.Drawing.Size(104, 45);
      this.helpToolStripMenuItem.Text = "Help";
      // 
      // ReportButton
      // 
      this.ReportButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.ReportButton.Location = new System.Drawing.Point(1131, 63);
      this.ReportButton.Name = "ReportButton";
      this.ReportButton.Size = new System.Drawing.Size(188, 58);
      this.ReportButton.TabIndex = 3;
      this.ReportButton.Text = "Report";
      this.ReportButton.UseVisualStyleBackColor = true;
      // 
      // OptimizeButton
      // 
      this.OptimizeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.OptimizeButton.Location = new System.Drawing.Point(1325, 63);
      this.OptimizeButton.Name = "OptimizeButton";
      this.OptimizeButton.Size = new System.Drawing.Size(188, 58);
      this.OptimizeButton.TabIndex = 4;
      this.OptimizeButton.Text = "Optimize";
      this.OptimizeButton.UseVisualStyleBackColor = true;
      // 
      // OptimizeResultsButton
      // 
      this.OptimizeResultsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.OptimizeResultsButton.Location = new System.Drawing.Point(1519, 63);
      this.OptimizeResultsButton.Name = "OptimizeResultsButton";
      this.OptimizeResultsButton.Size = new System.Drawing.Size(188, 58);
      this.OptimizeResultsButton.TabIndex = 5;
      this.OptimizeResultsButton.Text = "Opt. Results";
      this.OptimizeResultsButton.UseVisualStyleBackColor = true;
      // 
      // comboBox1
      // 
      this.comboBox1.FormattingEnabled = true;
      this.comboBox1.Location = new System.Drawing.Point(168, 52);
      this.comboBox1.Name = "comboBox1";
      this.comboBox1.Size = new System.Drawing.Size(649, 49);
      this.comboBox1.TabIndex = 6;
      // 
      // AlgorithmLabel
      // 
      this.AlgorithmLabel.AutoSize = true;
      this.AlgorithmLabel.Location = new System.Drawing.Point(12, 49);
      this.AlgorithmLabel.Name = "AlgorithmLabel";
      this.AlgorithmLabel.Size = new System.Drawing.Size(150, 41);
      this.AlgorithmLabel.TabIndex = 7;
      this.AlgorithmLabel.Text = "Algorithm";
      // 
      // DescriptionLabel
      // 
      this.DescriptionLabel.AutoSize = true;
      this.DescriptionLabel.Location = new System.Drawing.Point(12, 120);
      this.DescriptionLabel.Name = "DescriptionLabel";
      this.DescriptionLabel.Size = new System.Drawing.Size(176, 41);
      this.DescriptionLabel.TabIndex = 8;
      this.DescriptionLabel.Text = "Description:";
      // 
      // Form1
      // 
      this.ClientSize = new System.Drawing.Size(1736, 1022);
      this.Controls.Add(this.DescriptionLabel);
      this.Controls.Add(this.AlgorithmLabel);
      this.Controls.Add(this.comboBox1);
      this.Controls.Add(this.OptimizeResultsButton);
      this.Controls.Add(this.OptimizeButton);
      this.Controls.Add(this.ReportButton);
      this.Controls.Add(this.RunButton);
      this.Controls.Add(this.splitContainer1);
      this.Controls.Add(this.menuStrip1);
      this.MainMenuStrip = this.menuStrip1;
      this.Name = "Form1";
      this.Load += new System.EventHandler(this.Form1_Load);
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel1.PerformLayout();
      this.splitContainer1.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
      this.splitContainer1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

        }

        private Label ParametersLabel;
        private RichTextBox richTextBox1;
        private DataGridView dataGridView1;
        private Button RunButton;

        private void Form1_Load(object sender, EventArgs e)
        {

        }

    private MenuStrip menuStrip1;
    private ToolStripMenuItem fileToolStripMenuItem;
    private ToolStripMenuItem editToolStripMenuItem;
    private ToolStripMenuItem helpToolStripMenuItem;
    private Button ReportButton;
    private Button OptimizeButton;
    private Button OptimizeResultsButton;
    private ComboBox comboBox1;
    private Label AlgorithmLabel;
    private Label DescriptionLabel;
  }
}