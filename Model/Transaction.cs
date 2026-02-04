public class Expense
{
    public Guid Id { get; set; }
    public string? Description { get; set; }
    public TransactionType TransactionType { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
}
