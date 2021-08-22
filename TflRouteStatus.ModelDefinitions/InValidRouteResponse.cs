using System;
using System.Text.Json.Serialization;

namespace TflRouteStatus.ModelDefinitions
{
    /// <summary>
    /// Response definition when routs is not valid
    /// </summary>
    public class InValidRouteResponse
    {
        /// <summary>
        /// Type
        /// </summary>
        [JsonPropertyName("$type")]
        public string Type { get; set; }

        /// <summary>
        /// Time
        /// </summary>
        [JsonPropertyName("timestampUtc")]
        public DateTime TimeStampUtc { get; set; }

        /// <summary>
        /// Exception type
        /// </summary>
        [JsonPropertyName("exceptionType")]
        public string ExceptionType { get; set; }

        /// <summary>
        /// HTTP STatus Code
        /// </summary>
        [JsonPropertyName("httpStatusCode")]
        public int HttpStatusCode { get; set; }

        /// <summary>
        /// HTTP Status
        /// </summary>
        [JsonPropertyName("httpStatus")]
        public string HttpStatus { get; set; }

        /// <summary>
        /// Relative URL
        /// </summary>
        [JsonPropertyName("relativeUri")]
        public string RelativeUri { get; set; }

        /// <summary>
        /// Message
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}
