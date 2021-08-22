using System.Threading.Tasks;

namespace TflRouteStatus.Interfaces
{
    /// <summary>
    /// Route Status interface
    /// </summary>
    public interface IRouteStatus
    {
        /// <summary>
        /// Validate all inputs from config
        /// </summary>
        /// <param name="api_url">API Url</param>
        /// <param name="app_id">App Id</param>
        /// <param name="app_key">App Key</param>
        /// <returns>true or false</returns>
         bool ValidateInputs(string api_url, string app_id, string app_key);

        /// <summary>
        /// Find Route Status
        /// </summary>
        /// <param name="api_url">API Url</param>
        /// <param name="app_id">App Id</param>
        /// <param name="app_key">App Key</param>
        /// <param name="route">route</param>
        /// <returns>response</returns>
        Task<string> FindRouteStatus(string api_url, string app_id, string app_key, string route);        
    }
}
