using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using QuickSnapApp.Configuration;
using System.Net.Http.Headers;

namespace QuickSnapApp.Pictures;
public sealed class PicturesProvider(HttpClient _httpClient, IOptions<ApiOptions> _apiOptions) : IPicturesProvider
{
    public async Task SubmitAsync(string token, PicturesSubmitRequestViewModel viewModel)
    {
        try
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"{_apiOptions.Value.ApiUrl}/api/pictures"),
                Content = new StringContent(JsonConvert.SerializeObject(viewModel), System.Text.Encoding.UTF8, "application/json"),

            };

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            var a = ex;
            throw;
        }
    }
}
