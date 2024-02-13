namespace AutorizacionDePagos.Api.Application.Services.ExternalClientService
{
    public class HttpClientService : IHttpClientService
    {
        private readonly HttpClient httpClient;

        public HttpClientService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<TResponse> PostAsync<TRequest, TResponse>(TRequest data, string url)
            where TRequest : class
            where TResponse : class
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync(url, data);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<TResponse>();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
