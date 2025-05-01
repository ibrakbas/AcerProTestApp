using AP.Generic.Abstraction;

namespace AP.Data.entites.helperEntities;


/// <summary> 
///  Hata Kayıtlarını içieren veritabanı tablosu
/// </summary>

public sealed class ErrorLogs : EasyBaseEntity<int>
{
    public string Message { get; set; }
    public string Trace { get; set; }
    public string RequestPath { get; set; }
    public string RequestMethod { get; set; }
}
