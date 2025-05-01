
using AP.Generic.Services;

namespace AP.Generic;


/// <summary>
/// Unit of work tasarımını uygulayan sınıf
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    public UnitOfWork(IRepository repository)
    {
        Repository = repository;
    }
    public IRepository Repository { get; }
}