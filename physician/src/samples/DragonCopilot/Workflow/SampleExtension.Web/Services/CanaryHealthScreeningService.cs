// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using Dragon.Copilot.Physician.Models;

namespace SampleExtension.Web.Services;

/// <summary>
/// Service that generates Canary Health Screening style adaptive cards.
/// This is a mock implementation that returns sample screening results.
/// </summary>
public static class CanaryHealthScreeningService
{
    private const string CanaryLogo = "https://csstatic.blob.core.windows.net/assets/dragon_canary_logo.png";
    private const string CanaryApiUrl = "https://public-api.prod.eus.canaryspeech.com/dragon/nursing/v1/process";
    private const string CardTitle = "Canary Speech Vocal Biomarker Assessment";
    private const string AssessmentTitle = "Canary Aggression Risk Assessment";
    private const string AssessmentDescription = "A Canary Aggression Risk Assessment was administered by analyzing vocal features that correlate with aggression risk in the environment. The result was as follows:";
    private const string AssessmentResult = "⚠️ Aggression risk: 73/100 (medium)";

    /// <summary>
    /// Creates a mock Canary Health Screening response with the note card from the nursing sample
    /// </summary>
    /// <param name="correlationId">The correlation ID for the request</param>
    /// <returns>A DspResponse containing Canary-style visualization resources</returns>
    public static DspResponse CreateCanaryScreeningResponse(string correlationId)
    {
        var response = new DspResponse
        {
            SchemaVersion = "1.0"
        };

        // Add the note card
        response.Resources?.Add(CreateCanaryNoteCard(correlationId));

        return response;
    }

    /// <summary>
    /// Creates the detailed note card for Canary Health Screening
    /// </summary>
    private static VisualizationResource CreateCanaryNoteCard(string correlationId)
    {
        return new VisualizationResource
        {
            Id = "canary-note-card",
            Type = "AdaptiveCard",
            Subtype = VisualizationSubtype.Note,
            CardTitle = CardTitle,
            PartnerLogo = CanaryLogo,
            AdaptiveCardPayload = new AdaptiveCardPayload
            {
                Type = "AdaptiveCard",
                Version = "1.5",
                Body = CreateCardBody()
            },
            References = new List<VisualizationReference>
            {
                new()
                {
                    Id = "ref-canary-speech-001",
                    Type = ReferenceType.Web,
                    Title = "Canary Speech",
                    Url = new Uri("https://canaryspeech.com")
                }
            },
            PayloadSources = new List<PayloadSource>
            {
                new()
                {
                    Identifier = "canary-speech-request-05be9709-2678-5b35-f328-0a0a969fa32b",
                    Description = "Canary Health Screening Service",
                    Url = new Uri(CanaryApiUrl)
                }
            },
            DragonCopilotCopyData = GetCopyDataContent()
        };
    }

    private static List<object> CreateCardBody()
    {
        return new List<object>
        {
            new
            {
                type = "TextBlock",
                text = AssessmentTitle,
                weight = "Bolder",
                wrap = true
            },
            new
            {
                type = "TextBlock",
                text = AssessmentDescription,
                size = "Small",
                wrap = true
            },
            new
            {
                type = "TextBlock",
                text = AssessmentResult,
                size = "Small",
                wrap = true
            }
        };
    }

    private static string GetCopyDataContent()
    {
        return AssessmentTitle + "\n" +
               AssessmentDescription + "\n" +
               "Aggression risk: 73/100 (medium)";
    }
}
