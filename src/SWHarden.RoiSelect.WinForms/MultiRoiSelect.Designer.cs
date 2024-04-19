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
        tableLayoutPanel1 = new TableLayoutPanel();
        tableLayoutPanel2 = new TableLayoutPanel();
        tableLayoutPanel3 = new TableLayoutPanel();
        panel1 = new Panel();
        pictureBox1 = new PictureBox();
        tableLayoutPanel1.SuspendLayout();
        tableLayoutPanel2.SuspendLayout();
        tableLayoutPanel3.SuspendLayout();
        panel1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
        SuspendLayout();
        // 
        // listBox1
        // 
        listBox1.Dock = DockStyle.Fill;
        listBox1.FormattingEnabled = true;
        listBox1.ItemHeight = 25;
        listBox1.Location = new Point(3, 3);
        listBox1.Name = "listBox1";
        listBox1.Size = new Size(288, 441);
        listBox1.TabIndex = 7;
        // 
        // btnAdd
        // 
        btnAdd.Dock = DockStyle.Fill;
        btnAdd.Location = new Point(3, 3);
        btnAdd.Name = "btnAdd";
        btnAdd.Size = new Size(138, 48);
        btnAdd.TabIndex = 6;
        btnAdd.Text = "Add";
        btnAdd.UseVisualStyleBackColor = true;
        // 
        // btnDelete
        // 
        btnDelete.Dock = DockStyle.Fill;
        btnDelete.Location = new Point(147, 3);
        btnDelete.Name = "btnDelete";
        btnDelete.Size = new Size(138, 48);
        btnDelete.TabIndex = 5;
        btnDelete.Text = "Delete";
        btnDelete.UseVisualStyleBackColor = true;
        // 
        // tableLayoutPanel1
        // 
        tableLayoutPanel1.ColumnCount = 2;
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 300F));
        tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 1, 0);
        tableLayoutPanel1.Controls.Add(panel1, 0, 0);
        tableLayoutPanel1.Dock = DockStyle.Fill;
        tableLayoutPanel1.Location = new Point(0, 0);
        tableLayoutPanel1.Name = "tableLayoutPanel1";
        tableLayoutPanel1.RowCount = 1;
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tableLayoutPanel1.Size = new Size(910, 513);
        tableLayoutPanel1.TabIndex = 8;
        // 
        // tableLayoutPanel2
        // 
        tableLayoutPanel2.ColumnCount = 1;
        tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanel2.Controls.Add(tableLayoutPanel3, 0, 1);
        tableLayoutPanel2.Controls.Add(listBox1, 0, 0);
        tableLayoutPanel2.Dock = DockStyle.Fill;
        tableLayoutPanel2.Location = new Point(613, 3);
        tableLayoutPanel2.Name = "tableLayoutPanel2";
        tableLayoutPanel2.RowCount = 2;
        tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
        tableLayoutPanel2.Size = new Size(294, 507);
        tableLayoutPanel2.TabIndex = 0;
        // 
        // tableLayoutPanel3
        // 
        tableLayoutPanel3.ColumnCount = 2;
        tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tableLayoutPanel3.Controls.Add(btnDelete, 1, 0);
        tableLayoutPanel3.Controls.Add(btnAdd, 0, 0);
        tableLayoutPanel3.Location = new Point(3, 450);
        tableLayoutPanel3.Name = "tableLayoutPanel3";
        tableLayoutPanel3.RowCount = 1;
        tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tableLayoutPanel3.Size = new Size(288, 54);
        tableLayoutPanel3.TabIndex = 0;
        // 
        // panel1
        // 
        panel1.BackColor = SystemColors.ControlDarkDark;
        panel1.Controls.Add(pictureBox1);
        panel1.Dock = DockStyle.Fill;
        panel1.Location = new Point(3, 3);
        panel1.Name = "panel1";
        panel1.Size = new Size(604, 507);
        panel1.TabIndex = 1;
        // 
        // pictureBox1
        // 
        pictureBox1.BackColor = SystemColors.ControlDark;
        pictureBox1.Location = new Point(154, 119);
        pictureBox1.Name = "pictureBox1";
        pictureBox1.Size = new Size(296, 268);
        pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        pictureBox1.TabIndex = 5;
        pictureBox1.TabStop = false;
        // 
        // MultiRoiSelect
        // 
        AutoScaleDimensions = new SizeF(10F, 25F);
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(tableLayoutPanel1);
        Name = "MultiRoiSelect";
        Size = new Size(910, 513);
        tableLayoutPanel1.ResumeLayout(false);
        tableLayoutPanel2.ResumeLayout(false);
        tableLayoutPanel3.ResumeLayout(false);
        panel1.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private ListBox listBox1;
    private Button btnAdd;
    private Button btnDelete;
    private TableLayoutPanel tableLayoutPanel1;
    private TableLayoutPanel tableLayoutPanel2;
    private TableLayoutPanel tableLayoutPanel3;
    private Panel panel1;
    private PictureBox pictureBox1;
}
