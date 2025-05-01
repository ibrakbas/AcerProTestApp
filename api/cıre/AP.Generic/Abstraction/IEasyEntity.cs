namespace AP.Generic.Abstraction;

 
internal interface IEasyEntity<TPrimaryKey>
{
     
    TPrimaryKey Id { get; set; }
}
