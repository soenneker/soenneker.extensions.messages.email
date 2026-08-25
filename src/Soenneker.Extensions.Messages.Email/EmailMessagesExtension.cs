using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Reflection;
using System.Text.Json.Serialization;
using Soenneker.Messages.Email;

namespace Soenneker.Extensions.Messages.Email;

/// <summary>
/// A collection of helpful EmailMessage extension methods
/// </summary>
public static class EmailMessagesExtension
{
    private static readonly ConcurrentDictionary<Type, (PropertyInfo[] Props, string[] Names)> _propertyCache = new();

    /// <summary>
    /// Converts the properties of an EmailMessage into a dictionary of token strings for use in email templates.
    /// </summary>
    /// <param name="message">The EmailMessage to extract tokens from.</param>
    /// <returns>A dictionary where each key-value pair represents a token and its string value.</returns>
    public static Dictionary<string, string> ToTokenDictionary(this EmailMessage message)
    {
        if (message is null)
            return new Dictionary<string, string>();

        (PropertyInfo[] props, string[] names) = _propertyCache.GetOrAdd(message.GetType(), static type =>
        {
            PropertyInfo[] raw = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            var count = 0;

            for (var i = 0; i < raw.Length; i++)
            {
                PropertyInfo property = raw[i];
                if (property.CanRead && property.GetIndexParameters().Length == 0)
                    count++;
            }

            var properties = new PropertyInfo[count];
            var propertyNames = new string[count];
            var destination = 0;

            for (var i = 0; i < raw.Length; i++)
            {
                PropertyInfo property = raw[i];
                if (!property.CanRead || property.GetIndexParameters().Length != 0)
                    continue;

                properties[destination] = property;
                propertyNames[destination] = property.GetCustomAttribute<JsonPropertyNameAttribute>(false)?.Name ?? property.Name;
                destination++;
            }

            return (properties, propertyNames);
        });

        var result = new Dictionary<string, string>(props.Length, StringComparer.Ordinal);

        for (var i = 0; i < props.Length; i++)
            result.Add(names[i], Convert.ToString(props[i].GetValue(message))!);

        return result;
    }
}
