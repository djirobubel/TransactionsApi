﻿namespace TransactionsApi.Models
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public string? Comment { get; set; }
        public DateTime TransactionDate { get; set; }

        public Guid ClientId { get; set; }
        public Client? Client { get; set; }
    }
}
