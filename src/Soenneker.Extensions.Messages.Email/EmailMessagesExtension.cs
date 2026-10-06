using System;
using System.Collections.Generic;
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
        if (message is null)
            return new Dictionary<string, string>();

        return new Dictionary<string, string>(13, StringComparer.Ordinal)
        {
            ["to"] = Convert.ToString((object?)message.To)!,
            ["cc"] = Convert.ToString((object?)message.Cc)!,
            ["bcc"] = Convert.ToString((object?)message.Bcc)!,
            ["replyTo"] = Convert.ToString((object?)message.ReplyTo)!,
            ["name"] = Convert.ToString((object?)message.Name)!,
            ["address"] = Convert.ToString((object?)message.Address)!,
            ["subject"] = Convert.ToString((object?)message.Subject)!,
            ["contentFileName"] = Convert.ToString((object?)message.ContentFileName)!,
            ["templateFileName"] = Convert.ToString((object?)message.TemplateFileName)!,
            ["format"] = Convert.ToString((object?)message.Format)!,
            ["priority"] = Convert.ToString((object?)message.Priority)!,
            ["tokens"] = Convert.ToString((object?)message.Tokens)!,
            ["partials"] = Convert.ToString((object?)message.Partials)!,
        };
    }
}
