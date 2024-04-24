namespace Ratio5D.Gui;

partial class Form1
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
        pictureBox1 = new PictureBox();
        hsbSweep = new HScrollBar();
        hsbFrame = new HScrollBar();
        multiRoiSelect1 = new SWHarden.RoiSelect.WinForms.MultiRoiSelect();
        progressBar1 = new ProgressBar();
        gbSweep = new GroupBox();
        gbFrame = new GroupBox();
        formsPlot1 = new ScottPlot.WinForms.FormsPlot();
        formsPlot2 = new ScottPlot.WinForms.FormsPlot();
        ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
        gbSweep.SuspendLayout();
        gbFrame.SuspendLayout();
        SuspendLayout();
        // 
        // pictureBox1
        // 
        pictureBox1.BackColor = SystemColors.ControlDark;
        pictureBox1.Location = new Point(29, 63);
        pictureBox1.Name = "pictureBox1";
        pictureBox1.Size = new Size(512, 512);
        pictureBox1.TabIndex = 0;
        pictureBox1.TabStop = false;
        // 
        // hsbSweep
        // 
        hsbSweep.Dock = DockStyle.Fill;
        hsbSweep.Location = new Point(3, 27);
        hsbSweep.Name = "hsbSweep";
        hsbSweep.Size = new Size(506, 39);
        hsbSweep.TabIndex = 1;
        // 
        // hsbFrame
        // 
        hsbFrame.Dock = DockStyle.Fill;
        hsbFrame.Location = new Point(3, 27);
        hsbFrame.Name = "hsbFrame";
        hsbFrame.Size = new Size(506, 39);
        hsbFrame.TabIndex = 3;
        // 
        // multiRoiSelect1
        // 
        multiRoiSelect1.Location = new Point(547, 12);
        multiRoiSelect1.Name = "multiRoiSelect1";
        multiRoiSelect1.Size = new Size(1017, 717);
        multiRoiSelect1.TabIndex = 5;
        // 
        // progressBar1
        // 
        progressBar1.Location = new Point(29, 12);
        progressBar1.Name = "progressBar1";
        progressBar1.Size = new Size(512, 34);
        progressBar1.TabIndex = 6;
        // 
        // gbSweep
        // 
        gbSweep.Controls.Add(hsbSweep);
        gbSweep.Location = new Point(29, 581);
        gbSweep.Name = "gbSweep";
        gbSweep.Size = new Size(512, 69);
        gbSweep.TabIndex = 7;
        gbSweep.TabStop = false;
        gbSweep.Text = "groupBox1";
        // 
        // gbFrame
        // 
        gbFrame.Controls.Add(hsbFrame);
        gbFrame.Location = new Point(29, 660);
        gbFrame.Name = "gbFrame";
        gbFrame.Size = new Size(512, 69);
        gbFrame.TabIndex = 8;
        gbFrame.TabStop = false;
        gbFrame.Text = "groupBox1";
        // 
        // formsPlot1
        // 
        formsPlot1.DisplayScale = 1.5F;
        formsPlot1.Location = new Point(1570, 12);
        formsPlot1.Name = "formsPlot1";
        formsPlot1.Size = new Size(628, 310);
        formsPlot1.TabIndex = 9;
        // 
        // formsPlot2
        // 
        formsPlot2.DisplayScale = 1.5F;
        formsPlot2.Location = new Point(1570, 337);
        formsPlot2.Name = "formsPlot2";
        formsPlot2.Size = new Size(628, 310);
        formsPlot2.TabIndex = 10;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(10F, 25F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(2210, 747);
        Controls.Add(formsPlot2);
        Controls.Add(formsPlot1);
        Controls.Add(gbFrame);
        Controls.Add(gbSweep);
        Controls.Add(progressBar1);
        Controls.Add(multiRoiSelect1);
        Controls.Add(pictureBox1);
        Name = "Form1";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Form1";
        ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
        gbSweep.ResumeLayout(false);
        gbFrame.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion

    private PictureBox pictureBox1;
    private HScrollBar hsbSweep;
    private HScrollBar hsbFrame;
    private SWHarden.RoiSelect.WinForms.MultiRoiSelect multiRoiSelect1;
    private ProgressBar progressBar1;
    private GroupBox gbSweep;
    private GroupBox gbFrame;
    private ScottPlot.WinForms.FormsPlot formsPlot1;
    private ScottPlot.WinForms.FormsPlot formsPlot2;
}
