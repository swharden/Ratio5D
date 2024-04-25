namespace Ratio5D.Gui;

partial class Form2
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
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
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        nudB1 = new NumericUpDown();
        nudB2 = new NumericUpDown();
        groupBox1 = new GroupBox();
        tableLayoutPanel1 = new TableLayoutPanel();
        groupBox2 = new GroupBox();
        tableLayoutPanel2 = new TableLayoutPanel();
        nudM1 = new NumericUpDown();
        nudM2 = new NumericUpDown();
        formsPlot1 = new ScottPlot.WinForms.FormsPlot();
        formsPlot2 = new ScottPlot.WinForms.FormsPlot();
        formsPlot3 = new ScottPlot.WinForms.FormsPlot();
        multiRoiSelect1 = new SWHarden.RoiSelect.WinForms.MultiRoiSelect();
        groupBox3 = new GroupBox();
        lblFolder = new Label();
        ((System.ComponentModel.ISupportInitialize)nudB1).BeginInit();
        ((System.ComponentModel.ISupportInitialize)nudB2).BeginInit();
        groupBox1.SuspendLayout();
        tableLayoutPanel1.SuspendLayout();
        groupBox2.SuspendLayout();
        tableLayoutPanel2.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)nudM1).BeginInit();
        ((System.ComponentModel.ISupportInitialize)nudM2).BeginInit();
        groupBox3.SuspendLayout();
        SuspendLayout();
        // 
        // nudB1
        // 
        nudB1.Dock = DockStyle.Fill;
        nudB1.Location = new Point(3, 3);
        nudB1.Name = "nudB1";
        nudB1.Size = new Size(86, 31);
        nudB1.TabIndex = 0;
        nudB1.Value = new decimal(new int[] { 7, 0, 0, 0 });
        // 
        // nudB2
        // 
        nudB2.Dock = DockStyle.Fill;
        nudB2.Location = new Point(95, 3);
        nudB2.Name = "nudB2";
        nudB2.Size = new Size(86, 31);
        nudB2.TabIndex = 4;
        nudB2.Value = new decimal(new int[] { 12, 0, 0, 0 });
        // 
        // groupBox1
        // 
        groupBox1.Controls.Add(tableLayoutPanel1);
        groupBox1.Location = new Point(1397, 12);
        groupBox1.Name = "groupBox1";
        groupBox1.Size = new Size(190, 67);
        groupBox1.TabIndex = 6;
        groupBox1.TabStop = false;
        groupBox1.Text = "Baseline Frames";
        // 
        // tableLayoutPanel1
        // 
        tableLayoutPanel1.ColumnCount = 2;
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tableLayoutPanel1.Controls.Add(nudB1, 0, 0);
        tableLayoutPanel1.Controls.Add(nudB2, 1, 0);
        tableLayoutPanel1.Dock = DockStyle.Fill;
        tableLayoutPanel1.Location = new Point(3, 27);
        tableLayoutPanel1.Name = "tableLayoutPanel1";
        tableLayoutPanel1.RowCount = 1;
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tableLayoutPanel1.Size = new Size(184, 37);
        tableLayoutPanel1.TabIndex = 0;
        // 
        // groupBox2
        // 
        groupBox2.Controls.Add(tableLayoutPanel2);
        groupBox2.Location = new Point(1631, 12);
        groupBox2.Name = "groupBox2";
        groupBox2.Size = new Size(190, 67);
        groupBox2.TabIndex = 7;
        groupBox2.TabStop = false;
        groupBox2.Text = "Measure Frames";
        // 
        // tableLayoutPanel2
        // 
        tableLayoutPanel2.ColumnCount = 2;
        tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tableLayoutPanel2.Controls.Add(nudM1, 0, 0);
        tableLayoutPanel2.Controls.Add(nudM2, 1, 0);
        tableLayoutPanel2.Dock = DockStyle.Fill;
        tableLayoutPanel2.Location = new Point(3, 27);
        tableLayoutPanel2.Name = "tableLayoutPanel2";
        tableLayoutPanel2.RowCount = 1;
        tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tableLayoutPanel2.Size = new Size(184, 37);
        tableLayoutPanel2.TabIndex = 0;
        // 
        // nudM1
        // 
        nudM1.Dock = DockStyle.Fill;
        nudM1.Location = new Point(3, 3);
        nudM1.Name = "nudM1";
        nudM1.Size = new Size(86, 31);
        nudM1.TabIndex = 0;
        nudM1.Value = new decimal(new int[] { 15, 0, 0, 0 });
        // 
        // nudM2
        // 
        nudM2.Dock = DockStyle.Fill;
        nudM2.Location = new Point(95, 3);
        nudM2.Name = "nudM2";
        nudM2.Size = new Size(86, 31);
        nudM2.TabIndex = 4;
        nudM2.Value = new decimal(new int[] { 45, 0, 0, 0 });
        // 
        // formsPlot1
        // 
        formsPlot1.DisplayScale = 1.5F;
        formsPlot1.Location = new Point(1287, 82);
        formsPlot1.Name = "formsPlot1";
        formsPlot1.Size = new Size(546, 332);
        formsPlot1.TabIndex = 8;
        // 
        // formsPlot2
        // 
        formsPlot2.DisplayScale = 1.5F;
        formsPlot2.Location = new Point(1287, 420);
        formsPlot2.Name = "formsPlot2";
        formsPlot2.Size = new Size(546, 336);
        formsPlot2.TabIndex = 9;
        // 
        // formsPlot3
        // 
        formsPlot3.DisplayScale = 1.5F;
        formsPlot3.Location = new Point(1287, 762);
        formsPlot3.Name = "formsPlot3";
        formsPlot3.Size = new Size(546, 289);
        formsPlot3.TabIndex = 10;
        // 
        // multiRoiSelect1
        // 
        multiRoiSelect1.Location = new Point(12, 82);
        multiRoiSelect1.Name = "multiRoiSelect1";
        multiRoiSelect1.Size = new Size(1269, 969);
        multiRoiSelect1.TabIndex = 11;
        // 
        // groupBox3
        // 
        groupBox3.Controls.Add(lblFolder);
        groupBox3.Location = new Point(12, 12);
        groupBox3.Name = "groupBox3";
        groupBox3.Size = new Size(1269, 67);
        groupBox3.TabIndex = 13;
        groupBox3.TabStop = false;
        groupBox3.Text = "Folder";
        // 
        // lblFolder
        // 
        lblFolder.AutoSize = true;
        lblFolder.Location = new Point(15, 30);
        lblFolder.Name = "lblFolder";
        lblFolder.Size = new Size(59, 25);
        lblFolder.TabIndex = 0;
        lblFolder.Text = "label1";
        // 
        // Form2
        // 
        AutoScaleDimensions = new SizeF(10F, 25F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1856, 1069);
        Controls.Add(groupBox3);
        Controls.Add(multiRoiSelect1);
        Controls.Add(formsPlot3);
        Controls.Add(formsPlot2);
        Controls.Add(formsPlot1);
        Controls.Add(groupBox2);
        Controls.Add(groupBox1);
        Name = "Form2";
        Text = "Form2";
        ((System.ComponentModel.ISupportInitialize)nudB1).EndInit();
        ((System.ComponentModel.ISupportInitialize)nudB2).EndInit();
        groupBox1.ResumeLayout(false);
        tableLayoutPanel1.ResumeLayout(false);
        groupBox2.ResumeLayout(false);
        tableLayoutPanel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)nudM1).EndInit();
        ((System.ComponentModel.ISupportInitialize)nudM2).EndInit();
        groupBox3.ResumeLayout(false);
        groupBox3.PerformLayout();
        ResumeLayout(false);
    }

    #endregion

    private NumericUpDown nudB1;
    private NumericUpDown nudB2;
    private GroupBox groupBox1;
    private TableLayoutPanel tableLayoutPanel1;
    private GroupBox groupBox2;
    private TableLayoutPanel tableLayoutPanel2;
    private NumericUpDown nudM1;
    private NumericUpDown nudM2;
    private ScottPlot.WinForms.FormsPlot formsPlot1;
    private ScottPlot.WinForms.FormsPlot formsPlot2;
    private ScottPlot.WinForms.FormsPlot formsPlot3;
    private SWHarden.RoiSelect.WinForms.MultiRoiSelect multiRoiSelect1;
    private GroupBox groupBox3;
    private Label lblFolder;
}