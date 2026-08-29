[![](https://img.shields.io/nuget/v/soenneker.extensions.messages.email.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.extensions.messages.email/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.extensions.messages.email/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.extensions.messages.email/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.extensions.messages.email.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.extensions.messages.email/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.extensions.messages.email/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.extensions.messages.email/actions/workflows/codeql.yml)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.Extensions.Messages.Email
Extension methods for converting and preparing `EmailMessage` objects for templates, transports, and other email-processing stages.

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

Token names come from `[JsonPropertyName]` when present (`Subject` therefore becomes `subject`); otherwise the CLR property name is used. Values are converted with `Convert.ToString`, so this is a flat token dictionary—not JSON serialization. Collections and dictionaries are not expanded into individual tokens. Only readable, non-indexed public properties declared directly on `EmailMessage` are included; properties inherited from its base message type are excluded. A null message returns an empty dictionary.

Property metadata is cached per runtime type, making repeated conversion inexpensive.
