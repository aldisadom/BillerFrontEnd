namespace Domain.Models;

public static class AppFilterHelper
{
    public const string InvalidName = "-----";
}

public class AppFilter
{
    public Guid SelectedId { get; set; }
    private Guid SelectedIdPrv { get; set; }
    public Dictionary<Guid, string> SelectionList { get; set; }

    public AppFilter(bool withInvalid = true)
    {
        SelectionList = [];
        if (withInvalid)
            SelectionList.Add(Guid.Empty, AppFilterHelper.InvalidName);
    }

    public void SelectFirstFromList()
    {
        if (SelectionList.Count > 0)
            SelectedId = SelectionList.ElementAt(0).Key;
    }
    public bool WasUnselected()
    {
        return SelectedIdPrv == Guid.Empty;
    }

    public void UpdateSelected()
    {
        SelectedIdPrv = SelectedId;
    }

    public void UpdateSelected(Guid id)
    {
        SelectedId = id;
        SelectedIdPrv = id;
    }

    public bool Changed()
    {
        return SelectedId != SelectedIdPrv;
    }

    public bool IsValid()
    {
        return SelectedId != Guid.Empty;
    }
}
