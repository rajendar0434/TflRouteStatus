using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TflRouteStatus.Interfaces;

namespace TflRouteStatus.Action
{
    /// <summary>
    /// Call TFL API
    /// </summary>
    public class CallAPI : ICallAPI
    {      
        /// <summary>
        /// Get API response by calling Tfl API
        /// </summary>
        /// <param name="api_url">API Url</param>
        /// <param name="app_id">App Id</param>
        /// <param name="app_key">App Key</param>
        /// <param name="route">route</param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> GetAPIResponse(string api_url, string app_id, string app_key, string route)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string returnMsg = string.Empty;

                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(Resources.MediaTypeWithQualityHeaderValue));
                    client.DefaultRequestHeaders.Add(Common.USER_AGENT, Resources.UserAgentMsg);

                    HttpResponseMessage httpResponse = await client.GetAsync(string.Format(api_url, route, app_id, app_key));

                    return httpResponse;
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
