namespace TransactionsApi.Dto
{
    public class TransactionDto
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public string? Comment { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
