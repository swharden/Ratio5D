namespace SWHarden.RoiSelect.WinForms;

// TODO: use primitive type for PixelRect
public readonly struct DataRoi(Rectangle rect, double[,] values)
{
    Rectangle Rect { get; } = rect;
    public double[,] Values { get; } = values;
    public double[] ValuesFlat { get; } = Flatten(values);

    public int Width => Rect.Width;
    public int Height => Rect.Height;

    public override string ToString() => $"{Rect} ({Values.Length} values)";

    private static double[] Flatten(double[,] values)
    {
        int rows = values.GetLength(0);
        int cols = values.GetLength(1);
        double[] flat = new double[rows * cols];
        int index = 0;
        for (int i = 0; i < rows; i++)
            for (int j = 0; j < cols; j++)
                flat[index++] = values[i, j];
        return flat;
    }
}
