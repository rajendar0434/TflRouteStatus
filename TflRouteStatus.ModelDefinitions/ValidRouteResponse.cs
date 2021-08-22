using System.Text.Json.Serialization;

namespace TflRouteStatus.ModelDefinitions
{
    /// <summary>
    /// Response definition when Routs is valid
    /// </summary>
    public class ValidRouteResponse
    {
        /// <summary>
        /// Type
        /// </summary>
        [JsonPropertyName("$type")]
        public string Type { get; set; }

        /// <summary>
        /// Id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// Display Name
        /// </summary>
        [JsonPropertyName("displayname")]
        public string DisplayName { get; set; }

        /// <summary>
        /// Status Severity
        /// </summary>
        [JsonPropertyName("statusSeverity")]
        public string StatusSeverity { get; set; }

        /// <summary>
        /// Status Severity Description
        /// </summary>
        [JsonPropertyName("statusSeverityDescription")]
        public string StatusSeverityDescription { get; set; }

        /// <summary>
        /// Bounds
        /// </summary>
        [JsonPropertyName("bounds")]
        public string Bounds { get; set; }

        /// <summary>
        /// Envelope
        /// </summary>
        [JsonPropertyName("envelope")]
        public string Envelope { get; set; }

        /// <summary>
        /// Url
        /// </summary>
        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}
