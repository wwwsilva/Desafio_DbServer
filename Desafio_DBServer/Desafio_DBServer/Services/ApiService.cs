using Desafio_DBServer.Helpers;
using Desafio_DBServer.Interfaces;
using Desafio_DBServer.Services;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(ApiService))]
namespace Desafio_DBServer.Services
{
    public class ApiService : IApiService
    {

        public async Task<T> GetAsync<T>(string url) where T : class
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        string contentString = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<T>(contentString);
                    }
                    else
                    {
                        throw new Exception(await response.Content.ReadAsStringAsync());
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<byte[]> GetImageBytesAsync(string url)
        {
            var myWebClient = new WebClient();
            return await myWebClient.DownloadDataTaskAsync(url);
        }
    }
}
