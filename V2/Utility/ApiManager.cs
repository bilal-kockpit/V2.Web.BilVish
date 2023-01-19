using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace V2.Utility
{
    public class ApiManager
    {
        private string _api;
        private string _authToken;
        public ApiManager(string api, string authToken = "")
        {
            _api = api;
            _authToken = authToken;
        }

        /// <summary>
        /// Api GET
        /// </summary>
        /// <returns></returns>
        public async Task<Tuple<HttpStatusCode, string>> Get()
        {
            using (var client = new HttpClient())
            {
                if(!string.IsNullOrEmpty(_authToken))
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_authToken}");
                using (var response = client.GetAsync(_api).Result)
                {
                    var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    return new Tuple<HttpStatusCode, string>(response.StatusCode, responseContent);
                }
            }
        }

        /// <summary>
        /// Api POST
        /// </summary>
        /// <param name="jsonBody"></param>
        /// <returns></returns>
        public async Task<Tuple<HttpStatusCode, string>> Post(string jsonBody)
        {
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            using (var client = new HttpClient())
            {
                if (!string.IsNullOrEmpty(_authToken))
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_authToken}");
                using (var response = await client.PostAsync(_api, content).ConfigureAwait(false))
                {
                    var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    return new Tuple<HttpStatusCode, string>(response.StatusCode, responseContent);
                }
            }
        }

        /// <summary>
        /// Api POST
        /// </summary>
        /// <param name="jsonBody"></param>
        /// <returns></returns>
        public async Task<Tuple<HttpStatusCode, string>> Post(string jsonBody, IFormFileCollection images)
        {
            using (var content = new MultipartFormDataContent())
            {
                if(images != null && images.Count > 0)
                {
                    foreach (var file in images)
                    {
                        content.Add(CreateFileContent(file.OpenReadStream(), file.FileName, file.ContentType));
                    }
                }

                if (!string.IsNullOrEmpty(jsonBody))
                {
                    StringContent CatalogueLines = new StringContent(jsonBody);
                    content.Add(CatalogueLines, "\"CatalogueLines\"");
                }
                
                using (var client = new HttpClient())
                {
                    if (!string.IsNullOrEmpty(_authToken))
                        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_authToken}");
                    using (var response = await client.PostAsync(_api, content).ConfigureAwait(false))
                    {
                        var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        return new Tuple<HttpStatusCode, string>(response.StatusCode, responseContent);
                    }
                }
            }
        }

        

        /// <summary>
        /// Api PUT
        /// </summary>
        /// <param name="jsonBody"></param>
        /// <returns></returns>
        public async Task<Tuple<HttpStatusCode, string>> Put(string jsonBody)
        {
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            using (var client = new HttpClient())
            {
                if (!string.IsNullOrEmpty(_authToken))
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_authToken}");
                using (var response = await client.PutAsync(_api, content).ConfigureAwait(false))
                {
                    var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    return new Tuple<HttpStatusCode, string>(response.StatusCode, responseContent);
                }
            }
        }

        /// <summary>
        /// Api DELETE
        /// </summary>
        /// <param name="jsonBody"></param>
        /// <returns></returns>
        public async Task<Tuple<HttpStatusCode, string>> Delete(string jsonBody)
        {
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            using (var client = new HttpClient())
            {
                if (!string.IsNullOrEmpty(_authToken))
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_authToken}");
                using (var response = await client.DeleteAsync(_api).ConfigureAwait(false))
                {
                    var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    return new Tuple<HttpStatusCode, string>(response.StatusCode, responseContent);
                }
            }
        }

        private StreamContent CreateFileContent(Stream stream, string fileName, string contentType)
        {
            var fileContent = new StreamContent(stream);
            fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                Name = "\"Files\"",
                FileName = "\"" + fileName + "\""
            };
            fileContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
            return fileContent;
        }
    }
}
