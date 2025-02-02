﻿using MediatR;

namespace TransactionsApi.CQRS.Commands.Transfer
{
    public class TransferCommand : IRequest<TransferCommandResult>
    {
        public Guid SenderId { get; set; }
        public Guid RecieverId { get; set; }
        public decimal Amount { get; set; }
    }
}
