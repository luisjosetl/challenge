namespace AutorizacionDePagos.Api.Application.Services.ExternalClientService
{
    public interface IHttpClientService
    {
        Task<TResponse> PostAsync<TRequest, TResponse>(TRequest data, string url) where TRequest : class where TResponse : class;
    }
}
