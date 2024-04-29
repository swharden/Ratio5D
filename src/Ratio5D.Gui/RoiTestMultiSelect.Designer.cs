namespace Ratio5D.Gui;

partial class RoiTestMultiSelect
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
        formsPlot1 = new ScottPlot.WinForms.FormsPlot();
        SuspendLayout();
        // 
        // multiRoiSelect1
        // 
        multiRoiSelect1.Location = new Point(0, 0);
        multiRoiSelect1.Margin = new Padding(1, 1, 1, 1);
        multiRoiSelect1.Name = "multiRoiSelect1";
        multiRoiSelect1.Size = new Size(721, 449);
        multiRoiSelect1.TabIndex = 0;
        // 
        // formsPlot1
        // 
        formsPlot1.DisplayScale = 1.5F;
        formsPlot1.Location = new Point(725, 7);
        formsPlot1.Margin = new Padding(2, 2, 2, 2);
        formsPlot1.Name = "formsPlot1";
        formsPlot1.Size = new Size(473, 434);
        formsPlot1.TabIndex = 1;
        // 
        // RoiTestMultiSelect
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1207, 449);
        Controls.Add(formsPlot1);
        Controls.Add(multiRoiSelect1);
        Margin = new Padding(2, 2, 2, 2);
        Name = "RoiTestMultiSelect";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "RoiTest";
        ResumeLayout(false);
    }

    #endregion

    private SWHarden.RoiSelect.WinForms.MultiRoiSelect multiRoiSelect1;
    private ScottPlot.WinForms.FormsPlot formsPlot1;
}