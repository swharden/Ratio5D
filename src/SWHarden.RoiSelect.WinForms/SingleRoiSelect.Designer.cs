﻿namespace SWHarden.RoiSelect.WinForms;

partial class SingleRoiSelect
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

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        pictureBox1 = new PictureBox();
        panel1 = new Panel();
        ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
        panel1.SuspendLayout();
        SuspendLayout();
        // 
        // pictureBox1
        // 
        pictureBox1.BackColor = SystemColors.ControlDarkDark;
        pictureBox1.Location = new Point(35, 45);
        pictureBox1.Name = "pictureBox1";
        pictureBox1.Size = new Size(337, 144);
        pictureBox1.TabIndex = 0;
        pictureBox1.TabStop = false;
        // 
        // panel1
        // 
        panel1.BackColor = SystemColors.ControlDark;
        panel1.Controls.Add(pictureBox1);
        panel1.Dock = DockStyle.Fill;
        panel1.Location = new Point(0, 0);
        panel1.Name = "panel1";
        panel1.Size = new Size(565, 537);
        panel1.TabIndex = 1;
        // 
        // SingleRoiSelect
        // 
        AutoScaleDimensions = new SizeF(10F, 25F);
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(panel1);
        Name = "SingleRoiSelect";
        Size = new Size(565, 537);
        ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
        panel1.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion

    private PictureBox pictureBox1;
    private Panel panel1;
}
