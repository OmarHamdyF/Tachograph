namespace Core.Application.Interfaces;

public interface IUnitOfWork : IDisposable
{
    int Complete();
    Task<int> CompleteAsync();
}