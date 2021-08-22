using System.Net.Http;
using System.Threading.Tasks;

namespace TflRouteStatus.Interfaces
{
    /// <summary>
    /// ICallAPi interface
    /// </summary>
    public interface ICallAPI
    {
        /// <summary>
        /// GetAPIResponse interface method
        /// </summary>
        /// <param name="api_url">API Url</param>
        /// <param name="app_id">App Id</param>
        /// <param name="app_key">App Key</param>
        /// <param name="route">route</param>
        /// <returns></returns>
        Task<HttpResponseMessage> GetAPIResponse(string api_url, string app_id, string app_key, string route);
    }
}
