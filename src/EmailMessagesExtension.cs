using System;
using System.Collections.Generic;
using Soenneker.Extensions.Object;
using Soenneker.Messages.Email;

namespace Soenneker.Extensions.Messages.Email;

/// <summary>
/// A collection of helpful EmailMessage extension methods
/// </summary>
public static class EmailMessagesExtension
{
    /// <summary>
    /// Converts the properties of an EmailMessage into a dictionary of token strings for use in email templates.
    /// </summary>
    /// <param name="message">The EmailMessage to extract tokens from.</param>
    /// <returns>A dictionary where each key-value pair represents a token and its string value.</returns>
    public static Dictionary<string, string> ToTokenDictionary(this EmailMessage message)
    {
        var tokensToReplace = new Dictionary<string, string>();

        IDictionary<string, object?> propertiesDict = message.ToDictionary();

        foreach (KeyValuePair<string, object?> kvp in propertiesDict)
        {
            tokensToReplace.Add(kvp.Key, Convert.ToString(kvp.Value)!);
        }

        return tokensToReplace;
    }
}
