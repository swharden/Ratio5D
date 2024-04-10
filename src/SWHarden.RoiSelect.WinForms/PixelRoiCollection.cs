namespace SWHarden.RoiSelect.WinForms;

public class RoiPixelCollection
{
    public readonly List<PixelRoi> Rois = [];
    public int Count => Rois.Count;

    public void Add(int x1, int x2, int y1, int y2, string name = "")
    {
        Rois.Add(new PixelRoi(x1, x2, y1, y2, name));
    }

    private void Load()
    {
        throw new NotImplementedException();
    }

    private void Save()
    {
        throw new NotImplementedException();
    }
}
