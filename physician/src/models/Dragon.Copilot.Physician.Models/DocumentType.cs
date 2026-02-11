// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Dragon.Copilot.Physician.Models;

/// <summary>
/// Document type information
/// </summary>
public class DocumentType
{
    /// <summary>
    /// Type text
    /// </summary>
    [JsonPropertyName("text")]
    public string? Text { get; set; }

    /// <summary>
    /// Codes associated with the document type
    /// </summary>
    [JsonPropertyName("codes")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IList<CodeInfo>? Codes { get; init; }
}
