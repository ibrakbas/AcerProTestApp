using System.Reflection;

namespace AP.Data;


/// <summary> 
///    AP.Data assembly referansı, veritabanı tablolarının ve diğer sınıfların referansını tutar
/// </summary>

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
