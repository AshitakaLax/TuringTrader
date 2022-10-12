namespace TuringTraderWin
{
  partial class MainWindow
  {
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.OptimizationGridView = new System.Windows.Forms.DataGridView();
      this.ParameterNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.ParameterValueColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.EnableColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
      this.StartColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.StopColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.IncrementColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.ParametersLabel = new System.Windows.Forms.Label();
      this.richTextBox1 = new System.Windows.Forms.RichTextBox();
      this.RunButton = new System.Windows.Forms.Button();
      this.menuStrip1 = new System.Windows.Forms.MenuStrip();
      this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.ReportButton = new System.Windows.Forms.Button();
      this.OptimizeButton = new System.Windows.Forms.Button();
      this.OptimizeResultsButton = new System.Windows.Forms.Button();
      this.AlgorithmComboBox = new System.Windows.Forms.ComboBox();
      this.AlgorithmLabel = new System.Windows.Forms.Label();
      this.DescriptionLabel = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.OptimizationGridView)).BeginInit();
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
      this.splitContainer1.Panel1.Controls.Add(this.OptimizationGridView);
      this.splitContainer1.Panel1.Controls.Add(this.ParametersLabel);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.richTextBox1);
      this.splitContainer1.Size = new System.Drawing.Size(1736, 816);
      this.splitContainer1.SplitterDistance = 429;
      this.splitContainer1.TabIndex = 0;
      // 
      // OptimizationGridView
      // 
      this.OptimizationGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.OptimizationGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.OptimizationGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ParameterNameColumn,
            this.ParameterValueColumn,
            this.EnableColumn,
            this.StartColumn,
            this.StopColumn,
            this.IncrementColumn});
      this.OptimizationGridView.Location = new System.Drawing.Point(0, 44);
      this.OptimizationGridView.Name = "OptimizationGridView";
      this.OptimizationGridView.RowHeadersVisible = false;
      this.OptimizationGridView.RowHeadersWidth = 102;
      this.OptimizationGridView.RowTemplate.Height = 49;
      this.OptimizationGridView.Size = new System.Drawing.Size(1736, 385);
      this.OptimizationGridView.TabIndex = 1;
      // 
      // ParameterNameColumn
      // 
      this.ParameterNameColumn.HeaderText = "Name";
      this.ParameterNameColumn.MinimumWidth = 12;
      this.ParameterNameColumn.Name = "ParameterNameColumn";
      this.ParameterNameColumn.Width = 250;
      // 
      // ParameterValueColumn
      // 
      this.ParameterValueColumn.HeaderText = "Value";
      this.ParameterValueColumn.MinimumWidth = 12;
      this.ParameterValueColumn.Name = "ParameterValueColumn";
      this.ParameterValueColumn.Width = 250;
      // 
      // EnableColumn
      // 
      this.EnableColumn.HeaderText = "Optimize";
      this.EnableColumn.MinimumWidth = 12;
      this.EnableColumn.Name = "EnableColumn";
      this.EnableColumn.Width = 250;
      // 
      // StartColumn
      // 
      this.StartColumn.HeaderText = "Starting Value";
      this.StartColumn.MinimumWidth = 12;
      this.StartColumn.Name = "StartColumn";
      this.StartColumn.Width = 250;
      // 
      // StopColumn
      // 
      this.StopColumn.HeaderText = "Ending Value";
      this.StopColumn.MinimumWidth = 12;
      this.StopColumn.Name = "StopColumn";
      this.StopColumn.Width = 250;
      // 
      // IncrementColumn
      // 
      this.IncrementColumn.HeaderText = "Increment By";
      this.IncrementColumn.MinimumWidth = 12;
      this.IncrementColumn.Name = "IncrementColumn";
      this.IncrementColumn.Width = 250;
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
      // richTextBox1
      // 
      this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.richTextBox1.Location = new System.Drawing.Point(0, 0);
      this.richTextBox1.Name = "richTextBox1";
      this.richTextBox1.Size = new System.Drawing.Size(1736, 383);
      this.richTextBox1.TabIndex = 0;
      this.richTextBox1.Text = "";
      // 
      // RunButton
      // 
      this.RunButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.RunButton.Location = new System.Drawing.Point(937, 46);
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
      this.ReportButton.Location = new System.Drawing.Point(1131, 46);
      this.ReportButton.Name = "ReportButton";
      this.ReportButton.Size = new System.Drawing.Size(188, 58);
      this.ReportButton.TabIndex = 3;
      this.ReportButton.Text = "Report";
      this.ReportButton.UseVisualStyleBackColor = true;
      // 
      // OptimizeButton
      // 
      this.OptimizeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.OptimizeButton.Location = new System.Drawing.Point(1325, 46);
      this.OptimizeButton.Name = "OptimizeButton";
      this.OptimizeButton.Size = new System.Drawing.Size(188, 58);
      this.OptimizeButton.TabIndex = 4;
      this.OptimizeButton.Text = "Optimize";
      this.OptimizeButton.UseVisualStyleBackColor = true;
      // 
      // OptimizeResultsButton
      // 
      this.OptimizeResultsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.OptimizeResultsButton.Location = new System.Drawing.Point(1519, 46);
      this.OptimizeResultsButton.Name = "OptimizeResultsButton";
      this.OptimizeResultsButton.Size = new System.Drawing.Size(188, 58);
      this.OptimizeResultsButton.TabIndex = 5;
      this.OptimizeResultsButton.Text = "Opt. Results";
      this.OptimizeResultsButton.UseVisualStyleBackColor = true;
      // 
      // AlgorithmComboBox
      // 
      this.AlgorithmComboBox.FormattingEnabled = true;
      this.AlgorithmComboBox.Location = new System.Drawing.Point(168, 52);
      this.AlgorithmComboBox.Name = "AlgorithmComboBox";
      this.AlgorithmComboBox.Size = new System.Drawing.Size(763, 49);
      this.AlgorithmComboBox.TabIndex = 6;
      this.AlgorithmComboBox.SelectedIndexChanged += new System.EventHandler(this.AlgorithmComboBox_SelectedIndexChanged);
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
      // MainWindow
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(17F, 41F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1736, 1022);
      this.Controls.Add(this.DescriptionLabel);
      this.Controls.Add(this.AlgorithmLabel);
      this.Controls.Add(this.AlgorithmComboBox);
      this.Controls.Add(this.OptimizeResultsButton);
      this.Controls.Add(this.OptimizeButton);
      this.Controls.Add(this.ReportButton);
      this.Controls.Add(this.RunButton);
      this.Controls.Add(this.splitContainer1);
      this.Controls.Add(this.menuStrip1);
      this.MainMenuStrip = this.menuStrip1;
      this.Name = "MainWindow";
      this.Text = "Form1";
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel1.PerformLayout();
      this.splitContainer1.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
      this.splitContainer1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.OptimizationGridView)).EndInit();
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    private SplitContainer splitContainer1;
    private Label ParametersLabel;
    private RichTextBox richTextBox1;
    private DataGridView OptimizationGridView;
    private Button RunButton;
    private MenuStrip menuStrip1;
    private ToolStripMenuItem fileToolStripMenuItem;
    private ToolStripMenuItem editToolStripMenuItem;
    private ToolStripMenuItem helpToolStripMenuItem;
    private Button ReportButton;
    private Button OptimizeButton;
    private Button OptimizeResultsButton;
    private ComboBox AlgorithmComboBox;
    private Label AlgorithmLabel;
    private Label DescriptionLabel;
    #endregion

    private DataGridViewTextBoxColumn ParameterNameColumn;
    private DataGridViewTextBoxColumn ParameterValueColumn;
        private DataGridViewCheckBoxColumn EnableColumn;
        private DataGridViewTextBoxColumn StartColumn;
        private DataGridViewTextBoxColumn StopColumn;
        private DataGridViewTextBoxColumn IncrementColumn;
    }
}