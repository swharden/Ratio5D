﻿namespace Ratio5D.Gui;

partial class RoiTestSingleSelect
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
        singleRoiSelect1 = new SWHarden.RoiSelect.WinForms.SingleRoiSelect();
        SuspendLayout();
        // 
        // singleRoiSelect1
        // 
        singleRoiSelect1.BackColor = SystemColors.ControlDark;
        singleRoiSelect1.Dock = DockStyle.Fill;
        singleRoiSelect1.Location = new Point(0, 0);
        singleRoiSelect1.Margin = new Padding(2, 2, 2, 2);
        singleRoiSelect1.Name = "singleRoiSelect1";
        singleRoiSelect1.Size = new Size(560, 270);
        singleRoiSelect1.TabIndex = 0;
        // 
        // RoiTestSingleSelect
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(560, 270);
        Controls.Add(singleRoiSelect1);
        Margin = new Padding(2, 2, 2, 2);
        Name = "RoiTestSingleSelect";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "RoiTestSingleSelect";
        ResumeLayout(false);
    }

    #endregion

    private SWHarden.RoiSelect.WinForms.SingleRoiSelect singleRoiSelect1;
}