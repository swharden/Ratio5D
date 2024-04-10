namespace Ratio5D.Core;

public static class SciTifExtensions
{
    public static SciTIF.Image Clone(this SciTIF.Image image)
    {
        double[] values = new double[image.Values.Length];
        Array.Copy(image.Values, values, image.Values.Length);
        return new SciTIF.Image(image.Width, image.Height, values);
    }

    public static SciTIF.Image ScaledBy(this SciTIF.Image image, double subtract, double scaleBy)
    {
        SciTIF.Image img = image.Clone();
        img.ScaleBy(subtract, scaleBy);
        return img;
    }

    public static void ScaleBy(this SciTIF.ImageRGB image, double subtract, double scaleBy)
    {
        image.Red.ScaleBy(subtract, scaleBy);
        image.Green.ScaleBy(subtract, scaleBy);
        image.Blue.ScaleBy(subtract, scaleBy);
    }
}
