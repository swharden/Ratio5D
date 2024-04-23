using System.Drawing.Drawing2D;

namespace SWHarden.RoiSelect.WinForms;

public class RoiBitmap : IDisposable
{
    private Bitmap OriginalImage { get; }
    private ImageData ImageData { get; }

    public Size OutputImageSize;

    public int OriginalWidth => OriginalImage.Width;
    public int OriginalHeight => OriginalImage.Height;

    public float ScaleX => (float)OutputImageSize.Width / OriginalImage.Width;
    public float ScaleY => (float)OutputImageSize.Height / OriginalImage.Height;

    public bool HighlightPixels = true;

    public RoiBitmap(Bitmap bmp)
    {
        ImageData = new(bmp);
        OriginalImage = bmp;
    }

    public RoiBitmap(double[,] values)
    {
        ImageData = new(values);
        OriginalImage = ImageData.GetBitmap();
    }

    public Bitmap GetBitmap(Size size, DraggableRoiCollection roiCollection)
    {
        OutputImageSize = size;

        Bitmap bmp = new(size.Width, size.Height);
        using Graphics gfx = Graphics.FromImage(bmp);
        gfx.InterpolationMode = InterpolationMode.NearestNeighbor;
        gfx.PixelOffsetMode = PixelOffsetMode.Half;

        Rectangle targetRect = new(0, 0, size.Width, size.Height);
        gfx.DrawImage(OriginalImage, targetRect);

        Color highlightColor = Color.FromArgb(30, Color.Yellow);
        using Brush highlightBrush = new SolidBrush(highlightColor);

        foreach (DraggableRoi roi in roiCollection.ROIs)
        {
            if (HighlightPixels && roi.IsSelected)
            {
                RectangleF[] rects = roi.GetPoints(ScaleX, ScaleY)
                    .Select(pt => new RectangleF(pt.X * ScaleX, pt.Y * ScaleY, ScaleX, ScaleY))
                    .ToArray();

                if (rects.Length > 0)
                    gfx.FillRectangles(highlightBrush, rects);
            }

            gfx.DrawRectangle(Pens.Yellow, roi.GetRect());

            if (roi.IsSelected)
            {
                foreach (PointF pt in roi.GetHandlePoints())
                {
                    DrawHandle(gfx, pt);
                }
            }
        }

        return bmp;
    }

    private static void DrawHandle(Graphics gfx, PointF pt)
    {
        const int CornerBlackRadius = 5;
        const int CornerWhiteRadius = 3;

        gfx.FillRectangle(
            Brushes.Black,
            pt.X - CornerBlackRadius,
            pt.Y - CornerBlackRadius,
            CornerBlackRadius * 2,
            CornerBlackRadius * 2);

        gfx.FillRectangle(
            Brushes.White,
            pt.X - CornerWhiteRadius,
            pt.Y - CornerWhiteRadius,
            CornerWhiteRadius * 2,
            CornerWhiteRadius * 2);
    }

    public void Dispose()
    {
        OriginalImage.Dispose();
        GC.SuppressFinalize(this);
    }
}
