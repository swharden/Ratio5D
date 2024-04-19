using System.Drawing.Drawing2D;

namespace SWHarden.RoiSelect.WinForms;

public class RoiBitmap(Bitmap originalImage, RoiPixelCollection roiCollection) : IDisposable
{
    private Bitmap OriginalImage { get; } = originalImage;
    private RoiPixelCollection RoiCollection { get; } = roiCollection;

    private Size OutputImageSize;

    public int OriginalWidth => OriginalImage.Width;
    public int OriginalHeight => OriginalImage.Height;

    float ScaleX => (float)OutputImageSize.Width / OriginalImage.Width;
    float ScaleY => (float)OutputImageSize.Height / OriginalImage.Height;

    const int CornerBlackRadius = 5;
    const int CornerWhiteRadius = 3;
    const int CornerMouseRadius = 7;

    public Bitmap GetBitmap(Size size)
    {
        OutputImageSize = size;

        Bitmap bmp = new(size.Width, size.Height);
        using Graphics gfx = Graphics.FromImage(bmp);
        gfx.InterpolationMode = InterpolationMode.NearestNeighbor;
        gfx.PixelOffsetMode = PixelOffsetMode.Half;

        Rectangle targetRect = new(0, 0, size.Width, size.Height);
        gfx.DrawImage(OriginalImage, targetRect);

        for (int i = 0; i < RoiCollection.Count; i++)
        {
            PixelRoi roi = RoiCollection.Rois[i];
            DrawRoi(gfx, roi);
        }

        return bmp;
    }

    private static PointF[] GetHandlePoints(RectangleF rect) =>
        Enum.GetValues<Handle>().Select(x => x.GetPoint(rect)).ToArray();

    private static RectangleF[] GetHandleRectangles(RectangleF rect, float radius) =>
        Enum.GetValues<Handle>().Select(x => x.GetRect(rect, radius)).ToArray();

    private void DrawRoi(Graphics gfx, PixelRoi roi)
    {
        RectangleF scaledRect = roi.ScaledRect(ScaleX, ScaleY);

        gfx.DrawRectangle(Pens.Yellow, scaledRect);

        if (!roi.IsSlected)
            return;

        foreach (PointF corner in GetHandlePoints(scaledRect))
        {
            DrawHandle(gfx, corner);
        }
    }

    private static void DrawHandle(Graphics gfx, PointF corner)
    {
        gfx.FillRectangle(
            Brushes.Black,
            corner.X - CornerBlackRadius,
            corner.Y - CornerBlackRadius,
            CornerBlackRadius * 2,
            CornerBlackRadius * 2);

        gfx.FillRectangle(
            Brushes.White,
            corner.X - CornerWhiteRadius,
            corner.Y - CornerWhiteRadius,
            CornerWhiteRadius * 2,
            CornerWhiteRadius * 2);
    }

    public bool IsMouseOver(PointF pt)
    {
        foreach (PixelRoi selectedRoi in RoiCollection.Rois.Where(x => x.IsSlected))
        {
            RectangleF roiRect = selectedRoi.ScaledRect(ScaleX, ScaleY);

            foreach (RectangleF cornerRect in GetHandleRectangles(roiRect, CornerMouseRadius))
            {
                if (cornerRect.Contains(pt))
                    return true;
            }

        }

        return false;
    }

    public void Dispose()
    {
        OriginalImage.Dispose();
        GC.SuppressFinalize(this);
    }
}
