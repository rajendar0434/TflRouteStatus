using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using TflRouteStatus.Interfaces;
using TflRouteStatus.ModelDefinitions;

namespace TflRouteStatus.Action
{
    /// <summary>
    /// Route Status Class
    /// </summary>
    public class RouteStatus : IRouteStatus
    {
        ICallAPI _callAPI;

        /// <summary>
        /// Route Status Constructor
        /// </summary>
        /// <param name="callAPI"></param>
        public RouteStatus(ICallAPI callAPI)
        {
            _callAPI = callAPI;
        }

        /// <summary>
        /// Validate inputs
        /// </summary>
        /// <param name="api_url">API Url</param>
        /// <param name="app_id">App Id</param>
        /// <param name="app_key">App Key</param>
        /// <returns>true or false</returns>
        public bool ValidateInputs(string api_url, string app_id, string app_key)
        {
            bool isValid = true;

            if (string.IsNullOrEmpty(api_url)
                    || string.IsNullOrEmpty(app_id)
                    || string.IsNullOrEmpty(app_key)
                    )
            {
                isValid = false;
            }

            return isValid;
        }

        /// <summary>
        /// Find Route Status by Calling CallAPI() method
        /// </summary>
        /// <param name="api_url">API Url</param>
        /// <param name="app_id">APP Id</param>
        /// <param name="app_key">App Key</param>
        /// <param name="route">route</param>
        /// <returns>response</returns>
        public async Task<string> FindRouteStatus(string api_url, string app_id, string app_key, string route)
        {
            try
            {
                string returnMsg = string.Empty;

                HttpResponseMessage response = await _callAPI.GetAPIResponse(api_url, app_id, app_key, route);

                if (response.IsSuccessStatusCode)
                {
                    var routeData = await response.Content.ReadAsAsync<List<ValidRouteResponse>>();
                    Environment.ExitCode = Convert.ToInt32(Resources.Success_Exit_Code);

                    returnMsg = string.Format(Resources.ValidRouteMsg, routeData[0].DisplayName, routeData[0].StatusSeverity, routeData[0].StatusSeverityDescription);
                }
                else
                {
                    var routeData = await response.Content.ReadAsAsync<InValidRouteResponse>();
                    Environment.ExitCode = Convert.ToInt32(Resources.Error_Exit_Code); ;

                    returnMsg = string.Format(Resources.InValidRouteMsg, route);
                }

                return returnMsg;
            }
            catch
            {
                throw;
            }
        }
    }
}
