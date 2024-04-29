namespace Ratio5D.Gui;

public partial class RoiTestMultiSelect : Form
{
    public RoiTestMultiSelect()
    {
        InitializeComponent();

        double[,] values = new double[10, 10];
        for (int y = 0; y < values.GetLength(0); y++)
            for (int x = 0; x < values.GetLength(1); x++)
                values[y, x] = Random.Shared.NextDouble();

        multiRoiSelect1.SetImage(values);
    }
}
