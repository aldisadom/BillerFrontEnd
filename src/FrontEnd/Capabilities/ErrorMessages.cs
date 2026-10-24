using BillerContracts.Clients;
using Domain.Exceptions;

namespace FrontEnd.Capabilities;

/// <summary>
/// Turns an exception into the text shown to the user
/// </summary>
public static class ErrorMessages
{
    /// <summary>
    /// Shown when the API can't be reached or its answer can't be read
    /// </summary>
    public const string Unreachable = "The server could not be reached or sent an unexpected answer. Try again later.";

    /// <summary>
    /// Shown for any other exception; the details are only in the log
    /// </summary>
    public const string Unexpected = "Something went wrong.";

    /// <summary>
    /// The API's message and extended message for an API error, the message of a <see cref="NotFoundException"/>,
    /// and a generic text for anything else
    /// </summary>
    /// <param name="exception"></param>
    public static string For(Exception exception) => exception switch
    {
        ClientAPIException { Error: { } error } when string.IsNullOrEmpty(error.Message)
            => $"The server answered {(int)error.StatusCode} {error.StatusCode}.",
        ClientAPIException { Error: { } error } when string.IsNullOrEmpty(error.ExtendedMessage)
            => error.Message,
        ClientAPIException { Error: { } error }
            => $"{error.Message}: {error.ExtendedMessage}",
        ClientAPIException or HttpRequestException
            => Unreachable,
        NotFoundException notFound
            => notFound.Message,
        _ => Unexpected
    };
}
