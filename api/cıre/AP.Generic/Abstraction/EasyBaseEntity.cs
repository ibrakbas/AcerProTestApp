namespace AP.Generic.Abstraction;

/// <summary> 
///  Veritabanı tablosu için temel özellikleri uygulayan bu soyut sınıf
/// </summary>
/// <typeparam name="TPrimaryKey">
/// Veritabanı tablosunun birincil anahtar türü
/// </typeparam>
public abstract class EasyBaseEntity<TPrimaryKey> : IEasyEntity<TPrimaryKey>, IEasyCreateDateEntity, IEasyUpdateDateEntity, IEasySoftDeleteEntity
{
    /// <summary>
    /// Oluşturulma Tarihi <see>
    ///     <cref>{DateTime}</cref>
    /// </see>
    /// </summary> 
    public virtual DateTime CreationDate { get; set; }

    /// <summary>
    /// Birincil Anahtar <see>
    ///     <cref>{TPrimaryKey}</cref>
    /// </see>
    /// </summary>
    public virtual TPrimaryKey Id { get; set; }

    /// <summary>
    /// Değiştirilme Tarihi 
    ///     <cref>{DateTime}</cref>
    /// </see>
    /// </summary> 
    public virtual DateTime? ModificationDate { get; set; }

    /// <summary>
    /// Silinme Tarihi <see>
    ///     <cref>{DateTime}</cref>
    /// </see>
    /// </summary>
    public virtual DateTime? DeletionDate { get; set; }

    /// <summary>
    /// Silinip Silinmediği Durumu<see>
    ///     <cref>{Boolean}</cref>
    /// </see>
    /// </summary> 
    public virtual bool IsDeleted { get; set; }


}
