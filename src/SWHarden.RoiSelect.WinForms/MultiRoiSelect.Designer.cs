namespace SWHarden.RoiSelect.WinForms;

partial class MultiRoiSelect
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
        listBox1 = new ListBox();
        btnAdd = new Button();
        btnDelete = new Button();
        pictureBox1 = new PictureBox();
        ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
        SuspendLayout();
        // 
        // listBox1
        // 
        listBox1.FormattingEnabled = true;
        listBox1.ItemHeight = 25;
        listBox1.Location = new Point(521, 3);
        listBox1.Name = "listBox1";
        listBox1.Size = new Size(230, 454);
        listBox1.TabIndex = 7;
        // 
        // btnAdd
        // 
        btnAdd.Location = new Point(521, 463);
        btnAdd.Name = "btnAdd";
        btnAdd.Size = new Size(112, 52);
        btnAdd.TabIndex = 6;
        btnAdd.Text = "Add";
        btnAdd.UseVisualStyleBackColor = true;
        // 
        // btnDelete
        // 
        btnDelete.Location = new Point(639, 463);
        btnDelete.Name = "btnDelete";
        btnDelete.Size = new Size(112, 52);
        btnDelete.TabIndex = 5;
        btnDelete.Text = "Delete";
        btnDelete.UseVisualStyleBackColor = true;
        // 
        // pictureBox1
        // 
        pictureBox1.BackColor = SystemColors.ControlDark;
        pictureBox1.Location = new Point(3, 3);
        pictureBox1.Name = "pictureBox1";
        pictureBox1.Size = new Size(512, 512);
        pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        pictureBox1.TabIndex = 4;
        pictureBox1.TabStop = false;
        // 
        // MultiRoiSelect
        // 
        AutoScaleDimensions = new SizeF(10F, 25F);
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(listBox1);
        Controls.Add(btnAdd);
        Controls.Add(btnDelete);
        Controls.Add(pictureBox1);
        Name = "MultiRoiSelect";
        Size = new Size(755, 520);
        ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private ListBox listBox1;
    private Button btnAdd;
    private Button btnDelete;
    private PictureBox pictureBox1;
}
