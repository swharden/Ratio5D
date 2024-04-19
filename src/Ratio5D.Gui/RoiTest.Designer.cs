namespace Ratio5D.Gui;

partial class RoiTest
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
        multiRoiSelect1 = new SWHarden.RoiSelect.WinForms.MultiRoiSelect();
        SuspendLayout();
        // 
        // multiRoiSelect1
        // 
        multiRoiSelect1.Dock = DockStyle.Fill;
        multiRoiSelect1.Location = new Point(0, 0);
        multiRoiSelect1.Name = "multiRoiSelect1";
        multiRoiSelect1.Size = new Size(1030, 748);
        multiRoiSelect1.TabIndex = 0;
        // 
        // RoiTest
        // 
        AutoScaleDimensions = new SizeF(10F, 25F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1030, 748);
        Controls.Add(multiRoiSelect1);
        Name = "RoiTest";
        Text = "RoiTest";
        ResumeLayout(false);
    }

    #endregion

    private SWHarden.RoiSelect.WinForms.MultiRoiSelect multiRoiSelect1;
}