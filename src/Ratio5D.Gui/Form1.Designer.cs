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
        lblSweep = new Label();
        hsbFrame = new HScrollBar();
        lblFrame = new Label();
        multiRoiSelect1 = new SWHarden.RoiSelect.WinForms.MultiRoiSelect();
        ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
        SuspendLayout();
        // 
        // pictureBox1
        // 
        pictureBox1.BackColor = SystemColors.ControlDark;
        pictureBox1.Location = new Point(40, 42);
        pictureBox1.Name = "pictureBox1";
        pictureBox1.Size = new Size(512, 512);
        pictureBox1.TabIndex = 0;
        pictureBox1.TabStop = false;
        // 
        // hsbSweep
        // 
        hsbSweep.Location = new Point(40, 557);
        hsbSweep.Name = "hsbSweep";
        hsbSweep.Size = new Size(512, 39);
        hsbSweep.TabIndex = 1;
        // 
        // lblSweep
        // 
        lblSweep.AutoSize = true;
        lblSweep.Location = new Point(555, 571);
        lblSweep.Name = "lblSweep";
        lblSweep.Size = new Size(96, 25);
        lblSweep.TabIndex = 2;
        lblSweep.Text = "Sweep 0/0";
        // 
        // hsbFrame
        // 
        hsbFrame.Location = new Point(40, 602);
        hsbFrame.Name = "hsbFrame";
        hsbFrame.Size = new Size(512, 39);
        hsbFrame.TabIndex = 3;
        // 
        // lblFrame
        // 
        lblFrame.AutoSize = true;
        lblFrame.Location = new Point(555, 616);
        lblFrame.Name = "lblFrame";
        lblFrame.Size = new Size(93, 25);
        lblFrame.TabIndex = 4;
        lblFrame.Text = "Frame 0/0";
        // 
        // multiRoiSelect1
        // 
        multiRoiSelect1.Location = new Point(657, 42);
        multiRoiSelect1.Name = "multiRoiSelect1";
        multiRoiSelect1.Size = new Size(1132, 780);
        multiRoiSelect1.TabIndex = 5;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(10F, 25F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1460, 680);
        Controls.Add(multiRoiSelect1);
        Controls.Add(lblFrame);
        Controls.Add(lblSweep);
        Controls.Add(hsbFrame);
        Controls.Add(hsbSweep);
        Controls.Add(pictureBox1);
        Name = "Form1";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Form1";
        ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private PictureBox pictureBox1;
    private HScrollBar hsbSweep;
    private Label lblSweep;
    private HScrollBar hsbFrame;
    private Label lblFrame;
    private SWHarden.RoiSelect.WinForms.MultiRoiSelect multiRoiSelect1;
}
