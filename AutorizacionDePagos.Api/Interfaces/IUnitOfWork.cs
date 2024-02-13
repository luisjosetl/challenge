namespace AutorizacionDePagos.Api.Interfaces
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync();
    }
}
