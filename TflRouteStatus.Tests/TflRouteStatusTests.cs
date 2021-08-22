
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TflRouteStatus.Action;
using TflRouteStatus.Interfaces;
using TflRouteStatus.ModelDefinitions;
using Xunit;

namespace TflRouteStatus.Tests
{
    /// <summary>
    /// All Test cases using XUnit
    /// </summary>
    public class TflRouteStatusTests : IDisposable
    {
        Mock<ICallAPI> mockRepo;
        IRouteStatus routeStatus;

        /// <summary>
        /// TflRouteStatusTests Constructor
        /// </summary>
        public TflRouteStatusTests()
        {
            mockRepo = new Mock<ICallAPI>();
        }

        /// <summary>
        /// Dispose objects
        /// </summary>
        public void Dispose()
        {
            mockRepo = null;
            routeStatus = null;
        }

        /// <summary>
        /// Validate inputs and return false when no inputs
        /// </summary>
        /// <returns>true</returns>
        [Fact]
        public void ValidateInputs_ReturnFalse_WhenNoInputs()
        {
            //Arrange 
            string url = string.Empty,
                   app_id = string.Empty,
                   app_key = string.Empty;
            routeStatus = new RouteStatus(mockRepo.Object);

            //Act
            var result = routeStatus.ValidateInputs(url, app_id, app_key);

            //Assert
            Assert.False(result);
        }

        /// <summary>
        /// Validate inputs and return true when inputs available
        /// </summary>
        /// <returns>true</returns>
        [Fact]
        public void ValidateInputs_ReturnTrue_WhenInputsAvailable()
        {
            //Arrange 
            string url = "https://api.tfl.gov.uk/Road/A2?app_id=1111&app_key=123",
                   app_id = "1111",
                   app_key = "222";

            routeStatus = new RouteStatus(mockRepo.Object);

            //Act
            var result = routeStatus.ValidateInputs(url, app_id, app_key);

            //Assert
            Assert.True(result);
        }

        /// <summary>
        /// Find RouteStatus return invalid response when invalid route
        /// </summary>
        /// <returns>true</returns>
        [Fact]
        public async Task FindRouteStatus_ReturnInvalidResponse_WhenInCorrectRoute()
        {
            //Arrange 
            string route = "A233",
                   api_url = "https://api.tfl.gov.uk/Road/A2?app_id=1111&app_key=123",
                   app_id = "1111",
                   app_key = "2222";
            mockRepo.Setup(repo => repo.GetAPIResponse(api_url, app_id, app_key, route)).Returns(InValidAPIResponse());
            routeStatus = new RouteStatus(mockRepo.Object);

            int i1 = Environment.ExitCode;

            //Act
            var result = await routeStatus.FindRouteStatus(api_url, app_id, app_key, route);

            int i2 = Environment.ExitCode;

            string inValidRouteMsg = $"{route} is not a valid road";

            //Assert
            Assert.Equal(inValidRouteMsg, result);
        }

        /// <summary>
        /// Find RouteStatus return valid response when valid route
        /// </summary>
        /// <returns>true</returns>
        [Fact]
        public async Task FindRouteStatus_ReturnValidResponse_WhenCorrectRoute()
        {
            //Arrange 
            string route = "A2",
                   api_url = "https://api.tfl.gov.uk/Road/A2?app_id=1111&app_key=123",
                   app_id = "1111",
                   app_key = "2222";
            mockRepo.Setup(repo => repo.GetAPIResponse(api_url, app_id, app_key, route)).Returns(ValidAPIResponse());
            routeStatus = new RouteStatus(mockRepo.Object);

            //Act
            var result = await routeStatus.FindRouteStatus(api_url, app_id, app_key, route);

            string validRouteMsg = $"The status of the A2 is as follows" + Environment.NewLine;
            validRouteMsg = validRouteMsg + "Road Status is Good" + Environment.NewLine;
            validRouteMsg = validRouteMsg + "Road Status Description is No Exceptional Delays";

            //Assert
            Assert.Equal(validRouteMsg, result);
        }

        /// <summary>
        /// Find RouteStatus return non-zero exit code when invalid route
        /// </summary>
        /// <returns>true</returns>
        [Fact]
        public async Task FindRouteStatus_ReturnNonZeroExitCode_WhenInCorrectRoute()
        {
            //Arrange 
            string route = "A233",
                    api_url = "https://api.tfl.gov.uk/Road/A2?app_id=1111&app_key=123",
                    app_id = "1111",
                    app_key = "2222";
            mockRepo.Setup(repo => repo.GetAPIResponse(api_url, app_id, app_key, route)).Returns(InValidAPIResponse());
            routeStatus = new RouteStatus(mockRepo.Object);

            //Act
            var result = await routeStatus.FindRouteStatus(api_url, app_id, app_key, route);

            //Assert
            Assert.NotEqual(0, Environment.ExitCode);
        }

        /// <summary>
        /// Find RouteStatus return zero exit code when valid route
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task FindRouteStatus_ReturnZeroExitCode_WhenCorrectRoute()
        {
            //Arrange 
            string route = "A2",
                   api_url = "https://api.tfl.gov.uk/Road/A2?app_id=1111&app_key=123",
                   app_id = "1111",
                   app_key = "2222";
            mockRepo.Setup(repo => repo.GetAPIResponse(api_url, app_id, app_key, route)).Returns(ValidAPIResponse());
            routeStatus = new RouteStatus(mockRepo.Object);

            //Act
            var result = await routeStatus.FindRouteStatus(api_url, app_id, app_key, route);

            //Assert
            Assert.Equal(0, Environment.ExitCode);
        }

        /// <summary>
        /// Valid API response
        /// </summary>
        /// <returns></returns>
        private Task<HttpResponseMessage> ValidAPIResponse()
        {
            var validRouteResponse = new List<ValidRouteResponse>();
            validRouteResponse.Add(new ValidRouteResponse
            {
                Type = "Tfl.Api.Presentation.Entities.ApiError, Tfl.Api.Presentation.Entities",
                Id = "a2",
                DisplayName = "A2",
                StatusSeverity = "Good",
                StatusSeverityDescription = "No Exceptional Delays",
                Bounds = "[[-0.0857,51.44091],[0.17118,51.49438]]",
                Envelope = "[[-0.0857,51.44091],[-0.0857,51.49438],[0.17118,51.49438],[0.17118,51.44091],[-0.0857,51.44091]]",
                Url = "/Road/a2"
            });

            var stringPayload = JsonConvert.SerializeObject(validRouteResponse);

            // Wrap our JSON inside a StringContent which then can be used by the HttpClient class
            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");


            return Task.FromResult(new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = httpContent });
        }

        /// <summary>
        /// InValid API response
        /// </summary>
        /// <returns></returns>
        private Task<HttpResponseMessage> InValidAPIResponse()
        {
            var inValidRouteResponse = new InValidRouteResponse
            {
                Type = "Tfl.Api.Presentation.Entities.ApiError, Tfl.Api.Presentation.Entities",
                TimeStampUtc = DateTime.Now,
                ExceptionType = "",
                HttpStatusCode = 404,
                HttpStatus = "NotFound",
                RelativeUri = "",
                Message = "The following road id is not recognised: A233"
            };

            var stringPayload = JsonConvert.SerializeObject(inValidRouteResponse);

            // Wrap our JSON inside a StringContent which then can be used by the HttpClient class
            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

            return Task.FromResult(new HttpResponseMessage { StatusCode = HttpStatusCode.NotFound, Content = httpContent });
        }
    }
}
