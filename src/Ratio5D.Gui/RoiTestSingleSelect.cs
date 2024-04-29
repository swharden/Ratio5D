namespace Ratio5D.Gui;

public partial class RoiTestSingleSelect : Form
{
    public RoiTestSingleSelect()
    {
        InitializeComponent();

        //Bitmap bmp = new("../../../../../dev/sample-data/brain-slice-1ch-64.png");
        //Bitmap bmp = new("../../../../../dev/sample-data/noise-3.png");
        //multiRoiSelect1.SetImage(bmp);

        double[,] values = new double[10, 10];
        for (int y = 0; y < values.GetLength(0); y++)
            for (int x = 0; x < values.GetLength(1); x++)
                values[y, x] = Random.Shared.NextDouble();

        singleRoiSelect1.SetImage(values);
    }
}
