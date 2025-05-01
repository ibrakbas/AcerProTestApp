namespace AP.Generic.Abstraction;

 
public interface IEasySoftDeleteEntity
{
    
    public DateTime? DeletionDate { get; set; }

     
    public bool IsDeleted { get; set; }
}
