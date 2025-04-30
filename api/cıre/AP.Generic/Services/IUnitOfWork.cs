namespace AP.Generic.Services;


/// <summary>
/// Abstraction of Unit Of Work pattern
/// </summary>
public interface IUnitOfWork
{
    IRepository Repository { get; }
}