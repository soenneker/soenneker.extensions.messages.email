[![](https://img.shields.io/nuget/v/soenneker.extensions.messages.email.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.extensions.messages.email/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.extensions.messages.email/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.extensions.messages.email/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.extensions.messages.email.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.extensions.messages.email/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.extensions.messages.email/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.extensions.messages.email/actions/workflows/codeql.yml)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.Extensions.Messages.Email
Converts the email-specific properties of an `EmailMessage` into a flat string token dictionary.

## Installation

```bash
dotnet add package Soenneker.Extensions.Messages.Email
```

## Usage

```csharp
using Soenneker.Extensions.Messages.Email;

EmailMessage message = CreateEmailMessage();
Dictionary<string, string> tokens = message.ToTokenDictionary();

string subject = tokens["subject"];
// subject == message.Subject
```

Token names come from `[JsonPropertyName]` when present (`Subject` therefore becomes `subject`); otherwise the CLR property name is used. Only readable, non-indexed public properties declared directly on the sealed `EmailMessage` type are included. Routing and audit properties inherited from its base message envelope are excluded.

Values use `Convert.ToString(object)`. Null property values become empty strings, and other values use their normal string conversion under the current culture. This is not JSON serialization: recipient lists, token dictionaries, and partial dictionaries are not expanded into child entries and generally convert to their .NET type name. Add their contents explicitly when a template needs them.

```csharp
Dictionary<string, string> tokens = message.ToTokenDictionary();
tokens["recipientName"] = customer.DisplayName;
tokens["orderTotal"] = order.Total.ToString("C", culture);
```

A null message returns a new empty dictionary. Every call returns a new ordinal-keyed dictionary, so modifying it does not modify the message. Property metadata is cached for repeated conversion; property values are read afresh on every call.

Email properties can contain addresses, subject text, and other personal data. Treat the resulting dictionary as message content and avoid logging or persisting it outside the email-processing boundary.
