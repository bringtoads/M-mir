using System;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Library.Models;
using System.Linq;

namespace Library.Helpers
{
    internal class RestApiHelper
    {
        private readonly HttpClient client;
        private readonly string _baseUrl;
        private readonly string _token;

        public RestApiHelper(JsonReader reader)
        {
            client = new HttpClient();
            _baseUrl = reader.BaseUrl;
            _token = reader.ApiToken;
        }
        public async Task<string> GetResponseAsync(string apiName, string param, string method, string contentType = "application/json")
        {
            string url = _baseUrl + apiName + (param ?? "");

            try
            {
                HttpRequestMessage request = new HttpRequestMessage(new HttpMethod(method), url);

                if (!string.IsNullOrEmpty(_token))
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _token);
                }

                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));

                HttpResponseMessage response = await client.SendAsync(request);


                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    return content;
                }
                else
                {
                    // Handle unsuccessful response here
                    Console.WriteLine($"HTTP request failed with status code: {response.StatusCode}");
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions here
                Console.WriteLine($"An error occurred: {ex.Message}");
                return string.Empty;
            }
        }
        public async Task<ResponseObject> PostResponseAsync( object data, string contentType = "application/json")
        {
            try
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(data);
                var content = new StringContent(json, Encoding.UTF8, contentType);

                if (!string.IsNullOrEmpty(_token))
                {

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
                }

                HttpResponseMessage response = await client.PostAsync(_baseUrl, content);
                if (response.IsSuccessStatusCode)
                {
                    string responseString = await response.Content.ReadAsStringAsync();
                    var botAnswer = JsonConvert.DeserializeObject<QueryResponse>(responseString);
                    return new ResponseObject
                    {
                        Status = response.StatusCode,
                        Result = botAnswer.Choices.First().Message.content
                    };

                }
                else
                {
                    return new ResponseObject
                    {
                        Status = response.StatusCode,
                        Result = $"Error: {response.ReasonPhrase}"
                    };
                }

            }
            catch (Exception ex)
            {
                // Handle exceptions here
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new ResponseObject()
                {
                    Status = System.Net.HttpStatusCode.InternalServerError,
                    Result = ex.Message
                };
            }
        }
    }
    internal class ResponseObject
    {
        public HttpStatusCode Status { get; set; }
        public string Result { get; set; }
    }

}
