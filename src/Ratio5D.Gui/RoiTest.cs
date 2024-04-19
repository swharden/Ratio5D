namespace Ratio5D.Gui;

public partial class RoiTest : Form
{
    public RoiTest()
    {
        InitializeComponent();
        Bitmap bmp = new("../../../../../dev/sample-data/brain-slice-1ch-64.png");
        //Bitmap bmp = new("../../../../../dev/sample-data/noise-3.png");
        multiRoiSelect1.SetImage(bmp);
    }
}
