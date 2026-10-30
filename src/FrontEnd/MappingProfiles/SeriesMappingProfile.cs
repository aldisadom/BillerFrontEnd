using BillerContracts.Requests.Series;
using BillerContracts.Responses.Series;

namespace FrontEnd.MappingProfiles;

/// <summary>
/// Series mapping
/// </summary>
public static class SeriesResponseExtensions
{
    /// <summary>
    /// Builds the update request for the series edit page
    /// </summary>
    public static SeriesUpdateRequest ToUpdateRequest(this SeriesResponse from)
    {
        return new SeriesUpdateRequest
        {
            Id = from.Id,
            Name = from.Name,
            NextNumber = from.NextNumber
        };
    }
}
