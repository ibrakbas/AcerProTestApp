using System.Reflection;

namespace AP.Presentation;

/// <summary> 
///    AP.Presentation assembly referansı,   Api Controllarının ve diğer sınıfların referansını tutar
/// </summary>

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
