namespace AP.Generic.Services;


/// <summary> 
/// Unit of work tasarımının arayüzü
/// </summary>
public interface IUnitOfWork
{
    IRepository Repository { get; }
}