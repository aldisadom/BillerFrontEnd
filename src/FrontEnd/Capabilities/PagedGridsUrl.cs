using Microsoft.AspNetCore.Components;

namespace FrontEnd.Capabilities;

/// <summary>
/// Keeps the page and page size of every server-paged grid of one page in the URL. The page takes the
/// grids' values from [SupplyParameterFromQuery] properties in OnParametersSetAsync when
/// <see cref="UrlChanged"/> is true, calls <see cref="Rendered"/> after its first render, and
/// <see cref="Write"/> from each grid's ItemsProvider.
/// </summary>
public sealed class PagedGridsUrl
{
    private readonly NavigationManager _navigationManager;
    private readonly GridPaging[] _grids;
    private string _current;
    private string? _pending;
    private bool _rendered;

    public PagedGridsUrl(NavigationManager navigationManager, params GridPaging[] grids)
    {
        _navigationManager = navigationManager;
        _grids = grids;
        _current = navigationManager.Uri;
    }

    /// <summary>
    /// False while a URL this page wrote is on its way: the grids already show it, and an older write
    /// that arrives first would undo the newer change.
    /// </summary>
    public bool UrlChanged()
    {
        string uri = _navigationManager.Uri;
        if (_pending is not null)
        {
            if (uri == _pending)
                _pending = null;

            return false;
        }

        _current = uri;
        return true;
    }

    /// <summary>
    /// Writes the URL from now on; before the first render the page may be prerendering, where navigation throws.
    /// </summary>
    public void Rendered()
    {
        _rendered = true;
        Write();
    }

    /// <summary>
    /// Writes every grid's page and page size, so a grid that loads at the same time as another
    /// doesn't overwrite its values with the URL from before.
    /// </summary>
    public void Write()
    {
        if (!_rendered)
            return;

        Dictionary<string, object?> query = [];
        foreach (GridPaging grid in _grids)
        {
            query[grid.PageKey] = grid.Page;
            query[grid.PageSizeKey] = (int)grid.PageSize;
        }

        string uri = _navigationManager.GetUriWithQueryParameters(_current, query);
        if (uri == _current)
            return;

        _current = uri;
        _pending = uri;
        _navigationManager.NavigateTo(uri, replace: true);
    }
}
