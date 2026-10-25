using BillerContracts;
using BillerContracts.Clients;
using Domain.Exceptions;
using Newtonsoft.Json;

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
        // Task.Run(...).Wait() on the invoice pages wraps the real exception
        AggregateException { InnerExceptions.Count: 1 } aggregate
            => For(aggregate.InnerExceptions[0]),
        ClientAPIException { Error: { } error }
            => ForApiError(error),
        ClientAPIException or HttpRequestException or TaskCanceledException or JsonException
            => Unreachable,
        NotFoundException notFound
            => notFound.Message,
        _ => Unexpected
    };

    private static string ForApiError(ErrorModel error)
    {
        bool hasMessage = !string.IsNullOrEmpty(error.Message);
        bool hasExtendedMessage = !string.IsNullOrEmpty(error.ExtendedMessage);

        if (hasMessage && hasExtendedMessage)
            return $"{error.Message}: {error.ExtendedMessage}";
        if (hasMessage)
            return error.Message;
        if (hasExtendedMessage)
            return error.ExtendedMessage;

        return $"The server answered {(int)error.StatusCode} {error.StatusCode}.";
    }
}
