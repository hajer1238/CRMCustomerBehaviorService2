namespace CRMCustomerBehavior.Models
{
   public class CustomerEvent
{
    public string Id { get; set; } = Guid.NewGuid().ToString(); // Assurez-vous d'avoir un ID
    public required string CustomerId { get; set; } // Ajoute 'required'
    public required string EventType { get; set; } // Ajoute 'required'
    public DateTime Timestamp { get; set; }
}
} 